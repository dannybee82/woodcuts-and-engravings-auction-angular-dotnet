using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Settings
{
    public interface IAllowedFileFormatList
    {

        List<AllowedFileFormat> AllowedFileList { get; set; }

    }
}
