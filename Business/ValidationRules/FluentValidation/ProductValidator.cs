using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public  class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            //Kurallar constructor un içine yazılır 
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p=> p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            //RuleFor(p => p.ProductName).Must(SratWithA).WithMessage("Ürünler A harfi ile başlamalı."); // kendi metodlarımızıda eklememiz gerekebilir

        }

        private bool SratWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
