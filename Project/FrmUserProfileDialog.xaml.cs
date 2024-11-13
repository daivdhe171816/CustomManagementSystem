using Project.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Project
{
    /// <summary>
    /// Interaction logic for FrmUserProfileDialog.xaml
    /// </summary>
    public partial class FrmUserProfileDialog : Window
    {
        private readonly UserManagermentContext userManagermentContext = new UserManagermentContext();
        private readonly int userId;

        public FrmUserProfileDialog(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadData();
        }

        private void LoadData()
        {
            var user = userManagermentContext.Users.FirstOrDefault(u => u.UserId == this.userId);
            var userProfile = userManagermentContext.UserProfiles.FirstOrDefault(up => up.UserId == this.userId);

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

            if (userProfile != null)
            {
                txtName.Text = userProfile.UserName;
                txtPhone.Text = userProfile.PhoneNumber;
                txtSex.Text = userProfile.Sex;
                txtAddress.Text = $"{userProfile.City}, {userProfile.Ward}, {userProfile.District}.";
            }

            if (user != null)
            {
                txtUsername.Text = user.UserName;
                txtEmail.Text = user.Email;
                txtPassword.Text = user.PassWord;

                var roleName = userManagermentContext.Roles
                    .Where(r => r.RoleId == user.RoleId)
                    .Select(r => r.RoleName)
                    .FirstOrDefault();

                txtRole.Text = roleName ?? "N/A"; // Display role name or "N/A" if not found
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();


        }


    }
}
