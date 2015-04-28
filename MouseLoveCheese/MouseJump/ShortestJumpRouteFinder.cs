using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseLoveCheese.MouseJump
{
    public class ShortestJumpRouteFinder
    {
        private readonly List<JumpRoute> routes = new List<JumpRoute>();

        public Table Table { get; private set; }

        public ShortestJumpRouteFinder(Table table)
        {
            this.Table = table;
        }

        #region プロパティ
        public bool FindComplted { get; private set; }

        public int MinSteps { get; private set; }

        public IEnumerable<JumpRoute> GetRouteEnumerator
        {
            get { return routes; }
        }
        #endregion

        /// <summary>
        /// スタート場所から最短距離のrouteを探す
        /// </summary>
        public IEnumerable<JumpRoute> FindShortestJumpRoute(Cell startCell)
        {
            ValidateCell(startCell);

            JumpRoute route = new JumpRoute(this.Table, startCell);
            routes.Add(route);
            while (true)
            {
                GenerateJumpRouteByCurrentRoutes();
                // すべてのルートが探し完了
                if (FindComplted)
                {
                    break;
                }
            }
            return routes.Where(a => a.IsEatAllCheese && a.Steps == MinSteps);
        }

        private void GenerateJumpRouteByCurrentRoutes()
        {
            List<JumpRoute> newRoutes = new List<JumpRoute>();
            // 現在すべてrouteに対して、次に飛べる場所の組み合わせroutelistを作成
            foreach (JumpRoute jr in routes)
            {
                if (jr.Complete) continue;
                newRoutes.AddRange(GenerateByOneJumpRoute(jr));
            }
            routes.AddRange(newRoutes);
            JudgeFindComplte();
        }

        /// <summary>
        /// すべてのrouteが完成できたかのかを判定
        /// </summary>
        private void JudgeFindComplte()
        {
            FindComplted = routes.All(a => a.Complete);
        }

        private void ValidateCell(Cell startCell)
        {
            if (!Table.Exists(startCell))
                throw new JumpException(1);
        }

        /// <summary>
        /// 最短距離のrouteになる可能性あるかどうかを判定
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        private bool IsPossibleShortestRoute(JumpRoute route)
        {
            if (MinSteps > 0)
            {
                if (route.Steps > MinSteps)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsNeedToGenerate(List<Cell> canJumps)
        {
            return canJumps.Count > 0;
        }

        /// <summary>
        /// 最短Step数を更新
        /// </summary>
        /// <param name="route"></param>
        private void RefreshMinRouteStep(JumpRoute route)
        {
            if (route.IsEatAllCheese)
            {
                if (MinSteps == 0)
                {
                    MinSteps = route.Steps;
                }
                else if (MinSteps > 0)
                {
                    if (route.Steps < MinSteps)
                    {
                        MinSteps = route.Steps;
                    }
                }
            }
        }

        private IEnumerable<JumpRoute> GenerateByOneJumpRoute(JumpRoute route)
        {
            if (!IsPossibleShortestRoute(route))
            {
                route.Complete = true;
                return Enumerable.Empty<JumpRoute>();
            }
            Cell startCell = route.LastCell;
            // 飛べる場所を計算
            Jumper jump = new Jumper(Table);
            List<Cell> canJumps = jump.GetCanJumpCells(startCell, route);
            // 新しい組み合わせ作成必要がない場合
            if (!IsNeedToGenerate(canJumps))
            {
                route.Complete = true;
                RefreshMinRouteStep(route);
                return Enumerable.Empty<JumpRoute>();
            }
            // routeのlastCellから飛べる場所の組み合わせRouteリストを作成
            return GenerateNewRoutes(route, canJumps);
        }

        private IEnumerable<JumpRoute> GenerateNewRoutes(JumpRoute route, List<Cell> canJumps)
        {
            List<JumpRoute> newRoutes = new List<JumpRoute>();
            for (int i = canJumps.Count - 1; i >= 0; i--)
            {
                JumpRoute routeNew = null;
                // 既にrouteListにあるroute
                if (i == 0)
                {
                    routeNew = route;
                }
                else
                {
                    routeNew = new JumpRoute(this.Table);
                    routeNew.AddCellList(route.GetCellsEnumerator);
                    newRoutes.Add(routeNew);
                }
                routeNew.AddCell(canJumps[i]);
            }
            return newRoutes;
        }
    }
}
