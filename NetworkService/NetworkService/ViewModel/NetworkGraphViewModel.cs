using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class NetworkGraphViewModel :BindableBase
    {

        public static ObservableCollection<Entity> Entities { get; set; } = new ObservableCollection<Entity>();
        public ObservableCollection<string> Ids { get; private set; } = new ObservableCollection<string>();
        private int value0;
        private int value1;
        private int value2;
        private int value3;

        public int Value0
        {
            get { return value0; }
            set
            {
                if(value != value0)
                {
                    value0 = value;
                    OnPropertyChanged("Value0");
                }
            }
        }

        public int Value1
        {
            get { return value1; }
            set
            {
                if (value != value1)
                {
                    value1 = value;
                    OnPropertyChanged("Value1");
                }
            }
        }

        public int Value2
        {
            get { return value2; }
            set
            {
                if (value != value2)
                {
                    value2 = value;
                    OnPropertyChanged("Value2");
                }
            }
        }

        public int Value3
        {
            get { return value3; }
            set
            {
                if (value != value3)
                {
                    value3 = value;
                    OnPropertyChanged("Value3");
                }
            }
        }

        private string selectedValue;
        public string SelectedValue
        {
            get { return selectedValue; }
            set
            {
                if(value != selectedValue)
                {
                    selectedValue = value;
                    OnPropertyChanged("SelectedValue");
                }
            }
        }

        private string FilterValue = null;

        public MyICommand ShowFilter { get; set; }
        

        public NetworkGraphViewModel()
        {
            for(int i=0; i<MainWindowViewModel.Entities.Count; i++)
            {
                Ids.Add(i.ToString());
            }
            value0 = 550;
            value1 = 550;
            value2 = 550;
            value3 = 550;
            ShowFilter = new MyICommand(OnShowFilter);

            ReadNewEntities(MainWindowViewModel.Path);
        }

        public void ReadNewEntities(string s)
        {
            var readThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        if (File.Exists(s))
                        {
                            string str;
                            string[] value;
                            double num;
                            string index;
                            using (StreamReader reader = new StreamReader(s))
                            {
                                Entities.Clear();
                                while (reader.Peek() >= 0)
                                {
                                    str = reader.ReadLine();
                                    value = str.Split(':', '_');
                                    num = int.Parse(value[5]);
                                    num = 550 - num * 4;
                                    index = value[4];

                                    Entity entity = new Entity();
                                    entity.EntityValue = num;
                                    entity.Id = index;

                                    if (FilterValue == null)
                                    {
                                        Entities.Insert(0, entity);
                                    }
                                    else
                                    {
                                        if (FilterValue.Equals(entity.Id))
                                            Entities.Insert(0, entity);
                                    }

                                }

                            }

                            Value0 = (int)Entities[0].EntityValue;
                            Value1 = (int)Entities[1].EntityValue;
                            Value2 = (int)Entities[2].EntityValue;
                            Value3 = (int)Entities[3].EntityValue;
                        }
                        
                    }catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    
                    Thread.Sleep(1000);
                }
            });
            readThread.IsBackground = true;
            readThread.Start();
            
        }

       private void OnShowFilter()
        {
            FilterValue = SelectedValue;
            Console.WriteLine("FilterValue = ");
            Console.WriteLine(FilterValue);
        }
      

    }
}
