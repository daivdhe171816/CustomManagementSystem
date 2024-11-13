using Project.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Project
{
    /// <summary>
    /// Interaction logic for FrmUpdateAdmin.xaml
    /// </summary>
    public partial class FrmUpdateAdminAndUSer : Window
    {
        private int userId;
        private UserManagermentContext userManagermentContext = new UserManagermentContext();

        public FrmUpdateAdminAndUSer(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadData();
        }


        private void LoadData()
        {
            var userProfile = userManagermentContext.UserProfiles.FirstOrDefault(up => up.UserId == this.userId);
            var user = userManagermentContext.Users.FirstOrDefault(u => u.UserId == this.userId);

            if (userProfile != null)
            {
                txtName.Text = userProfile.UserName ?? string.Empty;
                cboSex.Text = userProfile.Sex ?? string.Empty;
                txtPhone.Text = userProfile.PhoneNumber ?? string.Empty;
                txtCity.Text = userProfile.City ?? string.Empty;
                txtWard.Text = userProfile.Ward ?? string.Empty;
                txtDistrict.Text = userProfile.District ?? string.Empty;
                txtUsername.Text = user?.UserName ?? string.Empty;
                txtEmail.Text = user?.Email ?? string.Empty;
                cboRole.SelectedIndex = 0;

                // Load avatar image
                if (!string.IsNullOrEmpty(userProfile.Avatar))
                {
                    try
                    {
                        var avatarUri = new Uri(userProfile.Avatar, UriKind.RelativeOrAbsolute);

                        // Check if the URI is absolute and valid
                        if (avatarUri.IsAbsoluteUri && (avatarUri.Scheme == Uri.UriSchemeHttp || avatarUri.Scheme == Uri.UriSchemeHttps || avatarUri.Scheme == Uri.UriSchemeFile))
                        {
                            imgAvatar.Source = new BitmapImage(avatarUri);
                        }
                        else
                        {
                            // Handle relative URI or invalid scheme
                            imgAvatar.Source = new BitmapImage(new Uri("default_avatar.png", UriKind.Relative));
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions and set a default image
                        imgAvatar.Source = new BitmapImage(new Uri("default_avatar.png", UriKind.Relative));
                        Console.WriteLine($"Error loading avatar: {ex.Message}");
                    }
                }
                else
                {
                    imgAvatar.Source = new BitmapImage(new Uri("default_avatar.png", UriKind.Relative));
                }
            }
            else
            {
                // Set default values if userProfile is null
                txtName.Text = string.Empty;
                cboSex.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtWard.Text = string.Empty;
                txtDistrict.Text = string.Empty;
                txtUsername.Text = user.UserName;
                txtEmail.Text = user.Email;
                imgAvatar.Source = new BitmapImage(new Uri("default_avatar.png", UriKind.Relative));
            }

            var selectedRoleId = user?.RoleId;

            if (selectedRoleId == 1)
            {
                // Load all roles
                var roles = userManagermentContext.Roles.ToList();
                cboRole.ItemsSource = roles;
            }
            else
            {
                // Load only the user's current role
                var userRole = userManagermentContext.Roles.Where(r => r.RoleId == selectedRoleId).ToList();
                cboRole.ItemsSource = userRole;
            }

            cboRole.DisplayMemberPath = "RoleName";
            cboRole.SelectedValuePath = "RoleId";
            cboRole.SelectedValue = selectedRoleId;
        }




        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            var userProfile = userManagermentContext.UserProfiles.FirstOrDefault(up => up.UserId == this.userId);
            var user = userManagermentContext.Users.FirstOrDefault(u => u.UserId == this.userId);
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(cboSex.Text) ||
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtWard.Text) ||
                string.IsNullOrWhiteSpace(txtDistrict.Text) ||
                imgAvatar.Source == null ||
                cboRole.SelectedIndex == -1)

            {
                System.Windows.MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtName.Text == userProfile.UserName
                && txtUsername.Text == user.UserName
                && txtEmail.Text == user.Email
                && txtPhone.Text == userProfile.PhoneNumber
                && cboSex.Text == userProfile.Sex
                && txtCity.Text == userProfile.City
                && txtWard.Text == userProfile.Ward
                && txtDistrict.Text == userProfile.District
                && imgAvatar.Source.ToString() == userProfile.Avatar
                && ((Role)cboRole.SelectedItem).RoleId == user.RoleId
                )

            {
                MessageBox.Show("Chua co thay doi gi, khong update duoc dau....");

                return;
            }

            if (!txtPhone.Text.All(char.IsDigit))
            {
                System.Windows.MessageBox.Show("Please enter a valid phone number containing only numeric characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            var existingPhone = userManagermentContext.UserProfiles.FirstOrDefault(up => up.PhoneNumber.Equals(txtPhone.Text) && up.UserId != this.userId);


            if (existingPhone != null)
            {
                MessageBox.Show("Phone already exists.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Update user profile
            
            if (userProfile != null)
            {
                userProfile.UserName = txtName.Text.Trim();
                userProfile.Sex = cboSex.Text.Trim();
                userProfile.PhoneNumber = txtPhone.Text.Trim();
                userProfile.City = txtCity.Text.Trim();
                userProfile.Ward = txtWard.Text.Trim();
                userProfile.District = txtDistrict.Text.Trim();
                var selectedRole = (Role)cboRole.SelectedItem;
                user.RoleId = selectedRole.RoleId;

                // Update avatar if changed
                if (imgAvatar.Source != null)
                {
                    userProfile.Avatar = imgAvatar.Source.ToString();
                }

                userManagermentContext.SaveChanges();
                System.Windows.MessageBox.Show("User Profile updated successfully!");
            }
            else
            {
                userProfile = new UserProfile
                {
                    UserId = this.userId,
                    UserName = txtName.Text.Trim(),
                    Sex = cboSex.Text.Trim(),
                    PhoneNumber = txtPhone.Text.Trim(),
                    City = txtCity.Text.Trim(),
                    Ward = txtWard.Text.Trim(),
                    District = txtDistrict.Text.Trim(),
                    Address = txtCity.Text + ", " + txtWard + ", " + txtDistrict +".",
                    Avatar = imgAvatar.Source != null ? imgAvatar.Source.ToString() : null
                };

                userManagermentContext.UserProfiles.Add(userProfile);
                userManagermentContext.SaveChanges();
                System.Windows.MessageBox.Show("User Profile declared successfully!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Load selected image into imgAvatar
                imgAvatar.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var user = userManagermentContext.Users.FirstOrDefault(u => u.UserId == this.userId);
            var userProfile = userManagermentContext.UserProfiles.FirstOrDefault(u => u.UserId == this.userId);

            if (user != null)
            {
                if (user.RoleId == 1)
                {
                    FrmMyProfile frmMyProfile = new FrmMyProfile(userId);
                    frmMyProfile.Show();
                    this.Close();
                }
                else if (user.RoleId == 2 && userProfile != null)
                {
                    FrmProfileForUser frmProfileForUser = new FrmProfileForUser(userId);
                    frmProfileForUser.Show();
                    this.Close();
                }
                else if (userProfile == null)
                {
                    MessageBox.Show("Enter your information before exiting!");
                }
                else
                {
                    FrmSignIn signIn = new FrmSignIn();
                    signIn.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("User not found.");
            }
        }
    }
}
