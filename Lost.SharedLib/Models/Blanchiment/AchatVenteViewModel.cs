using System;
using System.ComponentModel.DataAnnotations;

namespace Lost.SharedLib
{
    public class AchatVenteViewModel : BaseViewModel
    {
        [Required(ErrorMessage = Constants.ErrorRequiredDate)]
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int Nb { get; set; }

        [CustomValidation(typeof(AchatVenteViewModel), nameof(ValidateGroupePersonne))]
        public bool IsPetiteMain { get; set; }

        [CustomValidation(typeof(AchatVenteViewModel), nameof(ValidateGroupePersonne))]
        public GroupeViewModel GroupeViewModel { get; set; }

        [CustomValidation(typeof(AchatVenteViewModel), nameof(ValidateGroupePersonne))]
        public PersonneViewModel PersonneViewModel { get; set; }

        public SemaineViewModel SemaineViewModel { get; set; }

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
                return Paye;
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
                return Nb;
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

        public double PersonnePayerAchatVente
        {
            get
            {
                return Nb;
            }
            set { }
        }
        public double PersonneTotal
        {
            get
            {
                return PersonnePayerAchatVente;
            }
            set { }
        }

        public AchatVenteViewModel()
        {
            Date = DateTime.Now;
        }

        public static ValidationResult ValidateGroupePersonne(object article, ValidationContext vc)
        {
            if (vc.ObjectInstance is AchatVenteViewModel achatVenteViewModel)
            {
                return achatVenteViewModel.GroupeViewModel == null && achatVenteViewModel.PersonneViewModel == null
                        ? new ValidationResult(Constants.ErrorRequiredGroupePersonne, new[] { vc.MemberName })
                        : ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}
