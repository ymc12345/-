﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using 学习卡.ViewModels;

namespace 学习卡.Views
{
	/// <summary>
	/// MainView.xaml 的交互逻辑
	/// </summary>
	public partial class MainView : Window
	{
		public MainView()
		{
			InitializeComponent();
			this.DataContext = new Mainviewmodel();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
