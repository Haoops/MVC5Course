using System;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models
{
    public class MustContainOneSpaceAttribute : DataTypeAttribute
    {
        public MustContainOneSpaceAttribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            var str = (string)value;

            return str.Contains(" ");
        }
    }
}