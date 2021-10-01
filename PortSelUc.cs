using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using UtilLib;

namespace PipeLib
{
    public partial class PortSelUc : UserControl
    {
        sPORT_SETS m_sPortSets;
        public PortSelUc()
        {
            InitializeComponent();
            m_sPortSets = new sPORT_SETS();
            
            m_sCmbPortType.Items.AddRange(Com.GetDescriptionListFromEnum(typeof(ePORT_TYPE)));
            m_sCmbDataBits.Items.AddRange(PipeCom.m_adtDataBit.Select(dtDataBit1 => dtDataBit1.ToString()).ToArray());
            m_sCmbStopBits.Items.AddRange(PipeCom.m_asStopBit);
            m_sCmbParity.Items.AddRange(PipeCom.m_asParity);
            m_sCmbHandShake.Items.AddRange(PipeCom.m_asFlwCtl);
        }

        public void Init(sPORT_SETS sPortSets1)
        {
            m_sPortSets = sPortSets1;
            UpdPortSetsToFrm();
        }

        // ポート設定をフォームに反映
        public void UpdPortSetsToFrm()
        {
            Com.DoSomethingWithoutEvents(new List<Control> { this }, () => {
                // イベント禁止
                m_sCmbPortType.Text = Com.GetDescription(m_sPortSets.m_nbPortType);
                switch (m_sPortSets.m_nbPortType)
                {
                    case ePORT_TYPE.SER:
                        m_sCmbPort.Enabled = true;
                        m_sCmbPort.Text = m_sPortSets.m_txSerPort;
                        m_sCmbBaudRate.Enabled = true;
                        m_sCmbBaudRate.Items.Clear();
                        m_sCmbBaudRate.Items.AddRange(PipeCom.m_adtSerBrt.Select(sSerBrt1 => sSerBrt1.ToString()).ToArray());
                        m_sCmbBaudRate.Text = m_sPortSets.m_dtSerBrt.ToString();
                        m_sCmbDataBits.Enabled = true;
                        m_sCmbStopBits.Enabled = true;
                        m_sCmbParity.Enabled = true;
                        m_sCmbHandShake.Enabled = true;
                        break;
                    case ePORT_TYPE.PIPE:
                        m_sCmbPort.Enabled = true;
                        m_sCmbPort.Text = m_sPortSets.m_sPipe.ToString();
                        m_sCmbBaudRate.Enabled = false;
                        m_sCmbDataBits.Enabled = false;
                        m_sCmbStopBits.Enabled = false;
                        m_sCmbParity.Enabled = false;
                        m_sCmbHandShake.Enabled = false;
                        break;
                    case ePORT_TYPE.USB:
                        m_sCmbPort.Enabled = false;
                        m_sCmbBaudRate.Enabled = true;
                        m_sCmbBaudRate.Items.Clear();
                        m_sCmbBaudRate.Items.AddRange(PipeCom.m_adtUsbBrt.Select(dtUsbBrt1 => dtUsbBrt1.ToString()).ToArray());
                        m_sCmbBaudRate.Text = m_sPortSets.m_dtUsbBrt.ToString();
                        m_sCmbDataBits.Enabled = false;
                        m_sCmbStopBits.Enabled = false;
                        m_sCmbParity.Enabled = false;
                        m_sCmbHandShake.Enabled = false;
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }

                m_sCmbDataBits.Text = m_sPortSets.m_dtDataBit.ToString();
                m_sCmbStopBits.Text = PipeCom.m_asStopBit.Where(sStopBit1 => (StopBits)sStopBit1.m_dtVal == m_sPortSets.m_nbStopBit).FirstOrDefault().m_txName;
                m_sCmbParity.Text = PipeCom.m_asParity.Where(sParity1 => (Parity)sParity1.m_dtVal == m_sPortSets.m_nbPrty).FirstOrDefault().m_txName;
                m_sCmbHandShake.Text = PipeCom.m_asFlwCtl.Where(sFlwCtl1 => (Handshake)sFlwCtl1.m_dtVal == m_sPortSets.m_nbFlwCtl).FirstOrDefault().m_txName;
            });
        }

        public sPORT_SETS GetPortSets()
        {
            return (m_sPortSets);
        }

        // ポートタイプ変更
        private void m_sCmbPortType_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_sPortSets.m_nbPortType = (ePORT_TYPE)((sTX_INT)m_sCmbPortType.SelectedItem).m_dtVal;
            UpdPortSetsToFrm();
        }

        // ポート変更
        private void m_sCmbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (m_sPortSets.m_nbPortType)
            {
                case ePORT_TYPE.SER:
                    m_sPortSets.m_txSerPort = m_sCmbPort.Text;
                    break;
                case ePORT_TYPE.PIPE:
                    m_sPortSets.m_sPipe = (sCMNI_PIPE)m_sCmbPort.SelectedItem;
                    break;
                case ePORT_TYPE.USB:
                    Debug.Assert(false);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

        }

        // ポートのドロップダウンリスト更新
        private void m_sCmbPort_DropDown(object sender, EventArgs e)
        {
            switch (m_sPortSets.m_nbPortType)
            {
                case ePORT_TYPE.SER:
                    // ポートを更新
                    m_sCmbPort.Items.Clear();
                    m_sCmbPort.Items.AddRange(SerialPort.GetPortNames());
                    break;
                case ePORT_TYPE.PIPE:
                    // パイプを更新
                    m_sCmbPort.Items.Clear();
                    m_sCmbPort.Items.AddRange(PipeCmni.GetRcgCmniPipeList());
                    break;
                case ePORT_TYPE.USB:
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        // ボーレート変更
        private void m_sCmbBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (m_sPortSets.m_nbPortType)
            {
                case ePORT_TYPE.SER:
                    // ポートを更新
                    m_sPortSets.m_dtSerBrt = (int)Com.GetValFromTxt(m_sCmbBaudRate.Text);
                    break;
                case ePORT_TYPE.PIPE:
                    Debug.Assert(false);
                    break;
                case ePORT_TYPE.USB:
                    m_sPortSets.m_dtUsbBrt = (int)Com.GetValFromTxt(m_sCmbBaudRate.Text);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
        }

        // データビット変更
        private void m_sCmbDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_sPortSets.m_dtDataBit = (int)Com.GetValFromTxt(m_sCmbDataBits.Text);
        }

        // ストップビット変更
        private void m_sCmbStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_sPortSets.m_nbStopBit = (StopBits)((sTX_INT)m_sCmbStopBits.SelectedItem).m_dtVal;
        }

        // パリティ変更
        private void m_sCmbParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_sPortSets.m_nbPrty = (Parity)((sTX_INT)m_sCmbParity.SelectedItem).m_dtVal;
        }

        // フロー制御変更
        private void m_sCmbHandShake_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_sPortSets.m_nbFlwCtl = (Handshake)((sTX_INT)m_sCmbHandShake.SelectedItem).m_dtVal;
        }
    }

    [Serializable]
    public class sPORT_SETS
    {
        public ePORT_TYPE m_nbPortType; // ポート種別
        public string m_txSerPort;      // シリアルポート名
        public sCMNI_PIPE m_sPipe;             // パイプ名
        public int m_dtSerBrt;          // シリアルボーレート
        public int m_dtUsbBrt;          // USBボーレート     
        public int m_dtDataBit;         // データビット
        public Handshake m_nbFlwCtl;    // フロー制御
        public StopBits m_nbStopBit;    // ストップビット
        public Parity m_nbPrty;         // パリティ

        public sPORT_SETS()
        {
            Com.InitData(this);
            m_sPipe = new sCMNI_PIPE(null, "");
        }

        public sPORT_SETS(sPORT_SETS sPrsSets1)
        {
            Com.CopyToData(sPrsSets1, this);
        }

        public override string ToString()
        {
            string txPort1 = "";
            switch (m_nbPortType)     
            {
                case ePORT_TYPE.SER:
                    txPort1 = $"{m_txSerPort}/{m_dtSerBrt.ToString()}";
                    break;
                case ePORT_TYPE.PIPE:
                    txPort1 = $"{m_sPipe.ToString()}";
                    break;
                case ePORT_TYPE.USB:
                    txPort1 = "USB";
                    break;
                default:
                    break;
            }
            return (txPort1);
        }
    }
}
