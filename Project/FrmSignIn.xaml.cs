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
using System.Windows.Shapes;
using System.ComponentModel;
using Project.Models;

namespace Project
{
    /// <summary>
    /// Interaction logic for FrmLogin.xaml
    /// </summary>
    public partial class FrmSignIn : Window
    {
        UserManagermentContext userManagermentContext = new UserManagermentContext();
        public FrmSignIn()
        {
            InitializeComponent();
        }

        private void EmailTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            EmailPlaceholder.Visibility = string.IsNullOrEmpty(txtEmail.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(txtPassword.Password) ? Visibility.Visible : Visibility.Hidden;
        }


        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Log input values (do not log passwords in a real application)
                string inputEmail = txtEmail.Text;
                string inputPassword = txtPassword.Password;
                Console.WriteLine($"Attempting login with Email: {inputEmail}");

                // Ensure input values are trimmed and checked for null or empty
                if (string.IsNullOrEmpty(inputEmail) || string.IsNullOrEmpty(inputPassword))
                {
                    MessageBox.Show("Please enter both email and password.");
                    return;
                }

                // Query the database using a case-insensitive comparison
                var user = userManagermentContext.Users.FirstOrDefault(u => (u.UserName.Equals(txtEmail.Text) || u.Email.Equals(txtEmail.Text)) &&
                                                                         u.PassWord == txtPassword.Password);

                if (user != null && user.RoleId == 1)
                {
                    FrmAdminHome frmhome = new FrmAdminHome(user.UserId);
                    frmhome.Show();
                    this.Close();
                }
                else if (user != null && user.RoleId == 2)
                {
                    Home home = new Home(user.UserId);
                    home.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login failed! Incorrect email or password.");
                }
            }
            catch (Exception ex)
            {
                // Handle and log any exceptions
                //MessageBox.Show($"An error occurred during login: {ex.Message}");
                //Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {


            FrmSignUp frmsignup = new FrmSignUp();
            frmsignup.Show();
            this.Close();
        }

        private bool _isPasswordVisible;
        private void ShowHideButton_Click(object sender, RoutedEventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;

            if (_isPasswordVisible)
            {
                txtPassword.Visibility = Visibility.Collapsed;
                txtVisiblePassword.Visibility = Visibility.Visible;
                txtVisiblePassword.Text = txtPassword.Password;
                ShowHideImage.Source = new BitmapImage(new Uri("https://cdn-icons-png.flaticon.com/512/25/25186.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                txtPassword.Visibility = Visibility.Visible;
                txtVisiblePassword.Visibility = Visibility.Collapsed;
                txtPassword.Password = txtVisiblePassword.Text;
                ShowHideImage.Source = new BitmapImage(new Uri("https://static-00.iconduck.com/assets.00/eye-closed-icon-512x459-ivg8wjcx.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void btnForgotPass_Click(object sender, RoutedEventArgs e)
        {
            FrmForgotPass frmForgotPass = new FrmForgotPass();
            frmForgotPass.ShowDialog();

        }

        
    }
}
