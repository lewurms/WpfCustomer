using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2Test2.LoaderService
{
    /// <summary>
    /// Interface welches die Methoden zum Laden und peichern von Daten vorgibt
    /// </summary>
    public interface LoaderInterface
    {
        /// <summary>
        /// Lade Daten
        /// </summary>
        /// <returns>true => daten wurden geladen 
        /// <para>false => daten wurden NICHT geladen</para>
        /// </returns>
        public (bool, IList<Customer> customers) LoadData();

        /// <summary>
        /// Speichere Daten
        /// </summary>
        /// <returns>true => daten wurden gespeichert  UND STRING enthält die Daten
        /// <para>false => daten wurden NICHT gespeichert UND STRING ist leer</para>
        /// </returns>
        public (bool, string) SaveData(IList<Customer> customers);

        public string GetPath();
    }
}
