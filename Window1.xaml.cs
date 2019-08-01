using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace frp
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : MetroWindow
    {
        public Window1()
        {
            InitializeComponent();
            textBox1.IsReadOnly = true;
            textBox1.Text = Resource1.gpl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ShowMessageAsync("提示", "开源地址已复制到你的剪切板，请遵守GPL3.0协议！");

            Clipboard.SetDataObject("https://github.com/mcoo/frp/");
        }
    }
}
