using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EducUp.Renders
{
    // This class define the gauge for steps:
    // ------------------------------------------------------------------------------
    // - Total     : Crediti totali da raccimolare
    // - Progress  : Crediti attualmente raccimolati
    // - Available : Crediti ancora da raccimolare ( = Total - Progess )

    public class CircularProgressBar : ContentView
    {
        public static readonly BindableProperty StrokeWidthProperty =
           BindableProperty.Create(nameof(StrokeWidth), typeof(double), typeof(CircularProgressBar), 5.0);

        public double StrokeWidth
        {
            get { return (double)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }

        public static readonly BindableProperty InnerSpaceWidthProperty =
           BindableProperty.Create(nameof(InnerSpaceWidth), typeof(double), typeof(CircularProgressBar), 5.0);

        public double InnerSpaceWidth
        {
            get { return (double)GetValue(InnerSpaceWidthProperty); }
            set { SetValue(InnerSpaceWidthProperty, value); }
        }

        public static readonly BindableProperty FillColorProperty =
            BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(CircularProgressBar), Color.DarkGray);

        public Color FillColor
        {
            get { return (Color)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        public static readonly BindableProperty ProgressStrokeColorProperty =
           BindableProperty.Create(nameof(ProgressStrokeColor), typeof(Color), typeof(CircularProgressBar), Color.Black);

        public Color ProgressStrokeColor
        {
            get { return (Color)GetValue(ProgressStrokeColorProperty); }
            set { SetValue(ProgressStrokeColorProperty, value); }
        }

        public static readonly BindableProperty AvailableStrokeColorProperty =
           BindableProperty.Create(nameof(AvailableStrokeColor), typeof(Color), typeof(CircularProgressBar), Color.Gray);

        public Color AvailableStrokeColor
        {
            get { return (Color)GetValue(AvailableStrokeColorProperty); }
            set { SetValue(AvailableStrokeColorProperty, value); }
        }

        public static readonly BindableProperty TotalProperty =
           BindableProperty.Create(nameof(Total), typeof(double), typeof(CircularProgressBar), 0.25);

        public double Total
        {
            get { return (double)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        public static readonly BindableProperty ProgressProperty =
           BindableProperty.Create(nameof(Progress), typeof(double), typeof(CircularProgressBar), 0.25);

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public double Available => Total - Progress;

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(CircularProgressBar), string.Empty);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CircularProgressBar), Color.Black);

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public static readonly BindableProperty TextSizeProperty =
            BindableProperty.Create(nameof(TextSize), typeof(int), typeof(CircularProgressBar), 16);

        public int TextSize
        {
            get { return (int)GetValue(TextSizeProperty); }
            set { SetValue(TextSizeProperty, value); }
        }

        public static readonly BindableProperty TextMarginProperty =
            BindableProperty.Create(nameof(TextMargin), typeof(int), typeof(CircularProgressBar), 2);

        public int TextMargin
        {
            get { return (int)GetValue(TextMarginProperty); }
            set { SetValue(TextMarginProperty, value); }
        }
    }
}
