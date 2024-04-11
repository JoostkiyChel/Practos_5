using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft;
using System.IO;
using Newtonsoft.Json;

namespace Practos_5
{
    internal class Converter
    {
        public static T DeserializeObject<T>()
        {
               OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                string json = File.ReadAllText(ofd.FileName);
                T obj = JsonConvert.DeserializeObject<T>(json);
                return obj;
            }
            else
            {
                return default(T);
            }
        }
    }
}
