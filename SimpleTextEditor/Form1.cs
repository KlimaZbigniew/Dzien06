using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleTextEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            bool? result = CheckModification();
            if (result == null || (bool)!result == true)
            {

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Załadowanie do edytora
                    LoadToEditor(openFileDialog.FileName);
                }
            }
        }

        private string currentFileName = null;

        private void LoadToEditor(string fileName)
        {
            try
            {
                richTextBox.Text = File.ReadAllText(fileName);
                richTextBox.Enabled = true;
                mnuSave.Enabled = true;
                mnuSaveAs.Enabled = true;
                tsFileName.Text = fileName;
                currentFileName = fileName;
            }
            catch (IOException exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (Exception exc)
            {

                throw;
            }
        }

        private void SaveToFile(string fileName)
        {
            try
            {
                File.WriteAllText(fileName, richTextBox.Text);
                isModified = false;
                tsModified.Text = String.Empty;
            }
            catch (IOException exc)
            {
                MessageBox.Show(exc.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
            catch (Exception)
            {

                throw;
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            if (currentFileName == null || currentFileName == string.Empty)
                mnuSaveAs_Click(sender, e);
            else
                SaveToFile(currentFileName);
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() ==DialogResult.OK)
            {
                SaveToFile(saveFileDialog.FileName);
                currentFileName = saveFileDialog.FileName;
                tsFileName.Text  = currentFileName;
            }
        }
        bool isModified = false;
        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            isModified = true;
            tsModified.Text = "MOD";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            bool? result = CheckModification();
            if (result !=null && (bool)!result)
                e.Cancel = true;

            //if (isModified)
            //{
            //    DialogResult result = MessageBox.Show("Istnieją nie zapisane dane. Czy kontynuować?", "Ostrzeżenie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //        if (result == DialogResult.No)
            //    {  }
            //}
        }
        /// <summary>
        /// Sprawdza czy istniejący dokument jest zmodyfikowany
        /// </summary>
        /// <returns>
        /// NULL - brak mofyfikacji
        /// true - kontynuuj
        /// false - wstrzymaj operacje
        /// </returns>
        private Boolean? CheckModification()
        {
            if (!isModified)
                return null;

            DialogResult result = MessageBox.Show("Istnieją nie zapisane dane. Czy kontynuować?", "Ostrzeżenie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return result == DialogResult.Yes;
        }

        private void mnuNew_Click(object sender, EventArgs e)
        {
            bool? result = CheckModification();
            if (result == null || (bool)result)
            {
                richTextBox.Text = string.Empty;
                richTextBox.Enabled = true;
                mnuSave.Enabled = true;
                mnuSaveAs.Enabled = true;
                tsFileName.Text = "Nowy dokument";
                currentFileName = "";
            }
        }
    }
}
