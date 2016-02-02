using ReactiveUI;

namespace Peticion
{
    public partial class HistoryView : IViewFor<HistoryViewModel>
    {
        public HistoryView(HistoryViewModel historyViewModel)
        {
            InitializeComponent();
            ViewModel = historyViewModel;

            this.WhenActivated(d =>
            {
                this.OneWayBind(ViewModel, vm => vm.Requests, v => v.requestsListView.ItemsSource); 
            });
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (HistoryViewModel)value; }
        }

        public HistoryViewModel ViewModel { get; set; }
    }
}
