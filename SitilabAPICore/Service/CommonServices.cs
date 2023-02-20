using ICSharpCode.SharpZipLib.BZip2;
using SitilabAPICore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SitilabAPICore.Service
{
    public static class CommonServices<T>
    {
        public static async Task<T> DeserializeXml(string fileName, bool isFile = true)
        {
            T model;
            string saveXmlDocContent = "";
            if (isFile)
                saveXmlDocContent = File.ReadAllText(Constants.tempFilesDirectory + fileName, Encoding.GetEncoding(1251));
            else
                saveXmlDocContent = fileName;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(saveXmlDocContent))
            {
                model = (T)serializer.Deserialize(reader);
                //Events.Message?.Invoke(eDIMessage.Id + "\nУспех@@@");
            }

            return model;
        }
    }

    public static class CommonServicesExtensions
    {
        public static string GetBase64String(this string content)
        {
            bool isRemove = false;
            bool isLast = false;
            string tempstr = "";
            foreach (var ch in content)
            {
                if (ch == '<')
                    isRemove = true;
                else if (ch == '>')
                {
                    isRemove = false;
                    isLast = true;
                }

                if (!isRemove && !isLast)
                {
                    tempstr += ch;
                }
                isLast = false;
            }

            return tempstr;
        }

        public static async Task<string> UnzippingBzip2FileAsync(string filePath)
        {
            FileInfo zipFileName = new FileInfo(filePath);
            string decompressedFileName = "";
            using (FileStream fileToDecompressAsStream = zipFileName.OpenRead())
            {
                decompressedFileName = Constants.tempFilesDirectory + @"decompressed.xml";
                using (FileStream decompressedStream = File.Create(decompressedFileName))
                {
                    try
                    {
                        BZip2.Decompress(fileToDecompressAsStream, decompressedStream, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return decompressedFileName;
        }

        public static async Task<string> DecodeBase64Async(this string base64, string fileName = "")
        {
            if (fileName == "")
                fileName = Constants.tempFilesDirectory + $"BZIP_{DateTime.Now.ToString("dd_mm_yyyy")}.bz2";
            else
                fileName = Constants.tempFilesDirectory + fileName;

            string utfLine = base64;
            Encoding utf = Encoding.UTF8;
            Encoding win = Encoding.GetEncoding("windows-1251");

            byte[] utfArr = utf.GetBytes(utfLine);
            byte[] winArr = Encoding.Convert(win, utf, utfArr);
            string winLine = win.GetString(winArr);

            string filePath = fileName;
            await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(winLine));
            return filePath;
        }
    }
}
