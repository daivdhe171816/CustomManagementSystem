
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
    /// Interaction logic for FrmForgotPass.xaml
    /// </summary>
    public partial class FrmForgotPass : Window
    {
        UserManagermentContext userManagermentContext = new UserManagermentContext();
        public FrmForgotPass()
        {
            InitializeComponent();
        }



        private void btn_Show_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string username = txtUsername.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please enter Email, Username, and Phone.");
                return;
            }

            var user = userManagermentContext.Users.FirstOrDefault(u => u.Email == email && u.UserName == username);
            var userProfile = userManagermentContext.UserProfiles.FirstOrDefault(up => up.PhoneNumber == phone);

            if (user != null && userProfile != null)
            {
                string password = GetUserPassword(user);

                MessageBox.Show($"Your password is: {password}");
            }
            else
            {
                MessageBox.Show("Email, Username, or Phone is incorrect.");
            }
        }

        private string GetUserPassword(User user)
        {
            // Example: Replace with your actual password retrieval logic
            return user.PassWord;
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailPlaceholder.Visibility = string.IsNullOrEmpty(txtEmail.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            NamePlaceholder.Visibility = string.IsNullOrEmpty(txtUsername.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            PhonePlaceholder.Visibility = string.IsNullOrEmpty(txtPhone.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            FrmSignIn frmSignIn = new FrmSignIn();
            this.Close();
        }
    }


}
