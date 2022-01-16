using NetworkService.Model;
using NetworkService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkService.Views
{
    /// <summary>
    /// Interaction logic for NetworkDisplay.xaml
    /// </summary>
    public partial class NetworkDisplay : UserControl
    {
        private string Slika = "";
        private Entity SelectedEntity { get; set; } = new Entity();
        private DisplayEntity displayEntity = new DisplayEntity();
        private bool dragging = false;
        private List<DisplayEntity> entities = new List<DisplayEntity>();
        private Canvas canvas;

        public NetworkDisplay()
        {
            
            InitializeComponent();
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!dragging && listView.SelectedItem != null)
            {
                dragging = true;
                Console.WriteLine(SelectedEntity.Name);
                SelectedEntity = (Entity)listView.SelectedItem;
                Slika = SelectedEntity.Type.ImageSource;
                Console.WriteLine(Slika);
                DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
            }
            
        }

        private void listView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            dragging = false;
            Slika = "";
        }

        private void dragOver(object sender, DragEventArgs e)
        {
            base.OnDragOver(e);
            if (((Canvas)sender).Resources["taken"] != null)
                e.Effects = DragDropEffects.None;
            else
                e.Effects = DragDropEffects.Copy;
            
            e.Handled = true;
        }

        private void drop(object sender, DragEventArgs e)
        {
            dragging = false;
            base.OnDrop(e);
            if(SelectedEntity != null)
            {
                if(((Canvas)sender).Resources["taken"] == null)
                {
                    BitmapImage background = new BitmapImage();
                    background.BeginInit();
                    background.UriSource = new Uri("pack://application:,,,/Views/" + Slika);
                    background.EndInit();
                    ((Canvas)sender).Background = new ImageBrush(background);
                    DisplayEntity de = new DisplayEntity();
                    de.Entity = NetworkEntitiesViewModel.CreateNewEntity(SelectedEntity);
                    de.Id = ((Canvas)sender).Name;
                    
                    entities.Add(de);

                    DisplayEntity temp = new DisplayEntity();
                    if(canvas != null)
                    {
                        foreach(DisplayEntity displayEntity in entities)
                        {
                            if (canvas.Name.Equals(displayEntity.Id))
                            {
                                temp = displayEntity;
                            }
                        }
                        canvas.Background = Brushes.White;
                        canvas = null;
                        if(temp != null)
                        {
                            entities.Remove(temp);
                        }
                    }
                    NetworkDisplayViewModel.DisplayEntities.Remove(SelectedEntity);
                }
            }
        }

        private void mouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach(DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas1")){
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }
                
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas1");
            if (findCanvas != null)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals(findCanvas.Name))
                    {
                        foreach(Entity entity2 in NetworkEntitiesViewModel.BackUpEntities)
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


        private void button12_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas12");
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

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas11");
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

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas10");
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

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas9");
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

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas8");
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

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas7");
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

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas6");
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

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas5");
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

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas4");
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

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas3");
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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            bool done = false;
            DisplayEntity entity1 = new DisplayEntity();
            Canvas findCanvas = (Canvas)stackPanel.FindName("itemCanvas2");
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

        private void itemCanvas2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas2"))
                    {
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }

        private void itemCanvas3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas3"))
                    {
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }

        private void itemCanvas4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas4"))
                    {
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }

        private void itemCanvas5_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas5"))
                    {
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }

        private void itemCanvas6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas6"))
                    {
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }

        private void itemCanvas7_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas7"))
                    {
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }

        private void itemCanvas8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas8"))
                    {
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }

        private void itemCanvas9_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas9"))
                    {
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }

        private void itemCanvas10_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas10"))
                    {
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }

        private void itemCanvas11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas11"))
                    {
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }

        private void itemCanvas12_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!dragging)
            {
                foreach (DisplayEntity entity in entities)
                {
                    if (entity.Id.Equals("itemCanvas12"))
                    {
                        dragging = true;
                        canvas = (Canvas)sender;
                        SelectedEntity = entity.Entity;
                        Slika = SelectedEntity.Type.ImageSource;
                        Console.WriteLine(Slika);
                        DragDrop.DoDragDrop(this, Slika, DragDropEffects.Copy);
                        break;
                    }
                }

            }
        }
    }
}
