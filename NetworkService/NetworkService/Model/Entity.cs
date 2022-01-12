using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Entity : BindableBase
    {
        private int id;
        private double entityValue;
        private string name;
        private ParkingType type;

        public ParkingType Type
        {
            get { return type; }
            set
            {
                if(value != null)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if(name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public int Id
        {
            get { return id; }
            set
            {
                if(id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public double EntityValue
        {
            get { return entityValue; }
            set
            {
                if(entityValue != value)
                {
                    entityValue = value;
                    OnPropertyChanged("EntityValue");
                }
            }
        }
    }
}
