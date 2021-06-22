using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Kotitev
    {
        public Kotitev()
        {

            jagenjcki = new List<Jagenjcek>();
            SteviloMrtvih = 0;
        }
        [Key]
        
        public int kotitevID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum kotitve")] 
        public DateTime DatumKotitve { get; set; }

        [Display(Name = "Število jagenjčkov")] 
        public int SteviloMladih { 
             get { 
                /*int skupno = 0;
                if (jagenjcki != null) {
                    skupno += jagenjcki.Count;
                }
                skupno += SteviloMrtvih;
                */
                return SteviloMrtvih + jagenjcki.Count;
            } 
        }
            
            
         
        [Display(Name = "Ovca")] 
        public string OvcaID { get; set; }
        public Ovca Ovca { get; set; }

        


        [Display(Name = "Oven")]
        public string OvenID { get; set; }
        public Oven Oven { get; set; }
       
        [Display(Name = "Število mrtvorojenih jagenjčkov")] 
        
        public int SteviloMrtvih { get; set; } = 0;

        public string? Opombe { get; set; }

        [Display(Name = "Jagenjčki")]

        public ICollection<Jagenjcek> jagenjcki { get; set; }

        

    }
}