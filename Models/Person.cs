    using System;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

namespace UniAuth.Models
{
    public class Person
    {
        public string Name { get; set; }
        public Address InvoiceAddress { get; set; }
        public Phone DefaultPhone { get; set; }
        public Email DefaultEmail { get; set; }
        public string Comment { get; set; }
    
            public async Task<HttpResponseMessage> AddUserAsync(HttpClient httpClient)
            {
                try
                {
                    // Convert the person data to JSON
                    var personDataJson = JsonSerializer.Serialize(this);

                    // Define the API endpoint URL
                    var apiUrl = "http://test-api.softrig.com/api/biz/contacts";

                    // Create a StringContent with the JSON data
                    var content = new StringContent(personDataJson, Encoding.UTF8, "application/json");

                    // Make the POST request and return the response
                    return await httpClient.PostAsync(apiUrl, content);
                }
                catch (Exception ex)
                {
                    // Handle exceptions here (you may want to log the exception)
                    Console.WriteLine(ex.Message);

                    // Return a failure response
                    return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                }
            }
        }
    }
    
    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
    }
    
    public class Phone
    {
        public string CountryCode { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
    }
    
    public class Email
    {
        public string EmailAddress { get; set; }
    }


