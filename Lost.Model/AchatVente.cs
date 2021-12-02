
using System.ComponentModel.DataAnnotations.Schema;

namespace Lost.Model
{
    public class AchatVente : Transaction
    {
        [Column("description")]
        public string Description { get; set; }
    }
}
