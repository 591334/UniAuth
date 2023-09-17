using Microsoft.AspNetCore.Mvc;
using System.Text;
using UniAuth.Models;

namespace UniAuth.Controllers
{
    public class AddUserController : Controller
    {

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(FormCollection form)
        {
            {
                try
                {
                    // Get the form data from the submitted form
                    string name = form["Name"];
                    string addressLine1 = form["addressLine1"];
                    string addressLine2 = form["addressLine2"];
                    string addressLine3 = form["addressLine3"];
                    string city = form["city"];
                    string countrycode = form["countrycode"];
                    string postalcode = form["postalcode"];
                    string phoneCountryCode = form["phoneCountryCode"];
                    string type = form["type"];
                    string mobileNumber = form["mobilenumber"];
                    string emailAddress = form["emailAddress"];
                    string comment = form["floatingTextarea2"];

                    // Now you can use these variables to construct the payload
                    var payload = new
                    {
                        Info = new
                        {
                            Name = name,
                            InvoiceAddress = new
                            {
                                AddressLine1 = addressLine1,
                                AddressLine2 = addressLine2,
                                AddressLine3 = addressLine3,
                                City = city,
                                CountryCode = countrycode,
                                PostalCode = postalcode,
                            },
                            DefaultPhone = new
                            {
                                CountryCode = phoneCountryCode,
                                Description = type,
                                Number = mobileNumber,
                            },
                            DefaultEmail = new
                            {
                                EmailAddress = emailAddress,
                            }
                        },
                        Comment = comment
                    };

                    // Convert the payload to JSON
                    var payloadJson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

                    // Define the API endpoint URL
                    var apiEndpoint = "https://test-api.softrig.com/api/biz/contacts";

                    // Create an HttpClient instance
                    using (var httpClient = new HttpClient())
                    {
                        // Set the content type header
                        httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

                        try
                        {
                            // Send the POST request and await the response
                            var response = await httpClient.PostAsync(apiEndpoint, new StringContent(payloadJson, Encoding.UTF8, "application/json"));

                            // Check the response status code
                            if (response.IsSuccessStatusCode)
                            {
                                // Handle the successful response
                                var responseContent = await response.Content.ReadAsStringAsync();
                                // Process the response content as needed
                                return Content(responseContent);
                            }
                            else
                            {
                                // Handle the error response
                                var errorMessage = $"API request failed with status code {response.StatusCode}";
                                return Content(errorMessage);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle any exceptions that occur during the request
                            return Content($"An error occurred: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during form data processing
                    return Content($"An error occurred: {ex.Message}");
                }
            }
    }
}
