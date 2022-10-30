using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        DriveInfo[] allDrives = DriveInfo.GetDrives();
        
        public string disk;
        public Form1()
        {
            InitializeComponent();
            
            foreach (DriveInfo d in allDrives)
            {
                if (Convert.ToString(d.DriveType) == "Fixed" | Convert.ToString(d.DriveType) == "Removable")
                {
                    comboBox1.Items.Add(d.Name);
                }       
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (disk is null)
            {
                MessageBox.Show("Выберите диск");
            }
            else 
            {
                Form2 newForm = new Form2(this);
                newForm.Show();
                Hide();
            }  
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            disk = comboBox1.SelectedItem.ToString();
            foreach (DriveInfo d in allDrives)
            {
                if (Convert.ToString(d) == disk)
                {

                }
            }


        }

        public void button2_Click(object sender, EventArgs e)
        {
    
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



    }

}
