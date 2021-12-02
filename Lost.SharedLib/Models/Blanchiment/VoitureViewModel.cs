using System;
using System.ComponentModel.DataAnnotations;

namespace Lost.SharedLib
{
    public class VoitureViewModel : BaseViewModel
    {
        [Required(ErrorMessage = Constants.ErrorRequiredDate)]
        public DateTime Date { get; set; }

        public int Nb { get; set; }

        [CustomValidation(typeof(VoitureViewModel), nameof(ValidateGroupePersonne))]
        public bool IsPetiteMain { get; set; }

        [CustomValidation(typeof(VoitureViewModel), nameof(ValidateGroupePersonne))]
        public GroupeViewModel GroupeViewModel { get; set; }

        [CustomValidation(typeof(VoitureViewModel), nameof(ValidateGroupePersonne))]
        public PersonneViewModel PersonneViewModel { get; set; }

        public SemaineViewModel SemaineViewModel { get; set; }

        public double ArgentSale
        {
            get
            {
                double voiture = Nb * 2000;
                return voiture;
            }
            set
            {

            }
        }
        public string ArgentSaleString
        {
            get
            {
                return ArgentSale.ToString("#,0");
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
        public string PayeString
        {
            get
            {
                return Paye.ToString("#,0");
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
        public string BeneficeString
        {
            get
            {
                return Benefice.ToString("#,0");
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

        public double GroupePayerVoiture
        {
            get
            {
                return GroupeViewModel.TauxBlanchimentViewModel.ValeurVoiture * Nb;
            }
            set { }
        }

        public double GroupeTotal
        {
            get
            {
                return GroupePayerVoiture;
            }
            set { }
        }

        public double PersonnePayerVoiture
        {
            get
            {
                return PersonneViewModel.TauxBlanchimentViewModel.ValeurVoiture * Nb;
            }
            set { }
        }
        public double PersonneTotal
        {
            get
            {
                return PersonnePayerVoiture;
            }
            set { }
        }

        public VoitureViewModel()
        {
            Date = DateTime.Now;
        }

        public static ValidationResult ValidateGroupePersonne(object article, ValidationContext vc)
        {
            if (vc.ObjectInstance is VoitureViewModel voitureViewModel)
            {
                return voitureViewModel.GroupeViewModel == null && voitureViewModel.PersonneViewModel == null
                        ? new ValidationResult(Constants.ErrorRequiredGroupePersonne, new[] { vc.MemberName })
                        : ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}
