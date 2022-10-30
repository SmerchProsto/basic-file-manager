using System;
using System.Windows.Forms;
using System.Timers;

namespace WindowsFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.listView1 = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.button7 = new System.Windows.Forms.Button();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.button10 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.файлыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.переименоватьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.папкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.создатьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.переименоватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.утилитыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.мониторРесурсовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.информацияОДискахToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.оСистемеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.информацияОПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.работаСUSBустройствамиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.переключитьсяНаUSBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.подключитьUSbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.отключитьUSBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label3 = new System.Windows.Forms.Label();
			this.cmdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cmdToolStripMenuItem.Click += new System.EventHandler(cmdToolStripMenuItem_Click);
			this.contextMenuStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(12, 160);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(171, 252);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// listView1
			// 
			this.listView1.AllowDrop = true;
			this.listView1.HideSelection = false;
			this.listView1.LargeImageList = this.imageList1;
			this.listView1.Location = new System.Drawing.Point(202, 160);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(441, 252);
			this.listView1.TabIndex = 1;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
			this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "w256h2561380453900FileDocument256x25632.png");
			this.imageList1.Images.SetKeyName(1, "folder.jpg");
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox1.Location = new System.Drawing.Point(202, 123);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(441, 30);
			this.textBox1.TabIndex = 2;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(33, 633);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(8, 9);
			this.textBox2.TabIndex = 6;
			// 
			// textBox3
			// 
			this.textBox3.BackColor = System.Drawing.SystemColors.Control;
			this.textBox3.Location = new System.Drawing.Point(46, 633);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new System.Drawing.Size(8, 9);
			this.textBox3.TabIndex = 7;
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(12, 633);
			this.textBox5.Multiline = true;
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(8, 9);
			this.textBox5.TabIndex = 13;
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(60, 633);
			this.textBox6.Multiline = true;
			this.textBox6.Name = "textBox6";
			this.textBox6.ReadOnly = true;
			this.textBox6.Size = new System.Drawing.Size(8, 9);
			this.textBox6.TabIndex = 14;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(153, 114);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem1.Text = "Открыть";
			this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem2.Text = "Переместить";
			this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem3.Text = "Копировать";
			this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem4.Text = "Удалить";
			this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem5.Text = "Создать папку";
			this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button7.ForeColor = System.Drawing.Color.Blue;
			this.button7.Location = new System.Drawing.Point(12, 106);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(171, 48);
			this.button7.TabIndex = 11;
			this.button7.Text = "←";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(680, 148);
			this.textBox7.Margin = new System.Windows.Forms.Padding(2);
			this.textBox7.Multiline = true;
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(231, 227);
			this.textBox7.TabIndex = 17;
			// 
			// button10
			// 
			this.button10.Location = new System.Drawing.Point(680, 379);
			this.button10.Margin = new System.Windows.Forms.Padding(2);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(231, 30);
			this.button10.TabIndex = 18;
			this.button10.Text = "Задание";
			this.button10.UseVisualStyleBackColor = true;
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(680, 123);
			this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(231, 21);
			this.comboBox1.TabIndex = 19;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
			this.label1.Location = new System.Drawing.Point(679, 36);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(203, 24);
			this.label1.TabIndex = 27;
			this.label1.Text = "Работа с процессами";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(680, 106);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(102, 13);
			this.label2.TabIndex = 28;
			this.label2.Text = "Выберите процесс";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлыToolStripMenuItem,
            this.папкиToolStripMenuItem,
            this.утилитыToolStripMenuItem,
            this.информацияОПрограммеToolStripMenuItem,
            this.работаСUSBустройствамиToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(994, 24);
			this.menuStrip1.TabIndex = 33;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// файлыToolStripMenuItem
			// 
			this.файлыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.переименоватьToolStripMenuItem1});
			this.файлыToolStripMenuItem.Name = "файлыToolStripMenuItem";
			this.файлыToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
			this.файлыToolStripMenuItem.Text = "Файлы";
			// 
			// создатьToolStripMenuItem
			// 
			this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
			this.создатьToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.создатьToolStripMenuItem.Text = "Создать";
			this.создатьToolStripMenuItem.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
			// 
			// переименоватьToolStripMenuItem1
			// 
			this.переименоватьToolStripMenuItem1.Name = "переименоватьToolStripMenuItem1";
			this.переименоватьToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
			this.переименоватьToolStripMenuItem1.Text = "Переименовать";
			this.переименоватьToolStripMenuItem1.Click += new System.EventHandler(this.переименоватьToolStripMenuItem1_Click);
			// 
			// папкиToolStripMenuItem
			// 
			this.папкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem1,
            this.переименоватьToolStripMenuItem});
			this.папкиToolStripMenuItem.Name = "папкиToolStripMenuItem";
			this.папкиToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
			this.папкиToolStripMenuItem.Text = "Папки";
			// 
			// создатьToolStripMenuItem1
			// 
			this.создатьToolStripMenuItem1.Name = "создатьToolStripMenuItem1";
			this.создатьToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
			this.создатьToolStripMenuItem1.Text = "Создать";
			this.создатьToolStripMenuItem1.Click += new System.EventHandler(this.создатьToolStripMenuItem1_Click);
			// 
			// переименоватьToolStripMenuItem
			// 
			this.переименоватьToolStripMenuItem.Name = "переименоватьToolStripMenuItem";
			this.переименоватьToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.переименоватьToolStripMenuItem.Text = "Переименовать";
			this.переименоватьToolStripMenuItem.Click += new System.EventHandler(this.переименоватьToolStripMenuItem_Click);
			// 
			// утилитыToolStripMenuItem
			// 
			this.утилитыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.мониторРесурсовToolStripMenuItem,
            this.информацияОДискахToolStripMenuItem,
            this.оСистемеToolStripMenuItem,
            this.cmdToolStripMenuItem});
			this.утилитыToolStripMenuItem.Name = "утилитыToolStripMenuItem";
			this.утилитыToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
			this.утилитыToolStripMenuItem.Text = "Утилиты";
			// 
			// мониторРесурсовToolStripMenuItem
			// 
			this.мониторРесурсовToolStripMenuItem.Name = "мониторРесурсовToolStripMenuItem";
			this.мониторРесурсовToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.мониторРесурсовToolStripMenuItem.Text = "Монитор ресурсов";
			this.мониторРесурсовToolStripMenuItem.Click += new System.EventHandler(this.мониторРесурсовToolStripMenuItem_Click);
			// 
			// информацияОДискахToolStripMenuItem
			// 
			this.информацияОДискахToolStripMenuItem.Name = "информацияОДискахToolStripMenuItem";
			this.информацияОДискахToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.информацияОДискахToolStripMenuItem.Text = "Информация о дисках";
			this.информацияОДискахToolStripMenuItem.Click += new System.EventHandler(this.информацияОДискахToolStripMenuItem_Click);
			// 
			// оСистемеToolStripMenuItem
			// 
			this.оСистемеToolStripMenuItem.Name = "оСистемеToolStripMenuItem";
			this.оСистемеToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.оСистемеToolStripMenuItem.Text = "О системе";
			this.оСистемеToolStripMenuItem.Click += new System.EventHandler(this.оСистемеToolStripMenuItem_Click);
			// 
			// информацияОПрограммеToolStripMenuItem
			// 
			this.информацияОПрограммеToolStripMenuItem.Name = "информацияОПрограммеToolStripMenuItem";
			this.информацияОПрограммеToolStripMenuItem.Size = new System.Drawing.Size(169, 20);
			this.информацияОПрограммеToolStripMenuItem.Text = "Информация о программе";
			this.информацияОПрограммеToolStripMenuItem.Click += new System.EventHandler(this.информацияОПрограммеToolStripMenuItem_Click);
			// 
			// работаСUSBустройствамиToolStripMenuItem
			// 
			this.работаСUSBустройствамиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.переключитьсяНаUSBToolStripMenuItem,
            this.подключитьUSbToolStripMenuItem,
            this.отключитьUSBToolStripMenuItem});
			this.работаСUSBустройствамиToolStripMenuItem.Name = "работаСUSBустройствамиToolStripMenuItem";
			this.работаСUSBустройствамиToolStripMenuItem.Size = new System.Drawing.Size(172, 20);
			this.работаСUSBустройствамиToolStripMenuItem.Text = "Работа с USB-устройствами";
			// 
			// переключитьсяНаUSBToolStripMenuItem
			// 
			this.переключитьсяНаUSBToolStripMenuItem.Name = "переключитьсяНаUSBToolStripMenuItem";
			this.переключитьсяНаUSBToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.переключитьсяНаUSBToolStripMenuItem.Text = "Переключиться на USB";
			this.переключитьсяНаUSBToolStripMenuItem.Click += new System.EventHandler(this.переключитьсяНаUSBToolStripMenuItem_Click);
			// 
			// подключитьUSbToolStripMenuItem
			// 
			this.подключитьUSbToolStripMenuItem.Name = "подключитьUSbToolStripMenuItem";
			this.подключитьUSbToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.подключитьUSbToolStripMenuItem.Text = "Подключить USB";
			// 
			// отключитьUSBToolStripMenuItem
			// 
			this.отключитьUSBToolStripMenuItem.Name = "отключитьUSBToolStripMenuItem";
			this.отключитьUSBToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.отключитьUSBToolStripMenuItem.Text = "Отключить USB";
			this.отключитьUSBToolStripMenuItem.Click += new System.EventHandler(this.отключитьUSBToolStripMenuItem_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(199, 106);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(74, 13);
			this.label3.TabIndex = 34;
			this.label3.Text = "Путь работы:";
			// 
			// cmdToolStripMenuItem
			// 
			this.cmdToolStripMenuItem.Name = "cmdToolStripMenuItem";
			this.cmdToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
			this.cmdToolStripMenuItem.Text = "cmd";
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(994, 420);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button10);
			this.Controls.Add(this.textBox7);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.treeView1);
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Form2";
			this.Text = "Form2";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form5_FormClosed);
			this.Load += new System.EventHandler(this.Form2_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
			this.contextMenuStrip1.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

		

		#endregion
		private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.ComboBox comboBox1;
        public System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.ListView listView1;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem папкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem переименоватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem утилитыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem мониторРесурсовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem информацияОДискахToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оСистемеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem информацияОПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem работаСUSBустройствамиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переключитьсяНаUSBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem подключитьUSbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отключитьUSBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переименоватьToolStripMenuItem1;
        private System.Windows.Forms.Label label3;
		private ToolStripMenuItem cmdToolStripMenuItem;
	}
}