using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private Canvas tempCanvas;

        public static ObservableCollection<Entity> DisplayEntities { get; private set; } = new ObservableCollection<Entity>();

        public MyICommand<ListView> ListViewSelectionChanged { get; set; }
        public MyICommand ListViewMouseLeftButtonUp { get; set; }
        public MyICommand<Canvas> drop { get; set; }
        public MyICommand<Canvas> mouseDown { get; set; }
        public MyICommand<Canvas> buttonClick { get; set; }
        public MyICommand<ListView> refreshClick { get; set; }



        public NetworkDisplayViewModel()
        {
            DisplayEntities.Clear();
            foreach (Entity entity in MainWindowViewModel.Entities)
            {
                NetworkDisplayViewModel.DisplayEntities.Add(entity);
            }
            ListViewSelectionChanged = new MyICommand<ListView>(OnListViewSelectionChanged);
            ListViewMouseLeftButtonUp = new MyICommand(OnListViewMouseLeftButtonUp);
            drop = new MyICommand<Canvas>(Ondrop);
            mouseDown = new MyICommand<Canvas>(OnmouseDown);
            buttonClick = new MyICommand<Canvas>(OnbuttonClick);
            refreshClick = new MyICommand<ListView>(OnrefreshClick);
        }

        private void OnListViewSelectionChanged(ListView listView)
        {
            if (!dragging && SelectedItem != null)
            {
                dragging = true;
                SelectedEntity = SelectedItem;
                Slika = SelectedEntity.Type.ImageSource;
                Console.WriteLine(Slika);
                DragDrop.DoDragDrop(listView, Slika, DragDropEffects.Copy);
            }
        }

        private void OnListViewMouseLeftButtonUp()
        {
            dragging = false;
            Slika = "";
        }

        private void Ondrop(Canvas canvas)
        {
            dragging = false;
            if (SelectedEntity != null)
            {
                if (canvas.Background == null || canvas.Background == Brushes.White)
                {
                    BitmapImage background = new BitmapImage();
                    background.BeginInit();
                    if (SelectedEntity.EntityValue > 80)
                    {
                        Slika = "pack://application:,,,/ViewModel/Images/error.png";
                    }
                    background.UriSource = new Uri(Slika);
                    background.EndInit();
                    canvas.Background = new ImageBrush(background);
                    DisplayEntity de = new DisplayEntity();
                    de.Entity = SelectedEntity;
                    de.Id = canvas.Name;

                    entities.Add(de);

                    DisplayEntity temp = new DisplayEntity();
                    if (tempCanvas != null && !canvas.Name.Equals(tempCanvas.Name))
                    {
                        foreach (DisplayEntity displayEntity in entities)
                        {
                            if (tempCanvas.Name.Equals(displayEntity.Id))
                            {
                                temp = displayEntity;
                            }
                        }
                        tempCanvas.Background = Brushes.White;
                        tempCanvas = null;
                        if (temp != null)
                        {
                            entities.Remove(temp);
                        }
                    }
                    DisplayEntities.Remove(SelectedEntity);
                }
            }
        }

        private void OnmouseDown(Canvas canvas)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals(canvas.Name))
                    {
                        dragging = true;
                        tempCanvas = canvas;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        DragDrop.DoDragDrop(canvas, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }

        private void OnbuttonClick(Canvas canvas)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = canvas;
            if (findCanvas != null)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals(findCanvas.Name))
                    {
                        foreach (Entity entity2 in NetworkEntitiesViewModel.BackUpEntities)
                        {
                            if (entity2.Id.Equals(entity.Entity.Id))
                            {
                                findCanvas.Background = Brushes.White;
                                NetworkDisplayViewModel.DisplayEntities.Add(entity2);
                                done = true;
                                entity1 = entity;
                                break;
                            }
                        }
                    }
                }
                if (done)
                {
                    entities.Remove(entity1);
                }
            }
        }

        private void OnrefreshClick(ListView list)
        {
            list.Items.Refresh();
        }

    }
}
