using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitilabAPICore
{
    public class Constants
    {
        public static readonly string host = "http://rr-kzn-slis.citisoft.ru:8088/websync.asmx/";
        public static readonly string rc_getInquiryAuth = "RC_GetInquiryAuth";
        public static readonly string rc_getInquiryJournal = "RC_GetInquiriesJournal";
        public static readonly string rc_getPDFResult = "RC_GetPDFResult";
        public static readonly string tempFilesDirectory = Environment.CurrentDirectory + @"\..\TempFiles\";
    }
}
