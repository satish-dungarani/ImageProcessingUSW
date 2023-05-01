using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Data.Entities
{
    public class ImageProcessingHistory
    {
        public int Id { get; set; }
        public string? RequestedImageUrl { get; set; }
        public string? ProcessedImageUrl { get; set; }

        public int? Width { get; set; }
        public int? Height { get; set; }

        public int? IPQuality { get; set; }

        public int UserId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
