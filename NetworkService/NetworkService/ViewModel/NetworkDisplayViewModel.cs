using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class NetworkDisplayViewModel : BindableBase
    {
        public static ObservableCollection<Entity> DisplayEntities { get; set; } = new ObservableCollection<Entity>();

        public NetworkDisplayViewModel()
        {
            
        }
    }
}
