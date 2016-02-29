using System.Collections.Generic;
using Android.Content;
using Android.Graphics.Drawables;

namespace TextDrawableSamples.Util
{
    /// <summary>
    /// Class DataSource.
    /// </summary>
    public class DataSource
    {
        /// <summary>
        /// The no navigation
        /// </summary>
        public static int NoNavigation = -1;

        /// <summary>
        /// The _m data source
        /// </summary>
        private readonly List<DataItem> _mDataSource;
        /// <summary>
        /// The _m provider
        /// </summary>
        private readonly DrawableProvider _mProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSource"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DataSource(Context context)
        {
            _mProvider = new DrawableProvider(context);
            _mDataSource = new List<DataItem>
            {
                ItemFromType(DrwableProviderEnum.SampleRect),
                ItemFromType(DrwableProviderEnum.SampleRoundRect),
                ItemFromType(DrwableProviderEnum.SampleRound),
                ItemFromType(DrwableProviderEnum.SampleRectBorder),
                ItemFromType(DrwableProviderEnum.SampleRoundRectBorder),
                ItemFromType(DrwableProviderEnum.SampleRoundBorder),
                ItemFromType(DrwableProviderEnum.SampleMultipleLetters),
                ItemFromType(DrwableProviderEnum.SampleFont),
                ItemFromType(DrwableProviderEnum.SampleSize),
                ItemFromType(DrwableProviderEnum.SampleAnimation),
                ItemFromType(DrwableProviderEnum.SampleMisc)
            };
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetCount()
        {
            return _mDataSource.Count;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>DataItem.</returns>
        public DataItem GetItem(int position)
        {
            return _mDataSource[position];
        }

        /// <summary>
        /// Items from type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>DataItem.</returns>
        private DataItem ItemFromType(DrwableProviderEnum type)
        {
            string label = null;
            Drawable drawable = null;
            int navigation = (int) type;
            switch (type)
            {
                case DrwableProviderEnum.SampleRect:
                    label = "Rectangle with Text";
                    drawable = _mProvider.GetRect("A");
                    break;
                case DrwableProviderEnum.SampleRoundRect:
                    label = "Round Corner with Text";
                    drawable = _mProvider.GetRoundRect("B");
                    break;
                case DrwableProviderEnum.SampleRound:
                    label = "Round with Text";
                    drawable = _mProvider.GetRound("C");
                    break;
                case DrwableProviderEnum.SampleRectBorder:
                    label = "Rectangle with Border";
                    drawable = _mProvider.GetRectWithBorder("D");
                    break;
                case DrwableProviderEnum.SampleRoundRectBorder:
                    label = "Round Corner with Border";
                    drawable = _mProvider.GetRoundRectWithBorder("E");
                    break;
                case DrwableProviderEnum.SampleRoundBorder:
                    label = "Round with Border";
                    drawable = _mProvider.GetRoundWithBorder("F");
                    break;
                case DrwableProviderEnum.SampleMultipleLetters:
                    label = "Support multiple letters";
                    drawable = _mProvider.GetRectWithMultiLetter();
                    navigation = NoNavigation;
                    break;
                case DrwableProviderEnum.SampleFont:
                    label = "Support variable font styles";
                    drawable = _mProvider.GetRoundWithCustomFont();
                    navigation = NoNavigation;
                    break;
                case DrwableProviderEnum.SampleSize:
                    label = "Support for custom size";
                    drawable = _mProvider.GetRectWithCustomSize();
                    navigation = NoNavigation;
                    break;
                case DrwableProviderEnum.SampleAnimation:
                    label = "Support for animations";
                    drawable = _mProvider.GetRectWithAnimation();
                    navigation = NoNavigation;
                    break;
                case DrwableProviderEnum.SampleMisc:
                    label = "Miscellaneous";
                    drawable = _mProvider.GetRect("\u03c0");
                    navigation = NoNavigation;
                    break;
            }
            return new DataItem(label, drawable, navigation);
        }
    }
}