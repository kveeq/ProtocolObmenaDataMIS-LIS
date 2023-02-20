using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SitilabAPICore.Model
{
    [XmlRoot(ElementName = "f")]
    public class F
    {

        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }

        [XmlAttribute(AttributeName = "v")]
        public string V { get; set; }
    }

    [XmlRoot(ElementName = "o")]
    public class O
    {

        [XmlElement(ElementName = "f")]
        public List<F> F { get; set; }

        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }

        [XmlElement(ElementName = "s")]
        public List<S> S { get; set; }

        [XmlElement(ElementName = "r")]
        public List<R> R { get; set; }
    }

    [XmlRoot(ElementName = "s")]
    public class S
    {

        [XmlElement(ElementName = "o")]
        public O O { get; set; }

        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }

        [XmlElement(ElementName = "r")]
        public R R { get; set; }
    }

    [XmlRoot(ElementName = "r")]
    public class R
    {

        [XmlAttribute(AttributeName = "n")]
        public string N { get; set; }

        [XmlAttribute(AttributeName = "i")]
        public int I { get; set; }
    }

    [XmlRoot(ElementName = "content")]
    public class ClaimModel
    {

        [XmlElement(ElementName = "o")]
        public O O { get; set; }
    }
}