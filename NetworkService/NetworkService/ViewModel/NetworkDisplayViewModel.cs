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
    public class NetworkDisplayViewModel : BindableBase
    {
        private string Slika = "";
        private Entity SelectedEntity { get; set; } = new Entity();
        private DisplayEntity displayEntity = new DisplayEntity();
        private bool dragging = false;
        private List<DisplayEntity> entities = new List<DisplayEntity>();
        public Entity SelectedItem { get; set; }
        //private Canvas canvas;

        public static ObservableCollection<Entity> DisplayEntities { get; private set; } = new ObservableCollection<Entity>();

        public MyICommand<ListView> ListViewSelectionChanged { get; set; }
        public MyICommand ListViewMouseLeftButtonUp { get; set; }
 



        public NetworkDisplayViewModel()
        {
            NetworkDisplayViewModel.DisplayEntities.Clear();
            foreach (Entity entity in MainWindowViewModel.Entities)
            {
                NetworkDisplayViewModel.DisplayEntities.Add(entity);
            }
            ListViewSelectionChanged = new MyICommand<ListView>(OnListViewSelectionChanged);
            ListViewMouseLeftButtonUp = new MyICommand(OnListViewMouseLeftButtonUp);
        }

        private void OnListViewSelectionChanged(ListView listView)
        {
            if (!dragging && SelectedItem != null)
            {
                dragging = true;
                SelectedEntity = SelectedItem;
                Slika = SelectedEntity.Type.ImageSource;
                Console.WriteLine(Slika);
                Console.WriteLine("Pre ovoga");
                DragDrop.DoDragDrop(listView, Slika, DragDropEffects.Copy);
            }
        }

        private void OnListViewMouseLeftButtonUp()
        {
            dragging = false;
            Slika = "";
        }

    }
}
