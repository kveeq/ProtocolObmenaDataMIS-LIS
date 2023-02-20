using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SitilabAPICore.Model
{
    [XmlRoot(ElementName = "f")]
    public class FPDF
    {

        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }

        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }

    [XmlRoot(ElementName = "content")]
    public class ModelForGetPDFFile
    {

        [XmlElement(ElementName = "f")]
        public List<FPDF> FPDF { get; set; }
    }
}
