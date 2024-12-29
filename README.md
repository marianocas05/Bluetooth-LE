# Conteúdo

## MainPage.xaml
Neste ficheiro, a parte estética da primeira página da aplicação é configurada. A interface contém:
1.	Imagem de Bluetooth: Uma imagem que representa o símbolo de Bluetooth.
2.	Texto: O texto está dividido em dois Labels — um mais destacado e outro de explicação, ambos centrados no ecrã.
3.	Botão de Pesquisa: Um botão que quando pressionado, chama a função TurnOnButtonClicked.

## MainPage.xaml.cs
Este ficheiro contém a lógica por detrás da MainPage. A principal função aqui é o método TurnOnButtonClicked, que é chamada quando o utilizador clica no botão "Search". O que o código tem as seguintes ações:
1.	Verificar o Estado do Bluetooth: Usamos o plugin plugin.BLE para verificar o estado do Bluetooth no dispositivo.
2.	Navegar para a DevicesPage: Se o Bluetooth estiver ligado, o código então navega para a DevicesPage, onde os dispositivos Bluetooth disponíveis são listados.
    - O código usa o CrossBluetoothLE.Current para aceder ao adaptador Bluetooth, e verifica o estado do Bluetooth com ble.State != BluetoothState.On.
    - o	Se o Bluetooth estiver desligado, aparece um alerta a pedir ao utilizador para o ligar.

## DevicesPage.xaml
Aqui a interface da página de dispositivos é configurada. A principal funcionalidade é exibir uma lista de dispositivos Bluetooth próximos que foram descobertos durante o scan. O layout contém:
1.	Texto Informativo: Um Label que contém texto que aparece enquanto o scan está a decorrer. 
2.	Lista de Dispositivos: O CollectionView é utilizado para mostrar a lista de dispositivos encontrados. Cada item contém o nome e o ID do dispositivo. 
3.	Botão de Scan: Ao clicar neste botão, a função ScanButtonClicked é chamada.

## DevicesPage.xaml
Este ficheiro contém a lógica por detrás da página de dispositivos. Ele trata de dois eventos principais:
1.	Iniciar o Scan de Dispositivos (ScanButtonClicked): Ao clicar no botão "Scan", a função começa a fazer scan dos dispositivos Bluetooth disponíveis ao arredor do dispositivo. Cada dispositivo encontrado é adicionado a um ObservableCollection<IDevice>, que é ligada ao CollectionView da página.
  	- O evento DeviceDiscovered do adaptador é utilizado para pegar em cada dispositivo encontrado.
    - O código adiciona o dispositivo à lista, mas só se ele ainda não estiver presente (para evitar elementos duplicados).
2.	Selecionar um Dispositivo para Conectar (in development): Quando o utilizador seleciona um item da lista de dispositivos, o método OnDeviceSelected é chamado. Um DisplayAlert pergunta ao utilizador se ele deseja conectar ao dispositivo selecionado.
    - Se o utilizador confirmar, o código tenta conectar ao dispositivo selecionado ao usar o método ConnectToDeviceAsync do IAdapter.
    - Se a conexão for bem-sucedida, um alerta de confirmação é exibido.
  
## Conclusão
O objetivo do projeto é conectar a dispositivos Bluetooth capazes de receber e processar áudio. No entanto, o uso do Bluetooth Low Energy (Bluetooth LE) não é a solução ideal, pois essa tecnologia é utilizada principalmente para dispositivos que priorizam baixo consumo de energia, como sensores de monitorizar a saúde, dispositivos fitness e outros aparelhos pequenos. O Bluetooth LE não oferece a "bandwidth" necessária para uma transmissão de áudio de qualidade, nem a estabilidade exigida para esse tipo de conexão. Portanto, a escolha mais adequada seria o Bluetooth Classic, que é mais apropriado para a transmissão de áudio em tempo real e garante uma conexão mais robusta e estável, sendo a melhor opção às necessidades do projeto.
