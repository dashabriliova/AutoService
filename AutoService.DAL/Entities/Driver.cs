using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoService.DAL.Entities
{
   public class Driver
    {
        public int DriverId { get; set; }
        [Required(ErrorMessage = "Введите фамилию, имя, отчество")]
        public string DriverFullName { get; set; }
        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime DriverDB { get; set; }
        public string DriverImage { get; set; }
        [Required(ErrorMessage = "Введите категории")]
        [RegularExpression(@"^[b-eB-E1,. ]+$", ErrorMessage ="Допустимые значения B, C, D, BE, CE, DE")]
        public string DriverCategory { get; set; }
        [Required(ErrorMessage = "Введите номер телефона")]
        [RegularExpression(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$", ErrorMessage = "Номер телефона должен иметь формат +375ххххххххх или 80ххххххххх")]
        public string DriverPhone { get; set; }
        [Required(ErrorMessage = "Введите адрес")]
        public string DriverAdress { get; set; }

        public List<Serv> Servs { get; set; }
    }
}
