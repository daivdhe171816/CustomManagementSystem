using Project.Models;
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

namespace Project
{
    /// <summary>
    /// Interaction logic for FrmSignUp.xaml
    /// </summary>
    public partial class FrmSignUp : Window
    {
        UserManagermentContext userManagermentContext = new UserManagermentContext();

        public FrmSignUp()
        {
            InitializeComponent();
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsernamePlaceholder.Visibility = string.IsNullOrEmpty(txtUsername.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailPlaceholder.Visibility = string.IsNullOrEmpty(txtEmail.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordPlaceholder.Visibility = string.IsNullOrEmpty(txtPassword.Password) ? Visibility.Visible : Visibility.Hidden;
        }

        private void RePasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RePasswordPlaceholder.Visibility = string.IsNullOrEmpty(txtRePassword.Password) ? Visibility.Visible : Visibility.Hidden;
        }



        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Password.Trim();
                string rePassword = txtRePassword.Password.Trim();

                // Basic validation
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) ||
                    string.IsNullOrEmpty(password) || string.IsNullOrEmpty(rePassword))
                {
                    MessageBox.Show("Please input all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Invalid email format.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (password != rePassword)
                {
                    MessageBox.Show("Passwords and Re-Password do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                 else if(txtVisiblePassword1.Text.Trim() != rePassword) 
                {
                    MessageBox.Show("Passwords and Re-Password do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                else if (txtVisibleRePassword2.Text.Trim() != password)
                {
                    MessageBox.Show("Passwords and Re-Password do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                else if (txtVisibleRePassword2.Text.Trim() != txtVisiblePassword1.Text.Trim())
                {
                    MessageBox.Show("Passwords and Re-Password do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Check if the email already exists
                var existingUser = userManagermentContext.Users.FirstOrDefault(u => u.UserName.Equals(username));
                if (existingUser != null)
                {
                    MessageBox.Show("Username already exists.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var existingEmail = userManagermentContext.Users.FirstOrDefault(u => u.Email.Equals(email));
                if(existingEmail != null)
                {
                    MessageBox.Show("Email already exists.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Create new user
                var newUser = new User
                {
                    UserName = username,
                    Email = email,
                    PassWord = password,
                    RoleId = 2
                };

                userManagermentContext.Users.Add(newUser);
                userManagermentContext.SaveChanges();

                MessageBox.Show("Sign-up successful. Go to my App with your Account now!!!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                FrmSignIn frmsignin = new FrmSignIn();
                frmsignin.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during sign-up: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrmSignIn frmsignin = new FrmSignIn();
            frmsignin.Show();
            this.Close();
        }

        private void btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            txtUsername.Text = "";
            txtEmail.Text = "";
            txtPassword.Password = "";
            txtRePassword.Password = "";

            UsernamePlaceholder.Visibility = Visibility.Visible;
            EmailPlaceholder.Visibility = Visibility.Visible;
            PasswordPlaceholder.Visibility = Visibility.Visible;
            RePasswordPlaceholder.Visibility = Visibility.Visible;
        }
        // Function to validate email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool _isPasswordVisible;
        private void ShowHideButton_Click(object sender, RoutedEventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;

            if (_isPasswordVisible)
            {
                txtPassword.Visibility = Visibility.Collapsed;
                txtVisiblePassword1.Visibility = Visibility.Visible;
                txtVisiblePassword1.Text = txtPassword.Password;
                ShowHideImage.Source = new BitmapImage(new Uri("https://cdn-icons-png.flaticon.com/512/25/25186.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                txtPassword.Visibility = Visibility.Visible;
                txtVisiblePassword1.Visibility = Visibility.Collapsed;
                txtPassword.Password = txtVisiblePassword1.Text;
                ShowHideImage.Source = new BitmapImage(new Uri("https://static-00.iconduck.com/assets.00/eye-closed-icon-512x459-ivg8wjcx.png", UriKind.RelativeOrAbsolute));
            }
        }
        private bool _isPasswordVisible2;

        private void ShowHideButton_Click2(object sender, RoutedEventArgs e)
        {
            _isPasswordVisible2 = !_isPasswordVisible2;

            if (_isPasswordVisible2)
            {
                txtRePassword.Visibility = Visibility.Collapsed;
                txtVisibleRePassword2.Visibility = Visibility.Visible;
                txtVisibleRePassword2.Text = txtRePassword.Password;
                ShowHideImage2.Source = new BitmapImage(new Uri("https://cdn-icons-png.flaticon.com/512/25/25186.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                txtRePassword.Visibility = Visibility.Visible;
                txtVisibleRePassword2.Visibility = Visibility.Collapsed;
                txtRePassword.Password = txtVisibleRePassword2.Text;
                ShowHideImage2.Source = new BitmapImage(new Uri("https://static-00.iconduck.com/assets.00/eye-closed-icon-512x459-ivg8wjcx.png", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
