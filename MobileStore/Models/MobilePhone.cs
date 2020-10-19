using System.Collections.Generic;

namespace MobileStore.Models
{
    public class MobilePhone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Size { get; set; }
        public double Weight { get; set; }
        public string ScreenSize { get; set; }
        public string Intelligibility { get; set; }
        public string CPU { get; set; }
        public int Memory { get; set; }
        public string OperatingSystem { get; set; }
        public int Price { get; set; }
        public ICollection<Media> Mediae { get; set; }
    }
}
