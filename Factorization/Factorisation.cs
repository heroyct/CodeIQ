using System;
using System.Collections.Generic;
using System.Linq;

namespace Factorization
{
    public class Factorisation
    {
        /// <summary>
        /// 数字をa * bの形に分解できる一覧を返します
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MultiplicatiMember> GetMultiplicatiMember(int number)
        {
            List<MultiplicatiMember> members = new List<MultiplicatiMember>();
            int numberPlus = Math.Abs(number);
            // -number < a <= number
            for (int i = numberPlus * -1; i <= numberPlus; i++)
            {
                for (int j = numberPlus * -1; j <= numberPlus; j++)
                {
                    if (i * j == number)
                    {
                        members.Add(new MultiplicatiMember(i, j));
                    }
                }
            }
            return members;
        }

        private static void AddMemberIfNotExist(List<MultiplicatiMember> member, int a, int b)
        {
            if (!member.Any(mem => mem.A == a && mem.B == b))
            {
                member.Add(new MultiplicatiMember(a, b));
            }
        }

        /// <summary>
        /// 因数分解処理
        /// αx2+βx＋γ > (ax+b)(cx+d)
        /// </summary>
        /// <param name="ka">α</param>
        /// <param name="kb">β</param>
        /// <param name="ky">γ</param>
        /// <returns></returns>
        public static List<FactorisationResult> Calc(int ka, int kb, int ky)
        {
            var results = new List<FactorisationResult>();
            // a * c = kaの全パターンを探す
            var acMembers = GetMultiplicatiMember(ka).ToArray();
            // b * d = kyの全パターンを探す
            var bdMembers = GetMultiplicatiMember(ky).ToArray();
            // ad + bc = kbを満たすパターンを探す
            foreach (var memac in acMembers)
            {
                foreach (var membd in bdMembers)
                {
                    if (memac.A * membd.B + memac.B * membd.A == kb)
                    {
                        results.Add(new FactorisationResult(memac.A, membd.A, memac.B, membd.B));
                    }
                }
            }
            return results;
        }
    }
}
