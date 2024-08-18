using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 学习卡.Controls
{
    /// <summary>
    /// Meter.xaml 的交互逻辑
    /// </summary>
    public partial class Meter : UserControl
    {
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(double), typeof(Meter), new PropertyMetadata(0.0));
		public double Maximum
		{
			get { return (double)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		public static readonly DependencyProperty MaximumProperty =
			DependencyProperty.Register("Maximum", typeof(double), typeof(Meter), new PropertyMetadata(100.0));
		public int Interval
		{
			get { return (int)GetValue(IntervalProperty); }
			set { SetValue(IntervalProperty, value); }
		}
		public static readonly DependencyProperty IntervalProperty =
			DependencyProperty.Register("Interval", typeof(int), typeof(Meter), new PropertyMetadata(10));
		public double Value
		{
			get { return (double)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(double), typeof(Meter), new PropertyMetadata(0.0,new PropertyChangedCallback(onValueChanged)));
        private static void onValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Meter).UpdatePointer();
        }
        private void UpdatePointer()
        {
            double step = 270 / (this.Maximum - this.Minimum);
            double angle = (this.Value - this.Minimum) * step + 135;

            DoubleAnimation da= new DoubleAnimation(angle, new TimeSpan(0,0,0,0,500));
            //this.rtPointer.Angle = angle;
            this.rtPointer.BeginAnimation(RotateTransform.AngleProperty, da);
        }
		public string Unit
		{
			get { return (string)GetValue(UnitProperty); }
			set { SetValue(UnitProperty, value); }
		}
		public static readonly DependencyProperty UnitProperty =
			DependencyProperty.Register("Unit", typeof(string), typeof(Meter), new PropertyMetadata(""));
		public string Header
		{
			get { return (string)GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}
		public static readonly DependencyProperty HeaderProperty =
			DependencyProperty.Register("Header", typeof(string), typeof(Meter), new PropertyMetadata(""));



		public Meter()
        {
			InitializeComponent();
            this.SizeChanged += Meter_SizeChanged;
        }

		private void Meter_SizeChanged(object sender, SizeChangedEventArgs e)
		{
            this.back_border.Width = Math.Min(this.RenderSize.Width, this.RenderSize.Height);
            this.back_border.Height = Math.Min(this.RenderSize.Width, this.RenderSize.Height);
            this.back_border.CornerRadius= new CornerRadius(this.back_border.Width / 2);

            //半径
            double radius = this.back_border.Width / 2;
            if (radius <= 0) return;

            this.canvasPlate.Children.Clear();
            string borderPathData = $"M2,{radius-2}A{radius } {radius-2} 0 1 1 {radius-2} {this.back_border.Height-2}";
            this.pathBoser.Data=PathGeometry.Parse(borderPathData);

            double lable = this.Minimum;
            double step = 270 / (this.Maximum - this.Minimum);
            for(int i = 0; i <= (this.Maximum-this.Minimum); i++)
            {
                int offet = 8;
                if (i % 10 == 0) 
                   { offet = 12;
                TextBlock tb = new TextBlock();
                tb.Text = lable.ToString();
                    tb.Width = 34;
                    tb.TextAlignment = TextAlignment.Center;
                    tb.Foreground= Brushes.White;
                    tb.FontSize = 10;
                    Canvas.SetLeft(tb, radius + (radius - 30) * Math.Cos((step * i + 135) * Math.PI / 180)-17);
                    Canvas.SetTop(tb, radius + (radius - 30) * Math.Sin((step * i + 135) * Math.PI / 180)-8);
                    this.canvasPlate.Children.Add(tb);
                    lable += 10;
                }
                else if (i % 5 == 0) offet = 10;
                Line line = new Line();
                line.X1 = radius+(radius-5)*Math.Cos((step * i+135)*Math.PI / 180);
                line.Y1 = radius+(radius-5)*Math.Sin((step * i + 135) * Math.PI / 180);
                line.X2 = radius+ (radius - offet) * Math.Cos((step * i + 135) * Math.PI / 180);
                line.Y2 = radius+ (radius - offet) * Math.Sin((step * i + 135) * Math.PI / 180);

                line.Stroke = Brushes.White;
                line.StrokeThickness = 1;
                this.canvasPlate.Children.Add(line);
            }
			string pointerData = $"M{radius} {radius + 2} {this.back_border.Width * 0.95} {radius} {radius} {radius - 2}";
			this.pointer.Data = PathGeometry.Parse(pointerData);
		}
	}
}
