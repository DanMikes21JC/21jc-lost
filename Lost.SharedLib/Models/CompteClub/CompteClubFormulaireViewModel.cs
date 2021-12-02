using System;

namespace Lost.SharedLib
{
    public class CompteClubFormulaireViewModel : BaseViewModel
    {
        public PersonneViewModel PersonneViewModel { get; set; }

        public DateTime Date { get; set; }

        public double Somme { get; set; }

        public string SommeString
        {
            get
            {
                return Somme.ToString("#,0");
            }
        }

        public string Raison { get; set; }

        public CompteClubFormulaireViewModel()
        {
            
        }
    }
}
