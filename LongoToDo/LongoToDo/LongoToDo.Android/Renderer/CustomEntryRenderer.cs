using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using LongoToDo.Controls;
using LongoToDo.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryColor), typeof(CustomEntryRenderer))]
namespace LongoToDo.Droid.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                GradientDrawable gd = new GradientDrawable();
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.ParseColor("#bcbcbc"));
            }
        }
    }
}