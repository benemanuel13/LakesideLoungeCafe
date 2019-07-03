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
using Android.Content.Res;
using Android.Graphics.Drawables;

namespace LakesideLoungeAndroid.Presentation.Layouts
{
    public class SplashScreen : LinearLayout
    {
        ImageView logo;
        TextView currentMessage;

        public SplashScreen(Context context) : base(context)
        {
            logo = new ImageView(context);

            Resources res = context.Resources;
#pragma warning disable CS0618 // Type or member is obsolete
            Drawable shape = res.GetDrawable(Resource.Drawable.Logo);
#pragma warning restore CS0618 // Type or member is obsolete

            logo.Background = shape;
            AddView(logo);

            currentMessage = new TextView(context);
            currentMessage.SetTextColor(Color.Black);
            currentMessage.SetTextSize(ComplexUnitType.Sp, 30);
            currentMessage.Text = "Lakeside Lounge Cafe";
            AddView(currentMessage);
        }

        public string CurrentMessage
        {
            set
            {
                currentMessage.Text = value;
            }
        }
    }
}