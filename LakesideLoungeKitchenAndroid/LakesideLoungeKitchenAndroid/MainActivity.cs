using Android.App;
using Android.Widget;
using Android.OS;

using Android.Bluetooth;
using Android.Content;

using LakesideLoungeKitchenAndroid.Presentation.Layouts;
using LakesideLoungeKitchenAndroid.Various;
using Android.Runtime;

using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

using Java.Util;

namespace LakesideLoungeKitchenAndroid
{
    [Activity(Label = "Lakeside Lounge Cafe (Kitchen)", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        BluetoothAdapter bluetoothAdapter;
        BluetoothDevice device = null;
        BluetoothSocket socket;
        LakesideReceiver reciever;

        StreamReader reader;

        OverallLayout layout;
        bool layoutFinished = false;

        protected override void OnCreate(Bundle bundle)
        {
            bool foundAdapter = StartBluetooth();

            if(foundAdapter)
                device = FindPairedDevice();

            if (device == null && foundAdapter == true)
                Discover();
            else if (foundAdapter != false)
            {
                CreateSocket();
                StartRecieving();
            }

            layout = new OverallLayout(this);
            SetContentView(layout);

            Task.Run(() => { });

            base.OnCreate(bundle);
        }

        protected async override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);

            layout.ShowSplashScreen();

            await Task.Run(() => { });

            layout.DoStartUp();
        }

        private BluetoothDevice FindPairedDevice()
        {
            ICollection<BluetoothDevice> devices = bluetoothAdapter.BondedDevices;

            foreach (BluetoothDevice device in devices)
            {
                if (device.Name == "LAKESIDELOUNGE")
                    return device;
            }

            return null;
        }

        private bool StartBluetooth()
        {
            bluetoothAdapter = BluetoothAdapter.DefaultAdapter;

            if (bluetoothAdapter == null)
                return false;

            if (!bluetoothAdapter.IsEnabled)
            {
                Intent enableBtIntent = new Intent(BluetoothAdapter.ActionRequestEnable);
                StartActivityForResult(enableBtIntent, 123);
            }

            return true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            UnregisterReceiver(reciever);
        }

        private void Discover()
        {
            reciever = new LakesideReceiver();
            reciever.ConnectionMade += Reciever_ConnectionMade;

            IntentFilter filter = new IntentFilter(BluetoothDevice.ActionFound);
            RegisterReceiver(reciever, filter);

            //BluetoothAdapter.

            filter = new IntentFilter(BluetoothAdapter.ActionDiscoveryFinished);
            RegisterReceiver(reciever, filter);

            filter = new IntentFilter(BluetoothAdapter.ActionDiscoveryStarted);
            RegisterReceiver(reciever, filter);

            if (bluetoothAdapter.IsDiscovering)
                bluetoothAdapter.CancelDiscovery();

            bool val = bluetoothAdapter.StartDiscovery();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case 123:
                    if (resultCode == Result.Ok)
                    {
                        
                    }
                    else if (resultCode == Result.Canceled)
                    { }
                    break;
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        private void Reciever_ConnectionMade(object sender, ConnectionMadeEventArgs e)
        {
            device = e.Device;

            CreateSocket();
            StartRecieving();
        }

        private void CreateSocket()
        {
            UUID uuid = UUID.FromString("6c460207-cba2-43ac-acb9-daf481d10f95");

            socket = device.CreateRfcommSocketToServiceRecord(uuid);

            while (true)
            {
                try
                {
                    socket.Connect();
                    break;
                }
                catch
                {
                    Thread.Sleep(2000);
                }
            }

            SendRecord("SEND_ORDER_QUEUE");
            //Set icon to "Connected"
        }

        public bool LayoutFinished
        {
            set
            {
                layoutFinished = value;
            }
        }

        private async void StartRecieving()
        {
            while (!layoutFinished)
            {
                await Task.Run(() => { });
            }

            Stream inputStream = socket.InputStream;
            reader = new StreamReader(inputStream);

            string record = "";

            while (true)
            {
                try
                {
                    await Task.Run(() => record = reader.ReadLine());
                }
                catch
                {
                    //point of failure (eg. Bluetooth Connection lost)

                    if (socket.IsConnected)
                    {
                        reader.Close();
                        socket.Close();
                    }

                    //Set icon to "Disconnected"

                    CreateSocket();
                    
                    inputStream = socket.InputStream;
                    reader = new StreamReader(inputStream);

                    continue;
                }

                if (record == "END")
                {
                    reader.Close();
                    socket.Close();
                    break;
                }

                if(record != "TEST")
                    layout.RecieveRecord(record);
            }
        }

        public void SendRecord(string record)
        {
            while (true)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(socket.OutputStream);
                    writer.WriteLine(record);
                    writer.Close();

                    return;
                }
                catch
                {
                    //point of failure.
                    Thread.Sleep(3000);
                }
            }
        }

        public override void OnBackPressed()
        {
            //base.OnBackPressed();
        }
    }
}

