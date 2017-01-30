using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccessQueryRunner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("please enter the  textbox value");
            }

            if (textBox1.Text.ToString().Equals("mitesh") && textBox2.Text.ToString().Equals("gandhi"))
            {


                DATABASE d = new DATABASE("");
                d.Show();
            }
            else{
                MessageBox.Show("wrong  login");
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text= "";
            textBox2.Text = "";
        }
    }
}
