using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Serialization;

namespace SitilabAPICore.Model
{
    //[XmlRoot(ElementName = "string")]
    [XmlRoot(ElementName = "yeti")]
    public class RC_GetInquiryAuth
    {

        [XmlAttribute(AttributeName = "xmlns")]
        public string? Xmlns { get; set; }

        [XmlText]
        public string? Text { get; set; }
    }
}