using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp2.Model;
using WpfApp2.Repositories;

namespace WpfApp2.ViewModel
{
    public class TablicaViewModel : ViewModelBase
    {
        private ObservableCollection<TablicaModel> _tablicaData;
        private TablicaRepository _tablicaRepository;

        public TablicaViewModel()
        {
            _tablicaRepository = new TablicaRepository();
            _tablicaData = new ObservableCollection<TablicaModel>();

            // Load data when the ViewModel is created
            LoadData();
        }

        public ObservableCollection<TablicaModel> TablicaData
        {
            get { return _tablicaData; }
            set { SetProperty(ref _tablicaData, value); }
        }

        private void LoadData()
        {
            var data = _tablicaRepository.GetAllData();
            foreach (var item in data)
            {
                TablicaData.Add(item);
            }
        }
    }
}
