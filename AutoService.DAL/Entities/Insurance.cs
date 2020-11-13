using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AutoService.DAL.Entities
{
   public class Insurance
    {
        public int InsuranceId { get; set; }
        [Required(ErrorMessage = "Введите дату оплаты страховки")]
        public DateTime InsuranceDate { get; set; }
        [Required(ErrorMessage = "Введите дату продления страховки")]
        public DateTime InsuranceDateProlongation { get; set; }

        //навигационные свойства
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public int AutoId { get; set; }
        public Auto Auto { get; set; }
    }
}
