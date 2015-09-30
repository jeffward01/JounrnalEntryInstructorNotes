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
using System.Windows.Shapes;

namespace JournalNotes
{
    /// <summary>
    /// Interaction logic for ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        private JournalEntry currentEntry;

        public ConnectWindow()
        {
            InitializeComponent();
        }

        public void LoadEntry(JournalEntry entry)
        {
            currentEntry = entry;

            //Populate New WIndow
            newWindow_textBox_EntryTitle.Text = entry.Title;
            newWindow_Label_EntryDate.Content = entry.EntryDate;
            newWindow_Label_EntryID.Content = entry.Id;
            newWindow_textBox_EntryContent.Text = entry.Entry;

        }


        //Save Button Click
    
        public void populateEntry(JournalEntry Empty)
        {
           
            Empty.Entry = newWindow_textBox_EntryContent.Text;
            Empty.Title = newWindow_textBox_EntryTitle.Text;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //This is a cast
            var mainWindow = (MainWindow)Owner;

            //Save Inputs to varibles
            populateEntry(currentEntry);
            

            //Sends newly build Journal Entry to MainWindow Controller to be re-written
            mainWindow.reWriteEntry(currentEntry);

            //Close Window
            Close();
        }
    }
}
