using System;
using System.ComponentModel.DataAnnotations;

namespace Lost.SharedLib
{
    public class TransactionViewModel : BaseViewModel
    {
        [Required(ErrorMessage = Constants.ErrorRequiredDate)]
        public DateTime Date { get; set; }

        public int NbBillet { get; set; }

        public int NbSac { get; set; }

        public int NbVoiture { get; set; }

        public bool IsPetiteMain { get; set; }

        public GroupeViewModel GroupeViewModel { get; set; }

        public PersonneViewModel PersonneViewModel { get; set; }

        public SemaineViewModel SemaineViewModel { get; set; }

        public double ArgentSale
        {
            get
            {
                double billet = NbBillet;
                double sac = NbSac * 150;
                double voiture = NbSac * 1200;
                return billet + sac + voiture;
            }
            set
            {

            }
        }

        public double Paye
        {
            get
            {
                if (PersonneViewModel != null)
                {
                    return PersonneTotal;
                }
                else if (GroupeViewModel != null)
                {
                    return GroupeTotal;
                }
                else
                {
                    return 0;
                }
            }
            set
            {

            }
        }

        public double Benefice
        {
            get
            {
                return ArgentSale - Paye;
            }
            set
            {

            }
        }

        public string From
        {
            get
            {
                if(PersonneViewModel != null)
                {
                    return PersonneViewModel.Nom;
                }
                else if (GroupeViewModel != null)
                {
                    return GroupeViewModel.Nom;
                }
                else
                {
                    return string.Empty;
                }
            }
            set { }
        }

        public double GroupePayerBillet
        {
            get
            {
                return GroupeViewModel.TauxBlanchimentViewModel.ValeurBillet / 100 * NbBillet;
            }
            set { }
        }

        public double GroupePayerSac
        {
            get
            {
                return GroupeViewModel.TauxBlanchimentViewModel.ValeurSac * NbSac;
            }
            set { }
        }

        public double GroupePayerVoiture
        {
            get
            {
                return GroupeViewModel.TauxBlanchimentViewModel.ValeurVoiture * NbVoiture;
            }
            set { }
        }

        public double GroupeTotal
        {
            get
            {
                return GroupePayerBillet + GroupePayerSac + GroupePayerVoiture;
            }
            set { }
        }

        public double PersonnePayerBillet
        {
            get
            {
                return PersonneViewModel.TauxBlanchimentViewModel.ValeurBillet / 100 * NbBillet;
            }
            set { }
        }

        public double PersonnePayerSac
        {
            get
            {
                return PersonneViewModel.TauxBlanchimentViewModel.ValeurSac * NbSac;
            }
            set { }
        }

        public double PersonnePayerVoiture
        {
            get
            {
                return PersonneViewModel.TauxBlanchimentViewModel.ValeurVoiture * NbVoiture;
            }
            set { }
        }

        public double PersonneTotal
        {
            get
            {
                return PersonnePayerBillet + PersonnePayerSac + PersonnePayerVoiture;
            }
            set { }
        }

        public TransactionViewModel()
        {
            Date = DateTime.Now;
        }
    }
}
