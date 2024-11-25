using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE;

namespace Projeto
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void TurnOnButtonClicked(object sender, EventArgs e)
        {
            //obter o adaptador Bluetooth
            var ble = CrossBluetoothLE.Current;
            var adapter = CrossBluetoothLE.Current.Adapter;

            //verificar se o bluetooth está ligado
            if (ble.State != BluetoothState.On)
            {
                //mostrar alerta se o Bluetooth estiver desligado
                await DisplayAlert("Bluetooth Turned Off",
                                   "Please, turn it on to continue.",
                                   "OK");
                return;
            }

            await Navigation.PushAsync(new DevicesPage());
        }
    }

}
