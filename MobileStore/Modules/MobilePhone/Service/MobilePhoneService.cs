using Microsoft.EntityFrameworkCore;
using MobileStore.Db;
using MobileStore.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileStore.Modules
{
    public class MobilePhoneService : IMobilePhoneService
    {
        private readonly MobileStoreDbContext _context;

        public MobilePhoneService(MobileStoreDbContext context)
        {
            _context = context;
        }

        public async Task<MobilePhone> Create(MobilePhoneRequest request)
        {
            try
            {
                var mobilePhone = await _context.MobilePhones.FirstOrDefaultAsync(mobilePhone => mobilePhone.Name == request.Name);

                if (mobilePhone != null)
                {
                    return default;
                }

                var serializedMobilePhone = JsonConvert.SerializeObject(request);
                mobilePhone = JsonConvert.DeserializeObject<MobilePhone>(serializedMobilePhone);

                _context.MobilePhones.Add(mobilePhone);
                await _context.SaveChangesAsync();

                return mobilePhone;
            }
            catch
            {
                return default;
            }
        }

        public async Task<MobilePhone> Delete(int id)
        {
            try
            {
                var mobilePhone = await _context.MobilePhones.FirstOrDefaultAsync(mobilePhone => mobilePhone.Id == id);

                _context.MobilePhones.Remove(mobilePhone);
                await _context.SaveChangesAsync();

                return mobilePhone;
            }
            catch
            {
                return default;
            }

        }

        public async Task<IEnumerable<MobilePhone>> Get()
        {
            var mobilePhones = await _context
                .MobilePhones
                .Include(mobilePhone => mobilePhone.Mediae)
                .AsNoTracking()
                .ToListAsync();

            return mobilePhones;
        }

        public async Task<MobilePhone> GetById(int id)
        {
            var mobilePhone = await _context
                .MobilePhones
                .Include(mobilePhone => mobilePhone.Mediae)
                .AsNoTracking()
                .FirstOrDefaultAsync(mobilePhone => mobilePhone.Id == id);

            return mobilePhone;
        }

        public async Task<MobilePhone> GetByName(string name)
        {
            var mobilePhone = await _context.MobilePhones.AsNoTracking().FirstOrDefaultAsync(mobilePhone => mobilePhone.Name == name);

            return mobilePhone;
        }

        public async Task<MobilePhone> Update(int id, MobilePhoneRequest request)
        {
            try
            {
                var mobilePhone = await GetById(id);
                mobilePhone.Id = id;
                mobilePhone.Intelligibility = request.Intelligibility;
                mobilePhone.Manufacturer = request.Manufacturer;
                mobilePhone.Memory = request.Memory;
                mobilePhone.Name = request.Name;
                mobilePhone.OperatingSystem = request.OperatingSystem;
                mobilePhone.Price = request.Price;
                mobilePhone.ScreenSize = request.ScreenSize;
                mobilePhone.Size = request.Size;
                mobilePhone.Weight = request.Weight;
                mobilePhone.CPU = request.CPU;

                _context.MobilePhones.Update(mobilePhone);
                await _context.SaveChangesAsync();

                return mobilePhone;
            }
            catch
            {
                return default;
            }
        }
    }
}
