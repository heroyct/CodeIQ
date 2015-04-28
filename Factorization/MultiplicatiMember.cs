namespace Factorization
{
    /// <summary>
    /// 数字をa x bに分解される結果を表すクラス
    /// </summary>
    public class MultiplicatiMember
    {
        public MultiplicatiMember(int a,int b)
        {
            this.A = a;
            this.B = b;
        }

        public int A { get; set; }

        public int B { get; set; }
    }
}
