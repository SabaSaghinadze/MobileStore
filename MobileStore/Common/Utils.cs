using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileStore.Common
{
    public static class Utils
    {
        public static List<Tuple<string, object>> GetNotNullPropertyNames<T>(T obj)
        {
            var result = typeof(T).GetProperties()
                .Select(x => Tuple.Create(x.Name, x.GetValue(obj)))
                .Where(x => x.Item2 != null)
                .ToList();

            return result;
        }

        public static Tuple<bool, MediaType> ValidateAndGetMediaType(IFormFile file)
        {
            var fileExtension = file.FileName.Split('.').Last();

            MediaType mediaType;

            if (ImageFormats.Contains(fileExtension))
            {
                mediaType = MediaType.Image;
            }
            else if (VideoFormats.Contains(fileExtension))
            {
                mediaType = MediaType.Video;
            }
            else
            {
                return null;
            }

            return new Tuple<bool, MediaType>(true, mediaType);
        }

        private static readonly List<string> ImageFormats = new List<string>()
        {
            "jpg",
            "jpeg",
            "png",
            "tiff",
            "gif"
        };

        private static readonly List<string> VideoFormats = new List<string>()
        {
            "avi",
            "mp4",
            "mkv",
            "flv"
        };
    }
}
