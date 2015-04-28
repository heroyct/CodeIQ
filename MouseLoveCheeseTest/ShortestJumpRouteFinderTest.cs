using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MouseLoveCheese.MouseJump;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MouseLoveCheeseTest
{
    [TestClass]
    public class JumpRouteGeneraterTest : MouseJumpTestBase
    {
        [TestMethod]
        public void TestFindShortestJumpRoute()
        {
            ShortestJumpRouteFinder generater1 = new ShortestJumpRouteFinder(GetTestTable1());
            List<JumpRoute> routes1 = generater1.FindShortestJumpRoute(new Cell(0, 0)).ToList();
            Assert.AreEqual(1, routes1.Count);
            Assert.AreEqual("0,0 2,1 0,2 1,4 3,3 4,1 2,0 1,2 0,4", routes1[0].ToString());
            // 2×2
            ShortestJumpRouteFinder generater2 = new ShortestJumpRouteFinder(GetTestTable2());
            List<JumpRoute> routes2 = generater2.FindShortestJumpRoute(new Cell(0, 0)).ToList();
            Assert.AreEqual(0, routes2.Count);
            Assert.AreEqual(1, generater2.GetRouteEnumerator.Count());
            Assert.AreEqual("0,0", generater2.GetRouteEnumerator.ToList()[0].ToString());
            // 2x3
            ShortestJumpRouteFinder generater3 = new ShortestJumpRouteFinder(GetTestTable3());
            List<JumpRoute> routes3 = generater3.FindShortestJumpRoute(new Cell(0, 0)).ToList();
            Assert.AreEqual(1, routes3.Count);
            Assert.AreEqual("0,0 1,2", routes3[0].ToString());
            // 3x4
            ShortestJumpRouteFinder generater4 = new ShortestJumpRouteFinder(GetTestTable4());
            List<JumpRoute> routes4 = generater4.FindShortestJumpRoute(new Cell(1, 0)).ToList();
            Assert.AreEqual(1, routes4.Count);
            Assert.AreEqual("1,0 2,2 0,3 1,1 2,3 0,2 2,1 1,3 0,1 2,0", routes4[0].ToString());
            // 2x2例外
            ShortestJumpRouteFinder generater5 = new ShortestJumpRouteFinder(GetTestTable2());
            bool hasException = false;
            try
            {
                generater5.FindShortestJumpRoute(new Cell(-1, 0));
            }
            catch (JumpException ex)
            {
                Assert.AreEqual("有効なスタート場所ではありません。", ex.Message);
                hasException = true;
            }
            Assert.AreEqual(true, hasException);
        }

        [TestMethod]
        public void TestGenerateJumpRouteByCurrentRoutes()
        {
            // 5x5 (0,0)からスタートCanJumpeToNext
            ShortestJumpRouteFinder finder1 = new ShortestJumpRouteFinder(GetTestTable1());
            // ReSharper disable once PossibleNullReferenceException
            List<JumpRoute> routes = finder1.GetType()
                .GetField("routes", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField)
                .GetValue(finder1) as List<JumpRoute>;
            JumpRoute route = new JumpRoute(finder1.Table, new Cell(0, 0));
            // ReSharper disable once PossibleNullReferenceException
            routes.Add(route);
            finder1.GetType().InvokeMember("GenerateJumpRouteByCurrentRoutes",
              BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, finder1, null);

            List<JumpRoute> routesNew1 = finder1.GetRouteEnumerator.ToList();
            Assert.AreEqual(2, routesNew1.Count);
            Assert.AreEqual(true, routesNew1.Any(a => a.ToString() == "0,0 2,1"));
            Assert.AreEqual(true, routesNew1.Any(a => a.ToString() == "0,0 1,2"));

            // 5x5 (0,0) (2,1)からスタート
            ShortestJumpRouteFinder finder2 = new ShortestJumpRouteFinder(GetTestTable1());
            // ReSharper disable once PossibleNullReferenceException
            List<JumpRoute> routes2 = finder2.GetType()
                .GetField("routes", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField)
                .GetValue(finder2) as List<JumpRoute>;
            JumpRoute route2 = new JumpRoute(finder2.Table, new Cell(0, 0));
            route2.AddCell(new Cell(2, 1));
            // ReSharper disable once PossibleNullReferenceException
            routes2.Add(route2);
            finder2.GetType().InvokeMember("GenerateJumpRouteByCurrentRoutes",
              BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, finder2, null);

            List<JumpRoute> routesNew2 = finder2.GetRouteEnumerator.ToList();
            Assert.AreEqual(5, routesNew2.Count);
            Assert.AreEqual(true, routesNew2.Any(a => a.ToString() == "0,0 2,1 4,0"));
            Assert.AreEqual(true, routesNew2.Any(a => a.ToString() == "0,0 2,1 0,2"));
            Assert.AreEqual(true, routesNew2.Any(a => a.ToString() == "0,0 2,1 1,3"));
            Assert.AreEqual(true, routesNew2.Any(a => a.ToString() == "0,0 2,1 4,2"));
            Assert.AreEqual(true, routesNew2.Any(a => a.ToString() == "0,0 2,1 3,3"));
        }

        [TestMethod]
        public void TestRefreshMinRouteStep()
        {
            // チーズゲットしていない
            ShortestJumpRouteFinder finder1 = new ShortestJumpRouteFinder(GetTestTable1());
            // ReSharper disable once PossibleNullReferenceException
            List<JumpRoute> routes = finder1.GetType()
                .GetField("routes", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField)
                .GetValue(finder1) as List<JumpRoute>;
            JumpRoute route = new JumpRoute(finder1.Table);
            finder1.GetType().InvokeMember("RefreshMinRouteStep",
              BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, finder1, new object[] { route });
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(0, finder1.MinSteps);

            // チーズ全部ゲットした
            ShortestJumpRouteFinder finder2 = new ShortestJumpRouteFinder(GetTestTable1());
            JumpRoute route2 = GetJumpRoute1();
            finder2.GetType().InvokeMember("RefreshMinRouteStep",
              BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, finder2, new object[] { route2 });
            Assert.AreEqual(7, finder2.MinSteps);

            // チーズ全部ゲットして、step数が現在の最短より小さい
            ShortestJumpRouteFinder finder3 = new ShortestJumpRouteFinder(GetTestTable1());
            JumpRoute route3 = GetJumpRoute1();
            // ReSharper disable once PossibleNullReferenceException
            finder3.GetType().GetProperty("MinSteps").SetValue(finder3, 3);
            finder3.GetType().InvokeMember("RefreshMinRouteStep",
              BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, finder3, new object[] { route3 });
            Assert.AreEqual(3, finder3.MinSteps);

            // チーズ全部ゲットして、step数が現在の最短より大きい
            ShortestJumpRouteFinder finder4 = new ShortestJumpRouteFinder(GetTestTable1());
            JumpRoute route4 = GetJumpRoute1();
            finder4.GetType().GetProperty("MinSteps").SetValue(finder4, 8);
            finder4.GetType().InvokeMember("RefreshMinRouteStep",
              BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, finder4, new object[] { route4 });
            Assert.AreEqual(7, finder4.MinSteps);
            //RefreshMinRouteStep
        }
    }     
}
