using MedicalClinicClientApp.HttpMedClient;
using MedicalClinicClientApp.Model;
using MedicalClinicClientApp.NotificationManagement;
using MedicalClinicClientApp.Session;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MedicalClinicClientApp
{
    /// <summary>
    /// Interaction logic for DeleteCommentWindow.xaml
    /// </summary>
    public partial class DeleteCommentWindow : Window
    {
        public DeleteCommentWindow()
        {
            InitializeComponent();
        }

        private void PackIconBootstrapIcons_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void PackIconEntypo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private async void ButtonDeleteComment_Click(object sender, RoutedEventArgs e)
        {
            if(deleteInput.Text == "")
            {
                Notification.Show("Пусте поле", "Поле номеру відгуку повинне бути заповнене", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
            }
            else
            {
                int num;
                try
                {
                    num = Convert.ToInt32(deleteInput.Text);
                }
                catch
                {
                    Notification.Show("Помилка формату", "Номер відгуку повинен бути цілим числом", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                    return;
                }

                var comments = await MedClinicHttpClient.GetComments();

                comments = comments.Where(x => x.ClientId == MyClient.Client.Id).ToList();

                Comment comment;
                try
                {
                    comment = comments[num - 1];
                }
                catch (Exception)
                {
                    Notification.Show("Відсутній відгук", "Відгук відсутній за заданим номером", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                    return;
                }

                await MedClinicHttpClient.DeleteComment(comment);
            }
        }
    }
}
