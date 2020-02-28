using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace CSVLibrary
{
    public class CSVWriter
    {
        string _Path = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Path were the File should be saved to INCLUDING .csv</param>
        public CSVWriter(string path)
        {
            _Path = path;
        }

        /// <summary>
        /// Writes all Properties of t to a csv file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toWrite">needs to have an empty constructor</param>
        /// <param name="seperator">Standard to ';' for csv files</param>
        public void Write<T>(T toWrite, char seperator = ';') where T : new()
        {
            using (StreamWriter sw = new StreamWriter(_Path, false))
            {
                StringBuilder writeThis = new StringBuilder();
                var type = toWrite.GetType();
                foreach (var property in type.GetProperties().Where(p => p.CanWrite))
                {
                    writeThis.Append($"{property.Name}={property.PropertyType.FullName}={property.GetValue(toWrite)};");
                }
                sw.Write(writeThis);
            }
        }
        public void Write<t>(List<t> toWrite, char seperator = ';') where t : new()
        {
            using (StreamWriter sw = new StreamWriter(_Path, false))
            {
                StringBuilder writeThis = new StringBuilder();
                foreach (var itemToWrite in toWrite)
                {
                    foreach (var property in itemToWrite.GetType().GetProperties().Where(p => p.CanWrite))
                    {
                        writeThis.Append($"{property.Name}={property.PropertyType}={property.GetValue(itemToWrite)};");
                    }
                    writeThis.AppendLine();
                }
                sw.Write(writeThis);
            }
        }
    }
}
