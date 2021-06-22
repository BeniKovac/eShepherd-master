using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Gonitev
    {
        
        [Key]
        public int GonitevID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum gonitve")] 
        public DateTime DatumGonitve { get; set; }

        public string OvcaID { get; set; }
        public Ovca ovca { get; set; }

        public string OvenID { get; set; }
        public Oven oven { get; set; }
        
        // pristej 145 dni do datuma kotitve
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Predviden datum kotitve")] 
        public DateTime PredvidenaKotitev { 
                get
                {
                    DateTime kotitev = DatumGonitve.AddDays(145);
                    return kotitev;
                }

         }
        
        public string? Opombe { get; set; }

        

    }
}