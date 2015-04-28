using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorization
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("please inter the correct parameter number");
                return;
            }
            if (IsInt(args[0]) == false || IsInt(args[1]) == false || IsInt(args[2]) == false)
            {
                Console.WriteLine("please inter a integer number");
                return;
            }
            var result = Factorisation.Calc(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));
            if (result.Count == 0)
            {
                Console.WriteLine("因数分解できません");
                return;
            }
            Console.WriteLine("以下は結果です");
            foreach (var res in result)
            {
                Console.WriteLine(res);
            }
        }

        private static bool IsInt(string number)
        {
            int it = -1;
            return int.TryParse(number, out it);
        }        
    }
}
