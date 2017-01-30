using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;
namespace AccessQueryRunner
{
    public partial class DATABASE : Form
    {
        DataTable userTables = null;
        string db = "";
        TabPage tb = null;
        List<string> tabl = new List<string>();
        DbConnection odb=null;
        public DATABASE(string db)
        {

            InitializeComponent();
          
            panel5.Visible = false;
            dataGridView1.Visible = false;
            tabControl1.Enabled = false;
            this.db = db;
            }

        
        private void MInimize_Click(object sender, EventArgs e)
        {

            if (MInimize.Text == "-")
            {

                ObjectExplorer.Hide();
                MInimize.Text = "+";
                panel3.Location = new Point(0, 95);


                panel3.Size = new System.Drawing.Size(1386, 900);
            }
            else if (MInimize.Text == "+")
            {
                ObjectExplorer.Show();
                MInimize.Text = "-";
                panel3.Location = new Point(304, 94);
                panel3.Size = new System.Drawing.Size(1066, 664);
                panel4.Location = new Point(304, 67);
                panel4.Size = new System.Drawing.Size(1066, 29);

            }

        }

        private void ObjectExplorer_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ObjectExplorer.Visible = false;
            panel1.Visible = false;
            panel3.Location = new Point(0, 95);
            panel4.Location = new Point(0, 67);

            panel3.Size = new System.Drawing.Size(1386, 900);
            panel4.Size = new System.Drawing.Size(1386, 29);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            TabPage tb = tabControl1.SelectedTab;
            // MessageBox.Show(tb.Created+""+tb.Controls.GetType());
            try
            {
                DbDataReader dr;
                RichTextBox selectedRtb = new RichTextBox();
                if (tb.Controls.ContainsKey("tx"))
                {
                    //MessageBox.Show("wytye");
                    selectedRtb = (RichTextBox)tb.Controls["tx"];
                    string a = selectedRtb.SelectedText;
                    MessageBox.Show(a);
                    if (a.StartsWith("select") || a.StartsWith("Select"))
                    {
                        MessageBox.Show("hello");
                        DbCommand cmd1 = odb.CreateCommand();
                        cmd1.CommandText = a;
                        dr = cmd1.ExecuteReader();
                        dataGridView1.Visible = true;

                        //DataTable dt = new DataTable();
                      // new DbDataAdapter(cmd1);
                        RichTextBox rx = (RichTextBox)tabPage1.Controls["outputs"];
                        rx.Text="\n\nquery executed succesfully";
                        rx.ForeColor = Color.Blue;
                            DataTable dt = new DataTable();
                            dt.Load(dr);
                            dataGridView1.DataSource = dt;
                           

                    }
                    else
                    {
                        // odb.Open();
                        DbCommand cmd = odb.CreateCommand();
                        cmd.CommandText = a;
                        string query = a;
                        cmd.ExecuteNonQuery();
                        string[] restrictions = new string[4];
                        restrictions[3] = "Table";

                        userTables =
                    odb.GetSchema("Tables", restrictions);

                        treeView1.Nodes[0].Nodes.Clear();
                        // Add list of table names to listBox
                        for (int i = 0; i < userTables.Rows.Count; i++)
                        {

                            treeView1.Nodes[0].Nodes.Add(userTables.Rows[i][2].ToString());//
                            treeView1.Nodes[0].Nodes[i].Nodes.Add("command");
                            treeView1.Nodes[0].Nodes[i].Nodes[0].Nodes.Add("Select");
                            treeView1.Nodes[0].Nodes[i].Nodes[0].Nodes.Add("Insert");
                            treeView1.Nodes[0].Nodes[i].Nodes[0].Nodes.Add("Delete");
                            treeView1.Nodes[0].Nodes[i].Nodes[0].Nodes.Add("Update");
                            treeView1.Nodes[0].Nodes[i].Nodes[0].Nodes.Add("Drop");

                        }
                       
                        if (a.StartsWith("create") || a.StartsWith("Created"))
                        {
                            RichTextBox rx = (RichTextBox)tabPage1.Controls["outputs"];
                            rx.Text="\n\nTable Created succesfully";
                            rx.ForeColor = Color.Blue;
                        }
                        else if (a.StartsWith("insert") || a.StartsWith("Insert"))
                        {
                            RichTextBox rx = (RichTextBox)tabPage1.Controls["outputs"];
                            rx.Text="\n\nValue inserted succesfully";
                            rx.ForeColor = Color.Blue;
                        }
                        else if (a.StartsWith("drop") || a.StartsWith("Drop"))
                        {
                            RichTextBox rx = (RichTextBox)tabPage1.Controls["outputs"];
                            rx.Text="\n\nTable dropped succesfully";
                            rx.ForeColor = Color.Blue;
                        }

                    }





                }






            }
            catch (Exception ex)
            {
                RichTextBox rx = (RichTextBox)tabPage1.Controls["outputs"];
                rx.Text = ex.Message;
                rx.ForeColor = Color.Red;

            }
           // MessageBox.Show(selectedRtb.Text + "");


            //tabControl1.TabPages.Add("NEW QUERY "+ (tabControl1.TabCount+1).ToString());


            //tabControl2.TabPages.Add("OUTPUT " + (tabControl2.TabCount+1).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void objectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectExplorer.Show();
            MInimize.Text = "-";
            panel1.Visible = true;
            panel3.Location = new Point(304, 94);
            panel3.Size = new System.Drawing.Size(1066, 664);
            panel4.Location = new Point(304, 67);
            panel4.Size = new System.Drawing.Size(1066, 29);
        }

        

        

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            odb.Close();

        }

        private void newDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            NewDatabase d = new NewDatabase("C:\\Users\\Mitesh\\Documents\\");
            d.Show();

            
            
           panel5.Visible = true;
           textBox1.Text=Class1.address;

            newDatabaseToolStripMenuItem.Enabled = false;



        }


        public void create(string path)
        {
            try
            {
                String connectionString =
                     @"Provider=Microsoft.ACE.OLEDB.12.0;Data"
                   + @" Source=" + path;


                ADOX.Catalog cat = new ADOX.Catalog();
                cat.Create(connectionString);


                ADODB.Connection con =
                     cat.ActiveConnection as ADODB.Connection;
                if (con != null)
                    con.Close();

                MessageBox.Show("Database '"
                          + path + "' Created");
            }
            catch (Exception ex)
            {
                //if (ex.Message == "Database already exist.")
                //{

                    MessageBox.Show(ex.Message);

                    treeView1.Nodes.Clear();
                    panel5.Visible = true;
                    textBox1.Text = "C:\\Users\\Mitesh\\Documents\\";
               
                //}
               

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            create(textBox1.Text);
            string names = textBox1.Text.Substring(26);
          treeView1.Nodes.Add(names);
          childnodes(textBox1.Text);
         // panel5.Visible = false;

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string path = openFileDialog1.FileName;
            string na = openFileDialog1.SafeFileName;
            create(path);
            treeView1.Nodes.Clear();
            panel5.Visible = false;
            treeView1.Nodes.Add(na);
            childnodes(path);
            // treeView1.Nodes[0].Nodes.Add("hello");
                //if)

            //{
            //    odb.Close();
            //}
        }

        void childnodes(string path)
        {
            DbProviderFactory factory =
    DbProviderFactories.GetFactory("System.Data.OleDb");
          


            odb = factory.CreateConnection();
            odb.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path;

            // We only want user tables, not system tables
            string[] restrictions = new string[4];
            restrictions[3] = "Table";

            odb.Open();

            // Get list of user tables
            userTables =
                odb.GetSchema("Tables", restrictions);


            // Add list of table names to listBox
            for (int i = 0; i < userTables.Rows.Count; i++)
            {

                treeView1.Nodes[0].Nodes.Add(userTables.Rows[i][2].ToString());//
                treeView1.Nodes[0].Nodes[i].Nodes.Add("command");
                treeView1.Nodes[0].Nodes[i].Nodes[0].Nodes.Add("Select");
                treeView1.Nodes[0].Nodes[i].Nodes[0].Nodes.Add("Insert");
                treeView1.Nodes[0].Nodes[i].Nodes[0].Nodes.Add("Delete");
                treeView1.Nodes[0].Nodes[i].Nodes[0].Nodes.Add("Update");
                treeView1.Nodes[0].Nodes[i].Nodes[0].Nodes.Add("Drop");

            }

            
        }
        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            string columns = "";
            string cols = "";

            TreeNode node = treeView1.SelectedNode;
            if (node.Text.Equals("Select") || node.Text.Equals("Insert") || node.Text.Equals("Delete") || node.Text.Equals("Update") || node.Text.Equals("Insert"))
            {
                tabControl1.Enabled = true;
                DbDataReader dr;
                DbCommand cmd1 = odb.CreateCommand();
                cmd1.CommandText = "select * from " + node.Parent.Parent.ToString().Substring(9);
                dr = cmd1.ExecuteReader();
                DataTable dt = new DataTable();

                dt.Load(dr);
                tabl = (from DataColumn col in dt.Columns
                        select col.ColumnName).ToList();


                for (int i = 0; i < tabl.Count - 1; i++)
                {
                    columns = columns + tabl[i] + ",";
                    cols = cols + tabl[i] + "=" + "' '" + " and|or ";
                }
                columns = columns + tabl[tabl.Count - 1] + " ";
                cols = cols + tabl[tabl.Count - 1] + "=" + "' '" + ";";
            }
                //MessageBox.Show(string.Format("You selected: {0}", node.Text));
                if(node.Text.Equals("Select"))
                {


                    TabPage tb1 = newQuery();
                    tabControl1.SelectedTab = tb1;
                    tb1.Text = "select query "+node.Parent.Parent.ToString().Substring(9);
                    RichTextBox selectedRtb = new RichTextBox();
                    if (tb1.Controls.ContainsKey("tx"))
                    {

                        selectedRtb = (RichTextBox)tb1.Controls["tx"];

                        selectedRtb.Text = "Select " +columns+" |* from " + node.Parent.Parent.ToString().Substring(9)+" Where "+ cols;

                    }
                }
            
                else if (node.Text.Equals("Insert"))
                {


                    TabPage tb1 = newQuery();
                    tabControl1.SelectedTab = tb1;
                    tb1.Text = "Insert query " + node.Parent.Parent.ToString().Substring(9);
                    RichTextBox selectedRtb = new RichTextBox();
                    if (tb1.Controls.ContainsKey("tx"))
                    {

                        selectedRtb = (RichTextBox)tb1.Controls["tx"];

                        selectedRtb.Text = "insert " + columns + " into " + node.Parent.Parent.ToString().Substring(9) + " values ( )";

                    }
                }
                else if (node.Text.Equals("Delete"))
                {

                    TabPage tb1 = newQuery();
                    tabControl1.SelectedTab = tb1;
                    tb1.Text = "Delete query " + node.Parent.Parent.ToString().Substring(9);
                    RichTextBox selectedRtb = new RichTextBox();
                    if (tb1.Controls.ContainsKey("tx"))
                    {

                       
                        selectedRtb = (RichTextBox)tb1.Controls["tx"];

                        selectedRtb.Text = "Delete " + columns + "| from " + node.Parent.Parent.ToString().Substring(9);

                    }
                }
            else if (node.Text.Equals("Update"))
            {

                string col1 = cols.Replace(" and|or ", ",");
                TabPage tb1 = newQuery();
                tb1.Text = "Update query " + node.Parent.Parent.ToString().Substring(9);
                tabControl1.SelectedTab = tb1;
                RichTextBox selectedRtb = new RichTextBox();
                if (tb1.Controls.ContainsKey("tx"))
                {

                  
                    selectedRtb = (RichTextBox)tb1.Controls["tx"];

                    selectedRtb.Text = "update " + node.Parent.Parent.ToString().Substring(9) + " set " + col1 + " where " + cols;

                }
            }

            else if (node.Text.Equals("Drop"))
            {

                MessageBox.Show("dhsgs");
                TabPage tb1 = newQuery();
                tabControl1.SelectedTab = tb1;
               // tabControl1.SelectedTab.BackColor =Color.Brown;
                tb1.Text = "Drop query " + node.Parent.Parent.ToString().Substring(9);
                RichTextBox selectedRtb = new RichTextBox();
                if (tb1.Controls.ContainsKey("tx"))
                {

                  
                    selectedRtb = (RichTextBox)tb1.Controls["tx"];

                    selectedRtb.Text = "drop table  " + node.Parent.Parent.ToString().Substring(9);

                }
            }







        }
        public TabPage newQuery()
        {

            RichTextBox tx = new RichTextBox();
            tx.Name = "tx";
            tx.Font = new System.Drawing.Font("Microsoft Sans Serif", 18, System.Drawing.FontStyle.Bold);
            tx.Size = new System.Drawing.Size(1066, 500);
         //   MessageBox.Show("sbajdasdjashskjs,c x");
            tb = new TabPage("new query");
            tabControl1.TabPages.Add(tb);
            tb.Controls.Add(tx);
           return tb;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            

        }

        private void newQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (odb != null)
            {
                tabControl1.Enabled = true;
                RichTextBox tx = new RichTextBox();
                tx.Name = "tx";
                tx.Font = new System.Drawing.Font("Microsoft Sans Serif", 18, System.Drawing.FontStyle.Bold);
                tx.Size = new System.Drawing.Size(1066, 500);
                TabPage tb = new TabPage("new query");
                tabControl1.TabPages.Add(tb);
                tb.Controls.Add(tx);
                tabControl1.SelectedTab = tb;
            }
            else
            {
                MessageBox.Show("please create or connect to database");
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        

       

       

       
    }
}