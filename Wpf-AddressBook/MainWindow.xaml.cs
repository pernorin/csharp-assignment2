using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Wpf_AddressBook.Models;

namespace Wpf_AddressBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Contact> contacts;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new ObservableCollection<Contact>();
            lv_Contacts.ItemsSource = contacts;
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = contacts.FirstOrDefault(x => x.Email == tb_Email.Text);  // kanske ta bort?

            if(contact == null)
            {

                contacts.Add(new Contact 
                { 
                    FirstName = tb_FirstName.Text,
                    LastName = tb_LastName.Text,
                    Email = tb_Email.Text,
                    Street = tb_Street.Text,
                    PostalCode = tb_PostalCode.Text,
                    City = tb_City.Text
                });

            }
            else
            {
                MessageBox.Show("Det finns redan en kontakt med denna e-postadress");
            }
            

            tb_FirstName.Text = ""; // kanske en egen metod
            tb_LastName.Text = "";
            tb_Email.Text = "";
            tb_Street.Text = "";
            tb_PostalCode.Text = "";
            tb_City.Text = "";

        }
        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Contact contact = (Contact)button!.DataContext;

            contacts.Remove(contact);
            
        }

        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact contact = (Contact)lv_Contacts.SelectedItem;
            tb_FirstName.Text = contact.FirstName;
            tb_LastName.Text = contact.LastName;
            tb_Email.Text = contact.Email;
            tb_Street.Text = contact.Street;
            tb_PostalCode.Text = contact.PostalCode;
            tb_City.Text = contact.City;

            //var contact = (ContactPerson)lv_Contacts.SelectedItems[0]!;
        }
    }
}
