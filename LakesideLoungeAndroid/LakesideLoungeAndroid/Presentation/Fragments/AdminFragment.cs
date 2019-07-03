using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Android.Graphics;

using LakesideLoungeAndroid.Presentation.Views;

namespace LakesideLoungeAndroid.Presentation.Fragments
{
    public class AdminFragment : Fragment
    {
        ViewGroup container;
        VariationDetailsTabView newTabView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.container = container;

            TextView placeholder = new TextView(this.Context);
            placeholder.SetTextSize(ComplexUnitType.Sp, 25);
            placeholder.SetTextColor(Color.Gray);
            placeholder.SetPadding(50, 200, 0, 0);
            placeholder.Text = "Please select a variation to edit.";

            container.AddView(placeholder);

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public void SelectVariation(int id)
        {
            container.RemoveAllViews();

            LinearLayout mainLayout = new LinearLayout(this.Context);
            mainLayout.Orientation = Orientation.Vertical;

            newTabView = new VariationDetailsTabView(id, this.Context);
            newTabView.Container = container;
            mainLayout.AddView(newTabView);

            LinearLayout buttons = new LinearLayout(this.Context);

            Button saveButton = new Button(this.Context);
            saveButton.Text = "Save";
            saveButton.SetPadding(0, 0, 10, 0);
            saveButton.Click += SaveButton_Click;
            buttons.AddView(saveButton);

            Button cancelButton = new Button(this.Context);
            cancelButton.Text = "Cancel";
            cancelButton.Click += CancelButton_Click;
            buttons.AddView(cancelButton);

            newTabView.ButtonsLayout = buttons;
            newTabView.PlaceButtons(300, 200);

            mainLayout.AddView(buttons);

            container.AddView(mainLayout);
        }
        
        private void SaveButton_Click(object sender, EventArgs e)
        {
            newTabView.Save();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            newTabView.Reload();
        }
    }
}