using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static System.Environment;

namespace WpfApp2Test2.LoaderService
{
    /// <summary>
    /// LÄdt und speichert Daten im und aus dem Json Format
    /// </summary>
    public class JsonLoader : LoaderInterface
    {

        public (bool, IList<Customer>? customers) LoadData()
        {
            try
            {
                string path = this.GetPath();
                if (File.Exists(path))
                {
                    string loadedDatas = File.ReadAllText(path);
                    IList<Customer> customers = JsonSerializer.Deserialize<List<Customer>>(loadedDatas);
                    return (true, customers);
                }
                else
                    return (false, null);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public (bool, string?) SaveData(IList<Customer> customers)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(customers);
                string path = this.GetPath();
                File.WriteAllText(path, jsonString);

                return (true, jsonString);
            }
            catch (Exception ex)
            {
                return (false, null);
            }
        }

        public string GetPath()
        {
            return $"{Environment.GetFolderPath(SpecialFolder.Desktop)}\\CustomerData.json";
        }
    }
}
