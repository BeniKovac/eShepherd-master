using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models
{
    public class Oven
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID ovna")] 
        [StringLength(10)]
        [Required]
        public string OvenID { get; set; }

        public String CredaID { get; set; }
        [Display(Name = "Trenutna čreda")] 
        public Creda creda { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", NullDisplayText = "/", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum Rojstva")]        
        public DateTime? DatumRojstva { get; set; }

        public string Pasma { get; set; }
        [DisplayFormat(NullDisplayText = "/")]
        [Display(Name = "ID mame")] 
        public string? mamaID { get; set; }
        

        [DisplayFormat(NullDisplayText = "/")]        
        [Display(Name = "ID očeta")] 
        public string? oceID { get; set; }

        [DisplayFormat(NullDisplayText = "/")]
        [Display(Name = "Število sorojencev")] 
        public int? SteviloSorojencev { get; set; } = 0;


         [DisplayFormat(NullDisplayText = "/")]        
         public string Stanje { get; set; }

        public string? Opombe { get; set; }

        [DisplayFormat(NullDisplayText = "/")]
        public string Poreklo { get; set; }
        public ICollection<Ovca> ovce { get; set; }

        public ICollection<Kotitev> vseKotitve { get; set; }
        public ICollection<Gonitev> vseGonitve { get; set; }

    }
}