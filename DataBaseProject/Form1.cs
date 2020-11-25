using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace DataBaseProject
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //DatagridView Proprietes
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "Surname";
            dataGridView1.Columns[2].Name = "Email";

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //Selection Mode
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        //ADD TO DGVIEW
        
        private void add(String name, String surname, String email)
        {
            dataGridView1.Rows.Add(name,surname,email);

            clearTxts();
        }

        //CLEAR TXTS
        private void clearTxts()
        {
            nametxt.Text = "";
            surntxt.Text = "";
            emailtxt.Text = "";
        }
        //UPDATE DGVIEW
        private void update()
        {
            dataGridView1.SelectedRows[0].Cells[0].Value = nametxt.Text;
            dataGridView1.SelectedRows[0].Cells[1].Value = surntxt.Text;
            dataGridView1.SelectedRows[0].Cells[2].Value = emailtxt.Text;

            clearTxts();
        }
        //DELETE ROWS
        private void delete()
        {
            if (MessageBox.Show("Удалить?", "DELETE", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                int index = dataGridView1.SelectedRows[0].Index;

                dataGridView1.Rows.RemoveAt(index);

                clearTxts();
            }
        }

        private void OnButtonClear(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void OnButtonUpdate(object sender, EventArgs e)
        {
            update();
        }

        private void OnButtonAdd(object sender, EventArgs e)
        {
            add(nametxt.Text, surntxt.Text, emailtxt.Text);
        }

        private void OnButtonDelete(object sender, EventArgs e)
        {
            delete();
        }

        private void OnButtonImport(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(@"C:\test\table.txt");
            string[] values;

            for (int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split('/');
                string[] row = new string[values.Length];

                for (int j = 0; j < values.Length; j++)
                {
                    row[j] = values[j].Trim();
                }

                dataGridView1.Rows.Add(row);
            }
        }

        private void OnExportClick(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();

            exApp.Workbooks.Add();
            Excel.Worksheet wsh = (Excel.Worksheet)exApp.ActiveSheet;
            int i, j;
            for (i = 0; i <= dataGridView1.RowCount - 2; i++)
            {
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    wsh.Cells[i + 1, j + 1] = dataGridView1[j, i].Value.ToString();
                }
            }
            exApp.Visible = true;
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            nametxt.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            surntxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            emailtxt.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }
    }
}
