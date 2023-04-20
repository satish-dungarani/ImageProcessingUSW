using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Data.Entities
{
    public partial class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int City { get; set; }
        public int County { get; set; }
        public string Postcode { get; set; }
    }
}
