using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using Wpf_AddressBook.Services;

namespace Wpf_AddressBook
{
    
    public partial class MainWindow : Window
    {
        private ObservableCollection<Contact> _contacts;
        private IFileService _fileService;
        private Guid _currentId;        

        public MainWindow()
        {
            InitializeComponent();            
            _contacts = new ObservableCollection<Contact>();

            _fileService = new FileService();
            try
            {
                _contacts = _fileService.Read();
            }
            catch { }
            
            lv_Contacts.ItemsSource = _contacts;
            
            tb_FirstName.Focus();
        }

        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
                    _currentId = item.Id;
                }
            }
            catch { }

            btn_Add.Visibility = Visibility.Hidden;
            btn_Edit.Visibility = Visibility.Visible;            
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            if(tb_FirstName.Text != "" || tb_Email.Text != "")
            {
                Contact contactExists = _contacts.FirstOrDefault(x => x.Email == tb_Email.Text);

                if (contactExists == null)
                {
                    _contacts.Add(new Contact 
                    { 
                        FirstName = tb_FirstName.Text,
                        LastName = tb_LastName.Text,
                        Email = tb_Email.Text,
                        Street = tb_Street.Text,
                        PostalCode = tb_PostalCode.Text,
                        City = tb_City.Text
                    });
                    _fileService.Save(_contacts);
                }
                else
                {
                    MessageBox.Show("Det finns redan en kontakt med denna e-postadress");
                }
            }
            else
            {
                MessageBox.Show("Förnamn och e-postadress måste anges");
            }

            ClearForm();            
            tb_FirstName.Focus();
        }

        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            Button button = sender as Button;
            Contact contact = (Contact)button!.DataContext;
            _contacts.Remove(contact);
            _fileService.Save(_contacts);
            ClearForm();
            }
            catch { }            
        }
        
        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            Contact selectedContact = _contacts.Where(c => c.Id == _currentId).FirstOrDefault();            

            selectedContact.FirstName = tb_FirstName.Text;
            selectedContact.LastName = tb_LastName.Text;
            selectedContact.Email = tb_Email.Text;
            selectedContact.Street = tb_Street.Text;
            selectedContact.PostalCode = tb_PostalCode.Text;
            selectedContact.City = tb_City.Text;

            _fileService.Save(_contacts);

            lv_Contacts.UnselectAll();
            lv_Contacts.Items.Refresh();

            ClearForm();

            btn_Edit.Visibility = Visibility.Hidden;
            btn_Add.Visibility = Visibility.Visible;
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
    }
}