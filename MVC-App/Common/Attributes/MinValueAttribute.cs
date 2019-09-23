using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_App.Common.Attributes
{
    public class MinValueAttribute : ValidationAttribute
    {
        private static int _minValue;

        public MinValueAttribute(int minValue)
        {
            _minValue = minValue;
        }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                int appliedValue = int.Parse(value.ToString());
                if (appliedValue >= _minValue)
                    return true;
                else
                    this.ErrorMessage = "Value should be more than " + _minValue.ToString();
            }
            return false;
        }

    }
}