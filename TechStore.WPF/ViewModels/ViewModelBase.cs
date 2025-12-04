using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TechStore.WPF.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        protected virtual string Validate(string columnName)
        {
            return null; 
        }
    }
}