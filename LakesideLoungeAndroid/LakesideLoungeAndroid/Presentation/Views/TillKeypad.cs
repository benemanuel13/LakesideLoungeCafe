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

using LakesideLoungeAndroid.Presentation.Events;
namespace LakesideLoungeAndroid.Presentation.Views
{
    public class TillKeypad : GridLayout
    {
        public List<Button> buttons = new List<Button>();
        public event EventHandler<TillClickedEventArgs> TillButtonClicked;

        public TillKeypad(Context context) : base(context)
        {
            int count = 1;

            this.ColumnCount = 3;

            for(int row = 1; row < 4; row++)
            {
                for (int col = 1; col < 4; col++)
                {
                    Button newButton = new Button(context);
                    newButton.Text = (count).ToString();
                    newButton.TextSize = 20;
                    newButton.Tag = count.ToString();

                    newButton.Click += NewButton_Click;
                    AddView(newButton);

                    count++;
                }
            }

            Button newerButton = new Button(context);
            newerButton.Text = "0";
            newerButton.TextSize = 20;
            newerButton.Tag = "0";

            newerButton.Click += NewButton_Click;

            AddView(newerButton);

            newerButton = new Button(context);
            newerButton.Text = ".";
            newerButton.TextSize = 20;
            newerButton.Tag = ".";

            newerButton.Click += NewButton_Click;

            AddView(newerButton);

            newerButton = new Button(context);
            newerButton.Text = "Del";
            newerButton.TextSize = 20;
            newerButton.Tag = "Del";

            newerButton.Click += NewButton_Click;

            AddView(newerButton);
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            Button thisButton = (Button)sender;
            string tag = (string)thisButton.Tag;

            TillButtonClicked(sender, new TillClickedEventArgs(tag));
        }
    }
}