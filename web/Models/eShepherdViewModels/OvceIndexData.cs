using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models.eShepherdViewModels
{
    public class OvceIndexData
    {
        public PaginatedList<Ovca> Ovce { get; set; }
        public IEnumerable<Kotitev> Kotitve { get; set; }
        public IEnumerable<Gonitev> Gonitve { get; set; }
    }
}