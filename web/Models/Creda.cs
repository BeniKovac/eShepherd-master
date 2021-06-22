using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Creda
    {
        public Creda()
        {
            SeznamOvac = new List<Ovca>();
        }
        
        [Display(Name = "ID črede")] 
        [Key]
        public String CredeID { get; set; }

        [Display(Name = "Seznam ovac, ki so v čredi")] 
        public ICollection<Ovca>? SeznamOvac { get; set; }
    
        [Display(Name = "Število ovac v čredi")] 
        // treba pogledat, kako narest ce je seznam prazen - da se ni nobene ovce not
        public int SteviloOvc { 
            get
            { 
                if (SeznamOvac == null) {
                    return 0;
                }
                return SeznamOvac.Count;
            } 
        }
        public string? Opombe { get; set; }

        
        //[Display(Name = "ID ovna")] 
        //public string OvenID { get; set; }
        //public Oven oven { get; set; }

        
        public ICollection<Jagenjcek> SeznamJagenjckov { get; set; }


    }
}