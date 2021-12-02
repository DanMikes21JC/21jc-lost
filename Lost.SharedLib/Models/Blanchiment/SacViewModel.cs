using System;
using System.ComponentModel.DataAnnotations;

namespace Lost.SharedLib
{
    public class SacViewModel : BaseViewModel
    {
        [Required(ErrorMessage = Constants.ErrorRequiredDate)]
        public DateTime Date { get; set; }

        public int Nb { get; set; }

        [CustomValidation(typeof(SacViewModel), nameof(ValidateGroupePersonne))]
        public bool IsPetiteMain { get; set; }

        [CustomValidation(typeof(SacViewModel), nameof(ValidateGroupePersonne))]
        public GroupeViewModel GroupeViewModel { get; set; }

        [CustomValidation(typeof(SacViewModel), nameof(ValidateGroupePersonne))]
        public PersonneViewModel PersonneViewModel { get; set; }

        public SemaineViewModel SemaineViewModel { get; set; }

        public double ArgentSale
        {
            get
            {
                double sac = Nb * 150;
                return sac;
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

        public double GroupePayerSac
        {
            get
            {
                return GroupeViewModel.TauxBlanchimentViewModel.ValeurSac * Nb;
            }
            set { }
        }

        public double GroupeTotal
        {
            get
            {
                return GroupePayerSac;
            }
            set { }
        }

        public double PersonnePayerSac
        {
            get
            {
                return PersonneViewModel.TauxBlanchimentViewModel.ValeurSac * Nb;
            }
            set { }
        }
        public double PersonneTotal
        {
            get
            {
                return PersonnePayerSac;
            }
            set { }
        }

        public SacViewModel()
        {
            Date = DateTime.Now;
        }

        public static ValidationResult ValidateGroupePersonne(object article, ValidationContext vc)
        {
            if (vc.ObjectInstance is SacViewModel sacViewModel)
            {
                return sacViewModel.GroupeViewModel == null && sacViewModel.PersonneViewModel == null
                        ? new ValidationResult(Constants.ErrorRequiredGroupePersonne, new[] { vc.MemberName })
                        : ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}
