using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Lost.Model
{
    public class CompteClubTransaction : IdEntity
    {
        [Column("date")]
        public DateTime Date { get; set; }

        [Column("somme")]
        public double Somme { get; set; }

        [Column("raison")]
        public string Raison { get; set; }

        [ForeignKey(nameof(PersonneId))]
        public Personne Personne { get; set; }

        [ForeignKey(nameof(TauxBlanchiment))]
        [Column("id_personne")]
        public long? PersonneId { get; set; }

        public CompteClubTransaction()
        {

        }
    }
}
