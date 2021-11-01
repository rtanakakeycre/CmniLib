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
    public partial class PortSelFrm : Form
    {
        public delegate void dgUpdPrsList_Invoke(string txCmd1);

        public PortSelFrm()
        {
            InitializeComponent();

        }

        public PortSelFrm(CmniCtrlUc sCmniCtrlUc1, sPORT_SETS sPortSets1)
        {
            InitializeComponent();

            m_sPortSelUc.Init(sCmniCtrlUc1, sPortSets1);
        }

        public sPORT_SETS GetPortSets()
        {
            return(m_sPortSelUc.GetPortSets());
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void portSelUc1_Load(object sender, EventArgs e)
        {

        }
    }
}
