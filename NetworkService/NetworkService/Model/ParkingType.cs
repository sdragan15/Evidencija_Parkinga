using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{

    public class ParkingType : BindableBase
    {
        private string parking;
        private string imageSource;

        public string Parking
        {
            get
            {
                return parking;
            }
            set
            {
                if(parking != value)
                {
                    if(value == "Otvoren" || value == "Zatvoren")
                    {
                        parking = value;
                        OnPropertyChanged("Parking");
                    }
                }
            }
        }


        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                if(imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged("ImageSource");
                }
            }
        }

        public override string ToString()
        {
            return parking;
        }
    }
}
