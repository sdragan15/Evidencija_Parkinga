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
        bool ascending;
        bool descending;
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

        public bool Ascending
        {
            get { return ascending; }
            set
            {
                if(ascending != value)
                {
                    ascending = value;
                    OnPropertyChanged("Ascending");
                }
            }
        }

        public bool Descending
        {
            get { return descending; }
            set
            {
                if (descending != value)
                {
                    descending = value;
                    OnPropertyChanged("Descending");
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
