using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoloGameSundayPicker
{
    /// <summary>
    /// Gui data for main window
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<string> _Names = new ObservableCollection<string>(string.IsNullOrEmpty(Properties.Settings.Default.SavedNames) == false ? Properties.Settings.Default.SavedNames.Split(',').ToList() : new List<string>());
        /// <summary>
        /// Everyone's name
        /// </summary>
        public ObservableCollection<string> Names
        {
            get
            {
                return _Names;
            }

            set
            {
                if (_Names != value)
                {
                    _Names = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _NameToAdd = string.Empty;
        /// <summary>
        /// Name to add to list
        /// </summary>
        public string NameToAdd
        {
            get
            {
                return _NameToAdd;
            }

            set
            {
                if(_NameToAdd != value)
                {
                    _NameToAdd = value;
                    OnPropertyChanged();
                }
            }
        }

        private Dictionary<string, string> _NamePairs = new Dictionary<string, string>();
        /// <summary>
        /// Name pairs
        /// </summary>
        public Dictionary<string,string> NamePairs
        {
            get
            {
                return _NamePairs;
            }

            set
            {
                if (_NamePairs != value)
                {
                    _NamePairs = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }//END class MainWindowViewModel
}//END namespace
