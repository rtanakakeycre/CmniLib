using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using UtilLib;

namespace CmniLib
{
    public partial class Form1 : Form
    {
        public sPORT_SEL_INFO m_sPortSelInfo;

        public Form1()
        {
            InitializeComponent();
            int h1 = this.Handle.ToInt32();
            m_sPortSelInfo = new sPORT_SEL_INFO();
            Com.Deserialize(Com.GetExePath() + "PortSelFrm.xml", ref m_sPortSelInfo, true);
            
            // パイプを追加
            cmniCtrlUc1.AddCmniPort("Src", RcvData, m_sPortSelInfo.m_sPortSets);
            cmniCtrlUc1.AddCmniPort("Dst", RcvData2, m_sPortSelInfo.m_sPortSetsDst);

            // 通信管理開始
            cmniCtrlUc1.StaCmniCtrl();
            this.Text = $"{cmniCtrlUc1.m_sPrs.m_txName} {cmniCtrlUc1.m_sPrs.m_txId}";
            
            portEditUc1.SetPortSets(cmniCtrlUc1, cmniCtrlUc1.GetCmniPort("Src"));
            portEditUc2.SetPortSets(cmniCtrlUc1, cmniCtrlUc1.GetCmniPort("Dst"));

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cmniCtrlUc1.EndCmniCtrl();
            Com.Serialize(Com.GetExePath() + "PortSelFrm.xml", m_sPortSelInfo);
        }

        public void UpdPrsList(string txCmd1)
        {
            this.Invoke(new dgPRS_LIST_UPD(UpdPrsList_Invoke), txCmd1);
        }

        public void UpdPrsList_Invoke(string txCmd1)
        {
            m_sRtbRcv.Text += $"\t{txCmd1}";
            List<sOTH_PRS> asPrs1 = cmniCtrlUc1.GetRcgPrsList();
            m_sDgvPrsList.Rows.Clear();
            foreach (sPRS sPrs1 in asPrs1)
            {
                m_sDgvPrsList.Rows.Add();
                int idRow1 = m_sDgvPrsList.Rows.Count - 1;
                m_sDgvPrsList["_Name", idRow1].Value = sPrs1.m_txName;
                m_sDgvPrsList["_ID", idRow1].Value = sPrs1.m_txId;
            }
        }

        public void RcvData(byte[] adtCmd1, int ctCmd1)
        {

        }

        public void RcvData2(byte[] adtCmd1, int ctCmd1)
        {

        }

        private void portEditUc1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] adtCmd1 = { 1, 2, 3};
            cmniCtrlUc1.SendCmdToCmniPort("Src", adtCmd1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] adtCmd1 = { 1, 2, 3 };
            cmniCtrlUc1.SendCmdToCmniPort("Dst", adtCmd1);
        }
    }


    // ポート選択情報
    [Serializable]
    public class sPORT_SEL_INFO
    {
        public sPORT_SETS m_sPortSets;
        public sPORT_SETS m_sPortSetsDst;

        public sPORT_SEL_INFO()
        {
            m_sPortSets = new sPORT_SETS();
            m_sPortSetsDst = new sPORT_SETS();
        }

        public sPORT_SEL_INFO(sPORT_SEL_INFO sPortSelInfo1)
        {
            Com.CopyToData(sPortSelInfo1, this);
        }
    }

}
