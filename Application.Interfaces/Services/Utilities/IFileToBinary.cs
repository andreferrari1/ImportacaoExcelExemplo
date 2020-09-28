using System.IO;

namespace Application.Interfaces.Utilities
{
    public interface IFileToBinary
    {
        void Convert(Stream File, ref byte[] FileConverted);

    }
}
