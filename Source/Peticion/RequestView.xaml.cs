using ReactiveUI;

namespace Peticion
{
    public partial class RequestView : IViewFor<RequestViewModel>
    {
        public RequestView(RequestViewModel requestViewModel)
        {
            InitializeComponent();

            ViewModel = requestViewModel;

            this.WhenActivated(d =>
            {
                d(this.OneWayBind(ViewModel, vm => vm.HttpMethods, v => v.httpMethodsComboBox.ItemsSource));
                d(this.Bind(ViewModel, vm => vm.SelectedHttpMethod, v => v.httpMethodsComboBox.SelectedItem));
                d(this.Bind(ViewModel, vm => vm.Url, v => v.urlTextBox.Text));
                d(this.BindCommand(ViewModel, vm => vm.SendRequest, v => v.sendRequestButton));

                d(this.Bind(ViewModel, vm => vm.ResponseStatusCode, v => v.responseStatusCodeTextBlock.Text));
                d(this.Bind(ViewModel, vm => vm.ResponseBody, v => v.responseBodyTextBlock.Text));
            });
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (RequestViewModel)value; }
        }

        public RequestViewModel ViewModel { get; set; }
    }
}
