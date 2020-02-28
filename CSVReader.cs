using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSVLibrary
{
    public class CSVReader
    {
        string _Path = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name = "path" > Path were the File should be fetched from INCLUDING.csv</param>
        public CSVReader(string path)
        {
            _Path = path;
        }

        /// <summary>
        /// Writes all Properties of t to a csv file
        /// </summary>
        /// <typeparam name = "T" ></ typeparam >
        /// < param name="toWrite">needs to have an empty constructor</param>
        /// <param name = "seperator" > Standard to ';' for csv files</param>
        public T ReadSingle<T>(char seperator = ';') where T : new()
        {
            using (StreamReader sr = new StreamReader(_Path))
            {
                var newT = new T();
                while (!sr.EndOfStream)
                {
                    string[] daten = sr.ReadLine().Split(seperator);
                    foreach (var item in daten)
                    {
                        var infos = item.Split('=');
                        if (infos.Length == 3)
                        {
                            var PropertyName = infos[0];
                            var PropertyType = Type.GetType(infos[1]);
                            var PropertyValue = Convert.ChangeType(infos[2], PropertyType);
                            newT.GetType().GetProperty(PropertyName).SetValue(newT, PropertyValue);
                        }
                    }
                }
                return newT;
            }
        }
        public List<T> ReadAll<T>(char seperator = ';') where T : new()
        {
            using (StreamReader sr = new StreamReader(_Path))
            {
                List<T> returnValue = new List<T>();
                
                while (!sr.EndOfStream)
                {
                    string[] daten = sr.ReadLine().Split(seperator);
                    var newT = new T();
                    foreach (var item in daten)
                    {
                        var infos = item.Split('=');
                        if (infos.Length == 3)
                        {
                            var PropertyName = infos[0];
                            var PropertyType = Type.GetType(infos[1]);
                            var PropertyValue = Convert.ChangeType(infos[2], PropertyType);
                            newT.GetType().GetProperty(PropertyName).SetValue(newT, PropertyValue);
                        }
                    }
                    returnValue.Add(newT);
                }
                return returnValue;
            }
        }
    }
}
