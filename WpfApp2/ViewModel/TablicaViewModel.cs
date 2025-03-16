using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp2.Model;
using WpfApp2.Repositories;
using WpfApp2.ViewModel;

namespace WpfApp2.ViewModel
{
    public class TablicaViewModel : ViewModelBase
    {
        private readonly TablicaRepository _tablicaRepository;
        private ObservableCollection<TablicaModel> _tablicaData;
        private ObservableCollection<TablicaModel> _originalData; 

        public ObservableCollection<TablicaModel> TablicaData
        {
            get { return _tablicaData; }
            set
            {
                _tablicaData = value;
                OnPropertyChanged(nameof(TablicaData));
            }
        }

        private TablicaModel _selectedTablicaModel;
        public TablicaModel SelectedTablicaModel
        {
            get { return _selectedTablicaModel; }
            set { SetProperty(ref _selectedTablicaModel, value); }
        }

        public ICommand DeleteCommand { get; private set; }
        public ICommand FilterPredavaciCommand { get; private set; }
        public ICommand FilterPolazniciCommand { get; private set; } 
        public ICommand FilterTecajCommand { get; private set; } 

        public TablicaViewModel()
        {
            _tablicaRepository = new TablicaRepository();
            _originalData = new ObservableCollection<TablicaModel>(); // Inicijalizacija liste
            TablicaData = new ObservableCollection<TablicaModel>();

            DeleteCommand = new ViewModelCommand(DeleteRow, CanDeleteRow);
            FilterPredavaciCommand = new ViewModelCommand(FilterPredavaci);
            FilterPolazniciCommand = new ViewModelCommand(FilterPolaznici); 
            FilterTecajCommand = new ViewModelCommand(FilterTecaj); 

            LoadData();
        }

        private void LoadData()
        {
            var data = _tablicaRepository.GetAllData();
            _originalData = new ObservableCollection<TablicaModel>(data); // Pohrani originalne podatke
            TablicaData = new ObservableCollection<TablicaModel>(_originalData);

            Console.WriteLine($"Učitano zapisa: {_originalData.Count}");
        }

        private void DeleteRow(object parameter)
        {
            if (SelectedTablicaModel != null)
            {
                TablicaData.Remove(SelectedTablicaModel);
                _tablicaRepository.DeleteData(SelectedTablicaModel);
            }
        }

        private bool CanDeleteRow(object parameter)
        {
            return SelectedTablicaModel != null;
        }

        private void FilterPredavaci(object parameter)
        {
            if (_originalData != null && _originalData.Any())
            {
                var filteredData = _originalData
                    .Where(item => item.Role != null &&
                                  (item.Role.Equals("Predavac", StringComparison.OrdinalIgnoreCase) ||
                                   item.Role.Equals("Predavač", StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                TablicaData.Clear(); // Očisti postojeće podatke
                foreach (var item in filteredData)
                {
                    TablicaData.Add(item); // Dodaj filtrirane podatke
                }
            }
        }

        private void FilterPolaznici(object parameter)
        {
            if (_originalData != null && _originalData.Any())
            {
                var filteredData = _originalData
                    .Where(item => item.Role != null && item.Role.Equals("Polaznik", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                TablicaData.Clear(); // Očisti postojeće podatke
                foreach (var item in filteredData)
                {
                    TablicaData.Add(item); // Dodaj filtrirane podatke
                }
            }
        }

        private void FilterTecaj(object parameter)
        {
            if (_originalData != null && _originalData.Any())
            {
                var distinctCourses = _originalData
                    .Select(item => item.Course) 
                    .Distinct() 
                    .Where(course => !string.IsNullOrEmpty(course)) 
                    .ToList();

                var filteredData = distinctCourses
                    .Select(course => new TablicaModel { Course = course }) 
                    .ToList();

                TablicaData.Clear(); 
                foreach (var item in filteredData)
                {
                    TablicaData.Add(item); 
                }
            }
        }
    }
}
