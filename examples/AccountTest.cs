using PlatONet;
using System;
using System.Collections.Generic;
using System.Text;

namespace examples
{
    public class AccountTest
    {
        public static void Main(string[] args)
        {
            var privateKey = "0xd08baac64f52ae1b9c2ea559036650229f07f5d61d869dbb55562a9827fbaeb8";
            Account act = new Account(privateKey);
            var bytes = "0x3455444".HexToByteArray();
            var sign = act.Sign(bytes);
            Console.WriteLine(sign.ToString());
            //let data = "0x3455444";
            //let dataBuffer = Buffer.from("datatosign");
        }
    }
}
