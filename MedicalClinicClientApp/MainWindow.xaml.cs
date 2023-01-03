using MedicalClinicClientApp.HttpMedClient;
using MedicalClinicClientApp.NotificationManagement;
using MedicalClinicClientApp.Session;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedicalClinicClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var clients = await MedClinicHttpClient.GetClients();
            if (clients != null)
            {
                MyClient.Client = clients.FirstOrDefault(x => x.Password == passwordInput.Text && x.Login == loginInput.Text);
                if(MyClient.Client != null)
                {
                    Notification.Show("Авторизація успішна", "Ви увійшли в свій аккаунт", Notifications.Wpf.NotificationType.Success, 0, 0, 10);

                    ClinicSystemWindow clinicSystemWindow = new ClinicSystemWindow();
                    clinicSystemWindow.Show();
                    Hide();
                }
                else
                {
                    Notification.Show("Помилка авторизації", "Пароль чи логін невірні", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                }
            }
            else
            {
                Notification.Show("Помилка авторизації", "Пароль чи логін невірні", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void PackIconEntypo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PackIconBootstrapIcons_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
