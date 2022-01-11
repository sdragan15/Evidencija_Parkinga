using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class MainWindowViewModel:BindableBase
    {
        private int count = 15; // Inicijalna vrednost broja objekata u sistemu
                                // ######### ZAMENITI stvarnim brojem elemenata
                                //           zavisno od broja entiteta u listi

        public static ObservableCollection<Entity> Entities { get; set; } = new ObservableCollection<Entity>();
        private Entity entity;
        public MyICommand AddEntity { get; set; }

        private NetworkEntitiesViewModel networkEntitiesViewModel = new NetworkEntitiesViewModel();
        private BindableBase currentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public MainWindowViewModel()
        {
            CurrentViewModel = networkEntitiesViewModel;
            LoadEntities();

            AddEntity = new MyICommand(OnAddEntity);


            createListener(); //Povezivanje sa serverskom aplikacijom
        }

        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25565);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(count.ToString());
                            stream.Write(data, 0, data.Length);
                        }
                        else
                        {
                            //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                            Console.WriteLine(incomming); //Na primer: "Entitet_1:272"

                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji


                            string[] str = incomming.Split('_', ':');
                            entity = new Entity() { Id = int.Parse(str[1]), EntityValue = int.Parse(str[2]) };
                            AddNewEntity(entity);

                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }


        public void LoadEntities()
        {
            ObservableCollection<Entity> entities = new ObservableCollection<Entity>();
            entities.Add(new Entity { Id = 1, EntityValue = 15 });
            entities.Add(new Entity { Id = 5, EntityValue = 33 });
            Entities = entities;
            NetworkEntitiesViewModel.NetworkEntities.Add(new Entity { Id = 1, EntityValue = 15 });
        }

        private void OnAddEntity()
        {
            if (entity != null)
            {
                Entities.Add(entity);
                Console.WriteLine(entity.EntityValue);
            }
            
        }

        private void AddNewEntity(Entity entity)
        {
            foreach(Entity ent in Entities)
            {
                if(ent.Id == entity.Id)
                {
                    ent.EntityValue = entity.EntityValue;
                    return;
                }
            }
            Entities.Add(entity);
            Console.WriteLine("New entity added " + entity.Id);
        }

    }
}
