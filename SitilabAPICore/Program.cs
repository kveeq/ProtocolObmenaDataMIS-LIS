using SitilabAPICore;
using SitilabAPICore.Service;
using System;

namespace SitilabApiCore
{
    public class Program
    {
        ///Алгоритм: 
        /// <summary>
        /// 1) Подключиться к апи
        /// 2) Забрать base64
        /// 3) Сконвертировать base64 -> bzip2
        /// 4) Сконвертировать bzip2 -> xml
        /// 5) Пробежаться по xml и выполнить пункты 3 и 4 для получения внутренних данных документа
        /// 6) ...
        /// </summary>
        /// <param name="Algoritm"></param>
        public static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //Console.OutputEncoding = System.Text.Encoding.GetEncoding("UTF-16");
            //Console.InputEncoding = System.Text.Encoding.GetEncoding("UTF-16");
            if (!Directory.Exists(Constants.tempFilesDirectory))
                Directory.CreateDirectory(Constants.tempFilesDirectory);
            Console.WriteLine("Hello, world!");
            RC_GetInquiryAuthService rr = new RC_GetInquiryAuthService();
            //rr.GetInquiryAuth("20310357");
            rr.GetPdfResult("20310352", $"PDF_{DateTime.Now.ToString("dd_MM_yyyy")}.pdf");
        }
    }
}