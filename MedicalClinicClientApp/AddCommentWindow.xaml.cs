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
    /// Interaction logic for AddCommentWindow.xaml
    /// </summary>
    public partial class AddCommentWindow : Window
    {
        public AddCommentWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void PackIconBootstrapIcons_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void PackIconEntypo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private async void ButtonSendComment_Click(object sender, RoutedEventArgs e)
        {
            if(!doctorNameInput.Text.Equals("") || !doctorSurnameInput.Text.Equals("") || !commentInput.Text.Equals(""))
            {
                int mark;

                if ((bool)radioButton1.IsChecked)
                {
                    mark = 1;
                }
                else if ((bool)radioButton2.IsChecked)
                {
                    mark = 2;
                }
                else if ((bool)radioButton3.IsChecked)
                {
                    mark = 3;
                }
                else if ((bool)radioButton4.IsChecked)
                {
                    mark = 4;
                }
                else
                {
                    mark = 5;
                }


                var doctors = await MedClinicHttpClient.GetDoctors();

                var doctor = doctors.FirstOrDefault(x => x.Name == doctorNameInput.Text && x.Surname == doctorSurnameInput.Text);

                if(doctor == null)
                {
                    Notification.Show("Лікаря не знайдено", "Лікаря з такими даними не знайдено", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                }
                else
                {
                    Comment comment = new Comment()
                    {
                        ClientId = MyClient.Client.Id,
                        DoctorId = doctor.Id,
                        Mark = mark,
                        Text = commentInput.Text
                    };

                    await MedClinicHttpClient.AddComment(comment);
                }
                Close();
            }
            else
            {
                Notification.Show("Пусті поля", "Всі поля повинні бути заповнені", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
            }
        }
    }
}
