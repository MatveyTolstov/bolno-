using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fear_and_Pain
{
    internal class Convertor
    {
         private static string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static void Jsonser<T>(T Mydata, string filename)
        {
            string json = JsonConvert.SerializeObject(Mydata);
            File.WriteAllText(desktop + "\\" + filename, json);
        }
        public static T Jsonviser<T>(string filename) 
        {
            string json = "";
            try
            {
                json = File.ReadAllText(desktop + "\\" + filename);

            }
            catch (Exception)
            {
                File.Create(desktop + "\\" + filename).Close();
                json = File.ReadAllText(desktop + "\\" + filename);
            }
            T Mydata = JsonConvert.DeserializeObject<T>(json);
            return Mydata;
        }

        
    }
}

