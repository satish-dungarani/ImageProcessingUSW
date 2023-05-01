using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System;
using ImageProcessing.Data.Entities;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing.Processors;
using Path = System.IO.Path;
using ImageProcessing.Data.Interface;

namespace ImageProcessing.Services
{
    public class ImageProcessingService : IImageProcessingService
    {
        #region Props
        private readonly IImageProcessingRepository _imageProcessingRepository;

        #endregion

        #region Ctors

        public ImageProcessingService(IImageProcessingRepository imageProcessingRepository)
        {
            _imageProcessingRepository = imageProcessingRepository;
        }

        #endregion


        #region Methods

        public bool ApplyEffectInsideShape(ImageProcessingHistory entity)
        {
            System.IO.Directory.CreateDirectory("output");
            using Image image = Image.Load("C:\\Satish\\Assessment 2\\ImageProcessing\\ImageProcessing.Web\\wwwroot\\images\\IMG_20220916_001611.jpg");

            int outerRadii = Math.Min(image.Width, image.Height) / 2;
            IPath star = new Star(new PointF(image.Width / 2, image.Height / 2), 5, outerRadii / 2, outerRadii).AsClosedPath();

            // Apply the effect here inside the shape
            image.Mutate(x => x.Clip(star, y => y.GaussianBlur(15)));

            image.Save("output/fb.png");

            return true;
        }
        public async Task<bool> ResizeImage(ImageProcessingHistory entity)
        {
            try
            {
                using Image image = Image.Load(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", entity.RequestedImageUrl));

                switch (entity.IPQuality)
                {
                    case 1: // oil
                        image.Mutate(x => x.Resize(image.Width / Convert.ToInt32(entity.Width), image.Height / Convert.ToInt32(entity.Height)).OilPaint());
                        break;
                    case 2:  // b&W
                        image.Mutate(x => x.Resize(image.Width / Convert.ToInt32(entity.Width), image.Height / Convert.ToInt32(entity.Height)).Grayscale());
                        break;
                    case 3:
                        image.Mutate(x => x.Resize(image.Width / Convert.ToInt32(entity.Width), image.Height / Convert.ToInt32(entity.Height)).BackgroundColor(Color.DimGrey));
                        break;
                    default:
                        break;
                }

                image.Save(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", entity.ProcessedImageUrl));
                return await _imageProcessingRepository.InsertImageProcessingHistoryAsync(entity) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
