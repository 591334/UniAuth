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


