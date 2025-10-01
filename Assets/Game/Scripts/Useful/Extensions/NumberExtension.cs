using System;

namespace Game.Scripts.Useful.Extensions
{
    public static class NumberExtension
    {
        private static readonly string[] Suffixes = { "", "K", "M" };
    
        public static string ToShortString(this float number)
        {
            if (number < 1000) return number.ToString();
        
            int suffixIndex = 0;
            double reducedNumber = number;
        
            while (reducedNumber >= 1000 && suffixIndex < Suffixes.Length - 1)
            {
                reducedNumber /= 1000;
                suffixIndex++;
            }
        
            reducedNumber = Math.Ceiling(reducedNumber);
        
            if (reducedNumber >= 1000 && suffixIndex < Suffixes.Length - 1)
            {
                reducedNumber = 1;
                suffixIndex++;
            }
        
            return reducedNumber < 10 ? 
                reducedNumber.ToString("F1") + Suffixes[suffixIndex] : 
                reducedNumber.ToString("F0") + Suffixes[suffixIndex];
        }
    }
}