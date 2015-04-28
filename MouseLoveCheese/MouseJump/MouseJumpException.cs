using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseLoveCheese.MouseJump
{
    [Serializable]
    public class JumpException : ApplicationException
    {
        public int ErrorNumber { get; private set; }

        public override string Message
        {
            get
            {
                string msg = "";
                switch (ErrorNumber)
                {
                    case 1:
                        msg = "有効なスタート場所ではありません。";
                        break;
                }
                return msg;
            }
        }

        public JumpException(int errorNumber)
        {
            this.ErrorNumber = errorNumber;
        }
    }
}
