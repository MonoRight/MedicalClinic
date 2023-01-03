﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using MedicalClinicClientApp.Model;
using System.Threading.Tasks;
using System.Net.Http.Json;
using MedicalClinicClientApp.NotificationManagement;
using System.Windows;

namespace MedicalClinicClientApp.HttpMedClient
{
    public static class MedClinicHttpClient
    {
        public static HttpClient HttpClient { get; set; } = new HttpClient();
        private const string URL = "https://localhost:5001/api/";

        public static async Task EditClient(Client client)
        {
            try
            {
                HttpResponseMessage response = await HttpClient.PatchAsJsonAsync<Client>(URL + $"Clients/{client.Id}", client);

                if (response.IsSuccessStatusCode)
                {
                    Notification.Show("Дані було змінено", "Ваші дані було змінено", Notifications.Wpf.NotificationType.Success, 0, 0, 10);
                }
                else
                {
                    Notification.Show("Дані не було змінено", "Ваші дані не було змінено", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }       
        }

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

        public static async Task AddClient(Client client)
        {
            try
            {
                HttpResponseMessage response = await HttpClient.PostAsJsonAsync(URL + "Clients", client);

                Client? newClient = await response.Content.ReadFromJsonAsync<Client>();


                if (response.IsSuccessStatusCode)
                {
                    Notification.Show("Реєстрація пройшла успішно!", "Користувача додано до системи", Notifications.Wpf.NotificationType.Success, 0, 0, 10);
                }
                else
                {
                    Notification.Show("Реєстрація не пройшла!", "Користувача не додано до системи", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static async Task AddRecord(Record record)
        {
            try
            {
                HttpResponseMessage response = await HttpClient.PostAsJsonAsync(URL + "Records", record);

                Record? newRecord = await response.Content.ReadFromJsonAsync<Record>();


                if (response.IsSuccessStatusCode)
                {
                    Notification.Show("Запис додано до системи", "Ваш запис оброблено і додано до системи", Notifications.Wpf.NotificationType.Success, 0, 0, 10);
                }
                else
                {
                    Notification.Show("Запис відхилено", "Ваш запис не додано до системи", Notifications.Wpf.NotificationType.Error, 0, 0, 10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
