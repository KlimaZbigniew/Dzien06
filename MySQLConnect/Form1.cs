using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySQLConnect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection conn = null;
        bool isOpen = false;
        private void btnConnect_Click(object sender, EventArgs e)
        {
            string cs = $"server={tbHost.Text};userid={tbUser.Text};password={tbPassword.Text};database={tbDBName.Text};port=3306;sslMode=None";

            try
            {
                if (isOpen)
                {
                    conn.Close();
                    conn.Dispose();
                    isOpen = false;
                    btnConnect.Text = "Połącz";
                }
                else
                {
                    conn = new MySqlConnection(cs);
                    conn.Open();
                    isOpen = true;
                    btnConnect.Text = "Rozłącz";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Błąd połączenia z DB:\n\n\n\n\n {ex.Message}");
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!isOpen)
                return;

            lvGrid.Items.Clear();
            lvGrid.Columns.Clear();

            try
            {
                MySqlCommand cmd = new MySqlCommand(tbSQL.Text,conn);
                using (MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    //dficja kolumn w obiekscie listview
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        lvGrid.Columns.Add(rdr.GetName(i));
                    }

                    string[] arr = new string[rdr.FieldCount];
                    while (rdr.Read())
                    {                        
                        for (int i = 0; i < rdr.FieldCount; i++)
                        {
                            if (rdr.IsDBNull(i))
                                arr[i] = "(NULL)";
                            else
                                arr[i] = rdr.GetString(i);
                        }
                        ///rdr.GetString()
                          lvGrid.Items.Add(new ListViewItem(arr));

                    }


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Błąd pobrania danych:\n\n {ex.Message}");
            }
        }
    }
}
