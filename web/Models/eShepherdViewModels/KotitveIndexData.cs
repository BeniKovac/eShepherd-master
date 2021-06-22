using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Models.eShepherdViewModels
{
    public class KotitveIndexData
    {
        public PaginatedList<Kotitev> Kotitve { get; set; }
        /*public int maxKotitevID { get{
            int currentMAX = -1;
            foreach (Kotitev kotitev in Kotitve)
                {
                    if(kotitev.kotitevID > currentMAX)
                        currentMAX = kotitev.kotitevID;
                }
                return currentMAX;
            }
        }*/
        public IEnumerable<Jagenjcek> Jagenjcki { get; set; }
    }
}