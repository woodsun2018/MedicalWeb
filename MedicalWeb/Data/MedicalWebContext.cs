using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareLibrary;

namespace MedicalWeb.Models
{
    public class MedicalWebContext : DbContext
    {
        public MedicalWebContext (DbContextOptions<MedicalWebContext> options)
            : base(options)
        {
        }

        public DbSet<ShareLibrary.BioValues> BioValues { get; set; }
    }
}
