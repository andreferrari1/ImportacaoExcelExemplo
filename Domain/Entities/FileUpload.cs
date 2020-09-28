using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FileUpload
    {
        public string Name { get; set; }

        public byte[] Data { get; set; }

        public DateTime DateRegister { get; set; }
    }
}
