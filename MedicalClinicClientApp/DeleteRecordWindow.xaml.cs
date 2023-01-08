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
    /// Interaction logic for DeleteRecordWindow.xaml
    /// </summary>
    public partial class DeleteRecordWindow : Window
    {
        public DeleteRecordWindow()
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

        private async void ButtonDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (deleteInput.Text == "")
            {
                Notification.Show("Пусте поле", "Поле номеру запису повинне бути заповнене", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
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
                    Notification.Show("Помилка формату", "Номер запису повинен бути цілим числом", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                    return;
                }

                var records = await MedClinicHttpClient.GetRecords();

                records = records.Where(x => x.ClientId == MyClient.Client.Id).ToList();

                Record record;
                try
                {
                    record = records[num - 1];
                }
                catch
                {
                    Notification.Show("Відсутній запис", "Запис відсутній за заданим номером", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                    return;
                }

                if(record.Date < DateTime.Now)
                {
                    Notification.Show("Неможливо видалити запис", "НЕможливо видалити запис, який вже пройшов", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                    return;
                }

                await MedClinicHttpClient.DeleteRecord(record);
            }
        }
    }
}
