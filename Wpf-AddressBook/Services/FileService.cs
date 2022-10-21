using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Wpf_AddressBook.Models;

namespace Wpf_AddressBook.Services
{   // Hela klassen fungerar som i uppgift 1 bortsett från att filepath ligger direkt i denna fil
    internal interface IFileService 
    {
        public ObservableCollection<Contact> Read();
        public void Save(ObservableCollection<Contact> contacts);
    }
    internal class FileService : IFileService
    {
        private string _filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\wpf_contacts.json"; 

        public ObservableCollection<Contact> Read() 
        {
            var contacts = new ObservableCollection<Contact>();

            using var sr = new StreamReader(_filePath);
            contacts = JsonConvert.DeserializeObject<ObservableCollection<Contact>>(sr.ReadToEnd());

            return contacts;
        }
        public void Save(ObservableCollection<Contact> contacts) 
        {
            using StreamWriter sw = new StreamWriter(_filePath);
            sw.WriteLine(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}
