using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UtilLib;

namespace CmniLib
{
    public partial class CmniEditUc : UserControl
    {
        public CmniCtrlUc m_sCmniCtrlUc;

        public sCMNI_PORT m_sCmniPort;
        [Description("ラベルのテキストを設定します。")]
        [Category("表示")]
        [Bindable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue("")]
        [SettingsBindable(true)]
        public override string Text{ 
            set {
                m_sLblName.Text = value;   
            }
            get
            {
                return (m_sLblName.Text);
            }
        }

        public CmniEditUc()
        {
            InitializeComponent();
            m_sCmniPort = new sCMNI_PORT();
        }

        public void SetPortSets(CmniCtrlUc sCmniCtrlUc1, sCMNI_PORT sCmniPort1)
        {
            m_sCmniCtrlUc = sCmniCtrlUc1;

            m_sCmniPort = sCmniPort1;
            if (!m_sCmniPort.m_flCone)
            {
                m_sCmniPort.m_flCone = m_sCmniCtrlUc.StaCmniPort(m_sCmniPort);
            }

            UpdDsp();
        }

        public void UpdDsp()
        {
            Com.DoSomethingWithoutEvents(new List<Control> { this }, () =>
            {
                m_sBtnPort.Text = m_sCmniPort.ToString();
                m_sChkCmni.Checked = m_sCmniPort.m_flCone;
                m_sBtnPort.Text = m_sCmniPort.m_sPortSets.ToString();
                if (m_sCmniPort.m_flCone)
                {
                    m_sBtnPort.ForeColor = Color.Black;
                }
                else{
                    m_sBtnPort.ForeColor = Color.Gray;
                }

            });

        }

        private void m_sBtnPort_Click(object sender, EventArgs e)
        {
            PortSelFrm sFrm1 = new PortSelFrm(m_sCmniCtrlUc, m_sCmniPort.m_sPortSets);
            if(sFrm1.ShowDialog() == DialogResult.OK)
            {
                m_sCmniPort.m_sPortSets = sFrm1.GetPortSets();

                if (!m_sCmniPort.m_flCone)
                {
                    m_sCmniPort.m_flCone = m_sCmniCtrlUc.StaCmniPort(m_sCmniPort);
                }

                UpdDsp();
            }
        }

        private void m_sChkCmni_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_sCmniPort.m_flCone)
            {
                m_sCmniPort.m_flCone = m_sCmniCtrlUc.StaCmniPort(m_sCmniPort);
            }
            else
            {
                m_sCmniPort.m_flCone = m_sCmniCtrlUc.EndCmniPort(m_sCmniPort);
            }
            UpdDsp();
        }
    }
}
