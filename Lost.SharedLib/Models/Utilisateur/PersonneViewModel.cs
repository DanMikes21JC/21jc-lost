using Lost.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Lost.SharedLib
{
    public class PersonneViewModel : BaseViewModel
    {
        [Required(ErrorMessage = Constants.ErrorRequiredNom)]
        public string Nom { get; set; }

        public string Commentaire { get; set; }

        public string Adresse { get; set; }
        
        [CustomValidation(typeof(PersonneViewModel), nameof(ValidateTelPersonne))]
        public string Tel { get; set; }

        public bool IsPetiteMain { get; set; }

        public GroupeViewModel GroupeViewModel { get; set; }

        public TauxBlanchimentViewModel TauxBlanchimentViewModel { get; set; }

        public PersonneViewModel()
        {
            GroupeViewModel = new GroupeViewModel();
            TauxBlanchimentViewModel = new TauxBlanchimentViewModel();
        }

        public override string ToString()
        {
            return Nom;
        }

        public static ValidationResult ValidateTelPersonne(object article, ValidationContext vc)
        {
            if (vc.ObjectInstance is PersonneViewModel personneViewModel)
            {
                if(personneViewModel.Tel != "")
                {
                    if(PersonneDal.IsTelUsed(personneViewModel.Id, personneViewModel.Tel))
                    {
                        return new ValidationResult(Constants.ErrorTelPersonneAlreadyExist, new[] { vc.MemberName });
                    }
                }

                return ValidationResult.Success;
                
            }
            return ValidationResult.Success;
        }
    }
}
