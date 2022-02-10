using System.Security.Cryptography;
using System.Text;

namespace cryptographyPlayground
{
    public class Methods
    {
        // Encrypt and Decrypt using example:
        // https://docs.microsoft.com/pt-br/dotnet/standard/security/encrypting-data
        public void Code()
        {
            try
            {
                using (FileStream fileStream = new("TestData.txt", FileMode.OpenOrCreate))
                {
                    using (Aes aes = Aes.Create())
                    {
                        byte[] key =
                        {
                            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                        };
                        aes.Key = key;

                        byte[] iv = aes.IV;
                        fileStream.Write(iv, 0, iv.Length);

                        using (CryptoStream cryptoStream = new(
                            fileStream,
                            aes.CreateEncryptor(),
                            CryptoStreamMode.Write))
                        {
                            using (StreamWriter encryptWriter = new(cryptoStream))
                            {
                                encryptWriter.WriteLine("Hello World!");
                            }
                        }
                    }
                }

                Console.WriteLine("The file was encrypted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The encryption failed. {ex}");
            }
        }
        public async void Decode()
        {
            try
            {
                using (FileStream fileStream = new("TestData.txt", FileMode.Open))
                {
                    using (Aes aes = Aes.Create())
                    {
                        byte[] iv = new byte[aes.IV.Length];
                        int numBytesToRead = aes.IV.Length;
                        int numBytesRead = 0;
                        while (numBytesToRead > 0)
                        {
                            int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                            if (n == 0) break;

                            numBytesRead += n;
                            numBytesToRead -= n;
                        }

                        byte[] key =
                        {
                            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                            0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                        };

                        using (CryptoStream cryptoStream = new(
                           fileStream,
                           aes.CreateDecryptor(key, iv),
                           CryptoStreamMode.Read))
                        {
                            using (StreamReader decryptReader = new(cryptoStream))
                            {
                                string decryptedMessage = await decryptReader.ReadToEndAsync();
                                Console.WriteLine($"The decrypted original message: {decryptedMessage}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The decryption failed. {ex}");
            }
        }

        /// <summary>
        /// Convert string to binary
        /// </summary>
        /// <param name="inputStr"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        //public string ConvertStringToBinary(string inputStr, Encoding encoding)
        //{
        //    byte[] data = encoding.GetBytes(inputStr);
        //    return string.Join(" ", data.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
        //}
        //public void XorCalc()
        //{
        //    Console.Write("Message: ");
        //    string message = Console.ReadLine();
        //    Console.Write("\nSecretKey: ");
        //    string secretKey = Console.ReadLine();

        //    var messageBin = ConvertStringToBinary(message, Encoding.ASCII);
        //    var secretKeyBin = ConvertStringToBinary(secretKey, Encoding.ASCII);
        //}
        //public byte[] ConvertStringToBinary(string inputStr, Encoding encoding)
        //=> encoding.GetBytes(inputStr);
        public void XorCalc()
        {
            Console.Write("Message: ");
            byte[] message = Encoding.ASCII.GetBytes(Console.ReadLine());
            Console.Write("\nSecretKey: ");
            byte[] secretKey = Encoding.ASCII.GetBytes(Console.ReadLine());

            int maior = Math.Max(message.Length, secretKey.Length);
            message = adjustSize(message, maior);
            secretKey = adjustSize(secretKey, maior);

            byte[] resultado = new byte[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                resultado[i] = (byte)(message[i] ^ secretKey[i]);
            }

            string txtOutput = BitConverter.ToString(resultado);
            Console.WriteLine(txtOutput);
            //var messageBin = ConvertStringToBinary(message, Encoding.ASCII);
            //var secretKeyBin = ConvertStringToBinary(secretKey, Encoding.ASCII);
            //byte[] resultado = null;

            //for (int i = 0; i < messageBin.Length; i++)
            //{
            //    resultado[i] = (byte)(messageBin[i] ^ secretKeyBin[i]);
            //}

        }

        static byte[] adjustSize(byte[] data, int size)
        {
            if (data.Length == size) return data;
            if (data.Length > size) throw new InvalidOperationException();

            byte[] result = new byte[size];

            // get start index
            int start = size - data.Length;
            Buffer.BlockCopy(data, 0, result, start, data.Length);

            return result;
        }
    }

}
