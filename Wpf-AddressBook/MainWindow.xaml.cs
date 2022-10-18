using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        //private Guid _currentId;
        public MainWindow()
        {
            InitializeComponent();
            contacts = new ObservableCollection<Contact>();
            lv_Contacts.ItemsSource = contacts;

            //Kanske:
            tb_FirstName.Focus();
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            // kanske göra if-sats som kollar tomt fält här

            Contact contactExists = contacts.FirstOrDefault(x => x.Email == tb_Email.Text);  

            if(contactExists == null && tb_Email.Text != "")
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

            
            ClearForm();

            //Kanske:
            tb_FirstName.Focus();
        }
        private void ClearForm()
        {
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_Email.Text = "";
            tb_Street.Text = "";
            tb_PostalCode.Text = "";
            tb_City.Text = "";
        }
        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            

            try
            {
            Button button = sender as Button;
            Contact contact = (Contact)button!.DataContext;
            contacts.Remove(contact);
            ClearForm();
            }
            catch { }
            /*

            if(contact.Id == _currentId)
            {
                ClearForm();
                lv_Contacts.SelectedItems.Clear();
            }  */

            
            //lv_Contacts.SelectedItems.Clear();

            
            // ta bort kotakten från visningen först
        }

        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UIElement parent = Parent as UIElement; //loopa över alla textboxar och sätt readonly på dem - GetEnumerator?

            //_currentId = contact.Id;

            //  var items = obj!.Items;       items.SourceCollection; <-en lista (som går att loopa igenom

            try
            {
                var obj = sender as ListView;
                var item = (Contact)obj!.SelectedItem;

                if (item != null)
                { 
                    tb_FirstName.Text = item.FirstName;
                    tb_LastName.Text = item.LastName;
                    tb_Email.Text = item.Email;
                    tb_Street.Text = item.Street;
                    tb_PostalCode.Text = item.PostalCode;
                    tb_City.Text = item.City;
                }

            }
            catch { }

            

            btn_Add.Visibility = Visibility.Hidden;
            btn_Edit.Visibility = Visibility.Visible;


            //tb_FirstName.IsReadOnly = true; // Funkar!

        }
        
        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            //contacts.FirstOrDefault(x => x.Id == _currentId).FirstName = tb_FirstName.Text;

            

            ClearForm();

            btn_Edit.Visibility = Visibility.Hidden;
            btn_Add.Visibility = Visibility.Visible;

            //lv_Contacts_SelectionChanged(sender, e);
            //lv_Contacts.SelectedItems.Clear();
            //ClearForm();
        }
    }
}



// det behövs en knapp för att rensa så man kan lägga till ny contact när en annan är i focus
// samma funktion kanske kan användas för att rensa innan man raderar kontakt
// https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/routed-events-overview?view=netdesktop-6.0