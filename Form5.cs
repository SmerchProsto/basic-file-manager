using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;

namespace WindowsFormsApp1
{
    public partial class Form5 : Form
    {
        Thread B;
        public Form5(Form2 f2)
        {
            InitializeComponent();

            Client client = new Client();
            B = new Thread(() =>
            {
                Thread.Sleep(10);
                client.ClientPrav(this, f2);
            });

            B.Start();

            this.Text = "Основной функционал";
        }

        class Client
        {
            public void ClientPrav(Form5 s, Form2 haha)
            {
                try
                {
                    while (true)
                    {
                        s.textBox1.Invoke(new Action(() => s.textBox1.Text = null));
                        TcpClient client = new TcpClient("127.0.0.1", 7000);
                        NetworkStream stream = client.GetStream();

                        string request = "Co";
                        byte[] byteWrite = Encoding.ASCII.GetBytes(request);
                        stream.Write(byteWrite, 0, byteWrite.Length);
                        stream.Flush();

                        byte[] byteRead = new byte[256];
                        int Length = stream.Read(byteRead, 0, byteRead.Length);
                        string answer = Encoding.ASCII.GetString(byteRead, 0, Length);

                        s.textBox1.Invoke(new Action(() => s.textBox1.Text += answer));
                        Thread.Sleep(2000);

                        client.Close();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    MessageBox.Show("Client Закрыт");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Form2.zakr == 1)
            {
                B.Abort();
            }

        }


    }
}
 