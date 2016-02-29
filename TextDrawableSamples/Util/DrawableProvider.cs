using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using DrawableTextView = TextDrawable.TextDrawable;

namespace TextDrawableSamples.Util
{
    public enum DrwableProviderEnum
    {
        SampleRect = 1,
        SampleRoundRect = 2,
        SampleRound = 3,
        SampleRectBorder = 4,
        SampleRoundRectBorder = 5,
        SampleRoundBorder = 6,
        SampleMultipleLetters = 7,
        SampleFont = 8,
        SampleSize = 9,
        SampleAnimation = 10,
        SampleMisc = 11
    }

    /// <summary>
    ///     Class DrawableProvider.
    /// </summary>
    public class DrawableProvider
    {
        /// <summary>
        ///     The _m context
        /// </summary>
        private readonly Context _mContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DrawableProvider" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public DrawableProvider(Context context)
        {
            _mContext = context;
        }

        /// <summary>
        ///     The _rand
        /// </summary>
        private readonly Random _rand = new Random();

        /// <summary>
        ///     Gets the random color.
        /// </summary>
        /// <value>The random color.</value>
        public Color RandomColor => GetRandomColor();

        /// <summary>
        ///     Gets the random color.
        /// </summary>
        /// <returns>Color.</returns>
        private Color GetRandomColor()
        {
            var hue = _rand.Next(255);
            var color = Color.HSVToColor(
                new[]
                {
                    hue,
                    1.0f,
                    1.0f
                }
                );
            return color;
        }

        /// <summary>
        ///     Gets the rect.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>DrawableTextView.</returns>
        public DrawableTextView GetRect(string text)
        {
            return DrawableTextView.TextDrwableBuilder.BuildRect(text, RandomColor, RandomColor);
        }

        /// <summary>
        ///     Gets the round.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>DrawableTextView.</returns>
        public DrawableTextView GetRound(string text)
        {
            return DrawableTextView.TextDrwableBuilder.BuildRound(text, RandomColor, RandomColor);
        }

        /// <summary>
        ///     Gets the round rect.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>DrawableTextView.</returns>
        public DrawableTextView GetRoundRect(string text)
        {
            return DrawableTextView.TextDrwableBuilder.BuildRoundRect(text, RandomColor, ToPx(10), RandomColor);
        }

        /// <summary>
        ///     Gets the rect with border.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>DrawableTextView.</returns>
        public DrawableTextView GetRectWithBorder(string text)
        {
            return DrawableTextView.TextDrwableBuilder
                .BeginConfig()
                .WithBorder(ToPx(2))
                .EndConfig()
                .BuildRect(text, RandomColor, RandomColor);
        }

        /// <summary>
        ///     Gets the round with border.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>DrawableTextView.</returns>
        public DrawableTextView GetRoundWithBorder(string text)
        {
            return DrawableTextView.TextDrwableBuilder
                .BeginConfig()
                .WithBorder(ToPx(2))
                .EndConfig()
                .BuildRound(text, RandomColor, RandomColor);
        }

        /// <summary>
        ///     Gets the round rect with border.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>DrawableTextView.</returns>
        public DrawableTextView GetRoundRectWithBorder(string text)
        {
            return DrawableTextView.TextDrwableBuilder
                .BeginConfig()
                .WithBorder(ToPx(2))
                .EndConfig()
                .BuildRoundRect(text, RandomColor, ToPx(10), RandomColor);
        }

        /// <summary>
        ///     Gets the rect with multi letter.
        /// </summary>
        /// <returns>DrawableTextView.</returns>
        public DrawableTextView GetRectWithMultiLetter()
        {
            var text = "AK";
            return DrawableTextView.TextDrwableBuilder.BeginConfig()
                .FontSize(ToPx(20))
                .ToUpperCase()
                .EndConfig()
                .BuildRect(text, RandomColor, RandomColor);
        }

        /// <summary>
        ///     Gets the round with custom font.
        /// </summary>
        /// <returns>DrawableTextView.</returns>
        public DrawableTextView GetRoundWithCustomFont()
        {
            var text = "Bold";
            return DrawableTextView.TextDrwableBuilder.BeginConfig()
                .UseFont(Typeface.Default)
                .FontSize(ToPx(15))
                .TextColor(RandomColor)
                .Bold()
                .EndConfig()
                .BuildRect(text, Color.DarkGray, RandomColor /*toPx(5)*/);
        }

        /// <summary>
        ///     Gets the size of the rect with custom.
        /// </summary>
        /// <returns>Drawable.</returns>
        public Drawable GetRectWithCustomSize()
        {
            var leftText = "I";
            var rightText = "J";

            var builder =
                DrawableTextView.TextDrwableBuilder.BeginConfig().Width(ToPx(29)).WithBorder(ToPx(2)).EndConfig().Rect();


            var left = builder.Build(leftText, RandomColor, Color.Transparent);

            var right = builder.Build(rightText, RandomColor, Color.Transparent);

            Drawable[] layerList =
            {
                new InsetDrawable(left, 0, 0, ToPx(31), 0),
                new InsetDrawable(right, ToPx(31), 0, 0, 0)
            };
            return new LayerDrawable(layerList);
        }

        /// <summary>
        ///     Gets the rect with animation.
        /// </summary>
        /// <returns>Drawable.</returns>
        public Drawable GetRectWithAnimation()
        {
            var builder = DrawableTextView.TextDrwableBuilder.Rect();


            var animationDrawable = new AnimationDrawable();
            for (var i = 10; i > 0; i--)
            {
                var frame = builder.Build(i.ToString(), RandomColor, RandomColor);
                animationDrawable.AddFrame(frame, 1200);
            }
            animationDrawable.OneShot = false;
            animationDrawable.Start();

            return animationDrawable;
        }

        /// <summary>
        ///     To the px.
        /// </summary>
        /// <param name="dp">The dp.</param>
        /// <returns>System.Int32.</returns>
        public int ToPx(int dp)
        {
            var resources = _mContext.Resources;
            return (int) TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, resources.DisplayMetrics);
        }
    }
}