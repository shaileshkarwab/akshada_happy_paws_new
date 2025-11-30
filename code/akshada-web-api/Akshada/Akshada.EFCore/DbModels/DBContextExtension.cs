using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.EFCore.DbModels
{
    public static class DBContextExtension
    {
        public static void ConfigureExtension(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VaccinationSummary>(entity =>
            {
                entity.HasNoKey();
                // optional: map to a fake view name to avoid warnings
                entity.ToView(null);
            });

            modelBuilder.Entity<WalkingServiceRequestQuery>(entity =>
            {
                entity.HasNoKey();
                // optional: map to a fake view name to avoid warnings
                entity.ToView(null);
            });

            modelBuilder.Entity<WalkingServiceRate>(entity => {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<DailyOtherServiceList>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<OtherServiceExecutionDetail>(entity => {
                entity.HasNoKey();
                entity.ToView(null);
            });

            modelBuilder.Entity<DashServiceLocationCount>(entity => {
                entity.HasNoKey();
                entity.ToView(null);
            });
        }
    }
}
