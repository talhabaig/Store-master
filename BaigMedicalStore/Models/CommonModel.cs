using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using static BaigMedicalStore.Common.Enumeration;

namespace BaigMedicalStore.Models
{
    public class MessageModel
    {
        public string Message { get; set; }
        public string Type { get; set; } = MessageType.Success;
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
    }
}