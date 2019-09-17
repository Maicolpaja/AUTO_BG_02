using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using x = Microsoft.Office.Interop.Excel;
namespace wf_pickInformation
{
    public partial class frm_PickInformation : Form
    {
        public frm_PickInformation(List<ElementData> data)
        {
            InitializeComponent();
            dgv_Elements.DataSource = data;
            lbl_cant.Text = "Elementos seleccionados: " + dgv_Elements.Rows.Count.ToString();
            volumenTotal();
            //ptb_test.Image = Image.FromFile("../documentos/Imagen.png");
            //ptb_test.Load("https://playcodeacademy.com/wp-content/uploads/2017/01/codigo-fuente-programacion.jpg");
        }


        private void volumenTotal()
        {
            double volumenTotal = 0;
            foreach (DataGridViewRow row in dgv_Elements.Rows)
            {
                volumenTotal = volumenTotal + Convert.ToDouble(row.Cells["Volumen"].Value);
            }
            lbl_volumen.Text = volumenTotal.ToString() + " m3";
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            x.Application excel = new x.Application();
            excel.Application.Workbooks.Add(true);

            int indiceColumna = 0;

            //Asignar los valores de cabecera de cada columna
            foreach (DataGridViewColumn col in dgv_Elements.Columns)
            {
                indiceColumna++;
                excel.Cells[1, indiceColumna] = col.Name;
            }

            int indiceFila = 0;

            //Recorrer como una matriz
            foreach( DataGridViewRow row in dgv_Elements.Rows)
            {
                indiceFila++;
                indiceColumna = 0;

                foreach (DataGridViewColumn col in dgv_Elements.Columns)
                {
                    indiceColumna++;
                    excel.Cells[indiceFila + 1, indiceColumna] = row.Cells[col.Name].Value;
                }
            }
            excel.Visible = true;
        }
    }
}
