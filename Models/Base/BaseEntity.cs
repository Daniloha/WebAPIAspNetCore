using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiCadastro.Models.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long ID { get; set; }
    }
}
