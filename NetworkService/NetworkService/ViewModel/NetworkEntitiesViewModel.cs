using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class NetworkEntitiesViewModel:BindableBase
    {
        public static ObservableCollection<Entity> NetworkEntities { get; private set; } = new ObservableCollection<Entity>();

        public MyICommand Refresh { get; set; }

        public NetworkEntitiesViewModel()
        {
            Refresh = new MyICommand(OnRefresh);
        }

        private void OnRefresh()
        {
            NetworkEntities.Clear();
            foreach(Entity entity in MainWindowViewModel.Entities)
            {
                NetworkEntities.Add(entity);
            }
            OnPropertyChanged("NetworkEntities");
        }
    }
}
