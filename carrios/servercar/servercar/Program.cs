using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server
{
    class Program
    {
        public static Cars car;
        public static int Main(String[] args)
        {
            StartServer();
            return 0;
        }

        public static void StartServer()
        {
            //מונה את מספר הכניסות למערכת
            int counter = 0;
            car = new Cars();
            // ,זהו מנגנון תקשורת בין 
            //תהליכים גם על מחשבים מבוזרים וגם באותו מחשב
            IPHostEntry host = Dns.GetHostEntry("localhost");//מחשב שרת
            IPAddress ipAddress = host.AddressList[0];//הכתובת הספציפית
            //מספר הפורט שבערוץ זה יתקשרו
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 3000);
            //הגדרת סוקט
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            //מקשר את הסוקט לכתובת הערוץ
            listener.Bind(localEndPoint);
            listener.Listen(10);
            while (true)
            {
                try
                {
                    Console.WriteLine("Waiting for a connection...");
                  //  ממתין לקבלת בקשות התחברות מלקוחות חדשים 
                    Socket handler = listener.Accept();
                    //מספר הבקשות
                    counter++;
                    HandleClient client = new HandleClient();
                    //פונקציה לטיפול בבקשה
                    client.startClient(handler,
                   Convert.ToString(counter), car);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
        }
    }
}
