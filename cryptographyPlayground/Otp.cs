using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cryptographyPlayground
{
    public class Otp
    {
        public void totpTeste()
        {
            string msg = "JBSWY3DPEHPK3PXP";
            byte[] secretKey = Base32Encoding.ToBytes(msg);

            //Console.WriteLine("Bytes [DEC] " + string.Join(' ', secretKey.Select(o => o.ToString())));
            //Console.WriteLine("Bytes [HEX] " + string.Join(' ', secretKey.Select(o => o.ToString("X"))));
            //Console.WriteLine("B32         " + msg);
            //Console.WriteLine("B64         " + Convert.ToBase64String(secretKey));


            var totp = new Totp(secretKey, 30, OtpHashMode.Sha1);

            while (true)
            {
                var totpCode = totp.ComputeTotp(DateTime.UtcNow);
                Console.WriteLine(totpCode);
                for (int i = 0; i < 30; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(1000);
                }
                Console.WriteLine();
            }
        }
    }
}
