using ReactiveUI;

namespace Peticion
{
    public partial class HistoryView : IViewFor<HistoryViewModel>
    {
        public HistoryView()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                this.Bind(ViewModel, vm => vm.Requests, v => v.requestsItemsControl.ItemsSource); 
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
