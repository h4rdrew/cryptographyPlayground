using System;
using System.Security.Cryptography;
using System.Text;

namespace cryptographyPlayground
{
    public class Methods
    {
        public string XorCalc()
        {
            Console.Write("Message: ");
            byte[] message = Encoding.ASCII.GetBytes(Console.ReadLine() ?? string.Empty);
            Console.Write("\nSecretKey: ");
            byte[] secretKey = Encoding.ASCII.GetBytes(Console.ReadLine() ?? string.Empty);

            int maior = Math.Max(message.Length, secretKey.Length);
            message = adjustSize(message, maior);
            secretKey = adjustSize(secretKey, maior);

            byte[] resultado = new byte[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                resultado[i] = (byte)(message[i] ^ secretKey[i]);
            }

            string txtOutput = BitConverter.ToString(resultado);
            return txtOutput;
        }

        static byte[] adjustSize(byte[] data, int size)
        {
            if (data.Length == size) return data;
            if (data.Length > size) throw new InvalidOperationException();

            // void* result = malloc(sizeof(InvalidOperationException))
            // result.ctor();


            byte[] result = new byte[size];

            // byte* result = malloc(size)
            // result.ctor();


            // get start index
            int start = size - data.Length;
            Buffer.BlockCopy(data, 0, result, start, data.Length);

            return result;
        }

        static byte[] stringToByteArray(string str)
        {
            return Encoding.ASCII.GetBytes(str ?? string.Empty);
        }

        public void HMAC_Calculate(byte[] key, byte[] message)
        {
            var blocksize = MD5.Create();
            key = MD5.HashData(adjustSize(key, blocksize.HashSize));

            //var o_key_pad = key

            //var keyHash = MD5.HashData(stringToByteArray(key));
        }
    }

}
