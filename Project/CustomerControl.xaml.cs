using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Project
{
    /// <summary>
    /// Interaction logic for CustomerControl.xaml
    /// </summary>
    public partial class CustomerControl : Window
    {
        private int currentPage = 1;
        private int itemsPerPage = 3;
        private int totalItems;
        private int totalPages;

        private UserManagermentContext userManagermentContext = new UserManagermentContext();
        private int userId;

        public CustomerControl(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                // Load user profile
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

                // Count total items
                totalItems = userManagermentContext.Users.Count(u => u.UserId != userId);

                // Calculate total pages
                totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

                // Load data for current page
                var data = userManagermentContext.Users
                                                .Where(u => u.UserId != userId)
                                                .OrderBy(u => u.UserId)
                                                .Skip((currentPage - 1) * itemsPerPage)
                                                .Take(itemsPerPage)
                                                .Select(u => new
                                                {
                                                    ID = u.UserId,
                                                    Email = u.Email,
                                                    Name = u.UserName,
                                                    Password = u.PassWord,
                                                    Role = u.Role != null ? u.Role.RoleName : "N/A",
                                                })
                                                .ToList();
                tb_Paging.Text = currentPage.ToString() + "/" + totalPages.ToString();

                dgCustomer.ItemsSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedCustomer = dgCustomer.SelectedItem;

            if (selectedCustomer != null)
            {
                dynamic customer = selectedCustomer;
                int customerId = customer.ID;

                // Find user and profile to delete
                var userToDelete = userManagermentContext.Users.FirstOrDefault(u => u.UserId == customerId);
                var userProfileToDelete = userManagermentContext.UserProfiles.FirstOrDefault(up => up.UserId == customerId);

                if (userToDelete != null)
                {
                    if (userToDelete.RoleId == 1)
                    {
                        MessageBox.Show("Cannot delete Admin!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        if (userProfileToDelete != null)
                        {
                            userManagermentContext.UserProfiles.Remove(userProfileToDelete);
                        }

                        userManagermentContext.Users.Remove(userToDelete);
                        userManagermentContext.SaveChanges();

                        LoadData();

                        MessageBox.Show("Customer deleted successfully!");
                    }
                }
                else
                {
                    MessageBox.Show("Customer not found in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadData();
                return;
            }

            var filteredData = userManagermentContext.Users
                                                     .Where(u => u.UserId != userId)
                                                     .Where(u => u.UserId.ToString().Contains(searchText) ||
                                                                 u.UserName.ToLower().Contains(searchText) ||
                                                                 u.Email.ToLower().Contains(searchText) ||
                                                                 (u.Role != null && u.Role.RoleName.ToLower().Contains(searchText)))

                                                     .Select(u => new
                                                     {
                                                         ID = u.UserId,
                                                         Email = u.Email,
                                                         Name = u.UserName,
                                                         Password = u.PassWord,
                                                         Role = u.Role != null ? u.Role.RoleName : "N/A",
                                                     })
                                                     .ToList();
            dgCustomer.ItemsSource = filteredData;
        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            var selectedCustomer = dgCustomer.SelectedItem;

            if (selectedCustomer != null)
            {
                dynamic customer = selectedCustomer;
                int customerId = customer.ID;

                FrmUpdate frmUpdate = new FrmUpdate(customerId, userId);
                frmUpdate.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a customer to update.");
            }
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

        private void btn_Profile_Click(object sender, RoutedEventArgs e)
        {
            var selectedCustomer = dgCustomer.SelectedItem;

            if (selectedCustomer != null)
            {
                dynamic customer = selectedCustomer;
                int customerId = customer.ID;

                FrmUserProfileDialog frmUserProfileDialog = new FrmUserProfileDialog(customerId);
                frmUserProfileDialog.Show();
            }
            else
            {
                MessageBox.Show("Please select a customer to show their profile.");
            }
        }

        private void btn_First_Click(object sender, RoutedEventArgs e)
        {
            currentPage = 1;
            LoadData();
        }

        private void btn_Previous_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadData();
            }
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadData();
            }
        }

        private void btn_Last_Click(object sender, RoutedEventArgs e)
        {
            currentPage = totalPages;
            LoadData();
        }

        private void btn_Dashboard_Click(object sender, RoutedEventArgs e)
        {
            FrmAdminHome frmAdminHome = new FrmAdminHome(userId);
            frmAdminHome.Show();
            this.Close();
        }

        private void btn_HomePage_Click(object sender, RoutedEventArgs e)
        {
            FrmSignIn frmsignin = new FrmSignIn();
            frmsignin.Show();
            this.Close();
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            FrmSignIn frmsignin = new FrmSignIn();
            frmsignin.Show();
            this.Close();
        }

        private void btn_Logout_GotFocus(object sender, RoutedEventArgs e)
        {
            // Handle focus event if needed
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(txtNumOfCustomer.Text, out int result))
            {
                itemsPerPage = result;
                currentPage = 1;
                LoadData();
            }
            else
            {
                MessageBox.Show("Invalid number of items per page. Please enter a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
