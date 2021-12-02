using System;
using System.ComponentModel.DataAnnotations;

namespace Lost.SharedLib
{
    public class BilletViewModel : BaseViewModel
    {
        [Required(ErrorMessage = Constants.ErrorRequiredDate)]
        public DateTime Date { get; set; }

        public int Nb { get; set; }

        [CustomValidation(typeof(BilletViewModel), nameof(ValidateGroupePersonne))]
        public bool IsPetiteMain { get; set; }

        [CustomValidation(typeof(BilletViewModel), nameof(ValidateGroupePersonne))]
        public GroupeViewModel GroupeViewModel { get; set; }

        [CustomValidation(typeof(BilletViewModel), nameof(ValidateGroupePersonne))]
        public PersonneViewModel PersonneViewModel { get; set; }

        public SemaineViewModel SemaineViewModel { get; set; }

        public double ArgentSale
        {
            get
            {
                double billet = Nb;
                return billet;
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

        public double GroupePayerBillet
        {
            get
            {
                return (100 - GroupeViewModel.TauxBlanchimentViewModel.ValeurBillet) / 100 * Nb;
            }
            set { }
        }

        public double GroupeTotal
        {
            get
            {
                return GroupePayerBillet;
            }
            set { }
        }

        public double PersonnePayerBillet
        {
            get
            {
                return (100 - PersonneViewModel.TauxBlanchimentViewModel.ValeurBillet) / 100 * Nb;
            }
            set { }
        }
        public double PersonneTotal
        {
            get
            {
                return PersonnePayerBillet;
            }
            set { }
        }

        public BilletViewModel()
        {
            Date = DateTime.Now;
        }

        public static ValidationResult ValidateGroupePersonne(object article, ValidationContext vc)
        {
            if (vc.ObjectInstance is BilletViewModel billetViewModel)
            {
                return billetViewModel.GroupeViewModel == null && billetViewModel.PersonneViewModel == null
                        ? new ValidationResult(Constants.ErrorRequiredGroupePersonne, new[] { vc.MemberName })
                        : ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}
