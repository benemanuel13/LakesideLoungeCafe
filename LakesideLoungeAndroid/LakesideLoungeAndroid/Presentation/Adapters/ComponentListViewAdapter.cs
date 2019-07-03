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

using Android.Graphics;
using Android.Content.Res;
using Android.Graphics.Drawables;

using LakesideLoungeAndroid.Application;
using LakesideLoungeAndroid.Presentation.Fragments;
using LakesideLoungeAndroid.Presentation.Events;
using LakesideLoungeAndroid.Presentation.Views;

namespace LakesideLoungeAndroid.Presentation.Adapters
{
    public class ComponentListViewAdapter : BaseAdapter<ComponentModel>
    {
        public event EventHandler<ComponentCheckedEventArgs> ComponentChecked;
        public event EventHandler<PortionClickedEventArgs> PortionClicked;
        public event EventHandler<ArrowClickedEventArgs> ArrowClicked;

        List<CheckBoxWithId> checkBoxes = new List<CheckBoxWithId>();
        List<TextViewWithId> portions = new List<TextViewWithId>();
        List<ImageViewWithId> arrows = new List<ImageViewWithId>();

        ComponentsModel model;
        bool checking = false;
        bool subChecking = false;

        public ComponentListViewAdapter(int id, Context context, ComponentListMode mode, bool ignoreDefaults = true)
        {
            model = new ComponentsModel(id, mode);

            int position = 0;

            foreach (ComponentModel cModel in model.ComponentModels)
            {
                CheckBoxWithId thisItem = new CheckBoxWithId(context);
                thisItem.Group = cModel.Group;
                thisItem.Position = position++;
                thisItem.ComponentId = cModel.Id;

                TextViewWithId portions = new TextViewWithId(context);
                portions.ComponentId = cModel.Id;
                portions.Text = " " + cModel.Portions.ToString();
                portions.SetTextColor(Color.White);
                portions.SetBackgroundColor(Color.Black);
                portions.SetTextSize(Android.Util.ComplexUnitType.Sp, 25);
                portions.Click += Portions_Click;

                ImageViewWithId arrow = new ImageViewWithId(context);
                arrow.SetImageResource(Resource.Drawable.ViewComponents);
                arrow.SetPadding(5, 8, 0, 0);
                arrow.ComponentId = cModel.Id;
                arrow.Visibility = ViewStates.Invisible;
                arrow.Click += Arrow_Click;

                if (!ignoreDefaults && cModel.IsDefault)
                    thisItem.Checked = true;

                checkBoxes.Add(thisItem);
                this.portions.Add(portions);
                arrows.Add(arrow);
            }
        }

        public ComponentsModel Model
        {
            get
            {
                return model;
            }
        }

        private void Arrow_Click(object sender, EventArgs e)
        {
            ImageViewWithId arrow = (ImageViewWithId)sender;

            ArrowClicked(this, new ArrowClickedEventArgs(arrow));
        }

        private void Portions_Click(object sender, EventArgs e)
        {
            TextViewWithId portions = (TextViewWithId)sender;

            PortionClicked(this, new PortionClickedEventArgs(portions));
        }

        public override ComponentModel this[int position]
        {
            get
            {
                return model.ComponentModels[position];
            }
        }

        public override int Count
        {
            get
            {
                return model.ComponentModels.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return model.ComponentModels[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Activity context = (Activity)parent.Context;

            ComponentModel varModel = model.ComponentModels[position];

            View view = new LinearLayout(context);
            view.LayoutDirection = LayoutDirection.Ltr;
            
            CheckBoxWithId thisItem = checkBoxes[position];

            if (thisItem.Parent != null)
            {
                thisItem.CheckedChange -= ThisItem_CheckedChange;
                ((LinearLayout)thisItem.Parent).RemoveAllViews();
            }

            thisItem.Position = position;

            thisItem.Text = varModel.Name;
            thisItem.SetTextColor(Color.Black);
            thisItem.SetTextSize(Android.Util.ComplexUnitType.Sp, 25);
            thisItem.CheckedChange += ThisItem_CheckedChange;

            ((LinearLayout)view).AddView(thisItem);
            thisItem.LayoutParameters.Width = 615;

            TextViewWithId portions = this.portions[position];
            ImageViewWithId arrow = arrows[position];
            
            if (thisItem.Checked)
            {
                if(varModel.HasComponents)
                    arrow.Visibility = ViewStates.Visible;

                if (varModel.Portions > 0)
                    portions.Visibility = ViewStates.Visible;
                else
                    portions.Visibility = ViewStates.Invisible;
            }
            else
            {
                arrow.Visibility = ViewStates.Invisible;
                portions.Visibility = ViewStates.Invisible;
            }

            ((LinearLayout)view).AddView(portions);
            ((LinearLayout)view).AddView(arrow);
            portions.LayoutParameters.Width = 50;

            if (varModel.Group > 0)
                view.SetBackgroundColor(Color.Beige);

            view.SetPadding(10, 0, 0, 0);

            return view;
        }

        private void ThisItem_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            CheckBoxWithId thisBox = (CheckBoxWithId)sender;
            
            if (!checking || subChecking)
            {
                if(thisBox.Group > 0 && !thisBox.Checked && !subChecking)
                {
                    checking = true;
                    thisBox.Checked = true;
                    checking = false;
                    return;
                }
                else if(thisBox.Group > 0  && !subChecking)
                    foreach(CheckBoxWithId box in checkBoxes)
                        if (box.Group == thisBox.Group && box.Position != thisBox.Position)
                        {
                            subChecking = true;
                            box.Checked = false;
                            subChecking = false;
                        }

                TextViewWithId portions = this.portions[thisBox.Position];
                ImageViewWithId arrow = arrows[thisBox.Position];
                ComponentModel varModel = model.ComponentModels[thisBox.Position];

                if (thisBox.Checked)
                {
                    if(varModel.HasComponents)
                        arrow.Visibility = ViewStates.Visible;

                    if (varModel.Portions > 0)
                        portions.Visibility = ViewStates.Visible;
                    else
                        portions.Visibility = ViewStates.Invisible;
                }
                else
                {
                    arrow.Visibility = ViewStates.Invisible;
                    portions.Visibility = ViewStates.Invisible;
                }

                ComponentChecked(this, new ComponentCheckedEventArgs(model.ComponentModels[thisBox.Position]));
            }
        }

        public List<CheckBoxWithId> CheckBoxes
        {
            get
            {
                return checkBoxes;
            }
        }

        public List<TextViewWithId> Portions
        {
            get
            {
                return portions;
            }
        }
    }
}