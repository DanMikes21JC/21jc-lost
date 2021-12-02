using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lost.Model
{
    public class TauxBlanchiment : IdEntity
    {
        [Column("date_debut")]
        public DateTime DateDebut { get; set; }

        [Column("date_fin")]
        public DateTime DateFin { get; set; }

        [ForeignKey(nameof(GroupeId))]
        public Groupe Groupe { get; set; }

        [ForeignKey(nameof(Groupe))]
        [Column("id_groupe")]
        public long? GroupeId { get; set; }

        [ForeignKey(nameof(PersonneId))]
        public Personne Personne { get; set; }

        [ForeignKey(nameof(Personne))]
        [Column("id_personne")]
        public long? PersonneId { get; set; }

        [Column("valeur_sac")]
        public int ValeurSac { get; set; }

        [Column("valeur_voiture")]
        public int ValeurVoiture { get; set; }

        [Column("valeur_billet")]
        public double ValeurBillet { get; set; }
    }
}
