using MedicalClinicClientApp.HttpMedClient;
using MedicalClinicClientApp.Model;
using MedicalClinicClientApp.NotificationManagement;
using MedicalClinicClientApp.Session;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ClinicSystemWindow.xaml
    /// </summary>
    public partial class ClinicSystemWindow : Window
    {
        public ClinicSystemWindow()
        {
            InitializeComponent();
        }

        private void PackIconEntypo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PackIconBootstrapIcons_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private async void ButtonGetDoctros_Click(object sender, RoutedEventArgs e)
        {
            doctorsTextBlock.Text = string.Empty;
            var doctors = await MedClinicHttpClient.GetDoctors();

            string text = "";
            int i = 1;

            foreach(var doctor in doctors)
            {
                text += $"Лікар № {i}\nПосада: {doctor.WorkPosition}, Ім'я: {doctor.Name}, Прізвище: {doctor.Surname}, По-батькові: {doctor.MiddleName}, Дні роботи: {doctor.WorkDays}, Початок роботи: {doctor.StartWorkHour}, Кінець роботи: {doctor.EndWorkHour}\n\n";
                i++;
            }
            doctorsTextBlock.Text = text;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nameLabel.Content = MyClient.Client.Name;
            surnameLabel.Content = MyClient.Client.Surname;
            middleNameLabel.Content = MyClient.Client.MiddleName;
            emailLabel.Content = MyClient.Client.Email;
            telLabel.Content = MyClient.Client.TelephoneNumber;
            birthDateLabel.Content = MyClient.Client.BirthDate;
        }

        private async void ButtonSaveEditClient_Click(object sender, RoutedEventArgs e)
        {
            if(emailInput.Text.Equals(string.Empty) || telInput.Text.Equals(string.Empty) || passwordInput.Text.Equals(string.Empty))
            {
                Notification.Show("Пусті поля", "Всі поля повинні бути заповненні (якщо не хочете змінювати значення - продублюйте попереднє", Notifications.Wpf.NotificationType.Warning, 0, 0, 10);
            }
            else
            {
                MyClient.Client.Password = passwordInput.Text;
                MyClient.Client.TelephoneNumber = telInput.Text;
                MyClient.Client.Email = emailInput.Text;

                await MedClinicHttpClient.EditClient(MyClient.Client);

                emailLabel.Content = MyClient.Client.Email;
                telLabel.Content = MyClient.Client.TelephoneNumber;
            }
        }

        private async void ButtonCreateVisit_Click(object sender, RoutedEventArgs e)
        {
            DateTime visitDate; 
            try
            {
                visitDate = DateTime.ParseExact(recordDateInput.Text, "yyyy-MM-dd HH:mm", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (ArgumentNullException)
            {
                Notification.Show("Контент дати", "Дата пуста!", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                return;
            }
            catch (FormatException)
            {
                Notification.Show("Формат дати", "Введенна дата не відповідає формату!", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                return;
            }

            var doctors = await MedClinicHttpClient.GetDoctors();

            doctors = doctors.Where(x => x.Name == doctorNameInput.Text && x.Surname == doctorSurnameInput.Text).ToList();

            if(doctors.Count == 0)
            {
                Notification.Show("Лікар відсутній", "Лікаря за заданим прізвищем та ім'ям не існує в системі, перевірте правильність введених даних", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                return;
            }

            Record record = new Record()
            {
                ClientId = MyClient.Client.Id,
                DoctorId = doctors.First().Id,
                Date = visitDate
            };

            await MedClinicHttpClient.AddRecord(record);
        }
    }
}
