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
    public class ImageProcessingRepository : IImageProcessingRepository
    {
        #region Properties
        private readonly ImageProcessingDBContext _dbContext;
        #endregion

        #region Ctor
        public ImageProcessingRepository(ImageProcessingDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods

        // To Get ImageProcessingHistory List
        public async Task<IEnumerable<ImageProcessingHistory>> GetImageProcessingHistorysAsync()
        {
            return await _dbContext.ImageProcessingHistories.ToListAsync();
        }

        // To Get ImageProcessingHistory By Id
        public async Task<ImageProcessingHistory> GetImageProcessingHistoryByIdAsync(int Id)
        {
            return await _dbContext.ImageProcessingHistories.FirstOrDefaultAsync(m => m.Id == Id);
        }

        // To Delete ImageProcessingHistory By Id
        public async Task<bool> DeleteImageProcessingHistoryByIdAsync(int Id)
        {
            _dbContext.ImageProcessingHistories.Remove(new ImageProcessingHistory() { Id = Id });
            return await _dbContext.SaveChangesAsync() > 0;
        }

        // To Get ImageProcessingHistory Insert/Update ImageProcessingHistory
        public async Task<int> InsertImageProcessingHistoryAsync(ImageProcessingHistory ImageProcessingHistory)
        {
            _dbContext.ImageProcessingHistories.Add(ImageProcessingHistory);
            return await _dbContext.SaveChangesAsync();
        }

        #endregion

    }
}
