using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    public class Class1
    {
        private static int Func(int n)
        {
            long i = 1, s = 0;

            for (int q = 0; q < n; q++)
                i *= q + 1;

            while (i > 0)
            {
                s += i % 10;
                i /= 10;
            }

            return (int) s;
        }

        private static async Task<int> FactorialDigitSum(int n)
        {
            return await Task.Run(() => Func(n));
        }


        private static async Task LetsSayUserClickedAButtonOnGuiMethodAsync()
        {
            var result = await GetTheMagicNumberAsync();
            Console.WriteLine(result);
        }

        private static async Task<int> GetTheMagicNumberAsync()
        {
            return await IKnowIGuyWhoKnowsAGuyAsync();
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuyAsync()
        {
            return await IKnowWhoKnowsThisAsync(10) + await IKnowWhoKnowsThisAsync(5);
        }

        private static async Task<int> IKnowWhoKnowsThisAsync(int n)
        {
            return await FactorialDigitSum(n);
        }

        public static void Main(string[] args)
        {
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethodAsync());
            Console.Read();
        }
}
}
