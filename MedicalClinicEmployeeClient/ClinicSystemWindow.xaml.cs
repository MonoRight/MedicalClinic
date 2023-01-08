using MedicalClinicEmployeeClient.HttpMedClient;
using MedicalClinicEmployeeClient.Model;
using MedicalClinicEmployeeClient.NotificationManagement;
using MedicalClinicEmployeeClient.Session;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MedicalClinicEmployeeClient
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nameLabel.Content = MyDoctor.Doctor.Name;
            surnameLabel.Content = MyDoctor.Doctor.Surname;
            middleNameLabel.Content = MyDoctor.Doctor.MiddleName;
            workDaysLabel.Content = MyDoctor.Doctor.WorkDays;
            workPositionLabel.Content = MyDoctor.Doctor.WorkPosition;
            startWorkLabel.Content = MyDoctor.Doctor.StartWorkHour;
            endWorkLabel.Content = MyDoctor.Doctor.EndWorkHour;
        }

        private void PackIconBootstrapIconsLogout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyDoctor.Doctor = null;

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }

        private void PackIconBootstrapIcons_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void PackIconEntypo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void ButtonGetClients_Click(object sender, RoutedEventArgs e)
        {
            clientsTextBlock.Text = string.Empty;
            var clients = await MedClinicHttpClient.GetClients();
            var comments = await MedClinicHttpClient.GetComments();

            string text = "";
            int i = 1;

            foreach (var client in clients)
            {
                text += $"=======Клієнт № {i}=======\n   Ім'я: {client.Name},\n   Прізвище: {client.Surname},\n   " +
                    $"По-батькові: {client.MiddleName},\n   Електронна пошта: {client.Email},\n   Номер телефону: {client.TelephoneNumber},\n   День народження: {client.BirthDate}\n";
                
                var commentForClient = comments.Where(x => x.ClientId == client.Id);
                if (commentForClient.Count() > 0)
                {
                    text += "============Всі відгуки============\n";
                    foreach (var comment in commentForClient)
                    {
                        text += $"      Відгук: {comment.Text}\n      Оцінка: {comment.Mark}\n\n";
                    }
                }

                text += "\n\n";
                i++;
            }
            clientsTextBlock.Text = text;
        }

        private async void ButtonGetRecords_Click(object sender, RoutedEventArgs e)
        {
            recordsTextBlock.Text = string.Empty;
            var records = await MedClinicHttpClient.GetRecords();
            var clients = await MedClinicHttpClient.GetClients();

            records = records.Where(x => x.DoctorId == MyDoctor.Doctor.Id).ToList();
            if (records.Count == 0)
            {
                Notification.Show("Записи", "До вас вас не було записів", Notifications.Wpf.NotificationType.Information, 0, 0, 10);
                recordsTextBlock.Text = "Записи відсутні";
                return;
            }

            string text = "";
            int i = 1;

            foreach (var record in records)
            {
                text += $"=======Запис № {i}=======\n   Дата: {record.Date},\n   " +
                    $"Ім'я: {clients.First(x => x.Id == record.ClientId).Name},\n   Прізвище: {clients.First(x => x.Id == record.ClientId).Surname},\n   " +
                    $"Електронна пошта: {clients.First(x => x.Id == record.ClientId).Email},\n   Номер телефону: {clients.First(x => x.Id == record.ClientId).TelephoneNumber}\n\n";
                i++;
            }
            recordsTextBlock.Text = text;
        }

        private void ButtonAddVisit_Click(object sender, RoutedEventArgs e)
        {
            AddVisitWindow addVisitWindow = new AddVisitWindow();
            addVisitWindow.Show();
        }

        private async void ButtonShowComments_Click(object sender, RoutedEventArgs e)
        {
            commentsTextBlock.Text = string.Empty;

            var clients = await MedClinicHttpClient.GetClients();
            var comments = await MedClinicHttpClient.GetComments();

            comments = comments.Where(x => x.DoctorId == MyDoctor.Doctor.Id).ToList();

            if(comments.Count == 0)
            {
                Notification.Show("Відгуки", "У вас відсутні відгуки", Notifications.Wpf.NotificationType.Information, 0, 0, 10);
                commentsTextBlock.Text = "Відгуки відсутні";
                markLabel.Content = "Мій середній рейтинг: -";
                return;
            }

            string text = "";
            int i = 1;
            double avgMark = comments.Average(x => x.Mark);

            foreach (var comment in comments)
            {
                text += $"=======Відгук № {i}=======\n   Ім'я: {clients.First(x => x.Id == comment.ClientId).Name},\n   Прізвище: {clients.First(x => x.Id == comment.ClientId).Surname}\n   " +
                    $"Відгук: {comment.Text},\n   Оцінка: {comment.Mark}\n\n";
                i++;
            }

            markLabel.Content = "Мій середній рейтинг: " + avgMark;
            commentsTextBlock.Text = text;
        }

        private async void ButtonShowVisits_Click(object sender, RoutedEventArgs e)
        {
            List<Visit> visits;
            List<Client> clients;

            if(clientNameInput.Text != "" && clientSurnameInput.Text != "")
            {
                var clientsByNameSurname = await MedClinicHttpClient.GetClients();
                var visitsClient = await MedClinicHttpClient.GetVisits();

                clients = clientsByNameSurname.ToList();
                Client client = clientsByNameSurname.FirstOrDefault(x => x.Name == clientNameInput.Text && x.Surname == clientSurnameInput.Text);

                if(client == null)
                {
                    Notification.Show("Відсутній клієнт", "Клієнт з заданим ім'ям та прізвищем відсутній в системі", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                    return;
                }

                visits = visitsClient.Where(x => x.DoctorId == MyDoctor.Doctor.Id && x.ClientId == client.Id).ToList();

                if(visits.Count == 0)
                {
                    Notification.Show("Відсутні прийоми", "Клієнт з заданим ім'ям та прізвищем не мав візитів до вас", Notifications.Wpf.NotificationType.Information, 0, 0, 10);
                    visitsTextBlock.Text = "Візити відсутні";
                    return;
                }
            }
            else
            {
                var allClients = await MedClinicHttpClient.GetClients();
                var allVisits = await MedClinicHttpClient.GetVisits();

                clients = allClients.ToList();
                visits = allVisits.Where(x => x.DoctorId == MyDoctor.Doctor.Id).ToList();

                if(visits.Count == 0)
                {
                    Notification.Show("Відсутні прийоми", "У вас ще не було прийомів", Notifications.Wpf.NotificationType.Information, 0, 0, 10);
                    visitsTextBlock.Text = "Візити відсутні";
                    return;
                }
            }

            string text = "";
            int i = 1;

            var anamneses = await MedClinicHttpClient.GetAnamneses();

            foreach (var visit in visits)
            {
                text += $"=======Візит № {i}=======\n   Ім'я: {clients.First(x => x.Id == visit.ClientId).Name},\n   Прізвище: {clients.First(x => x.Id == visit.ClientId).Surname},\n   Дата: {visit.Date},\n   Захворювання: {visit.Illness},\n   Скарги: {visit.Complaint},\n   Аналізи: {visit.Analysis},   " +
                    $"\n   Ліки: {visit.Medicines},\n   Рекомендації: {visit.Recomendations},\n      Анамнез:\n         Локалізація: {anamneses.First(x => x.Id == visit.AnamnesId).Localisation},\n      " +
                    $"   Дата початку: {anamneses.First(x => x.Id == visit.AnamnesId).StartDate},\n         Стадія розвитку: {anamneses.First(x => x.Id == visit.AnamnesId).DevelopmentRate},\n      " +
                    $"   Рівень болю: {anamneses.First(x => x.Id == visit.AnamnesId).PainRate},\n   Ціна: {visit.Price}\n\n";
                i++;
            }

            visitsTextBlock.Text = text;
        }
    }
}
