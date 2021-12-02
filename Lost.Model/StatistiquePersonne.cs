using System.ComponentModel.DataAnnotations.Schema;

namespace Lost.Model
{
    public class StatistiquePersonne
    {
        public const string viewName = "personnebilletsacvoituresemaine";

        [Column("nom")]
        public string Nom { get; set; }

        [Column("numero")]
        public int Numero { get; set; }

        [Column("billet")]
        public double Billet { get; set; }

        [Column("sac")]
        public double Sac { get; set; }

        [Column("voiture")]
        public double Voiture { get; set; }

        [Column("benefice")]
        public double Benefice { get; set; }
    }
}
