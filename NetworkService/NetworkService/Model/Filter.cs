using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Filter : BindableBase
    {
        string parking;
        bool greaterThan;
        bool lessThan;
        string filterValue;

        public string Parking
        {
            get { return parking; }
            set
            {
                if(parking != value)
                {
                    parking = value;
                    OnPropertyChanged("Parking");
                }
            }
        }

        public bool GreaterThan
        {
            get { return greaterThan; }
            set
            {
                if(greaterThan != value)
                {
                    greaterThan = value;
                    OnPropertyChanged("GreaterThan");
                }
            }
        }

        public bool LessThan
        {
            get { return lessThan; }
            set
            {
                if (lessThan != value)
                {
                    lessThan = value;
                    OnPropertyChanged("LessThan");
                }
            }
        }

        public string FilterValue
        {
            get { return filterValue; }
            set
            {
                if (filterValue != value)
                {
                    filterValue = value;
                    OnPropertyChanged("FilterValue");
                }
            }
        }
    }
}
