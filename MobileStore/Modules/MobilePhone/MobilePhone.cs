using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Modules
{
    public class MobilePhoneRequest
    {
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
    }

    public class MobilePhoneRequestValidator : AbstractValidator<MobilePhoneRequest>
    {
        public MobilePhoneRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty();
            RuleFor(request => request.Price).NotEmpty().GreaterThan(0);
        }
    }
}
