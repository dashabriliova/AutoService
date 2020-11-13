using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoService.DAL.Entities
{
   public class Serv
    {
        public int ServId { get; set; }
        [Required(ErrorMessage = "Введите дату ТО")]
        public DateTime ServDate { get; set; }
        [Required(ErrorMessage = "Введите наименование работ")]
        public string ServNaim { get; set; }
        [Required(ErrorMessage = "Введите показания спидометра/одометра")]
        public int ServPokaz { get; set; }

        //навигационные свойства
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int AutoId { get; set; }
        public Auto Auto { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
