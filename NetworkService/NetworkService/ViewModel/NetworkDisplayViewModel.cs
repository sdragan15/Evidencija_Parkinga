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
        public static ObservableCollection<Entity> DisplayEntities { get; private set; } = new ObservableCollection<Entity>();

        private bool dragging { get; set; } = false;
        


        public NetworkDisplayViewModel()
        {
            NetworkDisplayViewModel.DisplayEntities.Clear();
            foreach (Entity entity in MainWindowViewModel.Entities)
            {
                NetworkDisplayViewModel.DisplayEntities.Add(entity);
            }
        }
    }
}
