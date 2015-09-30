using System;
using System.Collections.Generic;
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
using JournalNotes.Properties;

namespace JournalNotes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
            //Constructor
        public MainWindow()
        {
            InitializeComponent();
            currentJournal = new Journal("MyJournal");

            dataGrid_JournalEntries.ItemsSource = currentJournal.Entries;
        }
        //Properties
        private Journal currentJournal;

        //Methods
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Varibles
            string title = textBox_Title.Text;
            string entry = textBox_Entry.Text;
            DateTime currentDate = DateTime.Now;

            JournalEntry newEntry = new JournalEntry();
            newEntry.Id = currentJournal.Entries.Count + 1;
            newEntry.Title = title;
            newEntry.Entry = entry;
            newEntry.EntryDate = currentDate;

            currentJournal.Entries.Add(newEntry);

                
        }

        //Clear DeleteEntry on Focus
        private void textBox_DeleteEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox_DeleteEntry.Text = String.Empty;
        }

        private void textBox_EditEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox_EditEntry.Text = String.Empty;
        }

        private void textBox_DuplicateEntry_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox_DuplicateEntry.Text = String.Empty;
        }
    }
}
