using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp2.Model;
using WpfApp2.Repositories;

namespace WpfApp2.ViewModel
{
    public class TablicaViewModel : ViewModelBase
    {
        private readonly TablicaRepository _tablicaRepository;

        // ObservableCollection to hold the data for the DataGrid
        public ObservableCollection<TablicaModel> TablicaData { get; set; }

        // Selected item for deletion
        private TablicaModel _selectedTablicaModel;
        public TablicaModel SelectedTablicaModel
        {
            get { return _selectedTablicaModel; }
            set { SetProperty(ref _selectedTablicaModel, value); }
        }

        // Command for deleting the row
        public ICommand DeleteCommand { get; private set; }

        public TablicaViewModel()
        {
            _tablicaRepository = new TablicaRepository();
            TablicaData = new ObservableCollection<TablicaModel>();
            DeleteCommand = new ViewModelCommand(DeleteRow, CanDeleteRow); // Initialize DeleteCommand

            // Load data from repository (example method, you can customize it)
            LoadData();
        }

        // Method to load data from repository
        private void LoadData()
        {
            var data = _tablicaRepository.GetAllData();
            foreach (var item in data)
            {
                TablicaData.Add(item);
            }
        }

        // Method to delete a row
        private void DeleteRow(object parameter)
        {
            if (SelectedTablicaModel != null)
            {
                // Remove from the ObservableCollection (UI update)
                TablicaData.Remove(SelectedTablicaModel);

                // Call repository to delete data from the database
                _tablicaRepository.DeleteData(SelectedTablicaModel);
            }
        }

        // CanExecute method for DeleteCommand (only allow delete if an item is selected)
        private bool CanDeleteRow(object parameter)
        {
            return SelectedTablicaModel != null; // Only allow delete when a row is selected
        }
    }
}
