// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConverterExtensions.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Extension method used to convert to/from Windows/Windows.Media classes.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using OxyPlot;
using System;
using System.Drawing;

namespace test2.Modules
{
    public static class ConverterExtensions
    {
        public static Rectangle ToRect(this OxyRect r, bool aliased)
        {
            if (aliased)
            {
                var x = (int)r.Left;
                var y = (int)r.Top;
                var ri = (int)r.Right;
                var bo = (int)r.Bottom;
                return new Rectangle(x, y, ri - x, bo - y);
            }

            return new Rectangle(
                (int)Math.Round(r.Left), (int)Math.Round(r.Top), (int)Math.Round(r.Width), (int)Math.Round(r.Height));
        }
        public static Brush ToBrush(this OxyColor c)
        {
            return new SolidBrush(c.ToColor());
        }
        public static Color ToColor(this OxyColor c)
        {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }
    }
}
