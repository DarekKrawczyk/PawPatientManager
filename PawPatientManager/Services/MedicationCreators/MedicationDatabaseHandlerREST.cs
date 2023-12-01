using PawPatientManager.DTOs;
using PawPatientManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace PawPatientManager.Services.MedicationCreators
{
    public class MedicationDatabaseHandlerREST : IMedicationDatabaseHandler
    {
        private static string Link = "http://localhost:5295/";
        private HttpClient _httpClient;
        public MedicationDatabaseHandlerREST()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Link);

        }
        public async Task CreateMedication(Medication med)
        {
            MedicationDTO medication = new MedicationDTO
            {
                ID = med.ID,
                Name = med.Name,
                Description = med.Description,
                Amount = med.Amount
            };

            var json = JsonSerializer.Serialize(medication);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("api/Medication", content).Result;

            if(response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            }
            else
            {
                Console.WriteLine(response.StatusCode);
            }
        }

        public async Task DeleteMedication(Medication med)
        {
            var response = await _httpClient.DeleteAsync($"api/Medication/{med.ID}");

            //return response.IsSuccessStatusCode;
        }

        public async Task EditMedication(Medication selectedMed, Medication editedMed)
        {
            MedicationDTO medication = new MedicationDTO
            {
                ID = selectedMed.ID,
                Name = editedMed.Name,
                Description = editedMed.Description,
                Amount = editedMed.Amount
            };

            var json = JsonSerializer.Serialize(medication);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Medication/{selectedMed.ID}", content);

            //return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Medication>> GetAllMedications()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Medication");

                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions();
                    options.PropertyNameCaseInsensitive = true;

                    var json = await response.Content.ReadAsStringAsync();
                    IEnumerable<MedicationDTO> medications = JsonSerializer.Deserialize<IEnumerable<MedicationDTO>>(json, options);

                    List<Medication> result = new List<Medication>();

                    foreach(MedicationDTO medDT in medications)
                    {
                        result.Add(new Medication(medDT));
                    }

                    return result;
                }
                else
                {
                    // Handle unsuccessful response
                    //MessageBox.Show($"Failed to retrieve medications. Status code: {response.StatusCode}");
                    return Enumerable.Empty<Medication>();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                //MessageBox.Show($"An error occurred: {ex.Message}");
                return Enumerable.Empty<Medication>();
            }
        }

        public Task<Medication> GetConflictingMedication(Medication medication)
        {
            throw new NotImplementedException();
        }
    }
}
