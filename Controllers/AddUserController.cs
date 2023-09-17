using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
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
        public async Task<IActionResult> AddNewUser(IFormCollection form)
        {
            IdentityResponse ir = new IdentityResponse();
            string access_token = ir.access_token;

            try
            {
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

                var payloadJson = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var apiEndpoint = "https://test-api.softrig.com/api/biz/contacts";

                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    httpClient.DefaultRequestHeaders.Add("CompanyKey", "91aec9da-f8c0-4fdd-a2a4-a4aeb82275c3");
                    httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

                    var jwtSecurityToken = new JwtSecurityToken(access_token);

                    if (jwtSecurityToken.Payload.TryGetValue("AppFramework", out var baseUrl))
                    {
                        try
                        {
                            var response = await httpClient.PostAsync(apiEndpoint, new StringContent(payloadJson, Encoding.UTF8, "application/json"));

                            if (response.IsSuccessStatusCode)
                            {
                                var responseContent = await response.Content.ReadAsStringAsync();
                                return Content(responseContent);
                            }
                            else
                            {
                                var errorMessage = $"API request failed with status code {response.StatusCode}";
                                return Content(errorMessage);
                            }
                        }
                        catch (Exception ex)
                        {
                            return Content($"An error occurred: {ex.Message}");
                        }
                    }
                    else
                    {
                        return Content("AppFramework value not found in the JWT payload.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content($"An error occurred: {ex.Message}");
            }
            return Content("An unexpected error occurred.");
        }
    }
    }
}
