using System;
using System.ComponentModel.DataAnnotations;

namespace Lost.SharedLib
{
    public class SemaineViewModel : BaseViewModel
    {
        public string Libelle { get; set; }

        public int Num { get; set; }

        public DateTime DateDebut { get; set; }

        public SemaineViewModel()
        {

        }

        public override string ToString()
        {
            return Libelle;
        }
    }
}
