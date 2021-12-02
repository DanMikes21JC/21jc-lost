using System.ComponentModel.DataAnnotations;

namespace Lost.SharedLib
{
    public class GroupeViewModel : BaseViewModel
    {
        [Required(ErrorMessage = Constants.ErrorRequiredNom)]
        public string Nom { get; set; }

        public bool IsGroupeCartel { get; set; }

        public TauxBlanchimentViewModel TauxBlanchimentViewModel { get; set; }

        public GroupeViewModel()
        {
            TauxBlanchimentViewModel = new TauxBlanchimentViewModel();
        }

        public override string ToString()
        {
            return Nom;
        }
    }
}
