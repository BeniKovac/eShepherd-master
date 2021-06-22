using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{   
    public class Jagenjcek
        {
        [Key]
        public int skritIdJagenjcka { get; set; }

        [Required]
        [Display(Name = "ID jagenjčka")] 
        public String IdJagenjcka { get; set; }
        
        public int kotitevID { get; set; }
        public Kotitev kotitev { get; set; }


        [Display(Name = "Spol")] 
        public string spol { get; set; }
        
        public string? opombe { get; set; }

        public string? stanje { get; set; }
        

    }
}