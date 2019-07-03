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
using Android.Util;
using Android.Views.InputMethods;

using LakesideLoungeAndroid.Helpers;
using LakesideLoungeAndroid.Presentation.Views;

namespace LakesideLoungeAndroid.Presentation.Layouts
{
    public class NamesLayout : GridLayout
    {
        TextView nameEntryLabel;
        EditText nameEdit;
        TextView displayNameEntryLabel;
        EditText displayNameEdit;

        ViewGroup container;

        bool isEditing = false;

        public NamesLayout(Context context) : base(context)
        {
            this.SetPadding(0, 80, 0, 0);
            this.ColumnCount = 2;
            this.RowCount = 2;

            TextView nameLabel = new TextView(context);
            nameLabel.Text = "Name:";
            nameLabel.SetTextSize(ComplexUnitType.Sp, 25);
            nameLabel.SetTextColor(Color.Black);
            nameLabel.SetPadding(0, 0, 0, 30);
            this.AddView(nameLabel);

            FrameLayout nameFrame = new FrameLayout(context);

            nameEntryLabel = new TextView(context);
            nameEntryLabel.SetTextSize(ComplexUnitType.Sp, 25);
            nameEntryLabel.SetTextColor(Color.Gray);
            nameEntryLabel.Text = "Name here...";
            nameEntryLabel.Click += NameEntryLabel_Click;
            nameFrame.AddView(nameEntryLabel);

            nameEdit = new EditText(context);
            nameEdit.SetTextSize(ComplexUnitType.Sp, 23);
            nameEdit.SetTextColor(Color.Black);
            nameEdit.Visibility = ViewStates.Gone;
            nameEdit.TextChanged += NameEdit_TextChanged;
            nameFrame.AddView(nameEdit);

            this.AddView(nameFrame);

            TextView displayNameLabel = new TextView(context);
            displayNameLabel.Text = "Display Name:";
            displayNameLabel.SetTextSize(ComplexUnitType.Sp, 25);
            displayNameLabel.SetTextColor(Color.Black);
            displayNameLabel.SetPadding(0, 0, 25, 0);
            this.AddView(displayNameLabel);

            FrameLayout displayNameFrame = new FrameLayout(context);

            displayNameEntryLabel = new TextView(context);
            displayNameEntryLabel.SetTextSize(ComplexUnitType.Sp, 25);
            displayNameEntryLabel.SetTextColor(Color.Gray);
            displayNameEntryLabel.Text = "Display Name here...";
            displayNameEntryLabel.Click += DisplayNameEntryLabel_Click;
            displayNameFrame.AddView(displayNameEntryLabel);

            displayNameEdit = new EditText(context);
            displayNameEdit.SetTextSize(ComplexUnitType.Sp, 23);
            displayNameEdit.SetTextColor(Color.Black);
            displayNameEdit.Visibility = ViewStates.Gone;
            displayNameEdit.TextChanged += DisplayNameEdit_TextChanged;
            displayNameFrame.AddView(displayNameEdit);

            this.AddView(displayNameFrame);
        }

        public ViewGroup Container
        {
            set
            {
                container = value;
            }
        }

        private void NameEdit_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            ProcessKeyPress(nameEntryLabel, nameEdit, "Name here ...", e);
        }

        private void DisplayNameEdit_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            ProcessKeyPress(displayNameEntryLabel, displayNameEdit, "Display Name here ...", e);
        }

        private void ProcessKeyPress(TextView textView, EditText editText, string defaultText, Android.Text.TextChangedEventArgs e)
        {
            if (isEditing)
                return;

            char[] lastChars = e.Text.ToArray<char>();

            if (lastChars.Length == 0)
                return;

            if (lastChars[lastChars.Length - 1] == '\n')
            {
                isEditing = true;
                editText.Text = Helper.ConvertStringFromChars(lastChars).Substring(0, lastChars.Length - 1);
                isEditing = false;

                if (editText.Text != "")
                {
                    textView.Text = editText.Text;
                    textView.SetTextColor(Color.Black);
                }
                else
                {
                    textView.Text = defaultText;
                    textView.SetTextColor(Color.Gray);
                }

                FinaliseInput(textView, editText);
            }
        }

        private void NameEntryLabel_Click(object sender, EventArgs e)
        {
            InitialiseInput(nameEntryLabel, nameEdit, -100);
        }

        private void DisplayNameEntryLabel_Click(object sender, EventArgs e)
        {
            InitialiseInput(displayNameEntryLabel, displayNameEdit, -200);
        }

        private void InitialiseInput(TextView textView, EditText editText, float hosChange)
        {
            textView.Visibility = ViewStates.Gone;
            editText.Visibility = ViewStates.Visible;

            editText.SetSelection(editText.Text.Length);

            container.SetY(hosChange);
            container.LayoutParameters.Height = 1000;

            editText.RequestFocus();
            ShowKeyboard(editText);
        }

        private void FinaliseInput(TextView textView, EditText editText)
        {
            editText.Visibility = ViewStates.Gone;
            textView.Visibility = ViewStates.Visible;

            container.SetY(0);

            HideKeyboard(editText);

            if (editText == nameEdit)
                ((VariationDetailsTabView)this.Parent).EditModel.Name = editText.Text;
            else if (editText == displayNameEdit)
                ((VariationDetailsTabView)this.Parent).EditModel.DisplayName = editText.Text;
        }

        private void ShowKeyboard(EditText thisEdit)
        {
            InputMethodManager manager = this.Context.GetSystemService(Android.Content.Context.InputMethodService) as InputMethodManager;
            manager.ShowSoftInput(thisEdit, ShowFlags.Forced);
        }

        private void HideKeyboard(EditText thisEdit)
        {
            InputMethodManager manager = this.Context.GetSystemService(Android.Content.Context.InputMethodService) as InputMethodManager;
            manager.HideSoftInputFromWindow(thisEdit.WindowToken, HideSoftInputFlags.None);
        }

        public string Name
        {
            set
            {
                if (value != "")
                {
                    nameEntryLabel.Text = value;
                    nameEntryLabel.SetTextColor(Color.Black);
                }

                isEditing = true;
                nameEdit.Text = value;
                isEditing = false;
            }
        }

        public string DisplayName
        {
            set
            {
                if (value != "")
                {
                    displayNameEntryLabel.Text = value;
                    displayNameEntryLabel.SetTextColor(Color.Black);
                }

                isEditing = true;
                displayNameEdit.Text = value;
                isEditing = false;
            }
        }
    }
}