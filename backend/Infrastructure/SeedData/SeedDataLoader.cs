using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SeedData
{
    public class SeedDataLoader
    {
        private static readonly string SeedRoot =
        Path.Combine(AppContext.BaseDirectory, "SeedData", "Files");

        public static byte[] LoadImage(string fileName) =>
            File.ReadAllBytes(Path.Combine(SeedRoot, fileName));
    }
}
