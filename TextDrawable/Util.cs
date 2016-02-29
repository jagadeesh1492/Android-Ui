using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TextDrawable
{
    /// <summary>
    /// Class ColorGenerator.
    /// </summary>
    public class ColorGenerator
    {
        /// <summary>
        /// The default
        /// </summary>
        public static List<uint> Default = new List<uint>()
        {
            0xfff16364,
            0xfff58559,
            0xfff9a43e,
            0xffe4c62e,
            0xff67bf74,
            0xff59a2be,
            0xff2093cd,
            0xffad62a7,
            0xff805781
        };

        /// <summary>
        /// The material
        /// </summary>
        public static List<uint> Material = new List<uint>()
        {
            0xffe57373,
            0xfff06292,
            0xffba68c8,
            0xff9575cd,
            0xff7986cb,
            0xff64b5f6,
            0xff4fc3f7,
            0xff4dd0e1,
            0xff4db6ac,
            0xff81c784,
            0xffaed581,
            0xffff8a65,
            0xffd4e157,
            0xffffd54f,
            0xffffb74d,
            0xffa1887f,
            0xff90a4ae
        };


        /// <summary>
        /// The m colors
        /// </summary>
        private readonly List<uint> _mColors;

        /// <summary>
        /// The m random
        /// </summary>
        private readonly Random _mRandom;

        
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorGenerator"/> class.
        /// </summary>
        /// <param name="colorList">The color list.</param>
        internal ColorGenerator(List<uint> colorList)
        {
            _mColors = colorList;
            _mRandom = new Random(DateTime.UtcNow.Millisecond);
        }

        /// <summary>
        /// Gets the random color.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public Color GetRandomColor()
        {
            return new Color(Convert.ToInt32(_mColors[_mRandom.Next(_mColors.Count)]));
        }

        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Int32.</returns>
        public Color GetColor(Object key)
        {
            // var color=Color.ParseColor()
            return new Color(Convert.ToInt32(_mColors[(Math.Abs(key.GetHashCode())%_mColors.Count)]));
        }
    }
}
