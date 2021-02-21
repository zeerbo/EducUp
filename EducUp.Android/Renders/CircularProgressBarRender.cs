using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using EducUp.Droid.Renders;
using EducUp.Renders;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CircularProgressBar), typeof(CircularProgressBarRender))]
namespace EducUp.Droid.Renders
{
    public class CircularProgressBarRender : ViewRenderer<CircularProgressBar, Android.Views.View>
    {
        public CircularProgressBarRender(Context context) : base(context)
        {
            SetWillNotDraw(false);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CircularProgressBar> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                var radialGaugeView = new Android.Views.View(Context);
                SetNativeControl(radialGaugeView);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CircularProgressBar.TotalProperty.PropertyName ||
                e.PropertyName == CircularProgressBar.ProgressProperty.PropertyName)
            {
                Invalidate();
            }
        }

        protected override void OnDraw(Canvas canvas)
        {
            var density = DeviceDisplay.MainDisplayInfo.Density;
            float strokeWidth = Convert.ToSingle(Element.StrokeWidth * density);
            float strokeMiter = 10.0f;

            var rect = new Android.Graphics.Rect();
            this.GetDrawingRect(rect);

            float progressAngle = 360 * Convert.ToSingle(Element.Progress / Element.Total);

            float radius = Math.Min(rect.Height(), rect.Width()) / 2 - strokeWidth;

            RectF circleRect = new RectF(
                rect.ExactCenterX() - radius,
                rect.ExactCenterY() - radius,
                rect.ExactCenterX() + radius,
                rect.ExactCenterY() + radius
            );

            TextPaint paint = new TextPaint();
            paint.Color = Element.TextColor.ToAndroid();
            paint.TextSize = Element.TextSize;
            paint.GetTextBounds(Element.Text.ToString(), 0, Element.Text.ToString().Length, rect);
            if (((this.Width / 2) - (Element.TextMargin * 4)) < rect.Width())
            {
                float ratio = (float)((this.Width / 2) - Element.TextMargin * 4) / (float)rect.Width();
                paint.TextSize = paint.TextSize * ratio;
                paint.GetTextBounds(Element.Text.ToString(), 0, Element.Text.ToString().Length, rect);
            }
            int x = this.Width / 2 - rect.CenterX();
            int y = this.Height / 2 - rect.CenterY();

            Paint progressPaint = new Paint(PaintFlags.AntiAlias)
            {
                StrokeWidth = strokeWidth,
                StrokeMiter = strokeMiter,
                Color = Element.ProgressStrokeColor.ToAndroid()
            };
            progressPaint.SetStyle(Paint.Style.Stroke);

            Paint availablePaint = new Paint(PaintFlags.AntiAlias)
            {
                StrokeWidth = strokeWidth,
                StrokeMiter = strokeMiter,
                Color = Element.AvailableStrokeColor.ToAndroid()
            };
            availablePaint.SetStyle(Paint.Style.Stroke);

            Paint paintCircle = new Paint(PaintFlags.AntiAlias)
            {
                Color = Element.FillColor.ToAndroid()
            };

            canvas.DrawArc(circleRect, -90, progressAngle, false, progressPaint);
            canvas.DrawArc(circleRect, -90 + progressAngle, 360 - progressAngle, false, availablePaint);
            canvas.DrawText(Element.Text.ToString(), x, y, paint);
        }
    }
}