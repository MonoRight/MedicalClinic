using MedicalClinicClientApp.HttpMedClient;
using MedicalClinicClientApp.Model;
using MedicalClinicClientApp.NotificationManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MedicalClinicClientApp
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(passwordInput.Text.Equals(string.Empty) ||
                loginInput.Text.Equals(string.Empty) ||
                nameInput.Text.Equals(string.Empty) ||
                surnameInput.Text.Equals(string.Empty) ||
                middleNameInput.Text.Equals(string.Empty) ||
                birthDateInput.Text.Equals(string.Empty) ||
                emailInput.Text.Equals(string.Empty))
            {
                Notification.Show("Пусті поля!", "Всі поля повинні бути заповненні!", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
            }
            else
            {
                Client client;
                try
                {
                    DateTime birthDate = DateTime.ParseExact(birthDateInput.Text, "yyyy-MM-dd",
                               System.Globalization.CultureInfo.InvariantCulture);

                    client = new Client()
                    {
                        Login = loginInput.Text,
                        Password = passwordInput.Text,
                        Name = nameInput.Text,
                        Surname = surnameInput.Text,
                        MiddleName = middleNameInput.Text,
                        Email = emailInput.Text,
                        TelephoneNumber = telNumInput.Text,
                        BirthDate = birthDate
                    };

                    _ = MedClinicHttpClient.AddClient(client);

                }
                catch (ArgumentNullException)
                {
                    Notification.Show("Контент дати", "Дата пуста!", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                }
                catch (FormatException)
                {
                    Notification.Show("Формат дати", "Введенна дата не відповідає формату!", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void PackIconEntypo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void PackIconBootstrapIcons_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
