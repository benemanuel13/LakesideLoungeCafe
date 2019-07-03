using Android.App;
using Android.Widget;
using Android.OS;

using Android.Graphics.Drawables;
using Android.Views;
using Android.Content;
using Android.Runtime;
using Android.Bluetooth;

using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

using Java.Util;

using LakesideLoungeAndroid.Application;
using LakesideLoungeAndroid.Infrastructure;
using LakesideLoungeAndroid.Presentation.Layouts;
using LakesideLoungeAndroid.Various;

namespace LakesideLoungeAndroid.Presentation.Activities
{
    [Activity(Label = "Lakeside Lounge Cafe", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        BluetoothAdapter bluetoothAdapter;
        BluetoothServerSocket server;
        BluetoothSocket socket;

        OverallLayout layout;

        bool permissionGiven = false;
        private bool sendWaiting = false;

        MainActivityService svc = new MainActivityService();

        List<OrderModel> orderQueue;

        protected override void OnCreate(Bundle bundle)
        {
            if (StartBluetooth())
                MakeDiscoverable();

            BlueTooth.Context = this;

            layout = new OverallLayout(this);
            SetContentView(layout);

            base.OnCreate(bundle);
        }

        public List<OrderModel> OrderQueue
        {
            set
            {
                orderQueue = value;

                if (sendWaiting)
                {
                    SendOrderQueue();
                    sendWaiting = false;
                }
            }
        }

        private void AwaitPermission()
        {
            while (permissionGiven == false) { }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected async override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);

            Network.Context = this;

            layout.ShowSplashScreen();

            await Task.Run(() => AwaitPermission());

            layout.DoStartUp();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            IMenuItem item1 = menu.Add(0, 0, 0, "Current Order");
            item1.SetIcon(Resource.Drawable.Icon);

            IMenuItem item2 = menu.Add(0, 1, 0, "Live Orders");
            IMenuItem item3 = menu.Add(0, 2, 0, "Admin");
            IMenuItem item4 = menu.Add(0, 3, 0, "Reports");

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 0:
                    {
                        layout.SetLayout(Layout.CurrentOrder);
                        return true;
                    }
                case 1:
                    {
                        layout.SetLayout(Layout.LiveOrders);
                        return true;
                    }
                case 2:
                    {
                        layout.SetLayout(Layout.Admin);
                        return true;
                    }
                case 3:
                    {
                        layout.SetLayout(Layout.Reports);
                        return true;
                    }
                default:
                    {
                        return base.OnOptionsItemSelected(item);
                    }
            }
        }

        private bool StartBluetooth()
        {
            LakesideReceiver reciever = new LakesideReceiver();
            //reciever.ConnectionMade += Reciever_ConnectionMade;

            IntentFilter filter = new IntentFilter(BluetoothAdapter.ActionScanModeChanged);
            RegisterReceiver(reciever, filter);

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

        private async void MakeDiscoverable()
        {
            Intent discoverableIntent = new Intent(BluetoothAdapter.ActionRequestDiscoverable);
            discoverableIntent.PutExtra(BluetoothAdapter.ExtraDiscoverableDuration, 0);
            await Task.Run(() => StartActivityForResult(discoverableIntent, 345));
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            switch (requestCode)
            {
                case 123:
                    break;
                default:
                    permissionGiven = true;
                    StartAccepting();
                    break;
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        private async void StartAccepting()
        {
            UUID uuid = UUID.FromString("6c460207-cba2-43ac-acb9-daf481d10f95");

            if(server != null) server.Close();
            server = bluetoothAdapter.ListenUsingRfcommWithServiceRecord("LakesideLounge", uuid);

            if (socket != null && socket.IsConnected) socket.Close();

            while (true)
            {
                await Task.Run(() => socket = server.Accept());

                StartRecieving();
            }
        }

        private async void StartRecieving()
        {
            Stream inputStream = socket.InputStream;
            StreamReader reader = new StreamReader(inputStream);

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

                    return;
                }

                if (record == "END")
                {
                    reader.Close();
                    socket.Close();
                    break;
                }

                if (record != "TEST" && record != "SEND_ORDER_QUEUE")
                    layout.RecieveRecord(record);

                if (record == "SEND_ORDER_QUEUE")
                    SendOrderQueue();
            }
        }

        public void AddOrderToQueue(OrderModel model)
        {
            OrderModel newModel = new OrderModel(model.Id);
            orderQueue.Add(newModel);

            svc.AddOrderToQueue(newModel);
        }

        private void SendOrderQueue()
        {
            if (orderQueue == null)
            {
                sendWaiting = true;
                return;
            }

            foreach (OrderModel model in orderQueue)
            {
                if (!model.Void)
                    svc.SendOrder(model);
                else
                    svc.SendVoidOrder(model);
            }

            orderQueue.Clear();
            svc.ClearOrderQueue();
        }

        public bool SendRecord(string record)
        {
            if (socket != null)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(socket.OutputStream);
                    writer.WriteLine(record);
                    writer.Close();
                }
                catch
                {
                    return true;
                }
            }
            else
                return true;

            return false;
        }

        public override void OnBackPressed()
        {
            //base.OnBackPressed();
        }
    }

    public enum Layout
    {
        CurrentOrder,
        LiveOrders,
        Admin,
        Reports
    }
}