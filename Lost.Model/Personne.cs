using System.ComponentModel.DataAnnotations.Schema;

namespace Lost.Model
{
    public class Personne : IdEntity
    {
        [Column("nom")]
        public string Nom { get; set; }

        [Column("commentaire")]
        public string Commentaire { get; set; }

        [Column("adresse")]
        public string Adresse { get; set; }

        [Column("tel")]
        public string Tel { get; set; }

        [Column("petite_main")]
        public bool IsPetiteMain { get; set; }

        [ForeignKey(nameof(GroupeId))]
        public Groupe Groupe { get; set; }

        [ForeignKey(nameof(Groupe))]
        [Column("id_groupe")]
        public long? GroupeId { get; set; }

        [NotMapped]
        public TauxBlanchiment TauxBlanchiment { get; set; }

        public override string ToString()
        {
            return Nom;
        }
    }
}
