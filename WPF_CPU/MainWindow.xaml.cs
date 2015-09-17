using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace WpfPerformance
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Point pBefore = new Point();//鼠标点击前坐标
        Point eBefore = new Point();//圆移动前坐标
        bool isMove = false;//是否需要移动

        //Root 鼠标左键按下事件
        private void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(Ellipse))
            {
                this.pBefore = e.GetPosition(null);//获取点击前鼠标坐标
                Ellipse el = (Ellipse)e.OriginalSource;
                this.eBefore = new Point(Canvas.GetLeft(el), Canvas.GetTop(el));//获取点击前圆的坐标
                isMove = true;//开始移动了
                el.CaptureMouse();//鼠标捕获此圆
            }
        }

        //Root 鼠标左键放开事件
        private void LayoutRoot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(Ellipse))
            {
                Ellipse el = (Ellipse)e.OriginalSource;
                isMove = false;//结束移动了
                el.ReleaseMouseCapture();//鼠标释放此圆
            }
        }

        //Root 鼠标移动事件
        private void LayoutRoot_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource != null && e.OriginalSource.GetType() == typeof(Ellipse) && isMove)
            {
                Ellipse el = (Ellipse)e.OriginalSource;
                Point p = e.GetPosition(null);//获取鼠标移动中的坐标
                Canvas.SetLeft(el, eBefore.X + (p.X - pBefore.X));
                Canvas.SetTop(el, eBefore.Y + (p.Y - pBefore.Y));
            }
        }

    }
}