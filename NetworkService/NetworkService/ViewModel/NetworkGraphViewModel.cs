using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class NetworkGraphViewModel :BindableBase
    {

        public static ObservableCollection<int> Entities { get; set; } = new ObservableCollection<int>();


        public NetworkGraphViewModel()
        {
            Entities.Add(550);
            Entities.Add(550);
            Entities.Add(550);
            Entities.Add(550);
        }

        public static void ReadFromFile(string s)
        {
            Entities.Clear();
            string str;
            string[] value;
            double num;
            using(StreamReader reader = new StreamReader(s))
            {
                while (reader.Peek() >= 0)
                {
                    str = reader.ReadLine();
                    value = str.Split(':');
                    num = int.Parse(value[4]);
                    num = 550 - num * 4;


                    Entities.Insert(0, (int)num);
                    Console.WriteLine((int)num);
                }
                
            }
        }

    }
}
