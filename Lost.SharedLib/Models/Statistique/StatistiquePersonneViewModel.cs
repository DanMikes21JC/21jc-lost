using System.ComponentModel.DataAnnotations;

namespace Lost.SharedLib
{
    public class StatistiquePersonneViewModel : BaseViewModel
    {
        public string Nom { get; set; }
        public int Numero { get; set; }
        public double Billet { get; set; }
        public string BilletString
        {
            get
            {
                return Billet.ToString("#,0");
            }
        }
        public double Sac { get; set; }
        public string SacString
        {
            get
            {
                return Sac.ToString("#,0");
            }
        }
        public double Voiture { get; set; }
        public string VoitureString
        {
            get
            {
                return Voiture.ToString("#,0");
            }
        }
        public double Benefice { get; set; }
        public string BeneficeString
        {
            get
            {
                return Benefice.ToString("#,0");
            }
        }

        public StatistiquePersonneViewModel()
        {
            
        }
    }
}
