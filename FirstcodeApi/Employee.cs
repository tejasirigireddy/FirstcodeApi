using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FirstcodeApi.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace FirstcodeApi
{
    [Table("Employee")]

    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter first name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Enter last name")]
        public string? LastName { get; set; }


    }

}
