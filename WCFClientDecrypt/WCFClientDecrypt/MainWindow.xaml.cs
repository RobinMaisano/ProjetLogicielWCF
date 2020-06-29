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
using System.Text.RegularExpressions;
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
        private User user;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            this.msg = new MSG();
            this.user = new User();
            this.controller = new Controller();
            this.ChangeLayoutLogin();
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

        private void ChangeLayoutRegister()
        {
            this.label.Visibility = Visibility.Visible;
            this.label1.Visibility = Visibility.Hidden;
            this.labelLogin.Visibility = Visibility.Visible;
            this.loginBox.Visibility = Visibility.Visible;
            this.labelPswd.Visibility = Visibility.Visible;
            this.passwordBox.Visibility = Visibility.Visible;
            this.labelEmail.Visibility = Visibility.Visible;
            this.emailBox.Visibility = Visibility.Visible;
            this.RegisterChangeButton.Visibility = Visibility.Hidden;
            this.LoginChangeButton.Visibility = Visibility.Visible;
            this.RegisterButton.Visibility = Visibility.Visible;
            this.loginButton.Visibility = Visibility.Hidden;

            this.label2.Visibility = Visibility.Hidden;
            this.labelWelcome.Visibility = Visibility.Hidden;
            this.labelUsername.Visibility = Visibility.Hidden;
            this.labelFilesSelect.Visibility = Visibility.Hidden;
            this.listFiles.Visibility = Visibility.Hidden;
            this.UpdateButton.Visibility = Visibility.Hidden;
            this.decryptButton.Visibility = Visibility.Hidden;
            this.labelResult.Visibility = Visibility.Hidden;
            this.ResultBlock.Visibility = Visibility.Hidden;
            this.LogoutButton.Visibility = Visibility.Hidden;

            this.textBox.Visibility = Visibility.Hidden;
            this.button.Visibility = Visibility.Hidden;
            this.InfoPanel.Visibility = Visibility.Hidden;
        }

        private void ChangeLayoutLogin()
        {
            this.label.Visibility = Visibility.Hidden;
            this.label1.Visibility = Visibility.Visible;
            this.labelLogin.Visibility = Visibility.Visible;
            this.loginBox.Visibility = Visibility.Visible;
            this.labelPswd.Visibility = Visibility.Visible;
            this.passwordBox.Visibility = Visibility.Visible;
            this.labelEmail.Visibility = Visibility.Hidden;
            this.emailBox.Visibility = Visibility.Hidden;
            this.RegisterChangeButton.Visibility = Visibility.Visible;
            this.LoginChangeButton.Visibility = Visibility.Hidden;
            this.RegisterButton.Visibility = Visibility.Visible;
            this.loginButton.Visibility = Visibility.Visible;

            this.label2.Visibility = Visibility.Hidden;
            this.labelWelcome.Visibility = Visibility.Hidden;
            this.labelUsername.Visibility = Visibility.Hidden;
            this.labelFilesSelect.Visibility = Visibility.Hidden;
            this.listFiles.Visibility = Visibility.Hidden;
            this.UpdateButton.Visibility = Visibility.Hidden;
            this.decryptButton.Visibility = Visibility.Hidden;
            this.labelResult.Visibility = Visibility.Hidden;
            this.ResultBlock.Visibility = Visibility.Hidden;
            this.LogoutButton.Visibility = Visibility.Hidden;

            this.textBox.Visibility = Visibility.Hidden;
            this.button.Visibility = Visibility.Hidden;
            this.InfoPanel.Visibility = Visibility.Hidden;
        }

        private void ChangeLayoutLoggedIn()
        {
            this.label.Visibility = Visibility.Hidden;
            this.label1.Visibility = Visibility.Hidden;
            this.labelLogin.Visibility = Visibility.Hidden;
            this.loginBox.Visibility = Visibility.Hidden;
            this.labelPswd.Visibility = Visibility.Hidden;
            this.passwordBox.Visibility = Visibility.Hidden;
            this.labelEmail.Visibility = Visibility.Hidden;
            this.emailBox.Visibility = Visibility.Hidden;
            this.RegisterChangeButton.Visibility = Visibility.Hidden;
            this.LoginChangeButton.Visibility = Visibility.Hidden;
            this.RegisterButton.Visibility = Visibility.Hidden;
            this.loginButton.Visibility = Visibility.Hidden;

            this.label2.Visibility = Visibility.Visible;
            this.labelWelcome.Visibility = Visibility.Visible;
            this.labelUsername.Visibility = Visibility.Visible;
            this.labelFilesSelect.Visibility = Visibility.Visible;
            this.FillListFile();
            this.listFiles.Visibility = Visibility.Visible;
            this.UpdateButton.Visibility = Visibility.Visible;
            this.decryptButton.Visibility = Visibility.Visible;
            this.labelResult.Visibility = Visibility.Visible;
            this.ResultBlock.Visibility = Visibility.Visible;
            this.LogoutButton.Visibility = Visibility.Visible;

            this.textBox.Visibility = Visibility.Hidden;
            this.button.Visibility = Visibility.Hidden;
            this.InfoPanel.Visibility = Visibility.Hidden;
        }

        private void FillListFile()
        {
            string[] fileList = controller.GetListFile();
            this.listFiles.Items.Clear();
            foreach (string file in fileList)
            {
                this.listFiles.Items.Add(file);
            }
            
        }

        private void RegisterChangeButton_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeLayoutRegister();
        }

        private void LoginChangeButton_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeLayoutLogin();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Regex rgxlogin = new Regex(@"^[A-z]+$");
            Regex rgxpassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            Regex rgxmail = new Regex(@"^/\S+@\S+\.\S+/$");
            if(rgxlogin.IsMatch(this.loginBox.Text) && rgxpassword.IsMatch(this.passwordBox.Password) && rgxmail.IsMatch(this.emailBox.Text))
            {
                this.user.Setlogin(this.loginBox.Text);
                this.user.Setpassword(this.passwordBox.Password);
                object[] preData = new object[3];
                preData[0] = this.loginBox.Text;
                preData[1] = this.passwordBox.Password;
                preData[2] = this.emailBox.Text;
                this.msg.data = preData;
                this.msg = this.controller.m_register(this.msg);
                this.InfoPanelBox.Text = this.msg.info + "\n";

                //condition if register result is good
                if (this.msg.statusOp)
                {
                    this.InfoPanelBox.Text = this.InfoPanelBox.Text + "Register successful";
                    this.InfoPanel.Visibility = Visibility.Visible;
                    this.ChangeLayoutLoggedIn();
                }
                else
                {
                    this.InfoPanelBox.Text = this.InfoPanelBox.Text + "Register failed";
                    this.InfoPanel.Visibility = Visibility.Visible;
                }
                //TODO show|treat result of msg
            }
            else
            {
                if(!rgxlogin.IsMatch(this.loginBox.Text))
                {
                    this.InfoPanelBox.Text = this.InfoPanelBox.Text + "Login should be only Alphabetical letters \n";
                    this.InfoPanel.Visibility = Visibility.Visible;
                }
                if(!rgxpassword.IsMatch(this.passwordBox.Password))
                {
                    this.InfoPanelBox.Text = this.InfoPanelBox.Text + "Password must be at least 8 character long, contain a Lowercase, an Uppercase, a number and a special character \n";
                    this.InfoPanel.Visibility = Visibility.Visible;
                }
                if (!rgxmail.IsMatch(this.emailBox.Text))
                {
                    this.InfoPanelBox.Text = this.InfoPanelBox.Text + "The email entered is not a valid email";
                    this.InfoPanel.Visibility = Visibility.Visible;
                }
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Regex rgxlogin = new Regex(@"^[A-z]+$");
            Regex rgxpassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            if (rgxlogin.IsMatch(this.loginBox.Text) && rgxpassword.IsMatch(this.passwordBox.Password))
            {
                this.user.Setlogin(this.loginBox.Text);
                this.user.Setpassword(this.passwordBox.Password);
                object[] preData = new object[2];
                preData[0] = this.loginBox.Text;
                preData[1] = this.passwordBox.Password;
                this.msg.data = preData;
                
                this.msg = this.controller.m_login(this.msg);
                this.InfoPanelBox.Text = this.msg.info + "\n";

                //condition if login result is good
                if (this.msg.statusOp)
                {
                    this.InfoPanelBox.Text = this.InfoPanelBox.Text + "Login successful";
                    this.InfoPanel.Visibility = Visibility.Visible;
                    this.ChangeLayoutLoggedIn();
                }
                else
                {
                    this.InfoPanelBox.Text = this.InfoPanelBox.Text + "Login failed";
                    this.InfoPanel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (!rgxlogin.IsMatch(this.loginBox.Text))
                {
                    this.InfoPanelBox.Text = this.InfoPanelBox.Text + "Login should be only Alphabetical letters \n";
                    this.InfoPanel.Visibility = Visibility.Visible;
                }
                if (!rgxpassword.IsMatch(this.passwordBox.Password))
                {
                    this.InfoPanelBox.Text = this.InfoPanelBox.Text + "Password must be at least 8 character long, contain a Lowercase, an Uppercase, a number and a special character \n";
                    this.InfoPanel.Visibility = Visibility.Visible;
                }
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.user.ResetValues();
            this.ChangeLayoutLogin();
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> filesPath = new List<string;
            foreach(object file in this.listFiles.SelectedItems)
            {
                filesPath.Add(file.ToString());
            }
            object[] predata = new object[this.listFiles.SelectedItems.Count];
            int k = 0;
            Regex rgx = new Regex(@"[^\\]*$");
            foreach(string path in filesPath)
            {
                Dictionary<string, string> fileDict = new Dictionary<string, string>();
                string filename = rgx.Match(path).ToString();
                string fileContent = controller.GetFileContent(path);
                fileDict.Add(filename, fileContent);
                predata[k] = fileDict;
            }
            this.msg = this.controller.m_decrypt(this.msg);
            this.InfoPanelBox.Text = this.msg.info + "\n";

            //condition if Decrypt result is good
            if (this.msg.statusOp)
            {
                this.InfoPanelBox.Text = this.InfoPanelBox.Text + "Decrypt Request successful";
                this.InfoPanel.Visibility = Visibility.Visible;
                this.ChangeLayoutLoggedIn();
            }
            else
            {
                this.InfoPanelBox.Text = this.InfoPanelBox.Text + "Decrypt Request failed";
                this.InfoPanel.Visibility = Visibility.Visible;
            }
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            this.InfoPanelBox.Text = "";
            this.InfoPanel.Visibility = Visibility.Hidden;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            this.FillListFile();
        }

        private void RegisterPass_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeLayoutRegister();
        }

        private void DecryptPass_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeLayoutLoggedIn();
        }
    }
}
