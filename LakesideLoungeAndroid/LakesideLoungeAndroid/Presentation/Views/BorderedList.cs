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

namespace LakesideLoungeAndroid.Presentation.Views
{
    public class BorderedList : ListView
    {
        Paint borderPaint;

        public BorderedList(Context context) : base(context)
        {
            SetPadding(2, 2, 2, 2);

            borderPaint = new Paint();
            borderPaint.AntiAlias = true;
            borderPaint.SetStyle(Paint.Style.Stroke);
            borderPaint.Color = Color.Black;
            borderPaint.StrokeWidth = 1;
        }

        public override void OnDrawForeground(Canvas canvas)
        {
            base.OnDrawForeground(canvas);

            canvas.DrawRect(2, 2, Width - 2, Height - 2, borderPaint);
        }
    }
}