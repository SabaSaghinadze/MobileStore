using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MobileStore.Db;
using MobileStore.Models;
using System.Linq;

namespace MobileStore
{
    public static class Seeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var _context = scope.ServiceProvider.GetService<MobileStoreDbContext>();

                _context.Database.Migrate();

                var nokia3310 = new MobilePhone()
                {
                    Name = "Nokia 3310",
                    Price = 20
                };

                if (_context.MobilePhones.SingleOrDefault(mobilePhone => mobilePhone.Name == nokia3310.Name) == null)
                {
                    _context.MobilePhones.Add(nokia3310);
                }

                var redmiNote9 = new MobilePhone()
                {
                    Name = "Redmi note 9",
                    Manufacturer = "Xiaomi",
                    Memory = 64,
                    CPU = "Mediatek",
                    OperatingSystem = "Android 10",
                    Price = 600
                };

                if (_context.MobilePhones.SingleOrDefault(mobilePhone => mobilePhone.Name == redmiNote9.Name) == null)
                {
                    _context.MobilePhones.Add(redmiNote9);
                }

                var samsungGalaxyA50 = new MobilePhone()
                {
                    Name = "Samsung galaxy a59",
                    Memory = 128,
                    CPU = "Exynos",
                    Price = 600,
                    Manufacturer = "Samsung",
                    OperatingSystem = "Android Pie"
                };

                if (_context.MobilePhones.SingleOrDefault(mobilePhone => mobilePhone.Name == samsungGalaxyA50.Name) == null)
                {
                    _context.MobilePhones.Add(samsungGalaxyA50);
                }

                _context.SaveChanges();
            }
        }
    }
}
