using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlDigit
{
    public static class SnilsExtensions
    {
        public static int CalculateSnils(this long number)
        {
            return number
                       .GetDigitsFromLowToHigh()
                       .Select((digit, index) => digit * (index + 1))
                       .Sum()
                   % 101
                   % 100;
        }

        public static IEnumerable<int> GetDigitsFromLowToHigh(this long number)
        {
            do
            {
                yield return (int)(number % 10);
                number /= 10;
            } while (number > 0);
        }
    }
}
