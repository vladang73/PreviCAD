using Previgesst.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Previgesst.ViewModels
{
    public class UploadViewModel
    {
        public int ItemId { get; set; }
        public string Extensions { get; set; }
        public string Message { get; set; }

        public TypeUpload typeUpload { get; set; }

    }
}