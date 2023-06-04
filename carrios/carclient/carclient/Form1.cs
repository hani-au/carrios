using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace carclient
{
    public partial class Form1 : Form
    {
        // מחשב שרת
        IPHostEntry host;
        //כתובת שרת
        IPAddress ipAddress;
        //מספר הפורט שבערוץ זה יתקשרו
        IPEndPoint remoteEP;
        Socket socket;
        string numCar;
        byte[] bytes = new byte[1024];
        public Form1()
        {
            InitializeComponent();
            StartClient();
            
        }

        public void StartClient()
        {
            try
            {

                host = Dns.GetHostEntry("localhost");//השרת
                //פעמים בהם יש כמה כתובות מחשב שרת- הכתובת הראשונה
                ipAddress = host.AddressList[0];
                remoteEP = new IPEndPoint(ipAddress, 3000);
                // Create a TCP/IP socket. 
                socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // Connect the socket to the remote endpoint. Catch any errors. 
                try
                {
                    // התחברות לשרת מרוחק
                    socket.Connect(remoteEP);
                    Console.WriteLine("Socket connected to {0}",
                        socket.RemoteEndPoint.ToString());
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}",
                   ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}",
                   e.ToString());
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }




        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //השכרה
        private void button1_Click(object sender, EventArgs e)
        {
            string s = null;
            //המרת בקשת השכרה לביטים
            byte[] msg = Encoding.UTF8.GetBytes("1");
            // שליחת הנתונים באמצעות הסוקט
            int bytesSent = socket.Send(msg);
            // קבלת התגובה מהמכשיר המרוחק
            int bytesRec = socket.Receive(bytes);
            //המרת התגובה למחרוזת 
            s = Encoding.UTF8.GetString(bytes, 0, bytesRec);
            // הצגת התגובה
            MessageBox.Show(s);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        private void label2_Click_1(object sender, EventArgs e)
        {
        }
        //החזרה
        private void button2_Click(object sender, EventArgs e)
        {
            string s = null;
            //מספר הרכב המוחזר
            numCar = textBox1.Text.ToString();
            //המרה לביטים את מספר הרכב המוחזר
            byte[] msg = Encoding.UTF8.GetBytes("0," + numCar);
            // שליחת מספר הרכב בסוקט
            int bytesSent = socket.Send(msg);
            // קבלת התגובה מהשרת 
            int bytesRec = socket.Receive(bytes);
            //המרת התגובה למחרוזת
            s = Encoding.UTF8.GetString(bytes, 0, bytesRec);
            // הצגת תגובה
            MessageBox.Show(s);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}

