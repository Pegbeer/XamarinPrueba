using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Java.Lang;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using FormsTintImageEffect = PruebaXamarin.Effects.TintImageEffect;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using PruebaXamarin.Droid.Renderers;

[assembly: ResolutionGroupName(PruebaXamarin.Effects.TintImageEffect.GroupName)]
[assembly: ExportEffect(typeof(TintImageEffect), PruebaXamarin.Effects.TintImageEffect.Name)]
namespace PruebaXamarin.Droid.Renderers
{
    public class TintImageEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (FormsTintImageEffect)Element.Effects.FirstOrDefault(e => e is FormsTintImageEffect);

                if (effect == null || !(Control is ImageView image))
                    return;

                var filter = new PorterDuffColorFilter(effect.TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
                image.SetColorFilter(filter);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"An error occurred when setting the {typeof(TintImageEffect)} effect: {ex.Message}\n{ex.StackTrace}");
            }
        }

        protected override void OnDetached(){}

    }
}