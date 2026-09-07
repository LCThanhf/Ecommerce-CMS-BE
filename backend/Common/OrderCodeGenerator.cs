using System;
using System.Linq;

namespace ShoppingCms.Api.Common
{
    public static class OrderCodeGenerator
    {
        private static readonly Random _random = new Random();
        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Digits = "0123456789";

        public static string GenerateOrderCode()
        {
            // 6 ky tu dau: YYMMDD
            string datePart = DateTime.UtcNow.AddHours(7).ToString("yyMMdd");

            // 4 chu cai ngau nhien
            var randomLetters = Enumerable.Range(0, 4)
                .Select(_ => Letters[_random.Next(Letters.Length)])
                .ToList();

            // 2 so ngau nhien
            var randomDigits = Enumerable.Range(0, 2)
                .Select(_ => Digits[_random.Next(Digits.Length)])
                .ToList();

            // Gop 6 ky tu sau va xao tron ngau nhien
            var suffixChars = randomLetters.Concat(randomDigits)
                .OrderBy(_ => _random.Next())
                .ToArray();

            string suffixPart = new string(suffixChars);

            return $"{datePart}{suffixPart}";
        }
    }
}
