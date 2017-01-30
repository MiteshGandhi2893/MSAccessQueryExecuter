using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AccessQueryRunner;

namespace AccessQueryRunner
{
    public partial class NewDatabase : Form
    {
        string db = "";
        public NewDatabase(string db)
        {
            InitializeComponent();
            textBox1.Text = db;
        }

        private void NewDatabase_Load(object sender, EventArgs e)
        {

        }
        public void setString(string SB)
        {
            this.db = SB;
        }
        public string getString()
        {
            return db;
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
            Class1.address=textBox1.Text;
            this.Hide();
           
            
               
        }
    }
}
