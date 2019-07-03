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

using LakesideLoungeAndroid.Presentation.Fragments;
using LakesideLoungeAndroid.Presentation.Views;

namespace LakesideLoungeAndroid.Presentation.Layouts
{
    public class TillLayout : LinearLayout
    {
        private OrderFragment parent;
        TillKeypad keypad;
        Paint borderPaint;
        TextView change;
        EditText entry;
        decimal totalDue;

        public TillLayout(Context context, OrderFragment parent) : base(context)
        {
            borderPaint = new Paint();
            borderPaint.AntiAlias = true;
            borderPaint.SetStyle(Paint.Style.Stroke);
            borderPaint.Color = Color.Black;
            borderPaint.StrokeWidth = 1;

            Orientation = Orientation.Vertical;
            SetPadding(10, 10, 10, 10);
            SetBackgroundColor(Color.White);
            SetMinimumHeight(800);

            this.parent = parent;

            LinearLayout entryLayout = new LinearLayout(context);
            entryLayout.Orientation = Orientation.Horizontal;

            TextView entryTitle = new TextView(context);
            entryTitle.Text = "Tendered: ";
            entryTitle.TextSize = 20;
            entryTitle.SetTextColor(Color.Black);
            entryTitle.SetPadding(10, 30, 0, 0);

            entryLayout.AddView(entryTitle);

            entry = new EditText(context);
            entry.TextSize = 20;
            entry.TextAlignment = TextAlignment.TextEnd;
            entry.Text = "0";
            entry.SetTextColor(Color.Black);
            entry.SetSelection(entry.Text.Length);

            entryLayout.AddView(entry);

            AddView(entryLayout);

            entry.LayoutParameters.Width = 570;

            LinearLayout calcView = new LinearLayout(context);

            TextView calcTextView = new TextView(context);
            calcTextView.Text = "Change (£) : ";
            calcTextView.TextSize = 20;
            calcTextView.SetTextColor(Color.Black);
            calcTextView.SetPadding(10, 0, 0, 10);

            calcView.AddView(calcTextView);

            change = new TextView(context);
            change.Text = "Not calculated";
            change.TextSize = 20;
            change.SetTextColor(Color.Blue);

            calcView.AddView(change);

            AddView(calcView);

            keypad = new TillKeypad(context);
            keypad.SetPadding(15, 0, 0, 0);
            keypad.AlignmentMode = GridAlign.Bounds;
            keypad.TillButtonClicked += Keypad_TillButtonClicked;

            AddView(keypad);

            keypad.LayoutParameters.Width = 600;
            keypad.LayoutParameters.Height = 400;

            LinearLayout bottomKeys = new LinearLayout(context);
            bottomKeys.Orientation = Orientation.Horizontal;
            bottomKeys.SetBackgroundColor(Color.White);
            bottomKeys.SetPadding(5, 0, 0, 0);

            Button calcButton = new Button(context);
            calcButton.Text = "Calc";
            calcButton.TextSize = 20;
            calcButton.Click += CalcButton_Click;

            bottomKeys.AddView(calcButton);

            Button cancelButton = new Button(context);
            cancelButton.Text = "Cancel";
            cancelButton.TextSize = 20;
            cancelButton.Click += CancelButton_Click;

            bottomKeys.AddView(cancelButton);

            Button payButton = new Button(context);
            payButton.Text = "Pay";
            payButton.TextSize = 20;
            payButton.Click += PayButton_Click;

            bottomKeys.AddView(payButton);

            AddView(bottomKeys);

            bottomKeys.LayoutParameters.Width = 600;
            bottomKeys.LayoutParameters.Height = 130;
        }

        public decimal TotalDue
        {
            set
            {
                totalDue = value;
            }
        }

        private void Keypad_TillButtonClicked(object sender, Events.TillClickedEventArgs e)
        {
            switch(e.ButtonPressed)
            {
                case ".":
                    {
                        ProcessDot();
                        break;
                    }
                case "Del":
                    {
                        ProcessDel();
                        break;
                    }
                default:
                    {
                        int number = int.Parse(e.ButtonPressed);
                        ProcessNumber(number);
                        break;
                    }
            }
        }

        private void ProcessNumber(int number)
        {
            if (entry.Text == "0" && number == 0)
                return;
            else if (entry.Text == "0")
            {
                entry.Text = number.ToString();
                entry.SetSelection(1);
                return;
            }

            if(entry.Text.Contains("."))
            {
                int numberAfter = entry.Text.Length - entry.Text.IndexOf(".") - 1;

                if (numberAfter == 2)
                    return;
            }

            entry.Text = entry.Text + number;
            entry.SetSelection(entry.Text.Length);
        }

        private void ProcessDot()
        {
            if (entry.Text.Contains("."))
                return;

            entry.Text = entry.Text + ".";
            entry.SetSelection(entry.Text.Length);
        }

        private void ProcessDel()
        {
            if (entry.Text.Length == 0)
                return;

            entry.Text = entry.Text.Substring(0, entry.Text.Length - 1);

            if (entry.Text == "")
                entry.Text = "0";

            entry.SetSelection(entry.Text.Length);
        }

        private void PayButton_Click(object sender, EventArgs e)
        {
            PayOrder();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            parent.CancelTill();
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            decimal enteredAmount = decimal.Parse(entry.Text);
            decimal amountLeft = enteredAmount - totalDue;

            if (amountLeft < 0)
                change.Text = "Invalid";
            else
                change.Text = amountLeft.ToString();
        }

        private void PayOrder()
        {
            parent.PayOrder();
        }

        public override void OnDrawForeground(Canvas canvas)
        {
            base.OnDrawForeground(canvas);

            canvas.DrawRect(2, 2, Width - 2, Height - 2, borderPaint);
        }
    }
}