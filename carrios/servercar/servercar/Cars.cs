using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace server
{
    class Cars
    {
        //מערך המייצג את הרכבים בתחנה
        int[] car = new int[20] { 0, 0, 0, 0 ,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

        public void RentCars(Socket s)
        {
            int numCar = 0;
            lock (car)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (car[i] == 0)
                    {
                        numCar = i;
                        car[numCar] = 1;
                        break;
                    }
                    else
                        numCar++;
                }
            }
            byte[] msg;
            if(numCar<20)
            {
                numCar += 1;
                msg= Encoding.UTF8.GetBytes("רכב פנוי מספר "+numCar);
            }
            else
            {
                msg= Encoding.UTF8.GetBytes("בתחנה זו נגמרו הרכבים!");
            }
            // Send the data through the socket. 
            int bytesSent = s.Send(msg);

        }
        public void ReturnCar(Socket s, int numCar)
        {
            lock (car)
            {
                car[numCar-1] = 0;
            }
            byte[] msg = Encoding.UTF8.GetBytes(" רכב מספר " + numCar + " הוחזר בהצלחה ");
            // Send the data through the socket. 
            int bytesSent = s.Send(msg);
        }
    }
}
