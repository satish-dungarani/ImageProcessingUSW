using ImageProcessing.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Services
{
    public interface IImageProcessingService
    {
        bool ApplyEffectInsideShape(ImageProcessingHistory entity);
        Task<bool> ResizeImage(ImageProcessingHistory entity);

        Task<IEnumerable<ImageProcessingHistory>> GetHistoriesAsync();
    }
}
