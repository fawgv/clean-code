using System;
using System.Collections.Generic;
using System.Linq;


namespace ControlDigit
{
    public static class UpcExtensions
    {
        public static int CalculateUpc(this long number)
        {
            return number.GetDigitsFromLowToHigh()
                 .GetWeightedSum(3,1)
                 .GetControlDigit();
        }

        private static int GetWeightedSum(this IEnumerable<int> digitsArray, int odd, int even)
        {
            var isOdd = true;
            var sum = 0;
            foreach (var digit in digitsArray)
            {
                sum += isOdd ? digit * odd : digit * even;
                isOdd = isOdd ? false : true;
            }

            return sum;
        }

        private static int GetControlDigit(this int sum)
        {
            int remain = sum % 10;
            int controlDigit = (remain == 0) ? 0 : 10 - remain;
            return controlDigit;
        }
    }
}
