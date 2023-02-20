using ICSharpCode.SharpZipLib.BZip2;
using RestSharp;
using SitilabAPICore.Model;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SitilabAPICore.Service
{
    public class RC_GetInquiryAuthService
    {
        JsonSerializerOptions serializerOptions;
        RestClient restClient;
        public RC_GetInquiryAuthService()
        {
            serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            restClient = new RestClient(@$"{Constants.host}");
        }

        public async Task<FirstXmlModel> GetInquiryAuth(string InquiryId)
        {
            FirstXmlModel TodoItems = new FirstXmlModel();
            var request = new RestRequest(string.Format(Constants.rc_getInquiryAuth), Method.Post);
            Events.Requestheaders(ref request);     
            request.AddBody($"<?xml version=\"1.0\" encoding=\"windows-1251\" ?>\r\n<content>\r\n<e n=\"login\" v=\"testint\" t=\"string\"/>\r\n<e n=\"password\" v=\"RqMtGI1k\" t=\"string\" />\r\n<e n=\"requestId\" v=\"{InquiryId}\" t=\"string\" />\r\n</content>");
            var response = restClient.Execute(request);
            if (response.IsSuccessful)
            {
                string content = response.Content;
                Console.WriteLine("Кол-во символов: " + content.Length);
                content = content.GetBase64String();
                TodoItems.Text = content;

                var decoderResult = content.DecodeBase64Async();
                await CommonServicesExtensions.UnzippingBzip2FileAsync(decoderResult.Result);
                var deserializeModel = await CommonServices<ClaimModel>.DeserializeXml(@"decompressed.xml");

                string Fio = "";
                foreach (var item in deserializeModel.O.S)
                {
                    item.O.F.Where(x => x.N == "firstName" || x.N == "lastame" || x.N == "middleName").Select(x => x.V).ToList().ForEach((x) => Fio = x + " ");
                    Console.WriteLine(Fio+"\n");
                }
               
                Console.WriteLine($"\n\n{TodoItems.Text}\n\n");
                //TodoItems = XmlSerializer.Deserialize<RC_GetInquiryAuthModel>(content, serializerOptions);
            }

            return TodoItems;
        }

        public async Task<FirstXmlModel> GetInquiryJournal(string InquiryId)
        {
            FirstXmlModel TodoItems = new FirstXmlModel();
            var request = new RestRequest(string.Format(Constants.rc_getInquiryAuth), Method.Post);
            Events.Requestheaders(ref request);
            request.AddBody($"<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\r\n<content>\r\n    <e n=\"login\" v=\"testint\" t=\"string\"/>\r\n    <e n=\"password\" v=\"RqMtGI1k\" t=\"string\" />\r\n    <e n=\"fromDate\" v=\"13.12.2022\" t=\"datetime\" />\r\n    <e n=\"tillDate\" v=\"13.02.2023\" t=\"datetime\" />\r\n</content>");
            var response = restClient.Execute(request);
            if (response.IsSuccessful)
            {
                string content = response.Content;
                content = content.GetBase64String();
                TodoItems.Text = content;

                var decoderResult = await content.DecodeBase64Async();
                await CommonServicesExtensions.UnzippingBzip2FileAsync(decoderResult);
                var deserializeModel = await CommonServices<ClaimModel>.DeserializeXml(@"decompressed.xml");

                string Fio = "";
                foreach (var item in deserializeModel.O.S)
                {
                    item.O.F.Where(x => x.N == "firstName" || x.N == "lastame" || x.N == "middleName").Select(x => x.V).ToList().ForEach((x) => Fio = x + " ");
                    Console.WriteLine(Fio + "\n");
                }

                Console.WriteLine($"\n\n{TodoItems.Text}\n\n");
            }

            return TodoItems;
        }  
        
        public async Task<FirstXmlModel> GetPdfResult(string InquiryId, string filePath)
        {
            FirstXmlModel TodoItems = new FirstXmlModel();
            var request = new RestRequest(string.Format(Constants.rc_getPDFResult), Method.Post);
            Events.Requestheaders(ref request);
            request.AddBody($"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<content>\r\n    <e n=\"login\" t=\"string\" v=\"testint\"/>\r\n    <e n=\"password\" t=\"string\" v=\"RqMtGI1k\"/>\r\n    <e n=\"requestId\" v=\"{InquiryId}\"/>\r\n</content>");
            var response = restClient.Execute(request);
            if (response.IsSuccessful)
            {
                string content = response.Content;
                Console.WriteLine(content.Length);
                content = content.Replace("\r\n", "");
                content = content.Replace("xmlns=\"http://citilab.ru\">", "n=\"");
                content = content.Replace("</string>", "\" />");  
                var deserializeModel1 = CommonServices<@string>.DeserializeXml(content, false).Result;
                TodoItems.Text = content;

                var decoderResult = deserializeModel1.n.DecodeBase64Async("xml1TempFileForGetPDF.xml").Result;
                var deserializeModel =  CommonServices<ModelForGetPDFFile>.DeserializeXml(@"xml1TempFileForGetPDF.xml").Result;
                decoderResult = deserializeModel.FPDF.Where(x=>x.N == "pdfResults").FirstOrDefault().V.DecodeBase64Async(filePath).Result;

                Console.WriteLine($"\n\nSucces GET PDF File\n\n");
            }

            return TodoItems;
        }
    }
}