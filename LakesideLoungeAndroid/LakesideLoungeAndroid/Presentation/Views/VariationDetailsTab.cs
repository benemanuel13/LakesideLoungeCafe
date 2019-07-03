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

using Android.Util;
using Android.Graphics;

using LakesideLoungeAndroid.Presentation.Fragments;
using LakesideLoungeAndroid.Presentation.Layouts;
using LakesideLoungeAndroid.Application;

namespace LakesideLoungeAndroid.Presentation.Views
{
    public class VariationDetailsTabView : LinearLayout
    {
        TextView namesTab;
        TextView detailsTab;

        ViewGroup content;
        ViewGroup container;

        VariationEditModel model;
        LinearLayout buttonsLayout;

        public VariationDetailsTabView(int variationId, Context context) : base(context)
        {
            model = new VariationEditModel(variationId);

            Orientation = Orientation.Vertical;
            LinearLayout tabs = new LinearLayout(context);

            namesTab = new TextView(context);
            namesTab.Text = "Names";
            namesTab.SetPadding(0, 0, 25, 15);
            namesTab.SetTextSize(ComplexUnitType.Sp, 25);
            namesTab.SetTextColor(Color.Blue);
            namesTab.Click += NamesTab_Click;
            tabs.AddView(namesTab);

            detailsTab = new TextView(context);
            detailsTab.Text = "Details";
            detailsTab.SetTextSize(ComplexUnitType.Sp, 25);
            detailsTab.SetTextColor(Color.Black);
            detailsTab.Click += DetailsTab_Click;
            tabs.AddView(detailsTab);

            this.AddView(tabs);

            content = new NamesLayout(context);
            PopulateNamesTab();

            this.AddView(content);
        }

        public ViewGroup Container
        {
            set
            {
                container = value;
                if (content is NamesLayout)
                    ((NamesLayout)content).Container = container;
                else
                    ((DetailsLayout)content).Container = container;
            }
        }

        private void NamesTab_Click(object sender, EventArgs e)
        {
            if (content is NamesLayout)
                return;

            namesTab.SetTextColor(Color.Blue);
            detailsTab.SetTextColor(Color.Black);

            this.RemoveView(content);

            content = new NamesLayout(this.Context);
            PopulateNamesTab();

            this.AddView(content);

            ((NamesLayout)content).Container = container;

            PlaceButtons();
        }

        private void DetailsTab_Click(object sender, EventArgs e)
        {
            if (content is DetailsLayout)
                return;

            namesTab.SetTextColor(Color.Black);
            detailsTab.SetTextColor(Color.Blue);

            this.RemoveView(content);

            content = new DetailsLayout(model.Model, this.Context);
            PopulateDetailsTab();

            this.AddView(content);

            ((DetailsLayout)content).Container = container;
            PlaceButtons();
        }

        private void PopulateNamesTab()
        {
            NamesLayout thisLayout = (NamesLayout)content;
            thisLayout.Name = model.Name;
            thisLayout.DisplayName = model.DisplayName;
        }

        private void PopulateDetailsTab()
        {
            DetailsLayout thisLayout = (DetailsLayout)content;
            //foreach (PriceModel model in this.model.Prices)
            //    thisLayout.AddPriceModel(model);

            //thisLayout.DisplayPrices();
        }

        public void Save()
        {
            VariationEditModel.SaveVariationEditModel(model);
        }

        public void Reload()
        {
            model = new VariationEditModel(model.Id);

            if (content is NamesLayout)
                PopulateNamesTab();
            else
                PopulateDetailsTab();
        }

        public ViewGroup Content
        {
            get
            {
                return content;
            }
        }

        public VariationEditModel EditModel
        {
            get
            {
                return model;
            }
        }

        public LinearLayout ButtonsLayout
        {
            set
            {
                buttonsLayout = value;
            }
        }

        public void PlaceButtons()
        {
            if (content is NamesLayout)
                PlaceButtons(300, 200);
            else
                PlaceButtons(0, 0);
        }

        public void PlaceButtons(int left, int top)
        {
            buttonsLayout.SetPadding(left, top, 0, 0);
        }
    }
}