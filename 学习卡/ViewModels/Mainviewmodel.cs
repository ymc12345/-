using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Ribbon;
using System.Windows.Threading;
using 学习卡.Base;
using 学习卡.Models;

namespace 学习卡.ViewModels
{
   public  class Mainviewmodel: NotifyBase
	{
		DispatcherTimer timer = new DispatcherTimer();
		private DateTime _currentTime;
        public DateTime CurrentTime
		{
           get { return _currentTime; }
			set { SetProperty<DateTime>(ref _currentTime, value); }

		}
		#region 串口下拉
		private string _portName;
		public string portName
		{
			get { return _portName; }
			set { SetProperty<string>(ref _portName, value); }

		}
		public int BaudRate { get; set; } = 9600;
        		public int DataBits { get; set; } = 8;
        		public string Parity { get; set; } = "None";
        		public string StopBits { get; set; } = "One";

        public List<string> PortList { get; set; }
        public List<string>BaudRateList { get; set; } =new List<string>
		{ "9600", "19200", "38400", "57600", "115200" };
		public List<int> DataBitsList { get; set; } = new List<int> { 5,7,8 };
        public List<string> StopBitsList { get; set; }
        public List<string> ParityList { get; set; }
		#endregion

		private double _currentCPU;
        public double CurrentCPU
		{
			get { return _currentCPU; }
            set { SetProperty<double>(ref _currentCPU, value); }
		}

        private double _currentMem;
        public double CurrentMem
		{
            get { return _currentMem; }
            set { SetProperty<double>(ref _currentMem, value); }
		}

		private PerformanceCounter cpuCounter;
		private ManagementClass memCounter;

		private bool _startState;
        public bool StartState
		{
            			get { return _startState; }
			set 
			{ _startState = value;
				try
				{
					OnStart();
				}
               catch (Exception ex)
				{
					this.ShowMessage(ex.Message,"通信异常","Orang");
				}
			
			}
		}
		#region 图表数据属性
		public ChartValues<double> TemperatureValues { get; set; } = new ChartValues<double>()
		{
			38,32,35,39,36,36,34,55,34,33,34
		};
		public ChartValues<double> HumidityValues { get; set; } = new ChartValues<double>()
		{
			45, 40, 42, 45, 40, 42, 45, 40, 42, 45, 40
		};
		public ChartValues<double> BrightnessValues { get; set; } = new ChartValues<double>()
		{
			30, 25, 28, 30, 25, 28, 30, 25, 28, 30, 25
		};
		public ObservableCollection<string> XLables { get; set; } = new ObservableCollection<string>();
		#endregion

		#region OLED数据处理
		public string SendText { get; set; } = "hello";
		private string _oledText;
        		public string OledText
		{
            			get { return _oledText; }
            			set { SetProperty<string>(ref _oledText, value); }
		}
		//发送文本命令
        public Command SendTextCommand { get; set; }
		//重发命令
        public Command ResendCommand { get; set; }

		public ObservableCollection<SendLogModel> LogList { get; set; } = new ObservableCollection<SendLogModel>();

        #endregion

        public DeviceModel DeviceInfo { get; set; }= new DeviceModel();
        public MessageInfo messageinfo { get; set; }= new MessageInfo();

		 #region 状态灯

public List<LightModel> LightList { get; set; } = new List<LightModel>();
		public Command LighrCommand { get; set; }
		#endregion

		private bool _enabled=true;
        public bool Enabled
		{
            			get { return _enabled; }
            			set { SetProperty<bool>(ref _enabled, value); }
		}

		

        SerialPort serialPort = new SerialPort();
        public Mainviewmodel()
		{
			//执行发送命令
			SendTextCommand = new Command(SendTextCommandExecute);
			//执行重发命令
            ResendCommand = new Command(ResendCommandExecute);

			//执行状态灯命令
            LighrCommand = new Command(LighrCommandExecute);
            timer.Interval = new TimeSpan(1);
			timer.Tick += (se, ev) =>
			{
				CurrentTime = DateTime.Now;

				if (PortList == null)
				{
					PortList = SerialPort.GetPortNames().ToList();
					if (PortList.Count > 0)
						this.portName = PortList[0];
					this.RaisePropertyChanged("PortList");
				}
				else if (!PortList.SequenceEqual(SerialPort.GetPortNames()))
				{
					PortList = SerialPort.GetPortNames().ToList();
					this.RaisePropertyChanged("PortList");
					//如果新列表中没有当前显示的串口名称的话，将新列表的第0个子项赋值给当前串口
					if (!PortList.Exists(p => p == portName))
					{
						portName = "";
						if (PortList.Count > 0)
						this.portName = PortList[0];
					}
				}

			};
            timer.Start();


			StopBitsList = Enum.GetNames(typeof(StopBits)).ToList();
			
           	ParityList = Enum.GetNames(typeof(Parity)).ToList();

			var time = DateTime.Now;
			for(int i = 10; i >=0; i--)
			{
				XLables.Add(time.AddSeconds(i*-1).ToString("ss"));
			}

			for(ushort i = 0; i <=5; i++)
			{
				LightList.Add(new LightModel { Address = i });
			}


			cpuCounter = new PerformanceCounter();
			cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";
            memCounter = new ManagementClass();

			Task.Run(async () =>
			{
				while (true)
				{
					this.CurrentCPU= cpuCounter.NextValue();
                    this.CurrentMem = GetMemInfo();
					await Task.Delay(1000);
				}
			});
        
		}

		private void ResendCommandExecute(object obj)
		{
			if(!serialPort.IsOpen) return;
			try
			{
				string text= obj.ToString();
				this.SendOledText(text);
			}
            catch (Exception ex)
			{
				this.ShowMessage(ex.Message,"重发异常","orange");
			}
		}

		public void SendTextCommandExecute(object obj)
		{
			if (!serialPort.IsOpen) return;
			try
			{
				if(string.IsNullOrEmpty(SendText))
					throw new Exception("请输入要发送的文本");
				else if(SendText.Length > 60)
                    throw new Exception("发送文本不能超过60个字符");
				else if(SendText.ToList().Exists(s=>(int)s> 127))
                    throw new Exception("发送文本不能包含非ASCII字符");

				SendOledText(SendText);
			}
            catch (Exception ex)
			{
				this.ShowMessage(ex.Message,"发送异常","orange");
			}
		}

		bool iswriteing= false;
        private void LighrCommandExecute(object obj)
		{
			if(!serialPort.IsOpen) return;
			iswriteing = true;
			try
			{
				if (obj.ToString() == "all")
				{
					byte state= (byte)(LightList[5].State ? 0x1F : 0x00);
					byte[] bytes = new byte[] { 0x01, 0x0F, 0x00, 0x00, 0x00, 0x05, 0x01, state, 0x00, 0x00 };
                    CRC16(bytes);
                    this.SndAndReceive(bytes);
					LightList.ForEach(l => l.State =LightList[5].State);

				}
				else
				{
					int.TryParse(obj.ToString(), out int index);
					byte state = LightList[index].State ? (byte)0xFF : (byte)0x00;
					byte[] bytes = new byte[] { 0x01, 0x05, 0x00, (byte)index, state, 0x00,  0x00, 0x00 };
					CRC16(bytes);
					this.SndAndReceive(bytes);

					LightList[5].State = LightList[0].State&& LightList[1].State
						&&LightList[2].State
						&&LightList[3].State
						&&LightList[4].State;
				}
			}
            catch (Exception ex)
			{
              this.ShowMessage(ex.Message,"状态灯异常","orange");
			}
			finally
			{
               iswriteing = false;
			}
		}

		//发送文本
		private void SendOledText(string text)
		{
			byte[] text_bytes= Encoding.ASCII.GetBytes(text);

			List<byte> bytes = new List<byte>();
            bytes.Add(0x01);
            bytes.Add(0x10);
			bytes.Add(0x00);
            bytes.Add(0x08);
			bytes.Add((byte)(Math.Ceiling(text_bytes.Length * 1.0 / 2) / 256));
			bytes.Add((byte)(Math.Ceiling(text_bytes.Length * 1.0 / 2) % 256));
			byte len = (byte)text_bytes.Length;
			len +=(byte) (len % 2);
			bytes.Add(len);
            bytes.AddRange(text_bytes);
			if(text_bytes.Length % 2 == 1)
			{
				bytes.Add(0x00);
			}
			bytes.Add(0x00);
			bytes.Add(0x00);
			byte[] byteArray= bytes.ToArray();
			CRC16(byteArray);
			this.SndAndReceive(byteArray);
			
			this.OledText = text;
			//将发送的文本显示在发送日志中
			this.LogList.Insert(0, new SendLogModel { LogInfo = text });
			if(this.LogList.Count > 30)
			{
               this.LogList.RemoveAt(this.LogList.Count - 1);
			}
		}

		private double GetMemInfo()
		{
			memCounter.Path = new ManagementPath("Win32_PhysicalMemory");
			ManagementObjectCollection moc = memCounter.GetInstances();
			double available = 0, capacity = 0;

			foreach (ManagementObject mo in moc)
			{
				capacity += ((Math.Round(Int64.Parse(mo.Properties["Capacity"].Value.ToString()) / 1024 / 1024 / 1024.0, 1)));
			}
			moc.Dispose();


			memCounter.Path = new ManagementPath("Win32_PerfFormattedData_PerfOS_Memory");
			moc = memCounter.GetInstances();
			foreach (ManagementObject mo2 in moc)
			{
				available += ((Math.Round(Int64.Parse(mo2.Properties["AvailableMBytes"].Value.ToString()) / 1024.0, 1)));
			}
			moc.Dispose();

			return (capacity - available) / capacity * 100;

		}

		private void ShowMessage(string message,string title="运行提示",string color = "#00cff8")
		{
			messageinfo.Message = message;
            messageinfo.Title = title;
            messageinfo.MsgColor = color;
			messageinfo.MsgTime= DateTime.Now;
		}

		private void OnStart()
		{
			if (serialPort.IsOpen)
			{
               serialPort.Close();
                this.ShowMessage("串口已关闭", "运行提示", "#00ff00");
                				Enabled = true;
			}
			else
			{
				if (string.IsNullOrEmpty(portName))
				{
                   throw new Exception("请选择串口");
					
				}
				serialPort.PortName = portName;
				serialPort.BaudRate = BaudRate;
				serialPort.DataBits = DataBits;
				serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), Parity);
				serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), StopBits);
                serialPort.Open();
				this.ShowMessage("串口已打开", "运行提示", "#00ff00");
				Enabled= false;
				//启动监听
				Task.Run(async () =>
				{
					while (serialPort.IsOpen)
					{
						this.OnMonitor();
						await Task.Delay(1000);
					}
				});
			}
		}
		int[] lastRandom= new int[3];
		bool[] order = new bool[3];
		private void OnMonitor()
		{
			try
			{


				//温度湿度亮度
				byte[] bytes = new byte[8];
				bytes[0] = 0x01;
				bytes[1] = 0x03;
				bytes[2] = 0x00;
				bytes[3] = 0x00;
				bytes[4] = 0x00;
				bytes[5] = 0x03;
				CRC16(bytes);

				byte[] resp = SndAndReceive(bytes);

				if (resp != null && resp.Length > 0 && resp[1] == 0x03)
				{
					if (DeviceInfo.UseTemperatureSim)
					{
						this.GenerateRandom(ref lastRandom[0], ref order[0],
							DeviceInfo.MinTemperatureSim, DeviceInfo.MaxTemperatureSim);
						DeviceInfo.Temperature = lastRandom[0];
					}
					else
						DeviceInfo.Temperature = BitConverter.ToInt16(new byte[] { resp[4], resp[3] }) * 0.1f;

					if (DeviceInfo.UseHumiditySim)
					{
						this.GenerateRandom(ref lastRandom[1], ref order[1],
							DeviceInfo.MinHumiditySim, DeviceInfo.MaxHumiditySim);
						DeviceInfo.Humidity = lastRandom[1];
					}
					else
						DeviceInfo.Humidity = BitConverter.ToUInt16(new byte[] { resp[6], resp[5] });

					if (DeviceInfo.UseBrightnessSim)
					{
						this.GenerateRandom(ref lastRandom[2], ref order[2],
							DeviceInfo.MinBrightnessSim, DeviceInfo.MaxBrightnessSim);
						DeviceInfo.Brightness = lastRandom[2];
					}
					else
						DeviceInfo.Brightness = BitConverter.ToUInt16(new byte[] { resp[8], resp[7] });

					this.TemperatureValues.Add(DeviceInfo.Temperature);
					this.HumidityValues.Add(DeviceInfo.Humidity);
					this.BrightnessValues.Add(DeviceInfo.Brightness);
					XLables.Add(DateTime.Now.ToString("ss"));

					this.TemperatureValues.RemoveAt(0);
					this.HumidityValues.RemoveAt(0);
					this.BrightnessValues.RemoveAt(0);
					this.XLables.RemoveAt(0);
				}
				//灯珠状态

				bytes[1] = 0x01;
				bytes[5] = 0x05;
				CRC16(bytes);
				resp = SndAndReceive(bytes);
				if (resp != null && resp.Length > 0 && resp[1] == 0x01&!iswriteing)
				{
					LightList[0].State = (resp[3] & 1) != 0;
					LightList[1].State = (resp[3] & 2) != 0;
					LightList[2].State = (resp[3] & 4) != 0;
					LightList[3].State = (resp[3] & 8) != 0;
					LightList[4].State = (resp[3] & 16) != 0;
                    LightList[5].State = (resp[3] & 31) == 31;

				}
			}
            catch (Exception ex)
			{
				this.ShowMessage(ex.Message, "通信异常", "orange");
			}
		}

		private void GenerateRandom(ref int randomValue,ref bool order,int min,int max)
		{
			if (randomValue == 0)
			{
                randomValue = new Random().Next(min, max);

			}
			else
			{
				int rv = randomValue;
               if (order)
				{
					var _max=Math.Min(rv + 20, max);
					do
					{
						randomValue = new Random().Next(rv - 5, _max);
					}
					while (randomValue > max);

					if(_max== max)
					{
                        order = false;
					}
				}
				else
				{
					var _min = Math.Max(rv - 20, min);
					do
					{
						randomValue= new Random().Next(_min, rv + 5);
					}
                    while (randomValue < min);

					if(_min == min)
					{
                        order = true;
					}
				}
			}
		}

		private byte[] SndAndReceive(byte[] bytes)
		{
			
			lock (_lockObject)
			{
                // 向串口写入数据
                serialPort.Write(bytes, 0, bytes.Length);

				// 等待100毫秒
				Task.Delay(100).Wait();

				// 读取串口数据
				byte[] resp = new byte[serialPort.BytesToRead];
				serialPort.Read(resp, 0, resp.Length);
				return resp;
			}
		}

		//定义一个私有静态常量对象_lockObject，用于作为锁对象
		private static readonly object _lockObject = new object();
		public void CRC16(byte[] value)
		{
			ushort poly = 0xA001; ushort crcInit = 0xFFFF;
			if (value == null || !value.Any())
				throw new ArgumentException("");

			//运算
			ushort crc = crcInit;
			for (int i = 0; i < value.Length-2; i++)
			{
				crc = (ushort)(crc ^ (value[i]));
				for (int j = 0; j < 8; j++)
				{
					crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ poly) : (ushort)(crc >> 1);
				}
			}
			byte hi = (byte)((crc & 0xFF00) >> 8);  //高位置
			byte lo = (byte)(crc & 0x00FF);         //低位置

			value[value.Length - 2] = lo;
            value[value.Length - 1] = hi;
			
		}

	}
}
