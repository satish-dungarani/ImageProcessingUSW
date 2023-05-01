using ImageProcessing.Data.DataContext;
using ImageProcessing.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Data.Interface
{
    public interface IImageProcessingRepository
    {
        Task<IEnumerable<ImageProcessingHistory>> GetImageProcessingHistorysAsync();
        Task<ImageProcessingHistory> GetImageProcessingHistoryByIdAsync(int Id);
        Task<bool> DeleteImageProcessingHistoryByIdAsync(int Id);
        Task<int> InsertImageProcessingHistoryAsync(ImageProcessingHistory ImageProcessingHistory);
    }
}
