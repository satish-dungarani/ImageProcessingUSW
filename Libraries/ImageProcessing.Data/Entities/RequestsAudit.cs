using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Data.Entities
{
    public class RequestsAudit
    {
        public int Id { get; set; }
        public string? Unique_Session_Key { get; set; }
        public string? Requested_Url { get; set; }
        public string? IpAddress { get; set; }
        public string? Location { get; set; }
        public string? User_Agent { get; set; }
        public DateTime? Requested_DateTime { get; set; }
    }
}
