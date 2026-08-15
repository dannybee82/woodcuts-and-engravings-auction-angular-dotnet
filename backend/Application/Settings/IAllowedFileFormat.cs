using FileSignatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Settings
{
    public interface IAllowedFileFormat
    {
        string Extension { get; set; }

        IEnumerable<FileFormat> FileFormat { get; set; }
    }
}
