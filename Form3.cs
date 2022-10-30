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
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Threading;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        string disk;
        public Form3(Form2 f)
        {
            InitializeComponent();
            disk = f.disk;

            FillDrive();
            FillUSBDriveNodes();
            treeView1.BeforeExpand += treeView1_BeforeExpand;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox1.Text = e.Node.FullPath;
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if (dirs.Length != 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            if ((dirs[i].StartsWith($@"{disk}") & checkDir(dirs[i]) == "OSKurs") || (!dirs[i].StartsWith($@"{disk}")))
                            {
                                if (!dirs[i].EndsWith("System Volume Information"))
                                {
                                    TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                                    FillTreeDir(dirNode, dirs[i]);
                                    e.Node.Nodes.Add(dirNode);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }

        public string checkDir(string dir)
        {
            string cDir = dir;
            int a = 0;

            if (dir != disk)
            {
                while (dir[a] != Convert.ToChar($@"\"))
                {
                    a++;
                }
                if (dir[a + 1] == Convert.ToChar($@"\"))
                {
                    cDir = dir.Substring(a + 2);
                }
                else
                {
                    cDir = dir.Substring(a + 1);
                }

                for (a = 0; a < cDir.Length; a++)
                {
                    if (cDir[a] == Convert.ToChar($@"\"))
                    {
                        cDir = cDir.Substring(0, a);
                        break;
                    }
                }
                return cDir;
            }
            else
            {
                return dir;
            }
        }  //проверка на корневую папку

        private void FillTreeDir(TreeNode driveNode, string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode dirNode = new TreeNode();
                    dirNode.Text = dir.Remove(0, dir.LastIndexOf("\\") + 1);
                    driveNode.Nodes.Add(dirNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }

        public void FillDrive() //Выводим диск
        {
            try
            {
                foreach (DriveInfo d in DriveInfo.GetDrives())
                {
                    if (Convert.ToString(d) == disk)
                    {
                        TreeNode driveNode = new TreeNode { Text = d.Name };
                        FillKorDir(driveNode, d.Name);
                        treeView1.Nodes.Add(driveNode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }

        public void FillUSBDriveNodes() //Выводим внешние диски
        {
            try
            {
                foreach (DriveInfo d in DriveInfo.GetDrives())
                {
                    if (Convert.ToString(d.DriveType) == "Removable")
                    {
                        TreeNode driveNode = new TreeNode { Text = d.Name };
                        FillTreeDir(driveNode, d.Name);
                        treeView1.Nodes.Add(driveNode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }

        private void FillKorDir(TreeNode driveNode, string path) //Выводим коренную папку
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    if (Convert.ToString(dir) == $@"{disk}OSKurs")
                    {
                        TreeNode dirNode = new TreeNode();
                        dirNode.Text = dir.Remove(0, dir.LastIndexOf("\\") + 1);
                        driveNode.Nodes.Add(dirNode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }
    }
}
