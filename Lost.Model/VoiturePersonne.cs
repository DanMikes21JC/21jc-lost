
using System.ComponentModel.DataAnnotations.Schema;

namespace Lost.Model
{
    public class VoiturePersonne : IdEntity
    {
        [Column("type_voiture")]
        public string TypeVoiture { get; set; }

        [ForeignKey(nameof(DemandeurId))]
        public Personne Demandeur { get; set; }

        [ForeignKey(nameof(Personne))]
        [Column("id_personne")]
        public long DemandeurId { get; set; }
    }
}
