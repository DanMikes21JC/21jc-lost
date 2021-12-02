using System;
using System.ComponentModel.DataAnnotations;

namespace Lost.SharedLib
{
    public class TauxBlanchimentViewModel : BaseViewModel
    {
        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }

        public long? GroupeId { get; set; }

        public long? PersonneId { get; set; }


        [Required(ErrorMessage = Constants.ErrorRequiredValeurSac)]
        public int ValeurSac { get; set; }

        [Required(ErrorMessage = Constants.ErrorRequiredValeurVoiture)]
        public int ValeurVoiture { get; set; }

        [Required(ErrorMessage = Constants.ErrorRequiredValeurBillet)]
        public double ValeurBillet { get; set; }
    }
}
