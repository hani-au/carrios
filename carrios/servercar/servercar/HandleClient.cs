using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace server
{
    class HandleClient
    {
        Socket clientSocket;

        string clNo;
        Cars car;
        public void startClient(Socket inClientSocket, string clineNo, Cars car)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            this.car = car;
            Thread ctThread = new Thread(go);
            ctThread.Start();

        }
        private void go()
        {
            string data = null;
            byte[] bytes = null;
            bytes = new byte[1024];
            try
            {
                while (true)
                {
                    bytes.Clone();
                    //קבלת הבקשה ממכשיר מרוחק
                    int bytesRec = clientSocket.Receive(bytes);
                    //המרת הבקשה למחרוזת
                    data = Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    //הצבה במערך כל מילה בתא אחר
                    string[] msg = data.Split(",");
                    //במידה ולא התקבלה בקשה
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                    switch (msg[0])
                    {
                        //במקרה של בקשת השכרת רכב
                        case "1":
                            car.RentCars(clientSocket);
                            break;
                        //במקרה של בקשת החזרת רכב
                        case "0":
                            car.ReturnCar(clientSocket,
                           Convert.ToInt32(msg[1]));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" >> " + ex.ToString());
            }
            finally
            {
                //לבסוף- התנתקות מחיבור הלקוח
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }


        }
    }
}
