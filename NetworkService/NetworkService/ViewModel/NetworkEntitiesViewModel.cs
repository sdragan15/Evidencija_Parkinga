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
        public MyICommand Add { get; set; }
        public MyICommand Delete { get; set; }

        public ObservableCollection<ParkingType> parkingTypes { get; private set; } = new ObservableCollection<ParkingType>();

        public Entity newEntity { get; set; } = new Entity();
        public Entity selectedEntity { get; set; } = new Entity();


        public NetworkEntitiesViewModel()
        {
            newEntity.Type = new ParkingType();
            parkingTypes.Add(new ParkingType() { ImageSource = "", Parking = "Otvoren" });
            parkingTypes.Add(new ParkingType() { ImageSource = "", Parking = "Zatvoren" });
            Refresh = new MyICommand(OnRefresh);
            Add = new MyICommand(OnAdd);
            Delete = new MyICommand(OnDelete);
           // newEntity.Type.ImageSource = "hello";
        }

        private void OnRefresh()
        {
            foreach(Entity entity in MainWindowViewModel.Entities)
            {
                foreach(Entity ent in NetworkEntities)
                {
                    if (entity.Id == ent.Id)
                    {
                        ent.EntityValue = entity.EntityValue;
                        OnPropertyChanged("NetworkEntities");
                    }
                        
                }
                
            }
            
        }

        private void OnAdd()
        {
            foreach(Entity entity in NetworkEntities)
            {
                if(entity.Id == newEntity.Id)
                {
                    entity.Name = newEntity.Name;
                    entity.Type = new ParkingType() { Parking = newEntity.Type.Parking, ImageSource = newEntity.Type.ImageSource };
                    return;
                }
            }
            Entity entity2 = new Entity();
            entity2.Id = newEntity.Id;
            entity2.Name = newEntity.Name;
            entity2.Type = new ParkingType();
            entity2.Type.Parking = newEntity.Type.Parking;
            entity2.Type.ImageSource = newEntity.Type.ImageSource;

            NetworkEntities.Add(entity2);
        }

        private void OnDelete()
        {
            NetworkEntities.Remove(selectedEntity);
        }
    }
}
