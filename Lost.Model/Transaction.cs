using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Lost.Model
{
    public abstract class Transaction : IdEntity
    {
        [Column("date")]
        public DateTime Date { get; set; }

        [Column("is_petite_main")]
        public bool IsPetiteMain { get; set; }

        [Column("qty")]
        public int Nb { get; set; }

        [ForeignKey(nameof(SemaineId))]
        public Semaine Semaine { get; set; }

        [ForeignKey(nameof(Semaine))]
        [Column("id_semaine")]
        public long? SemaineId { get; set; }

        [ForeignKey(nameof(TauxBlanchimentId))]
        public TauxBlanchiment TauxBlanchiment { get; set; }

        [ForeignKey(nameof(TauxBlanchiment))]
        [Column("id_taux_blanchiment")]
        public long? TauxBlanchimentId { get; set; }
    }
}
