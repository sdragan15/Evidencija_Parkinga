using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        public MyICommand<TextBox> gotFocusId { get; set; }
        public MyICommand<TextBox> gotFocusFilter { get; set; }
        public MyICommand<TextBox> gotFocusName { get; set; }

        public ObservableCollection<ParkingType> parkingTypes { get; private set; } = new ObservableCollection<ParkingType>();

        public Entity newEntity { get; set; } = new Entity();
        public Entity selectedEntity { get; set; } = new Entity();
        public Filter FilterEntities { get; set; } = new Filter();
        public static TextBox WriteTextBox { get; set; } = new TextBox();

        public static string ImageEmtpy = AppDomain.CurrentDomain.BaseDirectory + "Images/parkingEmpty.jpg";
        public static string ImageFull = AppDomain.CurrentDomain.BaseDirectory + "Images/parkingFull.jpg";
        private Visibility keyboardVisible;
        private string valueChanged;

        public Visibility KeyboardVisible
        {
            get { return keyboardVisible; }
            set
            {
                if(keyboardVisible != value)
                {
                    keyboardVisible = value;
                    OnPropertyChanged("KeyboardVisible");
                }
            }
        }



        public NetworkEntitiesViewModel()
        {
            newEntity.Type = new ParkingType();
            parkingTypes.Add(new ParkingType() { ImageSource = "Images/parkingEmpty.jpg", Parking = "Otvoren" });
            parkingTypes.Add(new ParkingType() { ImageSource = "Images/parkingFull.jpg", Parking = "Zatvoren" });
            Refresh = new MyICommand(OnRefresh);
            Add = new MyICommand(OnAdd);
            Delete = new MyICommand(OnDelete);
            Filter = new MyICommand(OnFilter);
            gotFocusId = new MyICommand<TextBox>(OngotFocusId);
            gotFocusName = new MyICommand<TextBox>(OngotFocusName);
            gotFocusFilter = new MyICommand<TextBox>(OngotFocusFilter);
            KeyboardVisible = Visibility.Collapsed;
            OnRefresh();

            buttonPress = new MyICommand<string>(OnbuttonPress);
            buttonDelete = new MyICommand(OnbuttonDelete);
            buttonDeleteAll = new MyICommand(OnbuttonDeleteAll);
            buttonEnter = new MyICommand(OnbuttonEnter);
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

        private void OngotFocusId(TextBox textBox)
        {
            KeyboardVisible = Visibility.Visible;
            WriteTextBox = textBox;
            Input = textBox.Text;
            valueChanged = "Id";
            
        }

        private void OngotFocusName(TextBox textBox)
        {
            KeyboardVisible = Visibility.Visible;
            WriteTextBox = textBox;
            Input = textBox.Text;
            valueChanged = "Name";
        }
        private void OngotFocusFilter(TextBox textBox)
        {
            KeyboardVisible = Visibility.Visible;
            WriteTextBox = textBox;
            Input = textBox.Text;
            valueChanged = "Filter";
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
            NetworkDisplayViewModel.DisplayEntities.Add(entity2);
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
            try
            {
                if(FilterEntities.Parking != null)
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
                
            }catch (Exception)
            {

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

        public static Entity CreateNewEntity(Entity entity)
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

        public static string InputText { get; set; }

        private string input;
        public string Input
        {
            get { return input; }
            set
            {
                if (input != value)
                {
                    input = value;
                    OnPropertyChanged("Input");
                }
            }
        }
        public MyICommand<string> buttonPress { get; set; }
        public MyICommand buttonDelete { get; set; }
        public MyICommand buttonDeleteAll { get; set; }
        public MyICommand buttonEnter { get; set; }

    

        private void OnbuttonPress(string button)
        {
            Input += button;
        }

        private void OnbuttonDelete()
        {
            if (Input.Count() > 0)
            {
                Input = Input.Remove(Input.Length - 1);
            }

        }

        private void OnbuttonDeleteAll()
        {
            Input = "";
        }

        private void OnbuttonEnter()
        {
            if(valueChanged != null)
            {
                if (valueChanged.Equals("Id"))
                {
                    newEntity.Id = Input;
                }
                else if(valueChanged.Equals("Name"))
                {
                    newEntity.Name = Input;
                }
                else if (valueChanged.Equals("Filter"))
                {
                    FilterEntities.FilterValue = Input;
                }
            }
            WriteTextBox.Text = Input;
            Input = "";
            KeyboardVisible = Visibility.Collapsed;
        }
    }
}
