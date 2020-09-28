using System;
using System.ComponentModel;

namespace Domain.Entities
{
    public class File : IIdentityEntity
    {
        public int Id { get; set; }

        [DisplayName("Arquivo")]
        public string Name { get; set; }

        [DisplayName("Dados")]
        public byte[] Data { get; set; }

        [DisplayName("Data Cadastro")]
        public DateTime DateRegister { get; set; }
    }
}
