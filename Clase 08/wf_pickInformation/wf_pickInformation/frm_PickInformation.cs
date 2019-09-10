using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wf_pickInformation
{
    public partial class frm_PickInformation : Form
    {
        public frm_PickInformation(List<ElementData> data)
        {
            InitializeComponent();
            dgv_Elements.DataSource = data;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
