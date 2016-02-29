using System.Collections.Generic;
using Android.Graphics.Drawables;
using TextDrawable;

namespace TextDrawableSamples.Util
{
    /// <summary>
    /// Class DataItem.
    /// </summary>
    public class DataItem
    {
        /// <summary>
        /// The _label
        /// </summary>
        private readonly string _label;

        /// <summary>
        /// The _drawable
        /// </summary>
        private readonly Drawable _drawable;

        /// <summary>
        /// The _navigation information
        /// </summary>
        private readonly int _navigationInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataItem"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="drawable">The drawable.</param>
        /// <param name="navigationInfo">The navigation information.</param>
        public DataItem(string label, Drawable drawable, int navigationInfo)
        {
            this._label = label;
            this._drawable = drawable;
            this._navigationInfo = navigationInfo;
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetLabel()
        {
            return _label;
        }

        /// <summary>
        /// Gets the drawable.
        /// </summary>
        /// <returns>Drawable.</returns>
        public Drawable GetDrawable()
        {
            return _drawable;
        }

        /// <summary>
        /// Gets the navigation information.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetNavigationInfo()
        {
            return _navigationInfo;
        }
        
    }
}