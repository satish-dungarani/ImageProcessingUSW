namespace ImageProcessing.Web.Models
{
    public class ImageProcessingModel
    {
        public int Id { get; set; }
        public string? RequestedImageUrl { get; set; }
        public IFormFile? RequestedImage { get; set; }
        public string? ProcessedImageUrl { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public int IPQuality { get; set; }
        public int ResizeType { get; set; }
        public int ProcessingType { get; set; }
    }
}
