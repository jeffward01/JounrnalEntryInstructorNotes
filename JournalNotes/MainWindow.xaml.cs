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
using Microsoft.Win32;
using System.IO;
using System.Collections;
using Newtonsoft.Json;



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
            label_fileName.Content = fileName;
        }
        //Properties
        private Journal currentJournal;
        public string fileName = "Untitled";
        public string filePath;
       

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
            textBox_Title.Text = String.Empty;
            textBox_Entry.Text = String.Empty;


        }

        //Casting Technique
        //Clear all Textboxes
        private void onFocusClearText(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;

            textBox.Text = string.Empty;

           
        }


        //Delete Entry
        private void button_DeleteEntry_Click(object sender, RoutedEventArgs e)
        {
            //Grab input from textbox
            string EraseEntryNum = textBox_DeleteEntry.Text;
            
            //validate input and save input
            int EraseNum = readInt(EraseEntryNum);

            //cycle through journal entries locate id#
            JournalEntry toBeErased = null;

            foreach(var entry in currentJournal.Entries)
            {
                if(entry.Id == EraseNum)
                {
                    toBeErased = entry;
                    break;
                }
            }

            if (toBeErased != null)
            {
                currentJournal.Entries.Remove(toBeErased);
            }
        }

        //Dupliate Entry
        private void button_DuplicateEntry_Click(object sender, RoutedEventArgs e)
        {
            //Grab Input from user
            string EntryToDuplicate = textBox_DuplicateEntry.Text;

            //Validate and save as int
            int EntryNum = readInt(EntryToDuplicate);

            //Make Var for JournalEntry
            JournalEntry toBeDuplicated= null;

            foreach (var id in currentJournal.Entries)
            {
                if (id.Id == EntryNum)
                {
                    toBeDuplicated = id;
                    break;
                }
            }

            if (toBeDuplicated != null)
            {
                //Dupliate Entry Here

                JournalEntry newCopy = null;

                newCopy = toBeDuplicated;


                newCopy.Id = currentJournal.Entries.Count + 1;

                currentJournal.Entries.Add(newCopy);


            }




        }

        public int readInt(string myInput, bool isPositive = true, bool canBeZero = false)
        {

            try
            {
                //Grab Users input
                string input = myInput;

                //Make sure there is input
                if(string.IsNullOrEmpty(input))
                {
                    MessageBox.Show("Please enter a number before pressing button");
                    return 0;
                }

                int myInt = int.Parse(input);

                //Make sure myInput is positive
                if(isPositive == true && myInt < 0 )
                {
                    MessageBox.Show("Please enter a positive number before pressing button");
                    
                }
                if(canBeZero == false && myInt == 0)
                {
                    MessageBox.Show("Please enter a non zero number before pressing button");

                }

                return myInt;

            }
            catch (Exception)
            {

                MessageBox.Show("Please enter a whole number");
                return 0;

            }
            
            

        }

        //Clear All Entries
        private void button_DeleteEntry_Copy_Click(object sender, RoutedEventArgs e)
        {

            //Ask user if they are sure they want to clear all Entries
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear all entries?", "Clear all Entries", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                currentJournal.Entries.Clear();
                textBox_Title.Text = String.Empty;
                textBox_Entry.Text = String.Empty;

            }
            return;
        }

        private void button_EditEntry_Click(object sender, RoutedEventArgs e)
        {

            //Locate Entry
            string input = textBox_EditEntry.Text;

            //Validate UserInput
            int EntryID = readInt(input);

            //Make Var for JournalEntry
            JournalEntry toBeEditted = null;

            //Locate Object
            foreach (var id in currentJournal.Entries)
            {
                if (id.Id == EntryID)
                {
                    toBeEditted = id;
                    break;
                }
            }

            if (toBeEditted != null)
            {
                //Open Edit Entry Window Here
                openEditEntry(toBeEditted);
            }

        }
        public void openEditEntry(JournalEntry editEntry)
        {
            //open window
            var window = new ConnectWindow { Owner = this };
            window.LoadEntry(editEntry);
            window.ShowDialog();

          

        }
        public void reWriteEntry(JournalEntry newVersion)
        {
            int newVerstionID = newVersion.Id;

            //declare varible for oldversion
            JournalEntry OldVersion = null;

            //Locate old Version
            foreach (var entry in currentJournal.Entries)
            {
                if(entry.Id == newVerstionID)
                {
                    OldVersion = newVersion;
                    break;
                }

                //Resave New Version
                if(OldVersion != null)
                {
                    //Remove Old Version
                    currentJournal.Entries.RemoveAt(newVerstionID);
                    currentJournal.Entries.Add(OldVersion);

                    //Find location of newly modified entry
                    int modifiedEntry = currentJournal.Entries.Count;

                    //Move modified entry to previous location
                    currentJournal.Entries.Move(modifiedEntry, newVerstionID);

                }
            }
            //Refresh Grid
            dataGrid_JournalEntries.ItemsSource = null;
            dataGrid_JournalEntries.ItemsSource = currentJournal.Entries;
        }
      
        private void doubleClickDataGrid(object sender, SelectionChangedEventArgs e)
        {
            //Cast
            //Create a refrence object, save the refrence object as a selected item
            JournalEntry selected = (JournalEntry)dataGrid_JournalEntries.SelectedItem;

            //open new window with selection
            var window = new ConnectWindow { Owner = this };
            window.LoadEntry(selected);
            window.ShowDialog();
        }
        public void OpenSaveAs()
        {
            //OPens Save File Dialog Box
            SaveFileDialog savefiledialog1 = new SaveFileDialog();

            //Only Allows .txt files to be saved
            savefiledialog1.Filter = "Text Files|*.txt";

            //Users choice to act with Save Dialog
            bool? saveFileAs = savefiledialog1.ShowDialog();

            //If user does  hit save
            if(saveFileAs == true)
            {
                //file name/path of user choice
                string fileNameNew = savefiledialog1.FileName;

                //Save File Path to varible
                filePath = fileNameNew;

                //Change Label of top bar
                //Example of Instantiazion
                FileInfo fi = new FileInfo(savefiledialog1.FileName);
                string text = fi.Name;
                

                //save contents to string
                string myContents = Newtonsoft.Json.JsonConvert.SerializeObject(currentJournal, Formatting.Indented);

                //Writes the text to the file
                File.WriteAllText(fileNameNew, myContents);

                //Display FileName
                label_fileName.Content = text;
                
                //change globable filename to new filename
                fileName = text;
            }
            //If user decides not to save, do nothing
            return;
        }

       public void SendToPrinter()
        {
            try
            {
                PrintDialog printdialog = new PrintDialog();
                printdialog.ShowDialog();

            }
            catch(Exception)
            {
                MessageBox.Show("An Error has occured, please contact ADMIN");
            }

        }




        //Menu Items
        //
        //
        //


        //Save Menu Item
        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            if(fileName == "Untitled")
            {
                OpenSaveAs();
            }

            try
            {
                //Check contents of FIleName
                string fileNameContents = File.ReadAllText(filePath);

                //Check contents of current journal
                string myContents = Newtonsoft.Json.JsonConvert.SerializeObject(currentJournal, Formatting.Indented);


                if (fileNameContents == myContents)
                {
                    return;

                }
                File.WriteAllText(filePath, myContents);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured saving.  Please use SAVE AS and contact system Admin");
            }
        }


        private void MenuItem_SaveAs_Click(object sender, RoutedEventArgs e)
        {
            OpenSaveAs();
        }

        //Print Button
        private void MenuItem_Print_Click(object sender, RoutedEventArgs e)
        {
            SendToPrinter();

        }

        //Exit Menu Item
        private void MenutItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to quit?", "Exit Prompt", MessageBoxButton.YesNo);
                {
                if(result == MessageBoxResult.Yes)
                {
                    Environment.Exit(0);
                }
                return;
            }
        }

        //New Journal
        private void MenuItem_New_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to create a new Journal? Any unsaved changes will be lost", "New Item Prompt", MessageBoxButton.YesNo);
                {
                     if(result == MessageBoxResult.Yes)
                     {
                          currentJournal.Entries.Clear();
                          textBox_Title.Text = String.Empty;
                         textBox_Entry.Text = String.Empty;


                     }


                }

        }

        //Needs to be completed
        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            //Open open file Dialog
            OpenFileDialog dialog = new OpenFileDialog();

            bool? openFile = dialog.ShowDialog();

            if (openFile == true)
            {
                //Get the name of the file selected by user
                fileName = dialog.FileName;

                //OPen the file and read the contents into a string
                string myFile = File.ReadAllText(fileName);

                try
                {
                    Journal newJournal = JsonConvert.DeserializeObject<Journal>(myFile);


                    currentJournal = newJournal;
                    //Refresh Grid


                    dataGrid_JournalEntries.ItemsSource = null;

                   // dataGrid_JournalEntries.ItemsSource = currentJournal;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error opening file, contact system ADMIN");

                }
                

               
            }
        }

        private void button_SearchEntry_Click(object sender, RoutedEventArgs e)
        {
            //Get input from textbox
            string SearchString = textBox_SearchEntry.Text;

            //See if SearchString is found in Journal

            //Make Var for JournalEntry
            JournalEntry Existing = null;

            //Locate Object in Title
            foreach (var phrase in currentJournal.Entries)
            {
                if (phrase.Title == SearchString)
                {
                    Existing = phrase;
                    break;
                }
            }
            try
            {
                //Locate Object in Date
                foreach (var phrase in currentJournal.Entries)
                {
                    DateTime myDate = DateTime.Parse(SearchString);
                    if (phrase.EntryDate == myDate)
                    {
                        Existing = phrase;
                        break;
                    }
                }
            }
            catch
            { }
            
            //Locate Object in Entry
            foreach (var phrase in currentJournal.Entries)
            {
                if (phrase.Entry == SearchString)
                {
                    Existing = phrase;
                    break;
                }
            }

            try
            {
                //Locate Object in ID
                foreach (var phrase in currentJournal.Entries)
                {
                    int myID = Int32.Parse(SearchString);
                    if (phrase.Id == myID)
                    {
                        Existing = phrase;
                        break;
                    }
                }
            }
            catch { }

            if (Existing != null)
            {
                //Open Edit Entry Window Here
                openEditEntry(Existing);
            }
            else
            {

                MessageBox.Show("Your search cannot be found... Try something else.");

            }



        }
       
    }

}
