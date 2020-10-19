using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MobileStore.Common;
using MobileStore.Db;
using MobileStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Modules
{
    public class MediaService : IMediaService
    {
        private readonly MobileStoreDbContext _context;

        public MediaService(MobileStoreDbContext context)
        {
            _context = context;
        }
        public async Task<Media> Create(MediaRequest request)
        {
            var mediaTypeValidation = Utils.ValidateAndGetMediaType(request.File);

            if (mediaTypeValidation == null || !mediaTypeValidation.Item1)
            {
                return default;
            }

            var filePath = await UploadMedia(request.File, mediaTypeValidation.Item2);

            var notNullProperties = Utils.GetNotNullPropertyNames(request);
            var propertyNames = GetPropertyNames(notNullProperties);

            if (propertyNames.Count != 1)
            {
                return default;
            }

            var requestProperty = request.GetType().GetProperty(propertyNames[0]).GetValue(request, null);

            var media = new Media();
            media.GetType().GetProperty(propertyNames[0]).SetValue(media, requestProperty);
            media.FilePath = filePath;

            try
            {
                _context.Mediae.Add(media);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return default;
            }

            return media;
        }

        public async Task<Media> Delete(int id)
        {
            var media = await _context.Mediae.FirstOrDefaultAsync(media => media.Id == id);

            try
            {
                _context.Remove(media);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return default;
            }

            return media;
        }

        public async Task<Media> GetById(int id)
        {
            var media = await _context.Mediae.AsNoTracking().FirstOrDefaultAsync(media => media.Id == id);

            return media;
        }

        private async static Task<string> UploadMedia(IFormFile file, MediaType mediaType)
        {
            var folderPath = Path.Combine(Environment.CurrentDirectory, $"Media\\{mediaType}\\");

            var fileName = Guid.NewGuid().ToString() + '.' + file.FileName.Split('.').Last();
            var filePath = folderPath + fileName;

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }

            return filePath;
        }

        private static List<string> GetPropertyNames(List<Tuple<string, object>> props)
        {
            var properties = new List<string>();

            foreach (var prop in props)
            {
                if (prop.Item1 != "File")
                {
                    properties.Add(prop.Item1);
                }
            }

            return properties;
        }
    }
}
