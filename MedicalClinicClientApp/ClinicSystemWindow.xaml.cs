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


        private void PackIconBootstrapIconsLogout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MyClient.Client = null;

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
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
            var comments = await MedClinicHttpClient.GetComments();

            string text = "";
            int i = 1;

            foreach (var doctor in doctors)
            {
                text += $"=======Лікар № {i}=======\n   Спеціалізація: {doctor.WorkPosition},\n   Ім'я: {doctor.Name},\n   Прізвище: {doctor.Surname},\n   " +
                    $"По-батькові: {doctor.MiddleName},\n   Дні роботи: {doctor.WorkDays},\n   Початок роботи: {doctor.StartWorkHour},\n   Кінець роботи: {doctor.EndWorkHour}\n";
                var commentForDoctor = comments.Where(x => x.DoctorId == doctor.Id);
                if (commentForDoctor.Count() > 0)
                {
                    foreach (var comment in commentForDoctor)
                    {
                        text += $"============Відгуки============\n      Відгук: {comment.Text}\n      Оцінка: {comment.Mark}";
                    }
                    text += "\n";
                }

                text += "\n\n";
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
            if (emailInput.Text.Equals(string.Empty) || telInput.Text.Equals(string.Empty) || passwordInput.Text.Equals(string.Empty))
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

        private async void ButtonCreateRecord_Click(object sender, RoutedEventArgs e)
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

            if (visitDate < DateTime.Now)
            {
                Notification.Show("Значення дати", "Записатися можна тільки на майбутню дату", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                return;
            }

            var doctors = await MedClinicHttpClient.GetDoctors();

            doctors = doctors.Where(x => x.Name == doctorNameInput.Text && x.Surname == doctorSurnameInput.Text).ToList();

            if (doctors.Count == 0)
            {
                Notification.Show("Лікар відсутній", "Лікаря за заданим прізвищем та ім'ям не існує в системі, перевірте правильність введених даних", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                return;
            }

            int dayOfWeek = (int)visitDate.DayOfWeek;

            if (!doctors.First().WorkDays.Contains(dayOfWeek.ToString()))
            {
                Notification.Show("Лікар буде відсутній", "На цей день у лікаря вихідний, будь ласка, оберіть інший день тижня", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                return;
            }

            if (doctors.First().StartWorkHour > visitDate.Hour || doctors.First().EndWorkHour <= visitDate.Hour)
            {
                Notification.Show("Лікар буде відсутній", "Лікар буде відсутній в цей час, будь ласка, перевірте робочі години прийому лікаря", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                return;
            }

            var recordsOnDay = await MedClinicHttpClient.GetRecords();
            recordsOnDay = recordsOnDay
                .Where(x => x.DoctorId == doctors.First().Id && x.Date.Day == visitDate.Day && x.Date.Month == visitDate.Month && x.Date.Year == visitDate.Year)
                .ToList();

            TimeSpan rez;
            foreach (var recordOnDay in recordsOnDay)
            {
                rez = recordOnDay.Date - visitDate;
                if (Math.Abs(rez.TotalMinutes) < 30)
                {
                    Notification.Show("Лікар буде зайнятий", "В рамках цього часу (30 хв.) вже є запис до лікаря, будь ласка, оберіть інший час", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                    return;
                }
            }

            Record record = new Record()
            {
                ClientId = MyClient.Client.Id,
                DoctorId = doctors.First().Id,
                Date = visitDate
            };

            await MedClinicHttpClient.AddRecord(record);
        }

        private async void ButtonGetRecords_Click(object sender, RoutedEventArgs e)
        {
            recordsTextBlock.Text = string.Empty;
            var records = await MedClinicHttpClient.GetRecords();
            var doctors = await MedClinicHttpClient.GetDoctors();

            records = records.Where(x => x.ClientId == MyClient.Client.Id).ToList();
            if (records.Count == 0)
            {
                Notification.Show("Записи", "У вас не було записів", Notifications.Wpf.NotificationType.Information, 0, 0, 10);
                recordsTextBlock.Text = "Записи відсутні";
                return;
            }

            string text = "";
            int i = 1;

            foreach (var record in records)
            {
                text += $"=======Запис № {i}=======\n   Дата: {record.Date},\n   Спеціалізація: {doctors.First(x => x.Id == record.DoctorId).WorkPosition},\n   " +
                    $"Ім'я: {doctors.First(x => x.Id == record.DoctorId).Name},\n   Прізвище: {doctors.First(x => x.Id == record.DoctorId).Surname}\n\n";
                i++;
            }
            recordsTextBlock.Text = text;
        }

        private async void ButtonGetVisits_Click(object sender, RoutedEventArgs e)
        {
            visitTextBlock.Text = string.Empty;

            var visits = await MedClinicHttpClient.GetVisits();
            var doctors = await MedClinicHttpClient.GetDoctors();
            var anamneses = await MedClinicHttpClient.GetAnamneses();

            visits = visits.Where(x => x.ClientId == MyClient.Client.Id).ToList();

            if (visits.Count == 0)
            {
                Notification.Show("Відвідування", "У вас не було відвідувань", Notifications.Wpf.NotificationType.Information, 0, 0, 10);
                visitTextBlock.Text = "Відвідування відсутні";
                return;
            }

            string text = "";
            int i = 1;

            foreach (var visit in visits)
            {
                text += $"=======Візит № {i}=======\nДата: {visit.Date},\n   Захворювання: {visit.Illness},\n   Скарги: {visit.Complaint},\n   Аналізи: {visit.Analysis},   " +
                    $"\n   Ліки: {visit.Medicines},\n   Рекомендації: {visit.Recomendations},\n      Анамнез:\n         Локалізація: {anamneses.First(x => x.Id == visit.AnamnesId).Localisation},\n      " +
                    $"   Дата початку: {anamneses.First(x => x.Id == visit.AnamnesId).StartDate},\n         Стадія розвитку: {anamneses.First(x => x.Id == visit.AnamnesId).DevelopmentRate},\n      " +
                    $"   Рівень болю: {anamneses.First(x => x.Id == visit.AnamnesId).PainRate},\n   Ціна: {visit.Price},\n   Спеціалізація: {doctors.First(x => x.Id == visit.DoctorId).WorkPosition},\n   " +
                    $"Ім'я: {doctors.First(x => x.Id == visit.DoctorId).Name},\n   Прізвище: {doctors.First(x => x.Id == visit.DoctorId).Surname}\n\n";
                i++;
            }
            visitTextBlock.Text = text;
        }

        private void ButtonAddComment_Click(object sender, RoutedEventArgs e)
        {
            AddCommentWindow addCommentWindow = new AddCommentWindow();
            addCommentWindow.Show();
        }

        /// <summary>
        /// ADDDDDDD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDeleteComment_Click(object sender, RoutedEventArgs e)
        {
            DeleteCommentWindow deleteCommentWindow = new DeleteCommentWindow();
            deleteCommentWindow.Show();
        }

        /// <summary>
        /// ADDDDDDD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonShowComments_Click(object sender, RoutedEventArgs e)
        {
            commentTextBlock.Text = string.Empty;
            var comments = await MedClinicHttpClient.GetComments();

            comments = comments.Where(x => x.ClientId == MyClient.Client.Id).ToList();
            if (comments.Count == 0)
            {
                Notification.Show("Відгуки", "У вас не було відгуків", Notifications.Wpf.NotificationType.Information, 0, 0, 10);
                commentTextBlock.Text = "Відгуки відсутні";
                return;
            }

            string text = "";
            int i = 1;

            foreach (var comment in comments)
            {
                var doctors = await MedClinicHttpClient.GetDoctors();
                var doctor = doctors.Where(x => x.Id == comment.DoctorId).First();

                text += $"=======Відгук № {i}=======\n   Ім'я: {doctor.Name},\n   Прізвище: {doctor.Surname},\n   Спеціалізація: {doctor.WorkPosition},\n   Відгук: {comment.Text},\n   Оцінка: {comment.Mark}\n\n";
                i++;
            }
            commentTextBlock.Text = text;
        }


        /// <summary>
        /// ADDDDDDDDDD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            DeleteRecordWindow deleteRecordWindow = new DeleteRecordWindow();
            deleteRecordWindow.Show();
        }
    }
}
