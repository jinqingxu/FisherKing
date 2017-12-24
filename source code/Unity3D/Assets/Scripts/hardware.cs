using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace Project1
{
   public class hardware
    {   
		private SerialPort com;
		private List<byte>buffer=new List<byte>(4096);
		private float pull = 0;
		private Boolean keepread;
		private Thread readThread;
		private Thread writeThread;
		private Boolean keepwrite;
		private int msize;
		private void ReadPort()
		{
			
			while (keepread == true)
			{
				Thread.Sleep(500);
				String result = com.ReadExisting();

					String[] re = result.Split ('\n');
					pull = float.Parse (re [0]);
					if (pull > 500)
						pull = float.Parse (re [1]);
					

				keepread = false;
			}

		}
		public void connect()
		{
			com = new SerialPort();
			//com.PortName = "COM10";
			com.PortName="/dev/cu.wchusbserial1410";
			com.Open();
			Thread.Sleep(5000);
			/*getPullValue ();
			Thread.Sleep (1000);
			float tmp = pull;*/
		}
		public float getPullValue()
		{
			String data = "111";
			byte[] byteArray = System.Text.Encoding.Default.GetBytes(data);
			com.Write(byteArray, 0, data.Length);
			/*Thread.Sleep(500);
            String result = com.ReadExisting();
            String[] re = result.Split('\n');
            pull = float.Parse(re[0]);
            if (pull > 500) pull = float.Parse(re[1]);*/
			keepread = true;
			readThread = new Thread(ReadPort);
			readThread.Start();
			return pull;


		}
		private void serialPort_DataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				byte[] readBuffer = null;
				int n = com.BytesToRead;
				byte[] buf = new byte[n];
				com.Read(buf, 0, n);
				//1.缓存数据           
				buffer.AddRange(buf);
				//2.完整性判断         
				while (buffer.Count >= 7)
				{
					//至少包含标头(1字节),长度(1字节),校验位(2字节)等等
					//2.1 查找数据标记头            
					if (buffer[0] ==  0x00) //传输数据有帧头，用于判断       
					{
						int len = buffer[1];
						if (buffer.Count < len + 2)
						{
							//数据未接收完整跳出循环
							break;
						}
						readBuffer = new byte[len + 2];
						pull = BitConverter.ToSingle(readBuffer, 0);
						//得到完整的数据，复制到readBuffer中    
						buffer.CopyTo(0, readBuffer, 0, len + 2);

						//从缓冲区中清除
						buffer.RemoveRange(0, len + 2);
					}

				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
			}
		}
		/*public String getMada(){
			Thread.Sleep (2000);
			return mada;

		}*/
		private void WritePort()
		{

			while (keepwrite == true)
			{
				while(keepwrite==true){
					int speed = (int)(62.5*16);
					String data = "";
					data = speed.ToString();
					byte[] byteArray = System.Text.Encoding.Default.GetBytes(data);
					for (int i = 0; i < msize; ++i)
					{
						com.Write(byteArray, 0, data.Length);
						//Thread.Sleep (1000);
						Thread.Sleep(3000);
					}
					keepwrite = false;
					
			}

		}
		}
		public void startMotor(int size)
		{
			//int speed = (int)(62.5 * (Math.Pow(4, size - 1)));
			Thread.Sleep(2000);
			int speed = (int)(62.5*Math.Pow(2,size+2));
			String data = "";
			int time = size;
			data = speed.ToString();
			byte[] byteArray = System.Text.Encoding.Default.GetBytes(data);
			com.Write(byteArray, 0, data.Length);
			//Thread.Sleep (1000);
			Thread.Sleep(2500);
			/*for (int i = 0; i < size; ++i)
			{
				com.Write(byteArray, 0, data.Length);
				//Thread.Sleep (1000);
				Thread.Sleep(2500);
			}*/
		}

		/*public void startMotor(int size)
		{
			keepwrite = true;
			msize = size;
			writeThread = new Thread(WritePort);
			writeThread.Start();

		}*/

		public hardware(){
			connect ();
		}
    }
}
