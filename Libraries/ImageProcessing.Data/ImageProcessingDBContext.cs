using ImageProcessing.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing.Data
{
    public partial class ImageProcessingDBContext : DbContext
    {
        public ImageProcessingDBContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
