using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Patuh.Web.Controllers.ViewModels
{
    public class TrImageAttachmentViewModel
    {
        public long Id { get; set; }
        public long HeaderId { get; set; }
        public byte[] Image { get; set; }
        public int Sequence { get; set; }
        public string cCreated { get; set; }
        public string cLastUpdated { get; set; }
        public DateTime dCreated { get; set; }
        public DateTime dLastUpdated { get; set; }

    }  
  }


