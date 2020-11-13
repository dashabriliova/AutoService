using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoService.DAL.Entities
{
   public class Auto
    {
        public int AutoId { get; set; }
        [Required(ErrorMessage = "Введите наименование")]
        public string AutoName { get; set; }
        [Required(ErrorMessage = "Введите гос. номер ")]
        public string AutoNumber { get; set; }
        [Required(ErrorMessage = "Введите дату выпуска")]
        public DateTime AutoYear { get; set; }
        [Required(ErrorMessage = "Введите VIN код")]
        public string AutoVIN { get; set; }
        public string AutoImage { get; set; }

        //навигационные свойства
        public List<Insurance> Insurances { get; set; }
        public List<Inspection> Inspections { get; set; }
        public List<Serv> Servs { get; set; }
    }
}
