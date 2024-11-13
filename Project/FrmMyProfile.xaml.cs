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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Project
{
    /// <summary>
    /// Interaction logic for FrmProfile.xaml
    /// </summary>
    public partial class FrmMyProfile : Window
    {
        UserManagermentContext userManagermentContext = new UserManagermentContext();
        private int userId;
        public FrmMyProfile(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadData();
        }

        private void LoadData()
        {
            var user = userManagermentContext.Users.FirstOrDefault(u => u.UserId == this.userId);
            var userProfile = userManagermentContext.UserProfiles
                                            .FirstOrDefault(up => up.UserId == this.userId);

            if (userProfile != null)
            {
                tbHello.Text = $"{userProfile.UserName}";

                if (!string.IsNullOrEmpty(userProfile.Avatar))
                {
                    try
                    {
                        var avatarUri = new Uri(userProfile.Avatar, UriKind.RelativeOrAbsolute);

                        // Check if the URI is absolute and valid
                        if (avatarUri.IsAbsoluteUri && (avatarUri.Scheme == Uri.UriSchemeHttp || avatarUri.Scheme == Uri.UriSchemeHttps || avatarUri.Scheme == Uri.UriSchemeFile))
                        {
                            ImgAvatar.Source = new BitmapImage(avatarUri);
                        }
                        else
                        {
                            // Handle relative URI or invalid scheme
                            ImgAvatar.Source = new BitmapImage(new Uri("default_avatar.png", UriKind.Relative));
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions and set a default image
                        ImgAvatar.Source = new BitmapImage(new Uri("default_avatar.png", UriKind.Relative));
                        Console.WriteLine($"Error loading avatar: {ex.Message}");
                    }
                }
                else
                {
                    ImgAvatar.Source = new BitmapImage(new Uri("default_avatar.png", UriKind.Relative));
                }
            }

            if (userProfile == null)
            {
                MessageBox.Show("Profile not declared!!! Please update now.");
            }

            txtName.Text = userProfile.UserName;
            txtUsername.Text = user.UserName;
            txtEmail.Text = user.Email;
            txtPassword.Text = user.PassWord;
            txtPhone.Text = userProfile.PhoneNumber;
            var roleName = userManagermentContext.Roles
                                              .Where(r => r.RoleId == user.RoleId)
                                              .Select(r => r.RoleName)
                                              .FirstOrDefault();

            txtRole.Text = roleName ?? "N/A"; // Display role name or "N/A" if not found
            txtSex.Text = userProfile.Sex;
            txtAddress.Text = $"{userProfile.City}, {userProfile.Ward}, {userProfile.District}.";
        }



        private void btn_MyProfile_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            FrmAdminHome frmAdminHome = new FrmAdminHome(userId);
            frmAdminHome.Show();
            this.Close();
        }

        private void btn_Customer_Click(object sender, RoutedEventArgs e)
        {
            CustomerControl customerControl = new CustomerControl(userId);
            customerControl.Show();
            this.Close();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            FrmAdd frmAdd = new FrmAdd(userId);
            frmAdd.Show();
            this.Close();

        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            FrmSignIn frmSignin = new FrmSignIn();
            frmSignin.Show();
            this.Close();
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            FrmUpdateAdminAndUSer frmUpdate = new FrmUpdateAdminAndUSer(userId);
            frmUpdate.Show();
            this.Close();
        }

        private void btn_Changpass_Click(object sender, RoutedEventArgs e)
        {
            FrmChangpass frmChangpass = new FrmChangpass(userId);
            frmChangpass.Show();
            this.Close();
        }
    }
}
