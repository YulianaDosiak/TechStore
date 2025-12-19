using System.ComponentModel.DataAnnotations;

namespace TechStore.MVC.App.Validation
{
    public class PriceRangeAttribute : ValidationAttribute
    {
        private readonly double _minPrice;
        private readonly double _maxPrice;

        public PriceRangeAttribute(double minPrice, double maxPrice)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
            ErrorMessage = $"Ціна повинна бути від {_minPrice} до {_maxPrice} грн.";
        }

        public override bool IsValid(object value)
        {
            if (value is decimal price)
            {
                return (double)price >= _minPrice && (double)price <= _maxPrice;
            }
            return true;
        }
    }
}