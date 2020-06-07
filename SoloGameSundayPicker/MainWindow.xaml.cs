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

namespace SoloGameSundayPicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Gui data
        /// </summary>
        private MainWindowViewModel _ViewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();

            this.KeyUp += KeyPressedHandler;
            _NameToAdd_TextBox.TextChanged += NameChangedHandler;

            //Assign window's binding data context
            this.DataContext = _ViewModel;
        }//END MainWindow()

        /// <summary>
        /// when enter is pressed add name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyPressedHandler(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && _NameToAdd_TextBox.IsFocused)
            {
                _AddName_Button_Click(null, null);
            }
        }//END KeyPressedHandler()

        /// <summary>
        /// when enter is pressed add name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NameChangedHandler(object sender, EventArgs e)
        {
            _ViewModel.NameToAdd = _NameToAdd_TextBox.Text;
        }//END KeyPressedHandler()

        /// <summary>
        /// Adds name to list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _AddName_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nameToAdd = _ViewModel.NameToAdd.Replace(",", "");

                if (string.IsNullOrEmpty(nameToAdd) == false)
                {
                    if (_ViewModel.Names.Contains(nameToAdd) == false)
                    {
                        _ViewModel.Names.Add(nameToAdd);
                        _ViewModel.NameToAdd = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show($"Name {_ViewModel.NameToAdd} already exists.", "DUPLICATE NAME", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"ERROR OCCURED: {ex.Message}.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }//END _AddName_Button_Click()

        /// <summary>
        /// Delete selected name in list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_Names_ListBox.SelectedIndex != -1 && _Names_ListBox.SelectedIndex < _ViewModel.Names.Count)
                {
                    _ViewModel.Names.RemoveAt(_Names_ListBox.SelectedIndex);
                }
            }
            catch 
            {
                //DO NOTHING
            }
        }//END _Delete_Button_Click()

        /// <summary>
        /// Randomly pairs names
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool pairsComplete = false;

                while(pairsComplete == false)
                {
                    CryptoRandomNumberGen random = new CryptoRandomNumberGen();

                    //reset
                    _ViewModel.NamePairs = new Dictionary<string, string>();

                    //These are the names remaining to be picked
                    List<string> namesRemaining = new List<string>(_ViewModel.Names);
                    namesRemaining.Shuffle();

                    //These are the people picking
                    List<string> namesForPicking = new List<string>(_ViewModel.Names);
                    namesForPicking.Shuffle();

                    //go through each name
                    foreach (string name in namesForPicking)
                    {
                        //Only allow valid names for this person
                        List<string> validNamesForPerson = new List<string>(namesRemaining);
                        validNamesForPerson.Remove(name);

                        //Need to ensure that we don't reach a case there the last two names are the same
                        if(validNamesForPerson.Count > 0)
                        {
                            //chose a random person for this person
                            string chosenName = validNamesForPerson[random.Next(0, validNamesForPerson.Count)];

                            _ViewModel.NamePairs[name] = chosenName;

                            namesRemaining.Remove(chosenName);
                        }
                        else
                        {
                            //we failed
                            break;
                        }
                    }

                    //Did we get through all names?
                    if(namesRemaining.Count == 0)
                    {
                        pairsComplete = true;
                    }
                }

                Properties.Settings.Default.SavedNames = string.Join(",", _ViewModel.Names.ToList());
                Properties.Settings.Default.Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"ERROR OCCURED: {ex.Message}.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }//END _Generate_Button_Click()

        /// <summary>
        /// Copy results to clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Copy_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string tableStr = $"";
                if(_ViewModel.NamePairs.Count > 0)
                {

                    tableStr = TableFormater.FormatTable(_ViewModel.NamePairs);
                }

                //Copy to clipboard
                System.Windows.Clipboard.SetText(tableStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR OCCURED: {ex.Message}.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }//END _Copy_Button_Click()
    }//END class MainWindow
}//END Namespace
