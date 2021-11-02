using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Text.RegularExpressions;
using System.IO.Ports;
using System.Windows.Forms;
using UtilLib;
using System.Xml.Serialization;
using System.Threading;

namespace CmniLib
{
    // シリアルやプロセス間通信をバックグラウンドで行うクラスです。

    public delegate void dgPRS_LIST_UPD(string txCmd1);
    public delegate void dgRCV_DATA(sCMNI_PORT sCmniPort1, byte[] adtCmd1, int ctCmd1);

    public partial class CmniCtrlUc : Component
    {
        // データ受信イベント
        public event dgRCV_DATA m_dgRcvData;
        // プロセスリスト更新イベント
        public event dgPRS_LIST_UPD m_dgPrsListUpd;

        private string NPSS_PRFX = "CmniLib";
        // 通信ポートリスト
        public List<sCMNI_PORT> m_asCmniPort;
        // 全プロセス一覧
        public sPRS m_sPrs;
        public List<sOTH_PRS> m_asRcgPrs;           // 認識プロセスリスト

        // シリアルポートリスト
        [XmlIgnore]
        public List<sSRI_PORT> m_asPort;


        public CmniCtrlUc()
        {
            InitializeComponent();

            Init();
        }

        public CmniCtrlUc(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            Init();
        }

        private void Init()
        {
            m_asCmniPort = new List<sCMNI_PORT>();
            m_sPrs = new sPRS();
            m_asRcgPrs = new List<sOTH_PRS>();
            m_asPort = new List<sSRI_PORT>();
        }

        // シリアルデータ受信
        private void RcvSerData(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sSp1 = (SerialPort)sender;


                sSRI_PORT sSerPort1 = GetSerPort(sSp1);
                sCMNI_PORT sCmniPort1 = m_asCmniPort.Where(sCmnPort1 => sCmnPort1.m_sPortSets.m_nbPortType == ePORT_TYPE.SER && sCmnPort1.m_sPortSets.m_txSerPort == sSerPort1.m_txName).FirstOrDefault();

                //! 受信データを読み込む.
                byte[] adtCmd1 = new byte[CmniCom.SRI_RCV_DATA_SIZE];
                int ctCmd1;
                ctCmd1 = sSp1.Read(adtCmd1, 0, CmniCom.SRI_RCV_DATA_SIZE);

                if(m_dgRcvData != null)
                {
                    m_dgRcvData(sCmniPort1, adtCmd1, ctCmd1);
                }
            }
            catch
            {
                MessageBox.Show("シリアル通信エラー");
            }
        }

        // 通信ポートを追加
        public void AddCmniPort(sCMNI_PORT sCmniPort1)
        {
            m_asCmniPort.Add(sCmniPort1);
        }

        // 通信ポートを取得
        private sCMNI_PORT GetCmniPort(string txCmniPort1)
        {
            return (m_asCmniPort.Where(sCmniPort1 => sCmniPort1.m_txName == txCmniPort1).FirstOrDefault());
        }

        // 通信制御開始
        public void StaCmniCtrl()
        {
            // 自プロセス情報を更新
            m_sPrs.m_txName = Process.GetCurrentProcess().ProcessName;
            m_sPrs.m_txId = Process.GetCurrentProcess().Id.ToString();

            // サーバを起動
            Task.Run(() => ServerTask(RcvDataFromClient));


            // プロセスに認識要求を送信
            SendReqToPrsList($"Rcg\t{m_sPrs.m_txName}");
        }

        // 通信管理終了
        public void EndCmniCtrl()
        {
            // プロセスに認識要求を送信
            SendReqToRcgPrsList($"RcgEnd\t{m_sPrs.m_txName}");
        }

        // パイプ名取得
        private string GetPipeName(string txId1)
        {
            return ($"{NPSS_PRFX}_{txId1}");
        }

        // パイプ通信開始
        public bool StaCmniPort(sCMNI_PORT sCmniPort1)
        {
            bool flCone1 = false;
            switch (sCmniPort1.m_sPortSets.m_nbPortType)
            {
                case ePORT_TYPE.NON:
                    break;
                case ePORT_TYPE.SER:
                    // ポートを更新
                    flCone1 = SriOn(sCmniPort1.m_sPortSets);
                    break;
                case ePORT_TYPE.PIPE:
                    // パイプを更新
                    flCone1 = PipeOn(sCmniPort1.m_sPortSets);
                    break;
                case ePORT_TYPE.USB:
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            return (flCone1);
        }

        // パイプ通信終了
        public bool EndCmniPort(sCMNI_PORT sCmniPort1)
        {
            bool flCone1 = false;
            switch (sCmniPort1.m_sPortSets.m_nbPortType)
            {
                case ePORT_TYPE.NON:
                    break;
                case ePORT_TYPE.SER:
                    // ポートを更新
                    flCone1 = SriOn(sCmniPort1.m_sPortSets);
                    break;
                case ePORT_TYPE.PIPE:
                    // パイプを更新
                    flCone1 = PipeOff(sCmniPort1.m_sPortSets);
                    break;
                case ePORT_TYPE.USB:
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            return (flCone1);
        }

        // シリアルポート取得
        private sSRI_PORT GetSerPort(string txName1)
        {
            return (m_asPort.Where(sPort1 => sPort1.m_txName == txName1).FirstOrDefault());
        }

        // シリアルポート取得
        private sSRI_PORT GetSerPort(SerialPort sSp1)
        {
            return (m_asPort.Where(sPort1 => sPort1.m_sPort == sSp1).FirstOrDefault());
        }

        // 通信ポートへコマンドを送信
        public void SendCmdToCmniPort(string txCmniPort1, byte[] adtCmd1)
        {
            sCMNI_PORT sCmniPort1 = GetCmniPort(txCmniPort1);

            switch (sCmniPort1.m_sPortSets.m_nbPortType)
            {
                case ePORT_TYPE.NON:
                    break;
                case ePORT_TYPE.SER:
                    break;
                case ePORT_TYPE.PIPE:
                    // パイプを更新
                    SendCmdToPipe(sCmniPort1.m_sPortSets.m_sPipe, adtCmd1);
                    break;
                case ePORT_TYPE.USB:
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        // パイプにコマンドを送信
        private string SendCmdToPipe(sCMNI_PIPE sPipe1, byte[] adtCmd1)
        {
            string txRes1 = "";
            using (var sNpss1 = new NamedPipeClientStream($"{sPipe1.m_sPrs.m_txId} {sPipe1.m_txName}"))
            {
                // サーバに接続
                try
                {
                    sNpss1.Connect(0);

                    // サーバにリクエストを送信する
                    using (var sBw1 = new BinaryWriter(sNpss1, Encoding.UTF8, true))
                    {
                        sBw1.Write(adtCmd1);
                    }

                }
                catch
                {

                }
            }

            return txRes1;
        }


        // シリアル通信開始
        public bool SriOn(sPORT_SETS sPortSets1)
        {
            bool flCone1 = false;
            try
            {
                sSRI_PORT sPort1 = GetSerPort(sPortSets1.m_txSerPort);
                if (sPort1 == null)
                {
                    // ポートを新規に作成
                    m_asPort.Add(new sSRI_PORT(sPortSets1.m_txSerPort));
                    sPort1 = m_asPort.Last();
                    sPort1.m_sPort.DataReceived += RcvSerData;
                }


                if (sPort1.m_ctRef == 0)
                {
                    // ポートへの参照が0なら
                    sPort1.m_sPort.PortName = sPortSets1.m_txSerPort;
                    sPort1.m_sPort.BaudRate = sPortSets1.m_dtSerBrt;
                    sPort1.m_sPort.DataBits = sPortSets1.m_dtDataBit;
                    sPort1.m_sPort.Parity = sPortSets1.m_nbPrty;
                    sPort1.m_sPort.StopBits = sPortSets1.m_nbStopBit;
                    sPort1.m_sPort.Handshake = sPortSets1.m_nbFlwCtl;
                    //sPort1.m_sPort.Encoding = Encoding.GetEncoding("SHIFT-JIS");

                    sPort1.m_sPort.Open();
                }

                sPort1.m_ctRef++;
                flCone1 = true;
            }
            catch (Exception e)
            {
                // 通信設定が不正です。
                //MessageBox.Show(e.Message);
            }

            return (flCone1);
        }

        // シリアル通信を終了
        public void SriOff(sPORT_SETS sPortSets1)
        {
            sSRI_PORT sPort1 = GetSerPort(sPortSets1.m_txSerPort);
            sPort1.m_ctRef--;
            if (sPort1.m_ctRef <= 0)
            {
                sPort1.m_sPort.Close();
            }
        }

        // パイプのプロセスが有効か？
        public void ChgEfcPipePrs(sCMNI_PIPE sPipe1)
        {
            sPIPE sPipe3 = GetRcgPipeList().Where(sPipe2 => sPipe2.m_sPrs.m_txId == sPipe1.m_sPrs.m_txId && sPipe2.m_txName == sPipe1.m_sPrs.m_txName).FirstOrDefault();
            if (sPipe3 == null)
            {
                sPipe3 = GetRcgPipeList().Where(sPipe2 => sPipe2.m_sPrs.m_txName == sPipe1.m_sPrs.m_txName && sPipe2.m_txName == sPipe1.m_txName).FirstOrDefault();
                if (sPipe3 != null)
                {
                    // プロセスIDを合わせる
                    sPipe1.m_sPrs.m_txId = sPipe3.m_sPrs.m_txId;
                }
            }
        }

        // パイプ通信開始
        public bool PipeOn(sPORT_SETS sPortSets1)
        {
            bool flCone1 = false;
            try
            {
                ChgEfcPipePrs(sPortSets1.m_sPipe);

                string txRes1 = SendReqToPrs($"Cone\t{sPortSets1.m_sPipe.m_txName}", sPortSets1.m_sPipe.m_sPrs.m_txId);
                if (txRes1 == "ConeRes")
                {
                    flCone1 = true;
                }
            }
            catch
            {

            }
            return (flCone1);
        }

        // パイプ通信終了
        public bool PipeOff(sPORT_SETS sPortSets1)
        {
            bool flCone1 = true;
            try
            {
                SendReqToPrs($"ConeEnd\t{sPortSets1.m_sPipe.m_txName}", sPortSets1.m_sPipe.m_sPrs.m_txId);
                flCone1 = false;
            }
            catch
            {

            }
            return (flCone1);
        }

        // サーバのタスク
        private async Task ServerTask(Func<string, string> dgSvrRcv)
        {
            while (true)
            {
                using (var sNpss1 = new NamedPipeServerStream(GetPipeName(m_sPrs.m_txId)))
                {
                    // クライアントからの接続を待つ
                    await sNpss1.WaitForConnectionAsync();

                    while (true)
                    {
                        // クライアントからリクエストを受信する
                        string txReq1 = default(string);
                        using (var sBr1 = new BinaryReader(sNpss1, Encoding.UTF8, true))
                        {
                            txReq1 = sBr1.ReadString();
                        }

                        // リクエストを処理してレスポンスを作る
                        var txRes1 = dgSvrRcv(txReq1);

                        if (txRes1 == "")
                        {
                            break;
                        }

                        // クライアントにレスポンスを送信する
                        using (var sBw1 = new BinaryWriter(sNpss1, Encoding.UTF8, true))
                        {
                            sBw1.Write(txRes1);
                        }
                    }
                }
            }
        }

        // クライアントからデータを受信
        private string RcvDataFromClient(string txData1)
        {
            string txRes1 = "";

            string[] atxMsg1 = txData1.Split('\n');
            foreach (string txMsg1 in atxMsg1)
            {
                Match m1;
                string txReq1 = "";
                string txArg1 = "";
                string txArg2 = "";
                string txId1 = "";

                if ((m1 = Regex.Match(txMsg1, @"^(?<Req>\w+)\t(?<Name>\w+)\t(?<Id>\d+)$")).Success)
                {
                    // 要求コマンド
                    txReq1 = m1.Groups["Req"].Value;
                    txArg1 = m1.Groups["Name"].Value;
                    txId1 = m1.Groups["Id"].Value;
                }
                else if ((m1 = Regex.Match(txMsg1, @"^(?<Req>\w+)\t(?<Arg1>\w+)\t(?<Arg2>\w+)\t(?<Id>\d+)$")).Success)
                {
                    // 要求コマンド
                    txReq1 = m1.Groups["Req"].Value;
                    txArg1 = m1.Groups["Arg1"].Value;
                    txArg2 = m1.Groups["Arg2"].Value;
                    txId1 = m1.Groups["Id"].Value;
                }



                sOTH_PRS sPrs1 = GetRcgPrsFromId(txId1);
                if (sPrs1 == null)
                {
                    sPrs1 = new sOTH_PRS(txArg1, txId1);
                    m_asRcgPrs.Add(sPrs1);
                }

                switch (txReq1)
                {
                    case "Rcg":
                        // 認識要求
                        if (sPrs1.m_flRcg)
                        {
                            // 認識済みなので、返信の必要なし
                        }
                        else
                        {
                            sPrs1.m_flRcg = true;
                            // 認識要求をしてきたプロセスに認識要求を返信
                            txRes1 += $"RcgRes\t{m_sPrs.m_txName}\n";

                            // 認識要求をしてきたプロセスにパイプ認識要求を送信
                            foreach (var sCmniPort1 in m_asCmniPort)
                            {
                                // パイプ認識要求を送信
                                txRes1 += $"PipeRcg\t{sCmniPort1.m_txName}\n";
                            }
                        }
                        break;
                    case "PipeRcg":
                        // パイプ認識要求
                        sPrs1.m_asPipe.Add(new sPIPE(sPrs1, txArg1));
                        break;
                    case "RcgEnd":
                        // 認識終了要求
                        Debug.Assert(sPrs1.m_flRcg);
                        sPrs1.m_flRcg = false;
                        break;
                    case "Cone":
                        // 接続要求
                        {
                            sPIPE sPipe1 = sPrs1.m_asPipe.Where(sPipe5 => sPipe5.m_txName == txArg1).FirstOrDefault();
                            if (sPipe1 == null)
                            {
                                // パイプなし
                            }
                            else if (sPipe1.m_flCone)
                            {
                                // すでに他のパイプに接続済み
                            }
                            else
                            {
                                sPipe1.m_flCone = true;
                                sCMNI_PORT sCmniPort1 = m_asCmniPort.Where(sCmniPort5 => sCmniPort5.m_txName == sPipe1.m_txName).FirstOrDefault();

                                sCmniPort1.m_sRcvTaskCts = new CancellationTokenSource();
                                Task.Run(() => PipeRcvTask(sCmniPort1, $"{m_sPrs.m_txId} {sPipe1.m_txName}"));
                                txRes1 = "ConeRes";
                            }
                        }

                        break;
                    case "ConeEnd":
                        // 接続終了要求
                        {
                            sPIPE sPipe1 = sPrs1.m_asPipe.Where(sPipe5 => sPipe5.m_txName == txArg1).FirstOrDefault();
                            if (!sPipe1.m_flCone)
                            {
                                // すでに切断済み
                            }
                            else
                            {
                                sPipe1.m_flCone = false;
                                sCMNI_PORT sCmniPort1 = m_asCmniPort.Where(sCmniPort5 => sCmniPort5.m_txName == sPipe1.m_txName).FirstOrDefault();
                                sCmniPort1.m_sRcvTaskCts.Cancel();
                            }
                        }

                        break;
                    case "":
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }

            }

            // プロセス更新コールバック呼び出し
            if (m_dgPrsListUpd != null)
            {
                m_dgPrsListUpd(txData1);
            }

            return (txRes1);
        }

        // パイプ受信タスク
        private async Task PipeRcvTask(sCMNI_PORT sCmniPort1, string txPipeName1)
        {
            while (true)
            {
                using (var sNpss1 = new NamedPipeServerStream(txPipeName1, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, options: PipeOptions.Asynchronous))
                {
                    // クライアントからの接続を待つ
                    await sNpss1.WaitForConnectionAsync(sCmniPort1.m_sRcvTaskCts.Token);

                    while (true)
                    {
                        // クライアントからリクエストを受信する
                        byte[] adtCmd1 = null;
                        using (var sBr1 = new BinaryReader(sNpss1, Encoding.UTF8, true))
                        {
                            adtCmd1 = sBr1.ReadBytes(CmniCom.SRI_RCV_DATA_SIZE);
                        }

                        // リクエストを処理してレスポンスを作る
                        if(m_dgRcvData != null)
                        {
                            m_dgRcvData(sCmniPort1, adtCmd1, adtCmd1.Length);
                        }

                        if (adtCmd1.Length < CmniCom.SRI_RCV_DATA_SIZE)
                        {
                            // 全データ読み込み
                            break;
                        }
                    }
                }

                if (sCmniPort1.m_sRcvTaskCts.IsCancellationRequested)
                {
                    // タスク終了
                    break;
                }
            }
        }

        // クライアントからサーバに要求を送信
        private string SendReqToServer(string txId1, string txReq1)
        {
            string txRes1 = "";
            using (var sNpss1 = new NamedPipeClientStream(GetPipeName(txId1)))
            {
                // サーバに接続
                try
                {
                    sNpss1.Connect(0);

                    // サーバにリクエストを送信する
                    using (var sBw1 = new BinaryWriter(sNpss1, Encoding.UTF8, true))
                    {
                        sBw1.Write(txReq1);
                    }

                    while (true)
                    {
                        // サーバからレスポンスを受信する
                        using (var sBr1 = new BinaryReader(sNpss1, Encoding.UTF8, true))
                        {
                            txRes1 = sBr1.ReadString();
                        }

                        txReq1 = ClientProcess(txId1, txRes1);

                        using (var sBw1 = new BinaryWriter(sNpss1, Encoding.UTF8, true))
                        {
                            sBw1.Write(txReq1);
                        }
                    }
                }
                catch
                {

                }
            }

            return txRes1;
        }

        private string ClientProcess(string txId1, string txData1)
        {
            string txRes1 = "";

            string[] atxMsg1 = txData1.Split('\n');
            foreach (string txMsg1 in atxMsg1)
            {
                Match m1;
                if ((m1 = Regex.Match(txMsg1, @"^(?<Req>\w+)\t(?<Name>\w+)$")).Success)
                {
                    // 要求コマンド
                    string txReq1 = m1.Groups["Req"].Value;
                    string txName1 = m1.Groups["Name"].Value;

                    sOTH_PRS sPrs1 = GetRcgPrsFromId(txId1);
                    if (sPrs1 == null)
                    {
                        sPrs1 = new sOTH_PRS(txName1, txId1);
                        m_asRcgPrs.Add(sPrs1);
                    }

                    switch (txReq1)
                    {
                        case "RcgRes":
                            // 認識要求の返信
                            Debug.Assert(!sPrs1.m_flRcg);
                            sPrs1.m_flRcg = true;

                            foreach (var sCmniPort1 in m_asCmniPort)
                            {
                                // パイプ認識要求を送信
                                txRes1 += $"PipeRcg\t{sCmniPort1.m_txName}\t{m_sPrs.m_txId}\n";
                            }

                            break;
                        case "PipeRcg":
                            // パイプ認識要求
                            sPrs1.m_asPipe.Add(new sPIPE(sPrs1, txName1));
                            break;
                        default:
                            Debug.Assert(false);
                            break;
                    }
                }

            }

            if (m_dgPrsListUpd != null)
            {
                m_dgPrsListUpd(txData1);
            }

            return (txRes1);
        }

        // IDからプロセスを取得
        public sOTH_PRS GetRcgPrsFromId(string txId1)
        {
            sOTH_PRS sPrs2 = null;
            foreach (sOTH_PRS sPrs1 in m_asRcgPrs)
            {
                if (sPrs1.m_txId == txId1)
                {
                    sPrs2 = sPrs1;
                    break;
                }
            }

            return (sPrs2);
        }

        // プロセスリストに要求を送信
        public void SendReqToPrsList(string txReq1)
        {
            List<sOTH_PRS> asPrs1 = GetPrsList();
            foreach (sOTH_PRS sPrs1 in asPrs1)
            {
                SendReqToPrs(txReq1, sPrs1.m_txId);
            }
        }


        // プロセスに要求を送信
        public string SendReqToPrs(string txReq1, string txId1)
        {
            // クライアント
            return (SendReqToServer(txId1, $"{txReq1}\t{m_sPrs.m_txId}"));
        }

        // プロセスリストを取得
        public List<sOTH_PRS> GetPrsList()
        {
            List<sOTH_PRS> asPrs1 = new List<sOTH_PRS>();

            Process[] asPr1 = Process.GetProcesses();

            asPrs1 = asPr1.Where(sPr1 => sPr1.Id.ToString() != m_sPrs.m_txId).Select(sPr1 => new sOTH_PRS(sPr1.ProcessName, sPr1.Id.ToString())).ToList();
            return (asPrs1);
        }

        // 認識プロセスリストを取得
        public List<sOTH_PRS> GetRcgPrsList()
        {
            List<sOTH_PRS> asPrs1 = new List<sOTH_PRS>();
            foreach (sOTH_PRS sPrs1 in m_asRcgPrs)
            {
                if (sPrs1.m_flRcg)
                {
                    asPrs1.Add(new sOTH_PRS(sPrs1));
                }
            }

            return (asPrs1);
        }

        // 認識プロセスリストに要求を送信
        public void SendReqToRcgPrsList(string txReq1)
        {
            List<sOTH_PRS> asPrs1 = GetRcgPrsList();
            foreach (sOTH_PRS sPrs1 in asPrs1)
            {
                SendReqToPrs(txReq1, sPrs1.m_txId);
            }
        }

        // 認識パイプリストを取得
        public List<sPIPE> GetRcgPipeList()
        {
            List<sPIPE> asPipe1 = new List<sPIPE>();
            foreach (sOTH_PRS sPrs1 in m_asRcgPrs)
            {
                if (sPrs1.m_flRcg)
                {
                    foreach (sPIPE sPipe1 in sPrs1.m_asPipe)
                    {
                        asPipe1.Add(new sPIPE(sPipe1));
                    }
                }
            }

            return (asPipe1);
        }

        public sCMNI_PIPE[] GetRcgCmniPipeList()
        {
            List<sCMNI_PIPE> asCmniPipe1 = new List<sCMNI_PIPE>();
            Dictionary<string, string> dcPrs1 = new Dictionary<string, string>();

            // 重複プロセスをチェック
            foreach (sOTH_PRS sPrs1 in m_asRcgPrs)
            {
                if (sPrs1.m_flRcg)
                {
                    if (!dcPrs1.ContainsKey(sPrs1.ToString()))
                    {
                        dcPrs1.Add(sPrs1.ToString(), "Fst");
                    }
                    else
                    {
                        dcPrs1[sPrs1.ToString()] = "Dup";
                    }
                }
            }

            foreach (sOTH_PRS sPrs1 in m_asRcgPrs)
            {
                if (sPrs1.m_flRcg)
                {
                    foreach (sPIPE sPipe1 in sPrs1.m_asPipe)
                    {
                        asCmniPipe1.Add(new sCMNI_PIPE(sPrs1.ToPrs(), sPipe1.m_txName));
                        sCMNI_PIPE sCmniPipe1 = asCmniPipe1.Last();
                        if (dcPrs1[sPrs1.ToString()] == "Dup")
                        {
                            // 重複しているのでIDも表示
                            sCmniPipe1.m_flIdDsp = true;
                        }
                    }
                }
            }

            return (asCmniPipe1.ToArray());
        }

    }

    // 通信ポート
    [Serializable]
    public class sCMNI_PORT
    {
        // データ受信コールバック
        //[XmlIgnore]
        //public dgRCV_DATA m_dgRcvData;
        // 受信タスクキャンセルトークン
        [XmlIgnore]
        public CancellationTokenSource m_sRcvTaskCts;

        // 名称
        public string m_txName;
        // 接続フラグ
        public bool m_flCone;
        // ポート設定
        public sPORT_SETS m_sPortSets;


        public sCMNI_PORT()
        {
            Com.InitData(this);
            m_sPortSets = new sPORT_SETS();
        }

        public sCMNI_PORT(string txName1)
        {
            Com.InitData(this);
            m_txName = txName1;
            m_sPortSets = new sPORT_SETS();            
        }

        public override string ToString()
        {
            string txPipe1 = "";
            {
                txPipe1 = $"{m_txName}";
            }
            return (txPipe1);
        }
    }

    // プロセス
    [Serializable]
    public class sPRS
    {
        public string m_txName;
        public string m_txId;

        public sPRS()
        {
            Com.InitData(this);
        }

        public sPRS(string txName1, string txId1)
        {
            m_txName = txName1;
            m_txId = txId1;
        }

        public sPRS(sPRS sPrs1)
        {
            Com.CopyToData(sPrs1, this);
        }

        public override string ToString()
        {
            return (m_txName);
        }
    }

    // 他プロセス
    [Serializable]
    public class sOTH_PRS : sPRS
    {
        [XmlIgnore]
        public List<sPIPE> m_asPipe;  // パイプリスト
        public bool m_flRcg;       // 認識フラグ

        public sOTH_PRS() : base()
        {
            m_asPipe = new List<sPIPE>();
        }

        public sOTH_PRS(string txName1, string txId1) : base(txName1, txId1)
        {
            m_asPipe = new List<sPIPE>();
            m_flRcg = false;
        }

        public sOTH_PRS(sOTH_PRS sPrs1)
        {
            Com.CopyToData(sPrs1, this);
        }

        public override string ToString()
        {
            return (m_txName);
        }

        public sPRS ToPrs()
        {
            return (new sPRS(m_txName, m_txId));
        }
    }

    public class sPIPE
    {
        // 親プロセス
        public sPRS m_sPrs;
        // 名称
        public string m_txName;
        public bool m_flCone;       // 接続フラグ

        public sPIPE()
        {
            Com.InitData(this);
        }

        public sPIPE(sPRS sPrs1, string txName1)
        {
            m_sPrs = sPrs1;
            m_txName = txName1;
            m_flCone = false;
        }

        public sPIPE(sPIPE sPrs1)
        {
            Com.CopyToData(sPrs1, this);
        }

        public override string ToString()
        {
            string txPipe1 = "";
            if (m_sPrs != null)
            {
                txPipe1 = $"{m_sPrs.m_txName} {m_txName}";
            }
            return (txPipe1);
        }
    }

    [Serializable]
    public class sCMNI_PIPE
    {
        // 名称
        public sPRS m_sPrs;
        public string m_txName;
        public bool m_flIdDsp;

        public sCMNI_PIPE()
        {
            Com.InitData(this);
            m_sPrs = new sPRS();
        }

        public sCMNI_PIPE(sPRS sPrs1, string txName1)
        {
            m_sPrs = sPrs1;
            m_txName = txName1;
            m_flIdDsp = false;
        }

        public override string ToString()
        {
            string txPipe1 = "";
            if (m_sPrs != null)
            {
                if (m_flIdDsp)
                {
                    txPipe1 = $"{m_sPrs.m_txName}({m_sPrs.m_txId}) {m_txName}";
                }
                else
                {
                    txPipe1 = $"{m_sPrs.m_txName} {m_txName}";
                }
            }
            return (txPipe1);
        }
    }

    // シリアルポート
    public class sSRI_PORT
    {
        public string m_txName;         // ポート名
        public SerialPort m_sPort;      // ポート
        public int m_ctRef;             // 参照カウント
        public sSRI_PORT(string txName1)
        {
            m_txName = txName1;
            m_ctRef = 0;
            m_sPort = new SerialPort();
        }
    }

    // MSサンプルそのまま(sNpss1に文字列を読み書きしてくれるクラス)
    public class StreamString
    {
        private Stream ioStream;
        private UnicodeEncoding sNpss1Encoding;

        public StreamString(Stream ioStream)
        {
            this.ioStream = ioStream;
            sNpss1Encoding = new UnicodeEncoding();
        }

        public string ReadString()
        {
            int len = 0;

            len = ioStream.ReadByte() * 256;
            if (len < 0)
            {
                return ("");
            }
            len += ioStream.ReadByte();
            byte[] inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);

            return sNpss1Encoding.GetString(inBuffer);
        }

        public int WriteString(string outString)
        {
            byte[] outBuffer = sNpss1Encoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > UInt16.MaxValue)
            {
                len = (int)UInt16.MaxValue;
            }
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();

            return outBuffer.Length + 2;
        }
    }

}
