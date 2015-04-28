using System.Collections.Generic;
using System.Linq;
using Factorization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FactorizationTest
{
    [TestClass]
    public class FactorisationTest
    {
        [TestMethod]
        public void TestGetMultiplicatiMember_1()
        {
            // 1 1*1 -1*-1
            TestGetMultiplicatiMember(1, new List<MultiplicatiMember>() { new MultiplicatiMember(1, 1),new MultiplicatiMember(-1,-1) });
        }
        [TestMethod]
        public void TestGetMultiplicatiMember_2()
        {
            // 2 1*2 2*1 -1*-2,-2*-1
            TestGetMultiplicatiMember(2,
                new List<MultiplicatiMember>()
                {
                    new MultiplicatiMember(1, 2),
                    new MultiplicatiMember(2, 1),
                    new MultiplicatiMember(-1, -2),
                    new MultiplicatiMember(-2, -1)
                });
        }
        [TestMethod]
        public void TestGetMultiplicatiMember_4()
        {
            // 4 1*4 2*2 4*1
            TestGetMultiplicatiMember(4,
                new List<MultiplicatiMember>()
                {
                    new MultiplicatiMember(1, 4),
                    new MultiplicatiMember(2, 2),
                    new MultiplicatiMember(4, 1),
                    new MultiplicatiMember(-1, -4),
                    new MultiplicatiMember(-2, -2),
                    new MultiplicatiMember(-4, -1)
                });
        }
        [TestMethod]
        public void TestGetMultiplicatiMember_12()
        {
            // 12 1*12 2*6 6*2 3*4 4*3 12*1
            TestGetMultiplicatiMember(12,
                new List<MultiplicatiMember>()
                {
                    new MultiplicatiMember(1, 12),
                    new MultiplicatiMember(2, 6),
                    new MultiplicatiMember(6, 2),
                    new MultiplicatiMember(3, 4),
                    new MultiplicatiMember(4, 3),
                    new MultiplicatiMember(12, 1),
                     new MultiplicatiMember(-1, -12),
                    new MultiplicatiMember(-2, -6),
                    new MultiplicatiMember(-6, -2),
                    new MultiplicatiMember(-3, -4),
                    new MultiplicatiMember(-4, -3),
                    new MultiplicatiMember(-12, -1)
                });
        }
        [TestMethod]
        public void TestGetMultiplicatiMember_minus1()
        {
            // -1 -1*1 1*-1
            TestGetMultiplicatiMember(-1, new List<MultiplicatiMember>() { new MultiplicatiMember(1, -1),new MultiplicatiMember(-1,1) });
        }
        [TestMethod]
        public void TestGetMultiplicatiMember_minus2()
        {
            // -2 -1*2 -2*1 1*-2 2*-1
            TestGetMultiplicatiMember(-2, new List<MultiplicatiMember>()
            {
                new MultiplicatiMember(-1, 2),
                new MultiplicatiMember(-2, 1),
                new MultiplicatiMember(1, -2),
                new MultiplicatiMember(2, -1)
            });
        }
        [TestMethod]
        public void TestGetMultiplicatiMember_minus4()
        {
            // -4 -1*4 -2*2 2*-2 -4*1
            TestGetMultiplicatiMember(-4,
                new List<MultiplicatiMember>()
                {
                    new MultiplicatiMember(-1, 4),
                    new MultiplicatiMember(-2, 2),
                    new MultiplicatiMember(2, -2),
                    new MultiplicatiMember(-4, 1),
                    new MultiplicatiMember(4, -1),
                    new MultiplicatiMember(1, -4),
                });
        }
        [TestMethod]
        public void TestGetMultiplicatiMember_minus12()
        {
            // -12 -1*12 -2*6 -6*2 -3*4 -4*3 -12*1
            TestGetMultiplicatiMember(-12,
                new List<MultiplicatiMember>()
                {
                    new MultiplicatiMember(-1, 12),
                    new MultiplicatiMember(1, -12),
                    new MultiplicatiMember(12, -1),
                    new MultiplicatiMember(-2, 6),
                    new MultiplicatiMember(-6, 2),
                    new MultiplicatiMember(2, -6),
                    new MultiplicatiMember(6, -2),
                    new MultiplicatiMember(-3, 4),
                    new MultiplicatiMember(-4, 3),
                    new MultiplicatiMember(3, -4),
                    new MultiplicatiMember(4, -3),
                    new MultiplicatiMember(-12, 1)
                });
        }
        [TestMethod]
        public void TestGetMultiplicatiMember(int input,List<MultiplicatiMember> expectResult)
        {
            var msg = "input is " + input;
            var result = Factorisation.GetMultiplicatiMember(input);
            Assert.AreEqual(expectResult.Count, result.Count(),msg);

            bool isSame = true;
            foreach (var mem in result)
            {
                if (!expectResult.Any(a => a.A == mem.A && a.B == mem.B))
                {
                    isSame = false;
                    break;
                }
            }
            Assert.AreEqual(isSame, true, msg);
        }

        [TestMethod]
        public void TestCalc_1()
        {
            TestCalc(3,10,3,new FactorisationResult(1,3,3,1));
        }

        [TestMethod]
        public void TestCalc_2()
        {
            TestCalc(6, -1, -15, new FactorisationResult(2, 3, 3, -5));
        }

        [TestMethod]
        public void TestCalc_3()
        {
            TestCalc(12, -1, -6, new FactorisationResult(3, 2, 4, -3));
        }

        [TestMethod]
        public void TestCalc_4()
        {
            TestCalc(3, -8, 4, new FactorisationResult(1, -2, 3, -2));
        }

        [TestMethod]
        public void TestCalc_5()
        {
            TestCalc(3, 5, 7, null);
        }

        public void TestCalc(int inputA,int inputB,int inputC,FactorisationResult expectResult)
        {
            var msg = string.Format("error input is {0},{1},{2}", inputA, inputB, inputC);
            var result = Factorisation.Calc(inputA, inputB, inputC);
            if (expectResult == null)
            {
                Assert.AreEqual(result.Count,0,msg);
                return;
            }
            Assert.AreEqual(result.Any(a => a.A == expectResult.A
                                            && a.B == expectResult.B
                                            && a.C == expectResult.C
                                            && a.D == expectResult.D), true, msg);
        }
    }
}
