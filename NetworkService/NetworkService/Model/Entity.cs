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
        private int entityValue;

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

        public int EntityValue
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
