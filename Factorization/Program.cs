using System;

namespace Factorization
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("パラメータは三つを入力してくささい。");
                return;
            }
            if (IsInt(args[0]) == false || IsInt(args[1]) == false || IsInt(args[2]) == false)
            {
                Console.WriteLine("数字を入力してください。");
                return;
            }
            var result = Factorisation.Calc(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));
            Console.WriteLine("以下は結果です");
            if (result.Count == 0)
            {
                Console.WriteLine("因数分解できません");
                return;
            } 
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
