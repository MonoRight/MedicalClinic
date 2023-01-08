using MedicalClinicEmployeeClient.HttpMedClient;
using MedicalClinicEmployeeClient.NotificationManagement;
using MedicalClinicEmployeeClient.Session;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MedicalClinicEmployeeClient
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var doctors = await MedClinicHttpClient.GetDoctors();
            if (doctors != null)
            {
                MyDoctor.Doctor = doctors.FirstOrDefault(x => x.Password == passwordInput.Text && x.Login == loginInput.Text);
                if (MyDoctor.Doctor != null)
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
    }
}
