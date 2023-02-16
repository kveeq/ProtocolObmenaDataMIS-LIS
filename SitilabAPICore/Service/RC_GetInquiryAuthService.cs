using ICSharpCode.SharpZipLib.BZip2;
using RestSharp;
using SitilabAPICore.Model;
using System;
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

        public async Task<RC_GetInquiryAuth> GetInquiryAuth(string InquiryId)
        {
            RC_GetInquiryAuth TodoItems = new RC_GetInquiryAuth();
            var request = new RestRequest(string.Format(Constants.rc_getInquiryAuth), Method.Post);
            Events.Requestheaders(ref request);     
            request.AddBody($"<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<content>\r\n<e n=\"login\" v=\"testint\" t=\"string\"/>\r\n<e n=\"password\" v=\"RqMtGI1k\" t=\"string\" />\r\n<e n=\"requestId\" v=\"{InquiryId}\" t=\"string\" />\r\n</content>");
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
                    Console.WriteLine(Fio+"\n");
                }
               
                Console.WriteLine($"\n\n{TodoItems.Text}\n\n");
                //TodoItems = XmlSerializer.Deserialize<RC_GetInquiryAuthModel>(content, serializerOptions);
            }

            return TodoItems;
        }
    }


}