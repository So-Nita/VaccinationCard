using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Vaccination.Models.Customer
{
    public class CustomerCreateModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Province is required")]

        public string ProvinceId { get; set; }
        [Required(ErrorMessage = "Card type is required")]

        public string CardId { get; set; }
        [Required(ErrorMessage = "Identity Id is required")]
        public int IndentityId { get; set; }

        [Required(ErrorMessage = "Dose Received is required")]
        public int DoseReceived { get; set; }

        [Required(ErrorMessage = "Date Vaccinated is required")]
        public DateTime DateVaccinated { get; set; }
    }
}
