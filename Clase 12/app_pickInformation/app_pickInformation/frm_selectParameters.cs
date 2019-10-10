using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace app_pickInformation
{
    public partial class frm_selectParameters : Form
    {
        public static bool ejecutar;
        public static string parametro_01;
        public static string parametro_02;
        List<string> listParameters = new List<string> { "Elemento", "Descripcion de Partida", "Nivel del Elemento" };

        public frm_selectParameters()
        {
            InitializeComponent();
            ejecutar = false;
            parametro_01 = "";
            parametro_02 = "";
            txt_Parametro01.Text = "";
            cmb_Parametro02.DataSource = listParameters;
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            ejecutar = false;
            Close();

        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            ejecutar = true;
            parametro_01 = txt_Parametro01.Text;
            parametro_02 = cmb_Parametro02.SelectedItem.ToString();
            Close();
        }
    }
}
