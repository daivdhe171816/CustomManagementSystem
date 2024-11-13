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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        UserManagermentContext userManagermentContext = new UserManagermentContext();
        private int userId;
        public Home(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadData();
        }

        private void LoadData()
        {
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
                            ImgAvatar.Source = new BitmapImage(new Uri("https://t4.ftcdn.net/jpg/05/49/98/39/360_F_549983970_bRCkYfk0P6PP5fKbMhZMIb07mCJ6esXL.jpg", UriKind.Relative));
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions and set a default image
                        ImgAvatar.Source = new BitmapImage(new Uri("https://t4.ftcdn.net/jpg/05/49/98/39/360_F_549983970_bRCkYfk0P6PP5fKbMhZMIb07mCJ6esXL.jpg", UriKind.Relative));

                    }
                }
                else
                {
                    ImgAvatar.Source = new BitmapImage(new Uri("E:\\images.png", UriKind.Relative));
                }
            }
            else
            {

                MessageBox.Show("Profile not declared!!! Please update now.");
                FrmUpdateAdminAndUSer frmUpdateAdminAndUSer = new FrmUpdateAdminAndUSer(userId);
                frmUpdateAdminAndUSer.Show();
                this.Close();
            }
        }

        private void btn_Homepage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Cart_Click(object sender, RoutedEventArgs e)
        {

        }



        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            FrmSignIn frmSignIn = new FrmSignIn();
            frmSignIn.Show();
            this.Close();
        }

        private void btn_MyProfile_Click(object sender, RoutedEventArgs e)
        {
            var userProfile = userManagermentContext.UserProfiles
                                            .FirstOrDefault(up => up.UserId == this.userId);
            FrmProfileForUser frmProfileForUser = new FrmProfileForUser(userId);
            if (userProfile != null)
            {
                frmProfileForUser.Show();

            }
            else
            {
                MessageBox.Show("Profile not declared!!! Please update now.");
                FrmUpdateAdminAndUSer frmUpdateAdminAndUSer = new FrmUpdateAdminAndUSer(userId);
                frmUpdateAdminAndUSer.Show();
                this.Close();

            }

            this.Close();
        }
    }
}
