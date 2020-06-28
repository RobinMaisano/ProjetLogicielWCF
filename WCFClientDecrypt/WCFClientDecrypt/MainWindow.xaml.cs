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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WCFClientDecrypt.proxy;

namespace WCFClientDecrypt
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MSG msg;
        private Controller controller;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.msg = new MSG();
            this.controller = new Controller();
        }

        private void OnClick1(object sender, RoutedEventArgs e)
        {
            this.msg = this.controller.m_helloworld(this.msg);
            if(this.msg.statusOp == true)
            {
                this.textBox.Text = "Operation success";
            }
            else
            {
                this.textBox.Text = "Operation Failed" + this.msg.info;
            }
        }

        private void RegisterChangeButton_Click(object sender, RoutedEventArgs e)
        {
            this.label.Visibility = Visibility.Visible;
        }
    }
}
