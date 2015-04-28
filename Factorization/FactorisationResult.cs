namespace Factorization
{
    /// <summary>
    /// 因数分解の結果
    /// </summary>
    public class FactorisationResult
    {
        public FactorisationResult(int a, int b, int c, int d)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
        }

        public int A { get; set; }

        public int B { get; set; }

        public int C { get; set; }

        public int D { get; set; }

        public override string ToString()
        {
            return this.A + "," + this.B + "," + this.C + "," + this.D;
        }
    }
}
