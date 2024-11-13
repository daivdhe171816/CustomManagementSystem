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
    /// Interaction logic for FrmUpdate.xaml
    /// </summary>
    public partial class FrmUpdate : Window
    {
        private int userId;
        private int adminId;
        private UserManagermentContext userManagermentContext = new UserManagermentContext();

        public FrmUpdate(int userId, int adminId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadData();
            this.adminId = adminId;
        }
        public FrmUpdate(int userId)
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
                txtName.Text = userProfile.UserName;
                cboSex.Text = userProfile.Sex;
                txtPhone.Text = userProfile.PhoneNumber;
                txtCity.Text = userProfile.City;
                txtWard.Text = userProfile.Ward;
                txtDistrict.Text = userProfile.District;
                txtUsername.Text = user.UserName;
                txtEmail.Text = user.Email;
                txtPassword.Text = user.PassWord;
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
                txtUsername.Text = user.UserName;
                txtEmail.Text = user.Email;
                txtPassword.Text = user.PassWord;
                cboRole.SelectedIndex = 0;
            }
            if (user.RoleId == 1)
            {
                var data = userManagermentContext.Roles.ToList().Where(r => r.RoleId == 1);
                cboRole.ItemsSource = data;
                cboRole.DisplayMemberPath = "RoleName";
                cboRole.SelectedValuePath = "RoleId";

                var selectedRoleId = user?.RoleId;
                cboRole.SelectedValue = selectedRoleId;
            }
            else
            {
                var data = userManagermentContext.Roles.ToList();
                cboRole.ItemsSource = data;
                cboRole.DisplayMemberPath = "RoleName";
                cboRole.SelectedValuePath = "RoleId";

                var selectedRoleId = user?.RoleId;
                cboRole.SelectedValue = selectedRoleId;
            }
        }



        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var userProfile = userManagermentContext.UserProfiles.FirstOrDefault(up => up.UserId == this.userId);
            var user = userManagermentContext.Users.FirstOrDefault(u => u.UserId == this.userId);
            var selectedRoleId = user?.RoleId;
            cboRole.SelectedValue = selectedRoleId;
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(cboSex.Text) ||
                string.IsNullOrWhiteSpace(txtCity.Text) ||
                string.IsNullOrWhiteSpace(txtWard.Text) ||
                string.IsNullOrWhiteSpace(txtDistrict.Text) ||
                imgAvatar.Source == null ||
                cboRole.SelectedIndex == -1)

            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            var existingPhone = userManagermentContext.UserProfiles.FirstOrDefault(up => up.PhoneNumber.Equals(txtPhone.Text) && up.UserId != this.userId);


            if (existingPhone != null)
            {
                MessageBox.Show("Phone already exists.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Update user profile
            if (txtName.Text == userProfile.UserName
                && txtUsername.Text == user.UserName
                && txtEmail.Text == user.Email
                && txtPhone.Text == userProfile.PhoneNumber
                && txtPassword.Text == user.PassWord
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
                CustomerControl customer = new CustomerControl(adminId);
                customer.Show();
                this.Close();

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
                    Address = txtCity.Text + ", " + txtWard + ", " + txtDistrict + ".",
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

            CustomerControl customerControl = new CustomerControl(adminId);
            customerControl.Show();
            this.Close();
        }
    }
}
