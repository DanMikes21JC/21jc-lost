using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lost.Model
{
    public class Semaine : IdEntity
    {
        [Column("libelle")]
        public string Libelle { get; set; }

        [Column("numero")]
        public int Num { get; set; }

        [Column("date_debut")]
        public DateTime DateDebut { get; set; }
    }
}
