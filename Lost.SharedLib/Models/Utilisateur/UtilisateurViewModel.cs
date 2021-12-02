using System.ComponentModel.DataAnnotations;

namespace Lost.SharedLib
{
    public class UtilisateurViewModel : BaseViewModel
    {
        [Required(ErrorMessage = Constants.ErrorRequiredDiscordAuth)]
        public string DiscordAuth { get; set; }

        public PersonneViewModel PersonneViewModel { get; set; }

        [Required(ErrorMessage = Constants.ErrorRequiredRole)]
        public string Role { get; set; }

        public string NomPrenom
        {
            get
            {
                return PersonneViewModel.Nom;
            }
            set
            {
            }
        }

        public UtilisateurViewModel()
        {
            PersonneViewModel = new PersonneViewModel();
        }
    }
}
