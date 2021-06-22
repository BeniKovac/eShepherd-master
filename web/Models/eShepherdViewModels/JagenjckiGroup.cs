using System;
using System.ComponentModel.DataAnnotations;

namespace web.Models.eShepherdViewModels
{
    public class JagenjckiGroup
    {
        [DataType(DataType.Date)]
        public String kotitevID { get; set; }

        public int JagenjckiCount { get; set; }
    }
}