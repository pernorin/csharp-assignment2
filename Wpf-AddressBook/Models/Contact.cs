﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_AddressBook.Models
{
    internal class Contact //: INotifyPropertyChanged
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";

        //public event PropertyChangedEventHandler? PropertyChanged;
    }
}
// https://daedtech.com/wpf-and-notifying-property-change/
// https://wellsb.com/csharp/learn/wpf-data-binding-csharp-inotifypropertychanged
