using System;
using System.Windows.Input;
using LiveCharts;  
using LiveCharts.Wpf;  
using WpfApp2.Repositories;

namespace WpfApp2.ViewModel
{
    public class StatistikaViewModel : ViewModelBase
    {
        private int _numberOfPredavaci;
        private int _numberOfPolaznici;

        public int NumberOfPredavaci
        {
            get { return _numberOfPredavaci; }
            set { _numberOfPredavaci = value; OnPropertyChanged(nameof(NumberOfPredavaci)); OnPropertyChanged(nameof(PredavaciValues)); }
        }

        public int NumberOfPolaznici
        {
            get { return _numberOfPolaznici; }
            set { _numberOfPolaznici = value; OnPropertyChanged(nameof(NumberOfPolaznici)); OnPropertyChanged(nameof(PolazniciValues)); }
        }

        public ChartValues<int> PolazniciValues { get; set; }
        public ChartValues<int> PredavaciValues { get; set; }

        public ICommand RefreshStatisticsCommand { get; }

        private readonly PredavacRepository _predavacRepository;
        private readonly PolaznikRepository _polaznikRepository;

        public StatistikaViewModel()
        {
            _predavacRepository = new PredavacRepository();
            _polaznikRepository = new PolaznikRepository();

            RefreshStatisticsCommand = new ViewModelCommand(ExecuteRefreshStatisticsCommand);

            LoadStatistics();
        }

        private void LoadStatistics()
        {
            NumberOfPredavaci = GetPredavaciCount();
            NumberOfPolaznici = GetPolazniciCount();

            PolazniciValues = new ChartValues<int> { NumberOfPolaznici };
            PredavaciValues = new ChartValues<int> { NumberOfPredavaci };

            OnPropertyChanged(nameof(PolazniciValues));
            OnPropertyChanged(nameof(PredavaciValues));
        }

        private int GetPredavaciCount()
        {
            return _predavacRepository.GetPredavaciCount();
        }

        private int GetPolazniciCount()
        {
            return _polaznikRepository.GetPolazniciCount();
        }

        public string Omjer
        {
            get
            {
                double total = NumberOfPredavaci + NumberOfPolaznici;
                if (total == 0) return "Polaznici: 0% Predavaci: 0%";

                double polazniciRatio = (double)NumberOfPolaznici / total * 100;
                double predavaciRatio = (double)NumberOfPredavaci / total * 100;

                return $"Polaznici: {polazniciRatio:F2}% Predavaci: {predavaciRatio:F2}%";
            }
        }



        private void ExecuteRefreshStatisticsCommand(object obj)
        {
            LoadStatistics();
        }
    }
}
