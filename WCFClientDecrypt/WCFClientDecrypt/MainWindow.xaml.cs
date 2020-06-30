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
            this.controller = new Controller(this.user);
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
            this.ResultScroll.Visibility = Visibility.Hidden;
            this.ResultBlock.Visibility = Visibility.Hidden;
            this.LogoutButton.Visibility = Visibility.Hidden;

            this.textBox.Visibility = Visibility.Hidden;
            this.button.Visibility = Visibility.Hidden;
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
            this.ResultScroll.Visibility = Visibility.Hidden;
            this.ResultBlock.Visibility = Visibility.Hidden;
            this.LogoutButton.Visibility = Visibility.Hidden;

            this.textBox.Visibility = Visibility.Hidden;
            this.button.Visibility = Visibility.Hidden;
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
            this.ResultScroll.Visibility = Visibility.Visible;
            this.ResultBlock.Visibility = Visibility.Visible;
            this.LogoutButton.Visibility = Visibility.Visible;

            this.textBox.Visibility = Visibility.Hidden;
            this.button.Visibility = Visibility.Hidden;
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
                string infoAlert = this.msg.info + "\n";

                //condition if register result is good
                if (this.msg.statusOp)
                {
                    infoAlert = infoAlert + "Register successful";
                    MessageBox.Show(infoAlert);
                    this.ChangeLayoutLoggedIn();
                }
                else
                {
                    infoAlert = infoAlert + "Register failed";
                    MessageBox.Show(infoAlert);
                }
                //TODO show|treat result of msg
            }
            else
            {
                string infoAlert = "";
                if (!rgxlogin.IsMatch(this.loginBox.Text))
                {
                    infoAlert = infoAlert + "Login should be only Alphabetical letters \n";
                }
                if(!rgxpassword.IsMatch(this.passwordBox.Password))
                {
                    infoAlert = infoAlert + "Password must be at least 8 character long, contain a Lowercase, an Uppercase, a number and a special character \n";
                }
                if (!rgxmail.IsMatch(this.emailBox.Text))
                {
                    infoAlert = infoAlert + "The email entered is not a valid email";
                }
                MessageBox.Show(infoAlert);
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
                string infoAlert = this.msg.info + "\n";

                //condition if login result is good
                if (this.msg.statusOp)
                {
                    infoAlert = infoAlert + "Login successful";
                    MessageBox.Show(infoAlert);
                    this.ChangeLayoutLoggedIn();
                }
                else
                {
                    infoAlert = infoAlert + "Login failed";
                    MessageBox.Show(infoAlert);
                }
            }
            else
            {
                string infoAlert = "";
                if (!rgxlogin.IsMatch(this.loginBox.Text))
                {
                    infoAlert = infoAlert + "Login should be only Alphabetical letters \n";
                }
                if (!rgxpassword.IsMatch(this.passwordBox.Password))
                {
                    infoAlert = infoAlert + "Password must be at least 8 character long, contain a Lowercase, an Uppercase, a number and a special character \n";
                }
                MessageBox.Show(infoAlert);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.user.ResetValues();
            this.ChangeLayoutLogin();
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> filesPath = new List<string>();
            foreach (object file in this.listFiles.SelectedItems)
            {
                filesPath.Add(file.ToString());
            }
            object[] predata = new object[this.listFiles.SelectedItems.Count];
            int k = 0;
            Regex rgx = new Regex(@"[^\\]*$");
            foreach (string path in filesPath)
            {
                Dictionary<string, string> fileDict = new Dictionary<string, string>();
                string filename = rgx.Match(path).ToString();
                string fileContent = controller.GetFileContent(path);
                fileDict.Add("title", filename);
                fileDict.Add("content", fileContent);
                predata[k] = fileDict;
                k++;
            }
            this.msg.data = predata;
            this.msg = this.controller.m_decrypt(this.msg);
            string infoAlert = this.msg.info + "\n" + ((Dictionary<string, string>)this.msg.data[0])["title"] + "\n" + ((Dictionary<string, string>)this.msg.data[0])["content"] + "\n";

            //condition if Decrypt result is good
            if (this.msg.statusOp)
            {
                infoAlert = infoAlert + "Decrypt Request successful";
                MessageBox.Show(infoAlert);
                // TODO 
                // Start request Checking
                this.msg = this.controller.m_checkIsDecrypted_loop(this.msg);
                this.ResultBlock.Text = "";
                foreach(Dictionary<string, string> resultDict in this.msg.data)
                {
                    this.ResultBlock.Text = this.ResultBlock.Text + resultDict["title"] + "\n" +
                        resultDict["content"] + "\n" +
                        resultDict["key"] + "\n" +
                        resultDict["trust"] + "\n" +
                        resultDict["secretInfo"] + "\n" +
                        "\n";
                }
            }
            else
            {
                infoAlert = infoAlert + "Decrypt Request failed";
                MessageBox.Show(infoAlert);
            }
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
