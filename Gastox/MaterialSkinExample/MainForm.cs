using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
namespace MaterialSkinExample
{
    public partial class MainForm : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
        public MainForm()
        {
            InitializeComponent();
            
            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
			materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
			materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            
            // Add dummy data to the listview
            if (!File.Exists(Directory.GetCurrentDirectory().ToString() + "\\db.sdf"))
            {
                MessageBox.Show("No se encontró la base de datos. No sirve de nada este programa si no puede guardar info");
                Environment.Exit(1);
            }
            else
            {
                dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                dateTimePicker2.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());

            materialSingleLineTextField3.Text = DateTime.Now.ToShortDateString();
            cargargastos();
            
        }

	      void cargargastos()
	    {

                
                SqlCeCommand fechas = new SqlCeCommand();
                fechas.Parameters.AddWithValue("fecha1", dateTimePicker1.Value.ToString());
                fechas.Parameters.AddWithValue("fecha2", dateTimePicker2.Value.ToString());
                //MessageBox.Show(dateTimePicker1.Value.ToString() + "\n" + dateTimePicker2.Value.ToString());
                Conexion.abrir();
                DataTable gastos = Conexion.Consultar("*", "Gastos", "WHERE fecha BETWEEN @fecha1 and @fecha2;", "", fechas);
                Conexion.cerrar();
                if (gastos.Rows.Count > 0)
                {
                    float total = 0;
                    for (int i = 0; i < gastos.Rows.Count; i++)
                    {
                        float importe = float.Parse(gastos.Rows[i][2].ToString());
                        total += importe;
                        var data = new[] {

                        new[] { gastos.Rows[i][0].ToString(),gastos.Rows[i][1].ToString(), importe.ToString("$0.00"), gastos.Rows[i][3].ToString() }
                         };
                        foreach (string[] version in data)
                        {
                            var item = new ListViewItem(version);
                            materialListView1.Items.Add(item);
                        }
                    }
                    materialLabel1.Text = "Total: " + total.ToString("$0.00");
                }
                else
                {
                    materialLabel1.Text = "Total: $0.00";
                }
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
               }

	    private int colorSchemeIndex;
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
	       
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            //materialProgressBar1.Value = Math.Min(materialProgressBar1.Value + 10, 100);
        }

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {
            //materialProgressBar1.Value = Math.Max(materialProgressBar1.Value - 10, 0);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField2_Leave(object sender, EventArgs e)
        {
            float importe = 0;
            try
            {
                importe = float.Parse(materialSingleLineTextField2.Text);
                materialSingleLineTextField2.Text = importe.ToString("$0.00");

            }
            catch
            {
                materialSingleLineTextField2.Text = importe.ToString("$0.00");
            }
        }

        private void materialRaisedButton1_Click_1(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 2) colorSchemeIndex = 0;

            //These are just example color schemes
            switch (colorSchemeIndex)
            {
                case 0:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    break;
                case 1:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
                    break;
                case 2:
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
                    break;
            }
        }

        private void materialButton1_Click_1(object sender, EventArgs e)
        {
            materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.LIGHT : MaterialSkinManager.Themes.DARK;

        }

        private void materialTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (materialTabControl1.SelectedIndex == 0)
                materialSingleLineTextField1.Focus();
            if (materialTabControl1.SelectedIndex == 1)
            {
                materialListView1.Items.Clear();
                button1.Enabled = false;
                dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "00:00:00");
                dateTimePicker2.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + "23:59:59");
                SqlCeCommand fechas = new SqlCeCommand();
                fechas.Parameters.AddWithValue("fecha1", dateTimePicker1.Value.ToString());
                fechas.Parameters.AddWithValue("fecha2", dateTimePicker2.Value.ToString());
                //MessageBox.Show(dateTimePicker1.Value.ToString() + "\n" + dateTimePicker2.Value.ToString());
                Conexion.abrir();
                DataTable gastos = Conexion.Consultar("*", "Gastos", "WHERE fecha BETWEEN @fecha1 and @fecha2;", "", fechas);
                Conexion.cerrar();
                if (gastos.Rows.Count > 0)
                {
                    materialListView1.Items.Clear();
                    float total = 0;
                    for (int i = 0; i < gastos.Rows.Count; i++)
                    {
                        float importe = float.Parse(gastos.Rows[i][2].ToString());
                        total += importe;
                        var data = new[] {

                        new[] { gastos.Rows[i][0].ToString(),gastos.Rows[i][1].ToString(), importe.ToString("$0.00"), gastos.Rows[i][3].ToString() }
                         };
                        foreach (string[] version in data)
                        {
                            var item = new ListViewItem(version);
                            materialListView1.Items.Add(item);
                        }
                    }
                    materialLabel1.Text = "Total: " + total.ToString("$0.00");
                }
                else
                {
                    materialLabel1.Text = "Total: $0.00";
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            materialListView1.Items.Clear();
            SqlCeCommand fechas = new SqlCeCommand();
            fechas.Parameters.AddWithValue("fecha1", dateTimePicker1.Value.ToString());
            fechas.Parameters.AddWithValue("fecha2", dateTimePicker2.Value.ToString());
            //MessageBox.Show(dateTimePicker1.Value.ToString() + "\n" + dateTimePicker2.Value.ToString());
            Conexion.abrir();
            DataTable gastos = Conexion.Consultar("*", "Gastos", "WHERE fecha BETWEEN @fecha1 and @fecha2;", "", fechas);
            Conexion.cerrar();
            if (gastos.Rows.Count > 0)
            {
                float total = 0;
                for (int i = 0; i < gastos.Rows.Count; i++)
                {
                    float importe = float.Parse(gastos.Rows[i][2].ToString());
                    total += importe;
                    var data = new[] {

                        new[] { gastos.Rows[i][0].ToString(),gastos.Rows[i][1].ToString(), importe.ToString("$0.00"), gastos.Rows[i][3].ToString() }
                         };
                    foreach (string[] version in data)
                    {
                        var item = new ListViewItem(version);
                        materialListView1.Items.Add(item);
                    }
                }
                materialLabel1.Text = "Total: " + total.ToString("$0.00");
            }
            else
            {
                materialLabel1.Text = "Total: $0.00";
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            materialListView1.Items.Clear();
            SqlCeCommand fechas = new SqlCeCommand();
            fechas.Parameters.AddWithValue("fecha1", dateTimePicker1.Value.ToString());
            fechas.Parameters.AddWithValue("fecha2", dateTimePicker2.Value.ToString());
            //MessageBox.Show(dateTimePicker1.Value.ToString() + "\n" + dateTimePicker2.Value.ToString());
            Conexion.abrir();
            DataTable gastos = Conexion.Consultar("*", "Gastos", "WHERE fecha BETWEEN @fecha1 and @fecha2;", "", fechas);
            Conexion.cerrar();
            if (gastos.Rows.Count > 0)
            {
                float total = 0;
                for (int i = 0; i < gastos.Rows.Count; i++)
                {
                    float importe = float.Parse(gastos.Rows[i][2].ToString());
                    total += importe;
                    var data = new[] {

                        new[] { gastos.Rows[i][0].ToString(),gastos.Rows[i][1].ToString(), importe.ToString("$0.00"), gastos.Rows[i][3].ToString() }
                         };
                    foreach (string[] version in data)
                    {
                        var item = new ListViewItem(version);
                        materialListView1.Items.Add(item);
                    }
                }
                materialLabel1.Text = "Total: " + total.ToString("$0.00");
            }
            else
            {
                materialLabel1.Text = "Total: $0.00";
            }
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (materialSingleLineTextField1.Text.Length > 0 && materialSingleLineTextField2.Text.Length > 0 && materialSingleLineTextField3.Text.Length > 0)
            {
                try
                {
                    string concepto, importe, fecha;
                    concepto = materialSingleLineTextField1.Text;
                    importe = materialSingleLineTextField2.Text.Replace("$", "");
                    fecha = materialSingleLineTextField3.Text + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                    Conexion.abrir();
                    SqlCeCommand nuevogasto = new SqlCeCommand();
                    nuevogasto.Parameters.AddWithValue("co", concepto);
                    nuevogasto.Parameters.AddWithValue("im", importe);
                    nuevogasto.Parameters.AddWithValue("fe", fecha);
                    Conexion.Insertar("Gastos", "concepto,importe,fecha", "@co,@im,@fe", nuevogasto);
                    Conexion.cerrar();
                    materialSingleLineTextField1.Text = "";
                    materialSingleLineTextField2.Text = "";
                    materialSingleLineTextField3.Text = DateTime.Now.ToShortDateString();
                }
                catch
                {
                    MessageBox.Show("hubo algun problema con los datos, intentalo de nuevo");
                }
            }
            else MessageBox.Show("Faltan Datos");
        }

        private void materialListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedindex = materialListView1.SelectedItems;
            string id;
            id = selectedindex[0].Text;
          
            DialogResult borrar = MessageBox.Show("Está seguro de borrar este gasto?\nID Gasto: "+id,"Esta seguro de borrar este gasto?",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (borrar == DialogResult.Yes)
            {
                Conexion.abrir();
                SqlCeCommand borro = new SqlCeCommand();
                borro.Parameters.AddWithValue("id", id);
                Conexion.Eliminar("Gastos", "idgasto = @id", borro);
                Conexion.cerrar();
                materialTabControl1.SelectedIndex = 0;
                materialTabControl1.SelectedIndex = 1;
            }
        }

        private void materialSingleLineTextField3_Leave(object sender, EventArgs e)
        {
            try
            {
                DateTime fechapuesta = Convert.ToDateTime(materialSingleLineTextField3.Text);
            }
            catch
            {
                materialSingleLineTextField3.Text = DateTime.Now.ToShortDateString();
            }
        }
    }
}
