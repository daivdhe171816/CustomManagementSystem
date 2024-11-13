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
    /// Interaction logic for FrmChangpass.xaml
    /// </summary>
    public partial class FrmChangpass : Window
    {
        private int userId;
        UserManagermentContext userManagermentContext = new UserManagermentContext();
        public FrmChangpass(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void txtOldPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            OldPassPlaceholder.Visibility = string.IsNullOrEmpty(txtOldPass.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void txtNewPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewPassPlaceHolder.Visibility = string.IsNullOrEmpty(txtNewPass.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void txtReNewPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReNewPassPlaceHolder.Visibility = string.IsNullOrEmpty(txtReNewPass.Text) ? Visibility.Visible: Visibility.Hidden;
        }

        private void btn_Change_Click(object sender, RoutedEventArgs e)
        {
            var user = userManagermentContext.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if(txtNewPass.Text.EndsWith(txtOldPass.Text, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("New Password must different Old Password.");
                return;
            }
            if(txtNewPass.Text != txtReNewPass.Text)
            {
                MessageBox.Show("New Password and Re New Password not match. Enter again.");
                return ;
            }
            if (txtOldPass.Text.EndsWith(user.PassWord))
            {
                user.PassWord = txtNewPass.Text;
                MessageBox.Show("Change password succesfull!");
            } else
            {
                MessageBox.Show("Old Password is incorrect!");
                return ;
            }
            userManagermentContext.SaveChanges();

            

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var user = userManagermentContext.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (user.RoleId == 1)
            {
                FrmMyProfile frmMyProfile = new FrmMyProfile(userId);
                frmMyProfile.Show();
                this.Close();
            } else if(user.RoleId == 2)
            {
                FrmProfileForUser frmProfileForUser = new FrmProfileForUser(userId);
                frmProfileForUser.Show();
                this.Close();
            }

        }
    }
}
