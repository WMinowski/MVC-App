using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_App.Common.Attributes
{
    public class MaxValueAttribute : ValidationAttribute
    {


        private static int _maxValue;

        public MaxValueAttribute(int maxValue)
        {
            _maxValue = maxValue;
        }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                int appliedValue = int.Parse(value.ToString());
                if (appliedValue <= _maxValue)
                    return true;
                else
                    this.ErrorMessage = "Value should be less than " + _maxValue.ToString();
            }
            return false;
        }


    }
}