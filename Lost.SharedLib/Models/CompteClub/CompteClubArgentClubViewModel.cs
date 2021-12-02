using System;

namespace Lost.SharedLib
{
    public class CompteClubArgentClubViewModel : BaseViewModel
    {
        public string Nom { get; set; }

        public double Total { get; set; }

        public string TotalString
        {
            get
            {
                return Total.ToString("#,0");
            }
        }

        public CompteClubArgentClubViewModel()
        {
            
        }
    }
}
