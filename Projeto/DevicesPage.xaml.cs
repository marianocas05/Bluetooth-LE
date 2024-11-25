using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Projeto;

public partial class DevicesPage : ContentPage
{
    private IAdapter _adapter;
    private ObservableCollection<IDevice> _devices;

    public DevicesPage()
    {
        InitializeComponent();

        //iniciar o adaptador bluetooth e a lista de dispositivos
        _adapter = CrossBluetoothLE.Current.Adapter;
        _devices = new ObservableCollection<IDevice>();

        //conectar o conjunto de dispositivos ao CollectionView
        DevicesCollectionView.ItemsSource = _devices;
    }

    private async void ScanButtonClicked(object sender, EventArgs e)
    {
        try
        {
            //limpar a lista de dispositivos antes de iniciar a mexer nela
            _devices.Clear();

            //registar o evento para dispositivos descobertos
            _adapter.DeviceDiscovered += (s, a) =>
            {
                Debug.WriteLine($"Discovered Device: {a.Device.Name} ({a.Device.Id})");

                //garantir que o código corre na MainThread para atualizar a UI
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (!_devices.Contains(a.Device))
                    {
                        _devices.Add(a.Device);
                    }
                });
            };

            //iniciar o scan de dispositivos
            await _adapter.StartScanningForDevicesAsync();
        }
        catch (Exception ex)
        {
            //mostrar erro se algo correr mal
            await DisplayAlert("Error", $"An error occurred while scanning: {ex.Message}", "OK");
        }
    }

    private async void OnDeviceSelected(object sender, SelectionChangedEventArgs e)
    {
        //verificar se um dispositivo foi selecionado
        if (e.CurrentSelection.FirstOrDefault() is IDevice device)
        {
            bool connect = await DisplayAlert("Connect to Device",
                                              $"Do you want to connect to {device.Name}?",
                                              "Yes",
                                              "No");
            if (connect)
            {
                try
                {
                    //tentar conectar ao dispositivo
                    await _adapter.ConnectToDeviceAsync(device);
                    await DisplayAlert("Connected", $"Successfully connected to {device.Name}.", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Connection Error", $"Failed to connect to {device.Name}: {ex.Message}", "OK");
                }
            }

            //deselecionar o item após o clique
            DevicesCollectionView.SelectedItem = null;
        }
    }
}


//problemas que estou a ter:

//a partir do Android 12 (API 31), o uso de Bluetooth exige permissões específicas que não estavam
//presentes nas versões anteriores ainda mais o Android agora requer que essas permissões sejam
//feitas no tempo de execução e declaradas corretamente no manifesto da aplicação

//dispositivos de áudio (como earbuds, fones ou colunas bluetooth) geralmente não utilizam BLE para
//conexões de áudio. Em vez disso, eles operam no perfil Bluetooth Classic (A2DP, HFP, etc.),
//que não é compatível com o Plugin.BLE.