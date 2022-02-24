using System.ComponentModel.DataAnnotations;

namespace NetWeightCalculator.DTOs.CustomAttributes
{
    public class PrecisionAndScaleAttribute : RegularExpressionAttribute
    {
        public PrecisionAndScaleAttribute(int precision, int scale) 
            : base($@"^\d{{1,{precision - scale}}}((\.|,)\d{{1,{scale}}})?$")
        {
        }
    }
}
