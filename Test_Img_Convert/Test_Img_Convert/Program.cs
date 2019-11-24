using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test_Img_Convert
{
    class Program
    {
        static int counter = 0;
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            Parallel.For(1, 5, StartConvert);
            sw.Stop();
            //StartConvert(1);

            Console.WriteLine(counter);
            Console.WriteLine((sw.ElapsedMilliseconds / 1000.0).ToString());
            Console.Read();
        }

        static public void StartConvert(int number)
        {
            Bitmap bitmap;
            ImageConverter converter;
            foreach (string path in Directory.GetFiles("Img" + number.ToString()))
            {
                converter = new ImageConverter();
                bitmap = new Bitmap(path);
                Stream imageStream = new MemoryStream();
                bitmap.Save(imageStream, ImageFormat.Jpeg);
                byte[] bytePhoto = (byte[])converter.ConvertTo(Image.FromStream(imageStream), typeof(byte[]));

                Console.WriteLine(GetHash(bytePhoto));
                counter++;
            }
        }
        static public string GetHash(byte[] input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(input);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
