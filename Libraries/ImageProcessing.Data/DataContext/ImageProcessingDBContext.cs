using ImageProcessing.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Data.DataContext
{
    public partial class ImageProcessingDBContext : DbContext
    {
        public ImageProcessingDBContext(DbContextOptions<ImageProcessingDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<RequestsAudit> RequestsAudits { get; set; }
        public DbSet<ImageProcessingHistory> ImageProcessingHistories{ get; set; }
    }
}
