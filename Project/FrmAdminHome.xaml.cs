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
    /// Interaction logic for FrmHome.xaml
    /// </summary>
    public partial class FrmAdminHome : Window
    {
        private int userId;
        private UserManagermentContext userManagermentContext = new UserManagermentContext();

        public FrmAdminHome(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadUserData();
            LoadStatistics();
        }

        private void LoadUserData()
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
                    ImgAvatar.Source = new BitmapImage(new Uri("https://t4.ftcdn.net/jpg/05/49/98/39/360_F_549983970_bRCkYfk0P6PP5fKbMhZMIb07mCJ6esXL.jpg", UriKind.Relative));
                }
            }

        }

        private void LoadStatistics()
        {
            // Example: Load and display statistics
            int totalMembers = userManagermentContext.Users.Count();
            int totalAdmins = userManagermentContext.Users.Count(u => u.RoleId == 1);
            int totalCustomers = userManagermentContext.Users.Count(u => u.RoleId == 2);
            int totalManagers = userManagermentContext.Users.Count(u => u.RoleId == 3);

            // Update the TextBlocks
            tbTotalMembers.Text = totalMembers.ToString();
            tbTotalAdmins.Text = totalAdmins.ToString();
            tbTotalCustomers.Text = totalCustomers.ToString();
            tbTotalManagers.Text = totalManagers.ToString();
            tbTotalProducts.Text = "Chua co san pham";
            tbNewProducts.Text = "Chua co san pham";
        }



        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {

        }



        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            FrmSignIn frmSignIn = new FrmSignIn();
            frmSignIn.Show();
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

        private void btn_MyProfile_Click(object sender, RoutedEventArgs e)
        {
            FrmMyProfile frmProfile = new FrmMyProfile(userId);
            frmProfile.Show();
            this.Close();
        }
    }
}
