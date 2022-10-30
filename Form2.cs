using System;//
using System.Collections.Generic;
using System.ComponentModel;//
using System.Data;//
using System.Linq;//
using System.Text;
using System.Windows.Forms;//
using System.IO;//
using System.Drawing;//
using System.Diagnostics;//
using System.Threading;//
using System.Security.AccessControl;//
using System.Security.Principal;//
using System.Management;//
using System.Net.Sockets;//
using Microsoft.VisualBasic;//
using System.Runtime.InteropServices;


namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        const int OPEN_EXISTING = 3;
        const uint GENERIC_READ = 0x80000000;
        const uint GENERIC_WRITE = 0x40000000;
        const uint IOCTL_STORAGE_EJECT_MEDIA = 0x2D4808;

        public string disk;
        string selectdir;
        string selectfile;

        public static List<Process> nameProcess;
        public static List<Process> updatedProcess;

        public static Process lastProcess = null;

        public static string killedProcessName = null;
        public static DateTime killedProcessTime = DateTime.Now;
        public static Process startedProcess;

        string homePage;
        string backet;
        string systemPath;
        string logFileSystem;
        static string MyLogs;
        string Documentsfile;
        public string logProcess;
        public string logProcessClose;
        public string logTMP;
        DriveInfo[] allDrives = DriveInfo.GetDrives();

        private static System.Timers.Timer aTimer;

        public static int zakr = 0;

        public Form2(Form1 f)
        {
            InitializeComponent();
            disk = f.disk;
            check();
            toStart();

            nameProcess = new List<Process>();
            updatedProcess = new List<Process>();


            button7.Enabled = true;
            treeView1.Enabled = true;

            listView1.ContextMenuStrip = contextMenuStrip1;
            contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(cms_Opening1);

            FillDrive();
            treeView1.BeforeExpand += treeView1_BeforeExpand;
            listView1.SmallImageList = imageList1;

            listView1.AllowDrop = true;
            listView1.DragDrop += new DragEventHandler(listView1_DragDrop);
            listView1.DragEnter += new DragEventHandler(listView1_DragEnter);
            listView1.ItemDrag += new ItemDragEventHandler(listView1_ItemDrag);

            aTimer = new System.Timers.Timer();

            aTimer.Interval = 1000;

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;

            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;

            // Start the timer
            aTimer.Enabled = true;






        }


        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {

            DateTime dt = DateTime.MinValue;
            Process process = null;

            if (lastProcess != null) {
                foreach (var proc in Process.GetProcesses())
                {
                    process = proc;
                    if (lastProcess.ProcessName == proc.ProcessName)
                    {
                        lastProcess = null;
                        break;
					}
                }

                if (lastProcess != null){
                    try {
                            killedProcessName = lastProcess.ProcessName;
                            killedProcessTime = lastProcess.StartTime;
                        }
                    catch {

					}
                    
                }
                    
            }

            foreach (var proc in Process.GetProcesses())
            {
                try
                {
                    if (proc.StartTime > dt)
                    {
                        dt = proc.StartTime;
                        process = proc;
                        lastProcess = process;
                    }
                }
                catch { }
            }




            string curFile = @"C:\OSKurs\logprocess.txt";

            if (killedProcessName != null)
            {
                if (File.Exists(curFile))
                {
                    File.AppendAllText(curFile, "Последний процесс: " + killedProcessName + " запущен " + killedProcessTime + " завершен в " + killedProcessTime.Add((TimeSpan)(DateTime.Now - killedProcessTime)) + Environment.NewLine);
                }
                else
                {
                    using (File.Create(curFile)) { }
                    File.AppendAllText(curFile, "Последний процесс: " + killedProcessName + " запущен " + killedProcessTime + " завершен в " + (TimeSpan)(DateTime.Now - killedProcessTime) + Environment.NewLine);
                }

                killedProcessName = null;
            }

        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
		{
            listView1.DoDragDrop(listView1.SelectedItems, DragDropEffects.Move);
		}

		private void listView1_DragEnter(object sender, DragEventArgs e)
		{
            e.Effect = DragDropEffects.Move;
        }

		private void listView1_DragDrop(object sender, DragEventArgs e)
		{
            if (listView1.SelectedItems.Count == 0) { return; }
            Point Pt = listView1.PointToClient(new Point(e.X, e.Y));
            ListViewItem ItemDrag = listView1.GetItemAt(Pt.X, Pt.Y);

            if (ItemDrag == null) { return; }

            int ItemDragIndex = ItemDrag.Index;
            ListViewItem[] Sel = new ListViewItem[listView1.SelectedItems.Count];
            

            MessageBox.Show(ItemDrag.Text);

			for (int i = 0; i < listView1.SelectedItems.Count; i++)
			{
				Sel[i] = listView1.SelectedItems[i];
			}

            string file = selectdir + @"\" + Sel[Sel.Length - 1].Text;
            string path = @"C:\OSKurs";

            MessageBox.Show((string)Sel[Sel.Length - 1].Text);

           
            CopyFolder(file, path + @"\" + ItemDrag.Text + @"\" + Sel[Sel.Length - 1].Text);
            Logirovanie(MyLogs, "Файл: " + file + " перемещен в: " + path + @"\" + selectfile);
            Directory.Delete(file, true);
            MessageBox.Show("Папка успешно перемещена");
            update(selectdir);


        }

		private void check()//создание изначальных директорий и файлов настройка
        {
            string firstPath = $@"{disk}OSKurs";
            DirectoryInfo dirInfo = new DirectoryInfo(firstPath);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();

                string logs = $@"Логи";
                string Documents = $@"Документы";
                string system = $@"System";
                string backet = $@"Корзина";

                dirInfo.CreateSubdirectory(Documents);
                dirInfo.CreateSubdirectory(system);
                dirInfo.CreateSubdirectory(backet);
                dirInfo.CreateSubdirectory(logs);
            }

            systemPath = $@"{disk}\OSKurs\System";
            DirectoryInfo systemInfo = new DirectoryInfo(systemPath);
            DirectorySecurity systemSecurity = systemInfo.GetAccessControl();
            string Account = WindowsIdentity.GetCurrent().Name;
            systemSecurity.AddAccessRule(new FileSystemAccessRule(Account, FileSystemRights.Traverse, AccessControlType.Allow));
            systemSecurity.AddAccessRule(new FileSystemAccessRule(Account, FileSystemRights.Write, AccessControlType.Deny));

            systemInfo.SetAccessControl(systemSecurity);

            //logFileSystem = $@"{disk}\OSKurs\Логи\{DateTime.Today.ToShortDateString()}_FileSystemLogs.txt";
            MyLogs = $@"{disk}\OSKurs\Логи\";
            logProcess = $@"{disk}OSKurs\Логи\";
           // logTMP = $@"{disk}OSKurs\Логи\Logs.txt";
           // logProcessClose = $@"{disk}OSKurs\Логи\{DateTime.Today.ToShortDateString()}_ProcessLogsClose.txt";
            Documentsfile = $@"{disk}\OSKurs\Документы\doc.txt";

            if (!File.Exists(Documentsfile))
            {
                try
                {
                    using (File.Create(Documentsfile))
                    {

                    }
                }
                catch (Exception ex)
                {
                    
                }
            }


            homePage = $@"{disk}\OSKurs";
            backet = $@"{disk}\OSKurs\Корзина";
        }

        public void FillDrive()//Заполнение съемного устройства USB
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

        

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)       // Ctrl-N создать новую папку
            {
                CrFolder();
            }

            if (e.Control && e.KeyCode == Keys.Back)       // Клавиша назад
            {
                if (selectdir != homePage & selectdir != disk)
                {
                    selectdir = backDir(selectdir);
                    update(selectdir);
                }
                else
                {
                    MessageBox.Show("Назад уже нельзя ☻");
                }
            }


            if (e.Control && e.KeyCode == Keys.I)
            {
                MessageBox.Show("Курсовая работа: Операционные системы и Оболочки\nВыполнил: студент группы МОИС-01 Богрянцев\nЯзык: C#");
            }

            if (e.Control && e.KeyCode == Keys.U)
            {
                using (Process perfmon = new Process())
                {
                    perfmon.StartInfo.FileName = "perfmon.msc";
                    perfmon.Start();
                    LogProcessOn(perfmon.StartTime, perfmon.ProcessName, perfmon.StartInfo.FileName, Convert.ToString(perfmon.Id));
                    LogTMP(perfmon.ProcessName, perfmon.StartInfo.FileName, Convert.ToString(perfmon.Id));
                }
            }

            if (e.Control && e.KeyCode == Keys.D)
            {
                try
                {
                    using (Process diskmgmt = new Process())
                    {
                        diskmgmt.StartInfo.FileName = "diskmgmt.msc";
                        diskmgmt.Start();
                        LogProcessOn(diskmgmt.StartTime, diskmgmt.ProcessName, diskmgmt.StartInfo.FileName, Convert.ToString(diskmgmt.Id));
                        LogTMP(diskmgmt.ProcessName, diskmgmt.StartInfo.FileName, Convert.ToString(diskmgmt.Id));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex));
                }
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

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)//Отрисовка дерева папок
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
                            if (dirs[i].StartsWith($@"{disk}") & checkDir(dirs[i]) == "OSKurs")
                            {
                                TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                                FillTreeDir(dirNode, dirs[i]);
                                e.Node.Nodes.Add(dirNode);
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
        }

        public string SystemDir(string dir)
        {
            string cDir = dir;
            string cSdir = "";
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
                        cSdir = cDir.Substring(a + 1);
                        cDir = cDir.Substring(0, a);
                        break;
                    }
                }

                for (a = 0; a < cSdir.Length; a++)
                {
                    if (cSdir[a] == Convert.ToChar($@"\"))
                    {
                        cSdir = cSdir.Substring(0, a);
                        break;
                    }
                }
                cDir = cDir + $@"\" + cSdir;
                return cDir;
            }
            else
            {
                return dir;
            }
        }

        public string backDir(string dir)//назад в предыдущую директорию
        {
            string lDir = dir;
            int a = dir.Length - 1;
            if (dir != disk)
            {
                while (dir[a] != Convert.ToChar($@"\"))
                {
                    a--;
                }
                lDir = lDir.Substring(0, a);
                return lDir;
            }
            else
            {
                return dir;
            }
        }

        private void toStart()
        {
            if (Directory.Exists(homePage))
            {
                update(homePage);
            }
        }

        private void update(string path)//обновление при изменении директории
        {
            try
            {
                string[] files = Directory.GetFiles(path);
                listView1.Clear();
                foreach (string file in files)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = file.Remove(0, file.LastIndexOf('\\') + 1);
                    lvi.ImageIndex = 0;
                    listView1.Items.Add(lvi);
                }

                string[] files2 = Directory.GetDirectories(path);

                foreach (string file in files2)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = file.Remove(0, file.LastIndexOf('\\') + 1);
                    lvi.ImageIndex = 1;
                    listView1.Items.Add(lvi);
                }
                selectdir = path;
                textBox1.Text = path;
                textBox2.Text = selectdir;
                selectfile = null;
                textBox3.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }

        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (checkDir(e.Node.FullPath) == "OSKurs")
            {
                textBox1.Text = e.Node.FullPath;
                selectdir = e.Node.FullPath;
                update(e.Node.FullPath);
            }
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
           // LogProcessClose();
            File.Delete(logTMP);
           // Application.Exit();
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            zakr = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)//выбор файла или папки в listView
        {
            selectfile = Convert.ToString(e.Item.Text);
            textBox3.Text = selectfile;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)//открытие директории или файла
        {
            string path = selectdir + @"\" + selectfile;

            if (Directory.Exists(path))
            {
                try
                {
                    update(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex));
                }
            }
            else
            {
                try
                {
                    using (Process myProcess = new Process())
                    {
                        myProcess.StartInfo.FileName = path;
                        myProcess.Start();
                        LogProcessOn(myProcess.StartTime, myProcess.ProcessName, myProcess.StartInfo.FileName, Convert.ToString(myProcess.Id));
                        LogTMP(myProcess.ProcessName, myProcess.StartInfo.FileName, Convert.ToString(myProcess.Id));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex));
                }

            }
        }



        private void button7_Click(object sender, EventArgs e)//кнопка назад
        {
            if (selectdir != homePage & selectdir != disk)
            {
                selectdir = backDir(selectdir);
                update(selectdir);
            }
            else
            {
                MessageBox.Show("Назад уже нельзя ☻");
            }
        }

        private void button3_Click(object sender, EventArgs e)//кнопка удалить
        {
            string del = selectdir + @"\" + selectfile;
            try
            {
                if (SystemDir(del) != $@"OSKurs\System" & del != backet & del != $@"{disk}\OSKurs\Логи")
                {
                    if (selectdir != backet)
                    {
                        if (Directory.Exists(del) & selectfile != null)
                        {
                            if (Directory.Exists(backet + @"\" + selectfile))
                            {
                                Directory.Delete(backet + @"\" + selectfile, true);
                                Logirovanie(MyLogs, "Файл: " + del + " удален ");
                                Directory.Move(del, backet + @"\" + selectfile);
                                Logirovanie(MyLogs, "Файл: " + del + " удален ");
                                if (Directory.Exists(selectdir))
                                {
                                    update(selectdir);
                                    treeView1.Nodes.Clear();
                                    FillDrive();
                                }
                            }
                            else
                            {
                                Directory.Move(del, backet + @"\" + selectfile);
                                Logirovanie(MyLogs, "Файл: " + del + " удален ");
                                if (Directory.Exists(selectdir))
                                {
                                    update(selectdir);
                                    treeView1.Nodes.Clear();
                                    FillDrive();
                                }
                            }
                        }
                        else if (File.Exists(del) & selectfile != null)
                        {
                            if (File.Exists(backet + @"\" + selectfile))
                            {
                                File.Delete(backet + @"\" + selectfile);
                                Logirovanie(MyLogs, "Файл: " + del + " удален ");
                                File.Move(del, backet + @"\" + selectfile);
                                Logirovanie(MyLogs, "Файл: " + del + " удален ");
                                if (Directory.Exists(selectdir))
                                {
                                    update(selectdir);
                                }
                            }
                            else
                            {
                                File.Move(del, backet + @"\" + selectfile);
                                Logirovanie(MyLogs, "Файл: " + del + " удален ");
                                if (Directory.Exists(selectdir))
                                {
                                    update(selectdir);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("По данному пути ничего не найдено");
                        }
                    }
                    else
                    {
                        if (Directory.Exists(del) & selectfile != null)
                        {
                            Directory.Delete(del, true);
                            Logirovanie(MyLogs, "Файл: " + del + " удален ");
                            update(selectdir);
                            treeView1.Nodes.Clear();
                            FillDrive();
                        }
                        else if (File.Exists(del) & selectfile != null)
                        {
                            File.Delete(del);
                            Logirovanie(MyLogs, "Файл: " + del + " удален ");
                            update(selectdir);
                        }
                        else
                        {
                            MessageBox.Show("По данному пути ничего не найдено");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя удалять системные папки и файлы");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }

        private void button5_Click(object sender, EventArgs e) //кнопка переместить
        {
            Form3 copyDialog = new Form3(this);
            string file = selectdir + @"\" + selectfile;
            if (SystemDir(file) != $@"OSKurs\System" & file != backet & file != $@"{disk}\OSKurs\Логи")
            {
                if (File.Exists(file))
                {
                    if (copyDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        try
                        {
                            string path = copyDialog.textBox1.Text;
                            if (path != $"{disk}" & SystemDir(path) != $@"OSKurs\System")
                            {
                                if (!File.Exists(path + @"\" + selectfile))
                                {
                                    File.Move(file, path + @"\" + selectfile);
                                    MessageBox.Show("Файл успешно перемещён");
                                    update(selectdir);
                                    Logirovanie(MyLogs, "Файл: " + file + " перемещен в: "+ path + @"\" + selectfile);

                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show("По данному пути уже существует файл с таким названием. Вы уверены, что хотите его заменить?", "Перемещение", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        File.Delete(path + @"\" + selectfile);
                                        File.Move(file, path + @"\" + selectfile);
                                        Logirovanie(MyLogs, "Файл: " + file + " перемещен в: " + path + @"\" + selectfile);
                                        MessageBox.Show("Файл успешно перемещён");
                                        update(selectdir);

                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        MessageBox.Show("Операция была отменена");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Перемещать в данную директорию запрещено");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Convert.ToString(ex));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Операция была отменена");
                    }
                    copyDialog.Dispose();
                }
                else if (Directory.Exists(file))
                {
                    if (copyDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        try
                        {
                            string path = copyDialog.textBox1.Text;
                            if (path != $"{disk}" & SystemDir(path) != $@"OSKurs\System")
                            {
                                if (!Directory.Exists(path + @"\" + selectfile))
                                {
                                    try
                                    {
                                        CopyFolder(file, path + @"\" + selectfile);
                                        Logirovanie(MyLogs, "Файл: " + file + " перемещен в: " + path + @"\" + selectfile);
                                        Directory.Delete(file, true);
                                        MessageBox.Show("Папка успешно перемещена");
                                        update(selectdir);

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(Convert.ToString(ex));
                                    }
                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show("По данному пути уже существует папка с таким названием. Вы уверены, что хотите её заменить?", "Перемещение", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        try
                                        {
                                            Directory.Delete(path + @"\" + selectfile, true);
                                            CopyFolder(file, path + @"\" + selectfile);
                                            Logirovanie(MyLogs, "Файл: " + file + " перемещен в: " + path + @"\" + selectfile);
                                            Directory.Delete(file, true);
                                            MessageBox.Show("Папка успешно перемещена");
                                            update(selectdir);

                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(Convert.ToString(ex));
                                        }
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        MessageBox.Show("Операция была отменена");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Перемещать в данную директорию запрещено");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Convert.ToString(ex));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите файл для перемещения");
                }
            }
            else
            {
                MessageBox.Show("Нельзя перемещать системные папки и файлы");
            }
        }

        public void CrFolder()// Функция Создания папки
        {
            Form4 folderName = new Form4(this);
            if (SystemDir(selectdir) != $@"OSKurs\System")
            {
                if (folderName.ShowDialog(this) == DialogResult.OK)
                {
                    string name = folderName.textBox1.Text;
                    if (name != "")
                    {
                        if (!Directory.Exists(selectdir + @"\" + name))
                        {
                            try
                            {
                                Directory.CreateDirectory(selectdir + @"\" + name);                        
                                Logirovanie(MyLogs, "Создана папка: " + selectdir + @"\" + name);
                                MessageBox.Show("Директория успешно создана");
                                treeView1.Nodes.Clear();
                                FillDrive();
                                update(selectdir);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(Convert.ToString(ex));
                            }

                        }
                        else
                        {
                            MessageBox.Show("Такая директория уже существует или в начале используются недопустимые символы");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Укажите имя директории");
                    }
                }
                else
                {
                    MessageBox.Show("Операция была отменена");
                }
            }
            else
            {
                MessageBox.Show("Нельзя создать папку в системной папке");
            }
        }
      
        public void CopyFolder(string sourceFolder, string destFolder)//Копирование папки
        {
            string name;
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);

            }
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
                Logirovanie(MyLogs, "Скопировано: " + name + "Из: " + sourceFolder + " Куда: " + destFolder);

            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
                Logirovanie(MyLogs, "Скопировано: " + name + "Из: " + sourceFolder + " Куда: " + destFolder);
            }
            
            
        }
        public void CrFile()// Создание файла
        {
            Form4 folderName = new Form4(this);
            if (SystemDir(selectdir) != $@"OSKurs\System")
            {
                if (folderName.ShowDialog(this) == DialogResult.OK)
                {
                    string name = folderName.textBox1.Text;
                    if (name != "")
                    {
                        if (!Directory.Exists(selectdir + @"\" + name))
                        {
                            try
                            {
                                File.Create(selectdir + @"\" + name);
                                Logirovanie(MyLogs, "Создан файл: " + name + " В директории: " + selectdir);
                                MessageBox.Show("Файл успешно создан");
                                treeView1.Nodes.Clear();
                                FillDrive();
                                update(selectdir);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(Convert.ToString(ex));
                            }

                        }
                        else
                        {
                            MessageBox.Show("Уже существует или в начале используются недопустимые символы");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Укажите имя");
                    }
                }
                else
                {
                    MessageBox.Show("Операция была отменена");
                }
            }
            else
            {
                MessageBox.Show("Нельзя создать файл в системной папке");
            }
        }

        public void ReFile()
        {
            Form4 folderName = new Form4(this);
            if (selectdir != $@"OSKurs\System" && selectfile != "System" && selectfile != "Корзина" && selectfile != "Документы" && selectfile != "Логи")
            {
                if (folderName.ShowDialog(this) == DialogResult.OK)
                {
                    string oldName = selectfile;
                    string name = folderName.textBox1.Text;
                    if (name != "")
                    {
                        if (!Directory.Exists(selectdir + @"\" + name))
                        {
                            try
                            {
                                FileSystem.Rename(selectdir + @"\" + selectfile, selectdir + @"\" + name);
                                Logirovanie(MyLogs, "Файл: "  + oldName + " Переименован в: " + name);
                                MessageBox.Show("Файл успешно переименован");
                                treeView1.Nodes.Clear();
                                FillDrive();
                                update(selectdir);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(Convert.ToString(ex));
                            }

                        }
                        else
                        {
                            MessageBox.Show("Уже существует или в начале используются недопустимые символы");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Укажите имя");
                    }
                }
                else
                {
                    MessageBox.Show("Операция была отменена");
                }
            }
            else
            {
                MessageBox.Show("Нельзя переименовать системный объект");
            }
        }

        public void CopyAll() {
            Form3 copyDialog = new Form3(this);
            string file = selectdir + @"\" + selectfile;
            if (SystemDir(file) != $@"OSKurs\System" & file != backet & file != $@"{disk}\OSKurs\Логи")
            {
                if (File.Exists(file))
                {
                    if (copyDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        try
                        {
                            string path = copyDialog.textBox1.Text;
                            if (path != $"{disk}" & SystemDir(path) != $@"OSKurs\System")
                            {
                                if (!File.Exists(path + @"\" + selectfile))
                                {
                                    File.Copy(file, path + @"\" + selectfile, true);
                                    Logirovanie(MyLogs, "Скопировано: " + selectfile + " В директорию: " + path);
                                    MessageBox.Show("Файл успешно скопирован");
                                    update(selectdir);

                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show("По данному пути уже существует файл с таким названием. Вы уверены, что хотите его заменить?", "Копирование", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        File.Copy(file, path + @"\" + selectfile, true);
                                        Logirovanie(MyLogs, "Скопировано: " + selectfile + " В директорию: " + path);
                                        MessageBox.Show("Файл успешно скопирован");
                                        update(selectdir);

                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        MessageBox.Show("Операция была отменена");
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("Копировать в данную директорию запрещено");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Convert.ToString(ex));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Операция была отменена");
                    }
                    copyDialog.Dispose();
                }
                else if (Directory.Exists(file))
                {
                    if (copyDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        try
                        {
                            string path = copyDialog.textBox1.Text;
                            if (path != $"{disk}" & SystemDir(path) != $@"OSKurs\System")
                            {
                                if (!Directory.Exists(path + @"\" + selectfile))
                                {
                                    try
                                    {
                                        CopyFolder(file, path + @"\" + selectfile);
                                        Logirovanie(MyLogs, "Скопировано: " + selectfile + " В директорию: " + path);
                                        MessageBox.Show("Папка успешно скопирована");
                                        update(selectdir);

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(Convert.ToString(ex));
                                    }
                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show("По данному пути уже существует папка с таким названием. Вы уверены, что хотите её заменить?", "Копирование", MessageBoxButtons.YesNo);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        try
                                        {
                                            Directory.Delete(path + @"\" + selectfile, true);
                                           
                                            CopyFolder(file, path + @"\" + selectfile);
                                            Logirovanie(MyLogs, "Скопировано: " + selectfile + " В директорию: " + path);
                                            MessageBox.Show("Папка успешно скопирована");
                                            update(selectdir);

                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(Convert.ToString(ex));
                                        }
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        MessageBox.Show("Операция была отменена");
                                    }

                                }
                            }
                            else
                            {
                                MessageBox.Show("Копировать в данную директорию запрещено");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Convert.ToString(ex));
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите файл для копирования");
                }
            }
            else
            {
                MessageBox.Show("Нельзя копировать системные файлы и папки");
            }
        }

        //Логирование логи

        public void Logirovanie(string pathToFile, string text)
        {
            Form4 folderName = new Form4(this);
            folderName.Text = "";
            try
            {
                if (folderName.ShowDialog(this) == DialogResult.OK)
                {
                    text += "  Дата: " + DateTime.Now;
                    string name = folderName.textBox1.Text;
                    string path = pathToFile + name;
                    if (name != "")
                    {
                        if (!Directory.Exists(selectdir + @"\" + name))
                        {
                            try
                            {
                                StreamWriter output;
                                if (!File.Exists(path))
                                {
                                    File.Create(path).Close();

                                    output = new StreamWriter(path);
                                }
                                else
                                {
                                    output = File.AppendText(path);
                                }

                                output.WriteLine(text);

                                output.Close();

                                MessageBox.Show("Лог успешно сохранен");
                                treeView1.Nodes.Clear();
                                FillDrive();
                                update(selectdir);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(Convert.ToString(ex));
                            }

                        }
                        else
                        {
                            MessageBox.Show("Уже существует или в начале используются недопустимые символы");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Укажите имя");
                    }
                }
                else
                {
                    MessageBox.Show("Операция была отменена");
                }
            }
            catch { MessageBox.Show("Все закрыто)"); }
        }






        public void LogProcessOn(DateTime timeOn, string process, string adress, string id)
        {
            Form4 folderName = new Form4(this);
            folderName.Text = "";


            try
            {
                if (folderName.ShowDialog(this) == DialogResult.OK)// сюда
                {
                    string name = folderName.textBox1.Text;
                    using (StreamWriter sw = new StreamWriter(logProcess + name, true, System.Text.Encoding.Default))
                    {
                        sw.WriteLine($"Время события: {timeOn}\nПроцесс: {process} был запущен\nАдрес: {adress}\nID: {id}\n");
                    }
                }
                else
                {
                    MessageBox.Show("Операция была отменена");
                }
                }
            catch (Exception ex)
            {
                
            }
        }

        public void LogTMP(string process, string adress, string id)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(logTMP, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine($"{id},{process},{adress}");
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void LogProcessClose()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(logProcessClose, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine($"Время заверешния работы программы: {DateTime.Now}\nЗавершенные процессы:");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
            string[] procID = FindID();

            bool close = true;
            foreach (string id in procID)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (id == Convert.ToString(process.Id))
                    {
                        close = false;
                        break;
                    }
                    else
                    {
                        close = true;
                    }
                }

                if (close == true)
                {
                    string closeID, adress, name;
                    using (StreamReader sr = new StreamReader(logTMP, System.Text.Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            int b = 0;
                            while (line[b] != Convert.ToChar($@"$"))
                            {
                                b++;
                            }
                            closeID = line.Substring(0, b);
                            if (closeID == id)
                            {

                                line = line.Substring(b + 1);
                                b = 0;
                                while (line[b] != Convert.ToChar($@"$"))
                                {
                                    b++;
                                }
                                name = line.Substring(0, b);
                                adress = line.Substring(b + 1);
                                using (StreamWriter sw = new StreamWriter(logProcessClose, true, System.Text.Encoding.Default))
                                {
                                    sw.WriteLine($"Процесс: {name}\nАдрес: {adress}\nID: {id}\n");
                                }
                            }
                        }
                    }
                }
            }
        }
        //

        public string[] FindID()
        {
            int a = 0;
            string[] ID;


            using (StreamReader sr = new StreamReader(logTMP, System.Text.Encoding.Default))
            {

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    a++;
                }
            }

            ID = new string[a];
            a = 0;

            using (StreamReader sr = new StreamReader(logTMP, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int b = 0;
                    while (line[b] != Convert.ToChar($@"$"))
                    {
                        b++;
                    }
                    ID[a] = line.Substring(0, b);
                    a++;
                }
            }

            return ID;
        }

        void cms_Opening1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (selectdir == "" | selectdir == null)
            {
                toolStripMenuItem1.Enabled = false;
                toolStripMenuItem2.Enabled = false;
                toolStripMenuItem3.Enabled = false;
                toolStripMenuItem4.Enabled = false;
                toolStripMenuItem5.Enabled = false;
                //toolStripMenuItem6.Enabled = false;

            }
            else if (selectfile == "" | selectfile == null)
            {
                toolStripMenuItem1.Enabled = false;
                toolStripMenuItem2.Enabled = false;
                toolStripMenuItem3.Enabled = false;
                toolStripMenuItem4.Enabled = false;
                toolStripMenuItem5.Enabled = true;
            }
            else
            {
                toolStripMenuItem1.Enabled = true;
                toolStripMenuItem2.Enabled = true;
                toolStripMenuItem3.Enabled = true;
                toolStripMenuItem4.Enabled = true;
                toolStripMenuItem5.Enabled = true;
            }
        }

        //Меню ПКМ
        private void toolStripMenuItem1_Click(object sender, EventArgs e) //открыть
        {
            listView1_DoubleClick(sender, e);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)//переместить
        {
            button5_Click(sender, e);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)//копировать
        {
            CopyAll();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)//удалить
        {
            button3_Click(sender, e);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)//создать папку
        {
            CrFolder();
        }
        private void toolStripMenuItem6_Click(object sender, EventArgs e)//создать папку
        {
            CrFolder();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        static List<Process> GetChildPrecesses(int parentId)//Вывод потоков
        {
            var query = "Select * From Win32_Process Where ParentProcessId = " + parentId;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            var result = processList.Cast<ManagementObject>().Select(p => Process.GetProcessById(Convert.ToInt32(p.GetPropertyValue("ProcessId")))).ToList();

            return result;
        }

        

        private void button10_Click(object sender, EventArgs e)//Доп. задания
        {
            textBox7.Text = null;
            string cbox = comboBox1.Text;

            //Запускаем второе окно, для передачи информации через сокет
            Form5 s = new Form5(this);
            s.Show();
            Server serverOne = new Server();

            Thread A;
            string msg = "ComputerName: " + textBox7.Text;

            A = new Thread(() =>
            {
                serverOne.ServerMy(msg, cbox, s, this);
            });

            
          
            A.Start();
           

            if (zakr == 1)
            {
                A.Abort();
            }
        }

        

        public class Server// 
        {
            public void ServerMy(string otpravka, string cbox, Form5 s, Form2 haha)
            {
                TcpClient clientSocket = new TcpClient();
                TcpListener serverSocket = new TcpListener(System.Net.IPAddress.Any, 7000);



                using (var pc = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
                {
                    Console.WriteLine(pc.NextValue());
                    Thread.Sleep(1000);



                    try
                    {
                        serverSocket = new TcpListener(System.Net.IPAddress.Any, 7000);

                        serverSocket.Start();
                        Debug.Write("Сервер запущен!");
                        while (true)
                        {
                            Process[] proc;
                            List<Process> PotProc = new List<Process>();
                            proc = Process.GetProcessesByName(cbox);
                            Process p = proc[0];
                            long AffinityMask = 0x0002;
                            Random rnd = new Random();
                            
                            p.ProcessorAffinity = (IntPtr)AffinityMask;
                            int cpuCount = Environment.ProcessorCount;
                            int parentID;
                            int i = 0;

                            foreach (Process process in Process.GetProcesses())
                            {
                                int r = rnd.Next(0, 2);
                                if ( r== 0)
                                {
                                    AffinityMask = 0x0001;
                                    p.ProcessorAffinity = (IntPtr)AffinityMask;

                                }
                                else
                                {
                                    AffinityMask = 0x0002;
                                    p.ProcessorAffinity = (IntPtr)AffinityMask;
                                }
                                if (process.ProcessName == cbox)
                                {
                                    if (i == 0)
                                    {
                                        string info = ($"ID: {process.Id}  Name: {process.ProcessName} Priority: {process.BasePriority}");// выводим id и имя процесса
                                        string computerName = Environment.MachineName;//Имя компьютера
                                        haha.textBox7.Invoke(new Action(() => haha.textBox7.Text = null));
                                        haha.textBox7.Invoke(new Action(() => haha.textBox7.Text += "ComputerName: " + computerName + Environment.NewLine + "Functional CPU: " + Math.Ceiling(pc.NextValue()) + "%" + Environment.NewLine + "Process Count: " + cpuCount + Environment.NewLine +"Process is conneting to " + p.ProcessorAffinity + "core " + Environment.NewLine + info + Environment.NewLine));
                                        i++;
                                    }

                                    parentID = process.Id;

                                    PotProc = GetChildPrecesses(parentID);

                                    foreach (var sign in PotProc)
                                    {
                                        haha.textBox7.Invoke(new Action(() => haha.textBox7.Text += sign));
                                    }
                                }
                            }
                            // s.textBox1.Invoke(new Action(() => haha.Logirovanie(MyLogs, otpravka)));
                            otpravka = haha.textBox7.Text;

                            clientSocket = serverSocket.AcceptTcpClient();
                            NetworkStream stream = clientSocket.GetStream();
                            byte[] bytes = Encoding.ASCII.GetBytes(otpravka);
                            stream.Read(bytes, 0, bytes.Length);
                            string request = Encoding.ASCII.GetString(bytes);
                            s.textBox1.Invoke(new Action(() => s.textBox1.Text += request));
                            clientSocket.Close();

                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception oEX)
                    {
                        Debug.WriteLine(oEX.Message);
                        MessageBox.Show("Server Закрыт");
                        clientSocket.Close();
                        serverSocket.Stop();
                    }
                }
            }
        }

       



        private void ProcessList()
        {
            //Получаем массив типа System.Diagnostics.Process,
            //предоставляющий данные обо всех процессах,
            //выполняющегося на локальном компьютере.
            Process[] processlist = Process.GetProcesses();
            //Выполняем поиск всех процессов в полученном массиве
            //и добавляем их имена в элемент управления comboBox1.
            foreach (Process theprocess in processlist)
            {
                //Добавляем имя текущего процесса
                //в элемент управления comboBox1.
                comboBox1.Items.Add(theprocess.ProcessName);
            }
        }
        //Открытие дополнительных ыприложений приложения утилиты
        private void MonitResourse()
        {
            
        }

        public Form3 Form3
        {
            get => default;
            set
            {
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

        private void Form2_Load(object sender, EventArgs e)
        {
            ProcessList();
            this.Text = "Файловый менеджер";
        }

        private void OpenFolder()
        {
            string path, path2;

            path = textBox1.Text;
            path2 = path;

            listView1.Clear();
            if (textBox1.Text != "C:\\OSKurs\\System")
            {
                string[] files = Directory.GetFiles(path);

                foreach (string file in files)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = file.Remove(0, file.LastIndexOf('\\') + 1);
                    lvi.ImageIndex = 0;
                    listView1.Items.Add(lvi);
                }
                string[] files2 = Directory.GetDirectories(path2);

                foreach (string file in files2)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = file.Remove(0, file.LastIndexOf('\\') + 1);
                    lvi.ImageIndex = 1;
                    listView1.Items.Add(lvi);
                }
            }
        }

        //ОТКЛЮЧЕНИЕ ЮСБ УСТРОЙСТВ!!!!!
        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int CloseHandle(IntPtr handle);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern int DeviceIoControl(IntPtr deviceHandle, uint ioControlCode, IntPtr inBuffer, int inBufferSize, IntPtr outBuffer, int outBufferSize, ref int bytesReturned, IntPtr overlapped);

        [System.Runtime.InteropServices.DllImport("kernel32")]
        private static extern IntPtr CreateFile(string filename, uint desiredAccess,uint shareMode, IntPtr securityAttributes,int creationDisposition, int flagsAndAttributes,IntPtr templateFile);

        string diskName = string.Empty;
        List<string> kvp;
        private void UsbDiskList()
        {
            //string diskName = string.Empty;
            kvp = new List<string>();
            //предварительно очищаем список
            comboBox1.Items.Clear();

            //Получение списка накопителей подключенных через интерфейс USB
            foreach (ManagementObject drive in new ManagementObjectSearcher("select * from Win32_DiskDrive where InterfaceType='USB'").Get())
            {
                //Получаем букву накопителя
                foreach (ManagementObject partition in
                   new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + drive["DeviceID"] + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                {
                    foreach (System.Management.ManagementObject disk in
                       new System.Management.ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + partition["DeviceID"] + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                    {
                        //Получение буквы устройства
                        diskName = disk["Name"].ToString().Trim();
                        comboBox1.Items.Add(diskName + " (" + drive["Model"] + ")");



                        kvp.Add(diskName);
                        //richTextBox1.Text += diskName;
                    }
                }
            }
        }

        //	Метод для извлечения USB накопителя.
        static void EjectDrive(string driveLetter)
        {
            string path = "\\\\.\\" + driveLetter;

            IntPtr handle = CreateFile(path, GENERIC_READ | GENERIC_WRITE, 0, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);

            if ((long)handle == -1)
            {
                MessageBox.Show("Невозможно извлечь USB устройство!", "Извлечение USB накопителей", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int dummy = 0;
            DeviceIoControl(handle, IOCTL_STORAGE_EJECT_MEDIA, IntPtr.Zero, 0, IntPtr.Zero, 0, ref dummy, IntPtr.Zero);
            int returnValue = DeviceIoControl(handle, IOCTL_STORAGE_EJECT_MEDIA, IntPtr.Zero, 0, IntPtr.Zero, 0, ref dummy, IntPtr.Zero);
            CloseHandle(handle);
            MessageBox.Show("USB устройство, успешно извлечено!", "Извлечение USB накопителей", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //	Загрузка букв USB накопителей при запуске программы
        private void LoadInfo()
        {
            //Загрузка букв USB накопителей при запуске программы
            UsbDiskList();
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)//Создать файл
        {
            CrFile();
            //textBox7.Text += "Создан файл";
        }

        private void создатьToolStripMenuItem1_Click(object sender, EventArgs e)//Создать папку
        {
           // textBox7.Text += "Создана пака";
            CrFolder();
        }

        private void информацияОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Курсовая работа: Операционные системы и Оболочки\nВыполнил: студент группы МОИС-01 Богрянцев\nЯзык: C#");
        }

        private void мониторРесурсовToolStripMenuItem_Click(object sender, EventArgs e)// Вызов утилиты Монитор ресурсов
        {
            using (Process perfmon = new Process())
            {
                perfmon.StartInfo.FileName = "perfmon.msc";
                perfmon.Start();
                LogProcessOn(perfmon.StartTime, perfmon.ProcessName, perfmon.StartInfo.FileName, Convert.ToString(perfmon.Id));
                LogTMP(perfmon.ProcessName, perfmon.StartInfo.FileName, Convert.ToString(perfmon.Id));
            }
        }

        private void cmdToolStripMenuItem_Click(object sender, EventArgs e){
            ProcessStartInfo info = new ProcessStartInfo(@"C:\Users\smerc\OneDrive\Рабочий стол\курсач ОС\Курсач\OSKurs-проект\System\KusKus\WindowsFormsApp1\cmd\Osa3.exe");
            Process cmd = new Process();
            cmd.StartInfo = info;
            cmd.Start();
		}

        private void информацияОДискахToolStripMenuItem_Click(object sender, EventArgs e)// Вызов утилиты Информация о Дисках
        {
            try
            {
                using (Process diskmgmt = new Process())
                {
                    diskmgmt.StartInfo.FileName = "diskmgmt.msc";
                    diskmgmt.Start();
                    LogProcessOn(diskmgmt.StartTime, diskmgmt.ProcessName, diskmgmt.StartInfo.FileName, Convert.ToString(diskmgmt.Id));
                    LogTMP(diskmgmt.ProcessName, diskmgmt.StartInfo.FileName, Convert.ToString(diskmgmt.Id));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }

        private void оСистемеToolStripMenuItem_Click(object sender, EventArgs e)// Утилита О системе Вызовы
        {
            try
            {
                using (Process msinfo32 = new Process())
                {
                    msinfo32.StartInfo.FileName = "msinfo32.exe";
                    msinfo32.Start();
                    LogProcessOn(msinfo32.StartTime, msinfo32.ProcessName, msinfo32.StartInfo.FileName, Convert.ToString(msinfo32.Id));
                    LogTMP(msinfo32.ProcessName, msinfo32.StartInfo.FileName, Convert.ToString(msinfo32.Id));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }

        private void сохранениеПротоколаToolStripMenuItem_Click(object sender, EventArgs e)// вывод процесса запущенного
        {
            foreach (Process process in Process.GetProcesses())
            {
                // выводим id и имя процесса
                Console.WriteLine($"ID: {process.Id}  Name: {process.ProcessName}");
            }
        }

        private void переключитьсяНаUSBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.Removable)
                {

                    string put, put2;

                    put = (string.Format("{0}", drive.Name + "\\"));
                    put2 = put;


                    string[] files = Directory.GetFiles(put);
                    listView1.Clear();
                    foreach (string file in files)
                    {
                        ListViewItem lvi = new ListViewItem();
                        // установка названия файла
                        lvi.Text = file.Remove(0, file.LastIndexOf('\\') + 1);
                        lvi.ImageIndex = 0; // установка картинки для файла
                                            // добавляем элемент в ListView
                        listView1.Items.Add(lvi);
                    }

                    string[] files2 = Directory.GetDirectories(put);
                    // перебор полученных папок
                    foreach (string file in files2)
                    {
                        ListViewItem lvi = new ListViewItem();
                        // установка названия папок
                        lvi.Text = file.Remove(0, file.LastIndexOf('\\') + 1);
                        lvi.ImageIndex = 1; // установка картинки для файла
                                            // добавляем элемент в ListView
                        listView1.Items.Add(lvi);
                    }
                    selectdir = put;
                    textBox1.Text = put;
                    textBox2.Text = selectdir;
                    selectfile = null;
                    textBox3.Clear();

                }


            }
        }

        private void отключитьUSBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadInfo();
            EjectDrive(diskName);
            LoadInfo();
        }

        private void переименоватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReFile();
        }

        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReFile();
        }
    }
}
