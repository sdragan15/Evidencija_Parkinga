using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class NetworkEntitiesViewModel : BindableBase
    {
        public ObservableCollection<Entity> NetworkEntities { get; private set; } = new ObservableCollection<Entity>();
        public static ObservableCollection<Entity> BackUpEntities { get; private set; } = new ObservableCollection<Entity>();

        public MyICommand Refresh { get; set; }
        public MyICommand Add { get; set; }
        public MyICommand Delete { get; set; }
        public MyICommand Filter { get; set; }

        public ObservableCollection<ParkingType> parkingTypes { get; private set; } = new ObservableCollection<ParkingType>();

        public Entity newEntity { get; set; } = new Entity();
        public Entity selectedEntity { get; set; } = new Entity();
        public Filter FilterEntities { get; set; } = new Filter();


        public NetworkEntitiesViewModel()
        {
            newEntity.Type = new ParkingType();
            parkingTypes.Add(new ParkingType() { ImageSource = "", Parking = "Otvoren" });
            parkingTypes.Add(new ParkingType() { ImageSource = "", Parking = "Zatvoren" });
            Refresh = new MyICommand(OnRefresh);
            Add = new MyICommand(OnAdd);
            Delete = new MyICommand(OnDelete);
            Filter = new MyICommand(OnFilter);
            // newEntity.Type.ImageSource = "hello";
        }

        private void OnRefresh()
        {
            NetworkEntities.Clear();
            foreach (Entity entity1 in BackUpEntities)
            {
                NetworkEntities.Add(entity1);
            }

        }

        private void OnAdd()
        {
            foreach(Entity entity in BackUpEntities)
            {
                if (entity.Id.Equals(newEntity.Id))
                {
                    entity.Name = newEntity.Name;
                    entity.Type.Parking = newEntity.Type.Parking;
                    entity.Type.ImageSource = newEntity.Type.ImageSource;
                    entity.EntityValue = newEntity.EntityValue;
                    return;
                }
            }
            Entity entity2 = CreateNewEntity(newEntity);

            NetworkEntities.Add(entity2);
            BackUpEntities.Add(entity2);
            MainWindowViewModel.Entities.Add(entity2);
        }

        private void OnDelete()
        {
            BackUpEntities.Remove(selectedEntity);
            OnRefresh();
        }

        private void OnFilter()
        {
            NetworkEntities.Clear();

            try
            {
                if(int.Parse(FilterEntities.FilterValue) != 0 && (FilterEntities.LessThan || FilterEntities.GreaterThan))
                {
                    LessOrGreaterFilter();
                }
                else
                {
                    ParkingFilter();
                }
            }
            catch (Exception)
            {
                ParkingFilter();
            }
        }


        private void ParkingFilter()
        {
            if (FilterEntities.Parking.Equals("Otvoren"))
            {
                foreach (Entity entity in BackUpEntities)
                {
                    if (entity.Type.Parking.Equals("Otvoren"))
                    {
                        NetworkEntities.Add(entity);
                    }
                }
            }
            else if (FilterEntities.Parking.Equals("Zatvoren"))
            {
                foreach (Entity entity in BackUpEntities)
                {
                    if (entity.Type.Parking.Equals("Zatvoren"))
                    {
                        NetworkEntities.Add(entity);
                    }
                }
            }
        }

        public void LessOrGreaterFilter()
        {
            int fvalue;
            try
            {
                fvalue = int.Parse(FilterEntities.FilterValue);
            }
            catch (Exception)
            {
                Console.WriteLine("Sta se desava");
                fvalue = -1;
            }

            if (FilterEntities.Parking.Equals("Otvoren"))
            {
                foreach (Entity entity in BackUpEntities)
                {
                    if (entity.Type.Parking.Equals("Otvoren"))
                    {
                        if (FilterEntities.GreaterThan == true)
                        {
                            if (int.Parse(entity.Id) >= fvalue)
                            {
                                NetworkEntities.Add(entity);
                            }
                        }

                        else if (FilterEntities.LessThan == true)
                        {
                            if (int.Parse(entity.Id) < fvalue)
                            {
                                NetworkEntities.Add(entity);
                            }
                        }

                    }
                }
            }
            else if (FilterEntities.Parking.Equals("Zatvoren"))
            {
                foreach (Entity entity in BackUpEntities)
                {
                    if (entity.Type.Parking.Equals("Zatvoren"))
                    {
                        if (entity.Type.Parking.Equals("Otvoren"))
                        {
                            if (FilterEntities.GreaterThan == true)
                            {
                                if (int.Parse(entity.Id) >= fvalue)
                                {
                                    NetworkEntities.Add(entity);
                                }
                            }

                            else if (FilterEntities.LessThan == true)
                            {
                                if (int.Parse(entity.Id) < fvalue)
                                {
                                    NetworkEntities.Add(entity);
                                }
                            }

                        }
                    }
                }
            } 
        }

        public Entity CreateNewEntity(Entity entity)
        {
            try
            {
                Entity entity2 = new Entity();
                entity2.Id = entity.Id;
                entity2.Name = entity.Name;
                entity2.Type = new ParkingType();
                entity2.Type.Parking = entity.Type.Parking;
                entity2.Type.ImageSource = entity.Type.ImageSource;
                entity2.EntityValue = entity.EntityValue;
                return entity2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }
    }
}
