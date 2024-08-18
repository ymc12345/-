using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace 学习卡.Base
{
	public class NotifyBase : INotifyPropertyChanged
	{
		// 声明一个名为PropertyChangedEventHandler的委托事件
		public event PropertyChangedEventHandler? PropertyChanged;

		public void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
		{
			if (field == null || !field.Equals(value))
			{
                field = value;
                this.RaisePropertyChanged(propertyName);

			}
		}
		public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
