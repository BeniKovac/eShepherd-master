using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models.eShepherdViewModels
{
    public class CredeIndexData
    {
        
        public IEnumerable<Creda> Crede { get; set; }
        public IEnumerable<Ovca> Ovce { get; set; }
    }
}