using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Windows;
using MedicalClinicEmployeeClient.NotificationManagement;
using MedicalClinicEmployeeClient.Model;

namespace MedicalClinicEmployeeClient.HttpMedClient
{
    public static class MedClinicHttpClient
    {
        public static HttpClient HttpClient { get; set; } = new HttpClient();
        private const string URL = "https://localhost:5001/api/";

        public static async Task<List<Doctor>> GetDoctors()
        {
            List<Doctor> doctors = null;

            doctors = await HttpClient.GetFromJsonAsync<List<Doctor>>(URL + "Doctors");

            return doctors;
        }

        public static async Task<List<Client>> GetClients()
        {
            List<Client> clients = null;

            clients = await HttpClient.GetFromJsonAsync<List<Client>>(URL + "Clients");

            return clients;
        }

        public static async Task<List<Record>> GetRecords()
        {
            List<Record> records = null;

            records = await HttpClient.GetFromJsonAsync<List<Record>>(URL + "Records");

            return records;
        }

        public static async Task<List<Visit>> GetVisits()
        {
            List<Visit> visits = null;

            visits = await HttpClient.GetFromJsonAsync<List<Visit>>(URL + "Visits");

            return visits;
        }

        public static async Task<List<Anamnes>> GetAnamneses()
        {
            List<Anamnes> anamneses = null;

            anamneses = await HttpClient.GetFromJsonAsync<List<Anamnes>>(URL + "Anamneses");

            return anamneses;
        }

        public static async Task<List<Comment>> GetComments()
        {
            List<Comment> comments = null;

            comments = await HttpClient.GetFromJsonAsync<List<Comment>>(URL + "Comments");

            return comments;
        }

        public static async Task AddVisit(Visit visit)
        {
            try
            {
                HttpResponseMessage response = await HttpClient.PostAsJsonAsync(URL + "Visits", visit);

                Visit? newVisit = await response.Content.ReadFromJsonAsync<Visit>();


                if (response.IsSuccessStatusCode)
                {
                    Notification.Show("Прийом додано до системи", "Ваш прийом оброблено і додано до системи", Notifications.Wpf.NotificationType.Success, 0, 0, 10);
                }
                else
                {
                    Notification.Show("Прийом відхилено", "Ваш прийом не додано до системи", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task AddAnamnes(Anamnes anamnes)
        {
            try
            {
                HttpResponseMessage response = await HttpClient.PostAsJsonAsync(URL + "Anamneses", anamnes);

                Anamnes? newAnamnes = await response.Content.ReadFromJsonAsync<Anamnes>();


                if (response.IsSuccessStatusCode)
                {
                    Notification.Show("Анамнез додано до системи", "Анамнез оброблено і додано до системи", Notifications.Wpf.NotificationType.Success, 0, 0, 10);
                }
                else
                {
                    Notification.Show("Анамнез відхилено", "Анамнез не додано до системи", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
