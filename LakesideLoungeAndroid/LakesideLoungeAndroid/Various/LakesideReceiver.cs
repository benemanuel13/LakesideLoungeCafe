using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Bluetooth;

namespace LakesideLoungeAndroid.Various
{
    public class LakesideReceiver : BroadcastReceiver
    {
        //public event EventHandler<ConnectionMadeEventArgs> ConnectionMade;

        public override void OnReceive(Context context, Intent intent)
        {
            String action = intent.Action;

            if (action == BluetoothDevice.ActionFound)
            {
                //BluetoothDevice device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
                //if (device.Name == "LakesideLounge")
                //    ConnectionMade(this, new Various.ConnectionMadeEventArgs(device));
            }
        }
    }
}