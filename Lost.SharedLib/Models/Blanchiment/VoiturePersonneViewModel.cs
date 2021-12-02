using System;

namespace Lost.SharedLib
{
    public class VoiturePersonneViewModel : BaseViewModel
    {
        public delegate void TypeVoitureEventHandler(object sender, TypeVoitureEventArgs e);
        public event TypeVoitureEventHandler TypeVoitureEvent;

        public PersonneViewModel PersonneViewModel { get; set; }

        private string typeVoiture { get; set; }
        public string TypeVoiture {
            get
            {
                return typeVoiture;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    value = null;
                }
                if(typeVoiture != value)
                {
                    typeVoiture = value;
                    TypeVoitureEvent?.Invoke(this, new TypeVoitureEventArgs() { TypeVoiture = value });
                }
            }
        }

        public VoiturePersonneViewModel()
        {

        }
    }

    public class TypeVoitureEventArgs : EventArgs
    {
        public string TypeVoiture { get; set; }
    }
}
