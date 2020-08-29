using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using AutoService.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoService.Models
{
    public class CreateModel
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите e-mail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }
    }
    public class CreateAutoModel
    {
        [Required(ErrorMessage = "Введите наименование")]
        public string AutoName { get; set; }
        [Required(ErrorMessage = "Введите гос. номер ")]
        public string AutoNumber { get; set; }
        [Required(ErrorMessage = "Введите дату выпуска")]
        public DateTime AutoYear { get; set; }
        [Required(ErrorMessage = "Введите VIN код")]
        public string AutoVIN { get; set; }
        public string AutoImage { get; set; }
    }
    public class CreateDriverModel
    {
        [Required(ErrorMessage = "Введите фамилию, имя, отчество")]
        public string DriverFullName { get; set; }
        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime DriverDB { get; set; }
        public string DriverImage { get; set; }
        [Required(ErrorMessage = "Введите категории")]
        public string DriverCategory { get; set; }
        [Required(ErrorMessage = "Введите номер телефона")]
        public string DriverPhone { get; set; }
        [Required(ErrorMessage = "Введите адрес")]
        public string DriverAdress { get; set; }
    }
   
    public class LodinModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }
        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
    public class RoleModificationModel
    {
        [Required(ErrorMessage = "Введите роль")]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
    public class AutoListViewModel
    {
        public IEnumerable<Auto> Autos { get; set; }
        public string AutoNaim { get; set; }
    }
    public class DriverListViewModel
    {
        public IEnumerable<Driver> Drivers { get; set; }
        public string FullName { get; set; }
    }
}
