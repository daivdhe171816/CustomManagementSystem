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
using Microsoft.Win32;
namespace Project
{
    /// <summary>
    /// Interaction logic for FrmUpdate.xaml
    /// </summary>
    public partial class FrmAdd : Window
    {
        private int userId;
        UserManagermentContext userManagermentContext = new UserManagermentContext();
        public FrmAdd(int userId)
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

            var data = userManagermentContext.Roles.ToList();
            cboRole.ItemsSource = data;
            cboRole.DisplayMemberPath = "RoleName";
            cboRole.SelectedValuePath = "RoleId";


        }

        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            FrmAdminHome frmAdminHome = new FrmAdminHome(userId);
            frmAdminHome.Show();
            this.Close();
        }


        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            FrmSignIn frmsignin = new FrmSignIn();
            frmsignin.Show();
            this.Close();
        }

        private void btnImportAvatar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                // Load selected image into imgAvatar
                imgAvatar.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text)

                )

            {
                MessageBox.Show("Please fill Username, Email, Password or All fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!txtPhone.Text.All(char.IsDigit))
            {
                System.Windows.MessageBox.Show("Please enter a valid phone number containing only numeric characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check if Email is valid and unique
            string email = txtEmail.Text.Trim();
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }
            if (userManagermentContext.Users.Any(u => u.Email == email))
            {
                MessageBox.Show("This email address is already registered.");
                return;
            }

            // Check if Username is unique
            string username = txtUsername.Text.Trim();
            if (userManagermentContext.Users.Any(u => u.UserName == username))
            {
                MessageBox.Show("This username is already taken.");
                return;
            }

            // Check if Phone number is unique (assuming PhoneNumber is unique)
            string phoneNumber = txtPhone.Text.Trim();
            if (phoneNumber != null &&
                userManagermentContext.UserProfiles.Any(up => up.PhoneNumber == phoneNumber))
            {
                MessageBox.Show("This phone number is already registered.");
                return;
            }
            if (txtUsername.Text != null &&
                txtName.Text != null &&
                txtEmail.Text != null &&
                txtPhone.Text != null &&
                txtPassword.Text != null &&
                cboSex.Text != null &&
                cboRole.Text != null &&
                txtCity.Text != null &&
                txtWard.Text != null &&
                txtDistrict.Text != null &&
                cboRole.SelectedIndex != -1

                )
            {
                User newUser = new User
                {
                    Email = txtEmail.Text,
                    UserName = txtUsername.Text.Trim(),
                    PassWord = txtPassword.Text.Trim(),
                    RoleId = (int)cboRole.SelectedValue
                };

                // Add 
                userManagermentContext.Users.Add(newUser);
                userManagermentContext.SaveChanges();

                UserProfile newProfile = new UserProfile
                {
                    UserId = newUser.UserId,
                    UserName = txtName.Text.Trim(),
                    Sex = cboSex.Text.Trim(),
                    PhoneNumber = txtPhone.Text.Trim(),
                    City = txtCity.Text.Trim(),
                    District = txtDistrict.Text.Trim(),
                    Ward = txtWard.Text.Trim(),
                    Address = txtCity.Text + "," + txtWard + "," + txtDistrict,
                    Avatar = imgAvatar.Source?.ToString()
                };

                userManagermentContext.UserProfiles.Add(newProfile);
                userManagermentContext.SaveChanges();

                MessageBox.Show("User added successfully!");
            }
            else
            {
                User newUser = new User
                {
                    Email = txtEmail.Text,
                    UserName = txtUsername.Text,
                    PassWord = txtPassword.Text,
                    RoleId = 2
                };
                userManagermentContext.Users.Add(newUser);
                userManagermentContext.SaveChanges();

                MessageBox.Show("User added successfully but not have info!");
            }

        }

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

        private void btn_Customer_Click(object sender, RoutedEventArgs e)
        {
            CustomerControl customerControl = new CustomerControl(userId);
            customerControl.Show();
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
