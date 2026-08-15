using FileSignatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Settings
{
    public class AllowedFileFormat : IAllowedFileFormat
    {
        public string Extension { get; set; }
        public IEnumerable<FileFormat> FileFormat { get; set; }
    }
}
