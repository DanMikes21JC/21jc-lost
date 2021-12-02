using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lost.Model
{
    public abstract class IdEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id"), Key]
        public long Id { get; set; }

        protected const int enumLength = 50;

    }
}
