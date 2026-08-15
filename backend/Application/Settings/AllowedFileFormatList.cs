using Domain.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Settings
{
    public class AllowedFileFormatList : IAllowedFileFormatList
    {
        public List<AllowedFileFormat> AllowedFileList { get; set; }
    }
}
