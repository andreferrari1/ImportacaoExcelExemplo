using Application.Interfaces.Utilities;
using System.IO;

namespace Application.Utilities
{
    public class FileToBinary : IFileToBinary
    {
        public void Convert(Stream File, ref byte[] FileConverted)
        {
            byte[] binary = null;
            using (var fs1 = File)
            using (var ms1 = new System.IO.MemoryStream())
            {
                fs1.CopyTo(ms1);
                binary = ms1.ToArray();
            }
            FileConverted = binary;
        }
    }
}