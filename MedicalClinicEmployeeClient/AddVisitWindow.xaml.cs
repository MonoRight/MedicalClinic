using MedicalClinicEmployeeClient.HttpMedClient;
using MedicalClinicEmployeeClient.Model;
using MedicalClinicEmployeeClient.NotificationManagement;
using MedicalClinicEmployeeClient.Session;
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

namespace MedicalClinicEmployeeClient
{
    /// <summary>
    /// Interaction logic for AddVisitWindow.xaml
    /// </summary>
    public partial class AddVisitWindow : Window
    {
        public AddVisitWindow()
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

        private async void ButtonAddVisit_Click(object sender, RoutedEventArgs e)
        {
            if (clientNameInput.Text != "" && clientSurnameInput.Text != "" && illnessInput.Text != ""
                && complaintsInput.Text != "" && analysisInput.Text != "" && medecinesInput.Text != ""
                && recomendationsInput.Text != "" && priceInput.Text != "" && localisationInput.Text != ""
                && startDateInput.Text != "" && developementRateInput.Text != "" && painRateInput.Text != "")
            {
                DateTime startDate;
                DateTime nowTime = DateTime.Now;

                try
                {
                    startDate = DateTime.ParseExact(startDateInput.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (ArgumentNullException)
                {
                    Notification.Show("Контент дати", "Дата початку пуста", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                    return;
                }
                catch (FormatException)
                {
                    Notification.Show("Формат дати", "Введенна дата початку не відповідає формату", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                    return;
                }

                int price;
                try
                {
                    price = Convert.ToInt32(priceInput.Text);
                }
                catch
                {
                    Notification.Show("Помилка формату", "Ціна повинна бути цілим числом", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                    return;
                }

                var clients = await MedClinicHttpClient.GetClients();

                Client client = clients.FirstOrDefault(x => x.Name == clientNameInput.Text && x.Surname == clientSurnameInput.Text);

                if (client == null)
                {
                    Notification.Show("Клієнт відсутній", "Клієнт з заданим ім'ям та прізвищем відсутній в системі", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                    return;
                }

                Anamnes anamnes = new Anamnes()
                {
                    Localisation = localisationInput.Text,
                    StartDate = startDate,
                    DevelopmentRate = developementRateInput.Text,
                    PainRate = painRateInput.Text
                };

                await MedClinicHttpClient.AddAnamnes(anamnes);

                var anamneses = await MedClinicHttpClient.GetAnamneses();

                Anamnes addedAnamnes = anamneses.Last();

                Visit visit = new Visit()
                {
                    Date = nowTime,
                    Illness = illnessInput.Text,
                    Complaint = complaintsInput.Text,
                    Analysis = analysisInput.Text,
                    Medicines = medecinesInput.Text,
                    Recomendations = recomendationsInput.Text,
                    AnamnesId = addedAnamnes.Id,
                    ClientId = client.Id,
                    DoctorId = MyDoctor.Doctor.Id,
                    Price = price
                };

                await MedClinicHttpClient.AddVisit(visit);

                Close();
            }
            else
            {
                Notification.Show("Пусті поля", "Всі поля повинні бути заповнені", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
            }
        }
    }
}
