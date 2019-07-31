using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace frp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public void ChangeAppStyle()
        {
            // set the Red accent and dark theme only to the current window
            MahApps.Metro.ThemeManager.ChangeAppStyle(this,
                                        MahApps.Metro.ThemeManager.GetAccent("Black"),
                                        MahApps.Metro.ThemeManager.GetAppTheme("BaseDark"));
        }
        public static bool FileExist(string fullName)
        {
            bool response = false;
            if (File.Exists(fullName))
            {
                response = true;
            }
            return response;
        }
        public MainWindow()
        {
            InitializeComponent();
            //ChangeAppStyle();
            comboBox.Items.Add("UDP");
            comboBox.Items.Add("TCP");
            comboBox.Items.Add("HTTP");
            comboBox.Items.Add("HTTPS");
            //comboBox.Items.Add("STCP");
            this.ShowMessageAsync("提示", "Bata版本，请勿用于商业用途，许多功能未完善，敬请期待！作者Enjoy\n如果启动后未出现CMD请认真阅读帮助文件\n自定义域名需要HTTP，HTTPS协议");
            if (FileExist("./frpc.exe") == false)
            {
                File.WriteAllBytes("./frpc.exe", Resource1.frpc);
            }
            if (FileExist("./set.ini") == true)
            {
                INIFile aa = new INIFile("./set.ini");
                textBox_ip.Text=aa.IniReadValue("set","ip");
                textBox_port.Text = aa.IniReadValue("set", "port");
                textBox_server.Text = aa.IniReadValue("set", "server");
                textBox_toport.Text = aa.IniReadValue("set", "toport");
                passwordBox.Password = aa.IniReadValue("set", "key");
                textBox_name.Text = aa.IniReadValue("set", "name");
                switch (aa.IniReadValue("set", "type"))
                {
                    default:
                        this.ShowMessageAsync("错误", "请选择类型！🙂");
                        break;
                    case "udp":
                        comboBox.SelectedIndex = 0;
                        break;
                    case "tcp":
                        comboBox.SelectedIndex = 1;
                        break;
                    case "http":
                        comboBox.SelectedIndex = 2;
                        break;
                    case "https":
                        comboBox.SelectedIndex = 3;
                        break;
                }
            }

        }
        string type = "TCP";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_name.Text == "" || textBox_port.Text == ""   || passwordBox.Password == ""|| textBox_server.Text == "")
            {
                this.ShowMessageAsync("错误", "请填写完上面的内容！");
            }
            else
            {
                switch (comboBox.SelectedIndex)
                {
                    default:
                        this.ShowMessageAsync("错误", "请选择类型！🙂");
                        break;
                    case 0:
                        type = "udp";
                        Start1();
                        break;
                    case 1:
                        type = "tcp";
                        Start1();
                        break;
                    case 2:
                        type = "http";
                        Start2();
                        break;
                    case 3:
                        type = "https";
                        Start2();
                        break;
                }
            }
        }
        public void Start1()
        {
            string ip = textBox_ip.Text;
            string port = textBox_port.Text;
            string toport = textBox_toport.Text;
            string server = textBox_server.Text;
            string key = passwordBox.Password;
            string name = textBox_name.Text;
            INIFile aa = new INIFile("./set.ini");
            aa.IniWriteValue("set", "ip", ip);
            aa.IniWriteValue("set", "port", port);
            aa.IniWriteValue("set", "toport", toport);
            aa.IniWriteValue("set", "server", server);
            aa.IniWriteValue("set", "type", type);
            aa.IniWriteValue("set", "key", key);
            aa.IniWriteValue("set", "name", name);
            //string a = "[common]\nserver_addr = "+server+"\nserver_port = 7000\ntoken = "+key+"\n\n["+name+"]\ntype = " + type + "\nlocal_ip = " + ip + "\nlocal_port = " + port + "\nremote_port = " + toport + "\n";
            //DelectDir("./frpc.ini");
            //Write("./frpc.ini",a);
            string a = "frpc "+type+" -i " + ip + " -l " + port + " -s " + server + " -t " + key + " -n "+name +" -r "+toport;
            //this.ShowMessageAsync("测试", a);
            //Clipboard.SetDataObject(a);
            Execute(a, 5);
        }
        public void Start2()
        {
            string ip = textBox_ip.Text;
            string port = textBox_port.Text;
            string toport = textBox_toport.Text;
            string server = textBox_server.Text;
            string web = textBox_web.Text;
            string key = passwordBox.Password;
            string name = textBox_name.Text;
            string a = "";
            INIFile aa = new INIFile("./set.ini");
            aa.IniWriteValue("set", "ip", ip);
            aa.IniWriteValue("set", "port", port);
            aa.IniWriteValue("set", "toport", toport);
            aa.IniWriteValue("set", "server", server);
            aa.IniWriteValue("set", "web", web);
            aa.IniWriteValue("set", "type", type);
            aa.IniWriteValue("set", "key", key);
            aa.IniWriteValue("set", "name", name);
            if (isProtect.IsChecked == true)
            {
                a = "frpc " + type + " -i " + ip + " -l " + port + " -s " + server + " -t " + key + " -n " + name + " -d " + web + " --http_pwd "+passwordweb.Password + " --http_user "+textBox_user.Text;
                //a = "[common]\nserver_addr = " + server + "\nserver_port = 7000\ntoken = " + key + "\n\n[" + name + "]\ntype = " + type + "\nlocal_ip = " + ip + "\nlocal_port = " + port + "\ncustom_domains = " + web + "\nhttp_user = "+textBox_user.Text+"\nhttp_pwd = "+passwordweb.Password+ "\nremote_port = "+toport;
            }
            else
            {
                a = "frpc " + type + " -i " + ip + " -l " + port + " -s " + server + " -t " + key + " -n " + name+" -d "+web;
                //a = "[common]\nserver_addr = " + server + "\nserver_port = 7000\ntoken = " + key + "\n\n[" + name + "]\ntype = " + type + "\nlocal_ip = " + ip + "\nlocal_port = " + port + "\ncustom_domains = " + web+ "\nremote_port = " + toport;
            }
            //DelectDir("./frpc.ini");
            //Write("./frpc.ini", a);
            //Execute("frpc.exe -c frpc.ini", 5);
            //this.ShowMessageAsync("测试", a);
           // Clipboard.SetDataObject(a);
            Execute(a, 5);
        }
        public void Write(string path,string data)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.Write(data);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
        public static string Execute(string command, int seconds)
        {
            string output = ""; //输出字符串
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();//创建进程对象
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令
                startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动
                startInfo.RedirectStandardInput = false;//不重定向输入
                startInfo.RedirectStandardOutput = false; //重定向输出
                startInfo.CreateNoWindow = false;//不创建窗口
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束
                        }
                        else
                        {
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒
                        }
                        // output = process.StandardOutput.ReadToEnd();//读取进程的输出
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            return output;
        }
        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        //如果 使用了 streamreader 在删除前 必须先关闭流 ，否则无法删除 sr.close();
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        public void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.ShowMessageAsync("提示", "服务器请输入类似“127.0.0.1:7000”的地址一定要包括端口！作者地址已经复制到剪切板，如有问题请在上面提出");

            Clipboard.SetDataObject("https://mcoo.pw");
        }

        private void Button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            //捐赠！
            new Window1().Show();
  

        }
    }
}
