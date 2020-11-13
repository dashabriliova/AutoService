using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoService.DAL.Entities
{
   public class Inspection
    {
        public int InspectionId { get; set; }
        [Required(ErrorMessage = "Введите дату техосмотра")]
        public DateTime InspectionDate { get; set; }
        [Required(ErrorMessage = "Введите дату продления")]
        public DateTime InspectionMonthNext { get; set; }

        //навигационные свойства
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int AutoId { get; set; }
        public Auto Auto { get; set; }
    }
}
