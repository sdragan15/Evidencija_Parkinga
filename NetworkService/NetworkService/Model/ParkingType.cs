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
                    if(value.Equals("Otvoren")  || value.Equals("Zatvoren"))
                    {
                        parking = value;
                        OnPropertyChanged("Parking");

                        if (value.Equals("Otvoren"))
                        {
                            ImageSource = "Images/parkingEmpty.jpg";
                            OnPropertyChanged("ImageSource");
                        }
                        else
                        {
                            ImageSource = "Images/parkingFull.jpg";
                            OnPropertyChanged("ImageSource");
                        }
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
            return Parking;
        }
    }
}
