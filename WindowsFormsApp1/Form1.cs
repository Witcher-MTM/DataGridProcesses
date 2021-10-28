using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Button closebtn = new Button();
        public DataGrid dataGrid = new DataGrid();
        public string processName = string.Empty;
        public int rowIndex = 0;
        public Form1()
        {
            InitializeComponent();

            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].HeaderText = "Process name";
            dataGridView1.Columns[1].HeaderText = "ID";
            foreach (var item in Process.GetProcesses())
            {
                dataGridView1.Rows.Add(item.ProcessName, item.Id);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process.GetProcesses().ToList().ForEach(item =>
                {
                    if (item.ProcessName.Contains(processName))
                        item.Kill();
                });
                dataGridView1.Rows.RemoveAt(rowIndex);
            }
            catch (Exception ex) { }
            
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            processName = (sender as DataGridView).CurrentCell.Value.ToString();
            rowIndex = (sender as DataGridView).CurrentCell.RowIndex;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (this.textBox1.Text.ToLower().Contains(this.dataGridView1[0, i].Value.ToString().ToLower()))
                {
                    processName = this.textBox1.Text;
                    dataGridView1[0, i].Selected = true;
                }
            }

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (this.textBox1.Text ==  String.Empty)
                {
                    dataGridView1[0, i].Selected = false;
                }
            }
        }
    }
}
