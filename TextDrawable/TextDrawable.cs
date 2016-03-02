/*The MIT License(MIT)

Copyright(c) 2016 Jagadeesh Govindaraj and Amulya Khare

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

//Ported C# By Jagadeesh Govindaraj
//TweetMe @Jaganjan

using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;

namespace Android.Ui.TextDrawable
{
    /// <summary>
    /// Class TextDrawable. This class cannot be inherited.
    /// </summary>
    public sealed class TextDrawable : ShapeDrawable
    {
        /// <summary>
        /// The rand
        /// </summary>
        /// <summary>
        /// The rand
        /// </summary>
        private static readonly Random Rand = new Random();

        /// <summary>
        /// The _shade factor
        /// </summary>
        private static readonly float ShadeFactor = 0.9f;

        /// <summary>
        /// The _bitmap
        /// </summary>
        private readonly Bitmap _bitmap;

        /// <summary>
        /// The _bordercolor
        /// </summary>
        private readonly Color _bordercolor;

        /// <summary>
        /// The _border paint
        /// </summary>
        private readonly Paint _borderPaint;

        /// <summary>
        /// The _border thickness
        /// </summary>
        private readonly int _borderThickness;

        /// <summary>
        /// The _color
        /// </summary>
        private readonly Color _color;
        /// <summary>
        /// The _font size
        /// </summary>
        private readonly int _fontSize;

        /// <summary>
        /// The _height
        /// </summary>
        private readonly int _height;

        /// <summary>
        /// The _radius
        /// </summary>
        private readonly float _radius;

        /// <summary>
        /// The _text
        /// </summary>
        private readonly string _text;

        /// <summary>
        /// The _text paint
        /// </summary>
        private readonly Paint _textPaint;

        /// <summary>
        /// The _width
        /// </summary>
        private readonly int _width;

        /// <summary>
        /// The _shape
        /// </summary>
        private RectShape _shape;
        /// <summary>
        /// Initializes a new instance of the <see cref="TextDrawable" /> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        private TextDrawable(Builder builder) : base(builder._shape)
        {
            // shape properties
            _shape = builder._shape;
            _height = builder._height;
            _width = builder._width;
            _radius = builder.Radius;

            // text and color
            _text = builder._toUpperCase ? builder._text.ToUpper() : builder._text;
            _color = builder.Color;

            // text paint settings
            _fontSize = builder._fontSize;
            _textPaint = new Paint
            {
                Color = builder._textColor,
                AntiAlias = true,
                FakeBoldText = builder.IsBold
            };
            _textPaint.SetStyle(Paint.Style.Fill);
            _textPaint.SetTypeface(builder.Font);
            _textPaint.TextAlign = Paint.Align.Center;
            _textPaint.StrokeWidth = builder.BorderThickness;

            // border paint settings
            _borderThickness = builder.BorderThickness;
            _bordercolor = builder._borderColor;
            _borderPaint = new Paint
            {
                Color = _bordercolor,
                AntiAlias = true
            };
            _borderPaint.SetStyle(Paint.Style.Stroke);
            _borderPaint.StrokeWidth = _borderThickness;

            // drawable paint color
            var paint = Paint;
            paint.Color = _color;

            if (builder.Drawable != null)
            {
                _bitmap = ((BitmapDrawable)builder.Drawable).Bitmap;
            }
        }

        /// <summary>
        /// Interface IBuilder
        /// </summary>
        public interface IBuilder
        {
            /// <summary>
            /// Builds the specified text.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable Build(string text, Color color);

            /// <summary>
            /// Builds the specified drawable.
            /// </summary>
            /// <param name="drawable">The drawable.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable Build(Drawable drawable, Color color);
        }

        /// <summary>
        /// Interface IConfigBuilder
        /// </summary>
        public interface IConfigBuilder
        {
            /// <summary>
            /// Bolds this instance.
            /// </summary>
            /// <returns>IConfigBuilder.</returns>
            IConfigBuilder Bold();

            /// <summary>
            /// Borders the color.
            /// </summary>
            /// <param name="color">The color.</param>
            /// <returns>IConfigBuilder.</returns>
            IConfigBuilder BorderColor(Color color);

            /// <summary>
            /// Ends the configuration.
            /// </summary>
            /// <returns>IShapeBuilder.</returns>
            IShapeBuilder EndConfig();

            /// <summary>
            /// Fonts the size.
            /// </summary>
            /// <param name="size">The size.</param>
            /// <returns>IConfigBuilder.</returns>
            IConfigBuilder FontSize(int size);

            /// <summary>
            /// Heights the specified height.
            /// </summary>
            /// <param name="height">The height.</param>
            /// <returns>IConfigBuilder.</returns>
            IConfigBuilder Height(int height);

            /// <summary>
            /// Texts the color.
            /// </summary>
            /// <param name="color">The color.</param>
            /// <returns>IConfigBuilder.</returns>
            IConfigBuilder TextColor(Color color);
            /// <summary>
            /// To the upper case.
            /// </summary>
            /// <returns>IConfigBuilder.</returns>
            IConfigBuilder ToUpperCase();

            /// <summary>
            /// Uses the font.
            /// </summary>
            /// <param name="font">The font.</param>
            /// <returns>IConfigBuilder.</returns>
            IConfigBuilder UseFont(Typeface font);

            /// <summary>
            /// Widthes the specified width.
            /// </summary>
            /// <param name="width">The width.</param>
            /// <returns>IConfigBuilder.</returns>
            IConfigBuilder Width(int width);

            /// <summary>
            /// Withes the border.
            /// </summary>
            /// <param name="thickness">The thickness.</param>
            /// <returns>IConfigBuilder.</returns>
            IConfigBuilder WithBorder(int thickness);

            /// <summary>
            /// Withes the color of the border.
            /// </summary>
            /// <param name="color">The color.</param>
            /// <returns>IConfigBuilder.</returns>
            IConfigBuilder WithBorderColor(Color color);
        }

        /// <summary>
        /// Interface IShapeBuilder
        /// </summary>
        public interface IShapeBuilder
        {
            /// <summary>
            /// Begins the configuration.
            /// </summary>
            /// <returns>IConfigBuilder.</returns>
            IConfigBuilder BeginConfig();

            /// <summary>
            /// Builds the rect.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable BuildRect(string text, Color color);

            /// <summary>
            /// Builds the rect.
            /// </summary>
            /// <param name="drawable">The drawable.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable BuildRect(Drawable drawable, Color color);

            /// <summary>
            /// Builds the round.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable BuildRound(string text, Color color);

            /// <summary>
            /// Builds the round.
            /// </summary>
            /// <param name="drawable">The drawable.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable BuildRound(Drawable drawable, Color color);

            /// <summary>
            /// Builds the round rect.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <param name="radius">The radius.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable BuildRoundRect(string text, Color color, int radius);

            /// <summary>
            /// Builds the round rect.
            /// </summary>
            /// <param name="drawable">The drawable.</param>
            /// <param name="color">The color.</param>
            /// <param name="radius">The radius.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable BuildRoundRect(Drawable drawable, Color color, int radius);

            /// <summary>
            /// Rects this instance.
            /// </summary>
            /// <returns>IBuilder.</returns>
            IBuilder Rect();

            /// <summary>
            /// Rounds this instance.
            /// </summary>
            /// <returns>IBuilder.</returns>
            IBuilder Round();

            /// <summary>
            /// Rounds the rect.
            /// </summary>
            /// <param name="radius">The radius.</param>
            /// <returns>IBuilder.</returns>
            IBuilder RoundRect(int radius);
        }

        /// <summary>
        /// Return the intrinsic height of the underlying drawable object.
        /// </summary>
        /// <value>To be added.</value>
        /// <since version="Added in API level 1" />
        /// <remarks><para tool="javadoc-to-mdoc">Return the intrinsic height of the underlying drawable object. Returns
        /// -1 if it has no intrinsic height, such as with a solid color.
        /// </para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/graphics/drawable/Drawable.html#getIntrinsicHeight()" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para></remarks>
        public override int IntrinsicHeight => _width;

        /// <summary>
        /// Return the intrinsic width of the underlying drawable object.
        /// </summary>
        /// <value>To be added.</value>
        /// <since version="Added in API level 1" />
        /// <remarks><para tool="javadoc-to-mdoc">Return the intrinsic width of the underlying drawable object.  Returns
        /// -1 if it has no intrinsic width, such as with a solid color.
        /// </para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/graphics/drawable/Drawable.html#getIntrinsicWidth()" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para></remarks>
        /// <summary>
        /// Return the intrinsic width of the underlying drawable object.
        /// </summary>
        /// <value>To be added.</value>
        /// <since version="Added in API level 1" />
        /// <remarks><para tool="javadoc-to-mdoc">Return the intrinsic width of the underlying drawable object.  Returns
        /// -1 if it has no intrinsic width, such as with a solid color.
        /// </para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/graphics/drawable/Drawable.html#getIntrinsicWidth()" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para></remarks>
        public override int IntrinsicWidth => _height;

        /// <summary>
        /// Return the opacity/transparency of this Drawable.
        /// </summary>
        /// <value>To be added.</value>
        /// <since version="Added in API level 1" />
        /// <remarks><para tool="javadoc-to-mdoc">Return the opacity/transparency of this Drawable.  The returned value is
        /// one of the abstract format constants in
        /// <c><see cref="T:Android.Graphics.PixelFormat" /></c>:
        /// <c><see cref="F:Android.Graphics.Format.Unknown" /></c>,
        /// <c><see cref="F:Android.Graphics.Format.Translucent" /></c>,
        /// <c><see cref="F:Android.Graphics.Format.Transparent" /></c>, or
        /// <c><see cref="F:Android.Graphics.Format.Opaque" /></c>.
        /// </para>
        /// <para tool="javadoc-to-mdoc">Generally a Drawable should be as conservative as possible with the
        /// value it returns.  For example, if it contains multiple child drawables
        /// and only shows one of them at a time, if only one of the children is
        /// TRANSLUCENT and the others are OPAQUE then TRANSLUCENT should be
        /// returned.  You can use the method <c><see cref="M:Android.Graphics.Drawables.Drawable.ResolveOpacity(System.Int32, System.Int32)" /></c> to perform a
        /// standard reduction of two opacities to the appropriate single output.
        /// </para>
        /// <para tool="javadoc-to-mdoc">Note that the returned value does <i>not</i> take into account a
        /// custom alpha or color filter that has been applied by the client through
        /// the <c><see cref="M:Android.Graphics.Drawables.Drawable.SetAlpha(System.Int32)" /></c> or <c><see cref="M:Android.Graphics.Drawables.Drawable.SetColorFilter(Android.Graphics.ColorFilter)" /></c> methods.</para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/graphics/drawable/ShapeDrawable.html#getOpacity()" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para></remarks>
        /// <summary>
        /// Return the opacity/transparency of this Drawable.
        /// </summary>
        /// <value>To be added.</value>
        /// <since version="Added in API level 1" />
        /// <remarks><para tool="javadoc-to-mdoc">Return the opacity/transparency of this Drawable.  The returned value is
        /// one of the abstract format constants in
        /// <c><see cref="T:Android.Graphics.PixelFormat" /></c>:
        /// <c><see cref="F:Android.Graphics.Format.Unknown" /></c>,
        /// <c><see cref="F:Android.Graphics.Format.Translucent" /></c>,
        /// <c><see cref="F:Android.Graphics.Format.Transparent" /></c>, or
        /// <c><see cref="F:Android.Graphics.Format.Opaque" /></c>.
        /// </para>
        /// <para tool="javadoc-to-mdoc">Generally a Drawable should be as conservative as possible with the
        /// value it returns.  For example, if it contains multiple child drawables
        /// and only shows one of them at a time, if only one of the children is
        /// TRANSLUCENT and the others are OPAQUE then TRANSLUCENT should be
        /// returned.  You can use the method <c><see cref="M:Android.Graphics.Drawables.Drawable.ResolveOpacity(System.Int32, System.Int32)" /></c> to perform a
        /// standard reduction of two opacities to the appropriate single output.
        /// </para>
        /// <para tool="javadoc-to-mdoc">Note that the returned value does <i>not</i> take into account a
        /// custom alpha or color filter that has been applied by the client through
        /// the <c><see cref="M:Android.Graphics.Drawables.Drawable.SetAlpha(System.Int32)" /></c> or <c><see cref="M:Android.Graphics.Drawables.Drawable.SetColorFilter(Android.Graphics.ColorFilter)" /></c> methods.</para>
        /// <para tool="javadoc-to-mdoc">
        ///   <format type="text/html">
        ///     <a href="http://developer.android.com/reference/android/graphics/drawable/ShapeDrawable.html#getOpacity()" target="_blank">[Android Documentation]</a>
        ///   </format>
        /// </para></remarks>
        public override int Opacity => (int)Format.Translucent;

        /// <summary>
        /// Gets the random color.
        /// </summary>
        /// <value>The random color.</value>
        public static Color RandomColor => GetRandomColor();

        /// <summary>
        /// Gets the text drwable builder.
        /// </summary>
        /// <value>The text drwable builder.</value>
        public static IShapeBuilder TextDrwableBuilder => new Builder();
        /// <summary>
        /// Sets the alpha.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <summary>
        /// Sets the alpha.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        public override void SetAlpha(int alpha)
        {
            _textPaint.Alpha = alpha;
        }

        /// <summary>
        /// Sets the color filter.
        /// </summary>
        /// <param name="colorFilter">The color filter.</param>
        public override void SetColorFilter(ColorFilter colorFilter)
        {
            _textPaint.SetColorFilter(colorFilter);
        }

        /// <summary>
        /// Called when [draw].
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <param name="canvas">The canvas.</param>
        /// <param name="paint">The paint.</param>
        protected override void OnDraw(Shape shape, Canvas canvas, Paint paint)
        {
            base.OnDraw(shape, canvas, paint);

            var r = Bounds;

            // draw border
            if (_borderThickness > 0)
            {
                DrawBorder(canvas);
            }

            var count = canvas.Save();

            if (_bitmap == null)
            {
                canvas.Translate(r.Left, r.Top);
            }

            // draw text
            var width = _width < 0 ? r.Width() : _width;
            var height = _height < 0 ? r.Height() : _height;
            var fontSize = _fontSize < 0 ? Math.Min(width, height) / 2 : _fontSize;

            if (_bitmap == null)
            {
                _textPaint.TextSize = _fontSize;
                Rect textBounds = new Rect();
                _textPaint.GetTextBounds(_text, 0, _text.Length, textBounds);
                canvas.DrawText(_text, width / 2, height / 2 - textBounds.ExactCenterY(), _textPaint);
            }
            else
            {
                canvas.DrawBitmap(_bitmap, (width - _bitmap.Width) / 2, (height - _bitmap.Height) / 2, null);
            }

            canvas.RestoreToCount(count);
        }

        /// <summary>
        /// Gets the random color.
        /// </summary>
        /// <returns>Color.</returns>
        private static Color GetRandomColor()
        {
            var hue = Rand.Next(255);
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
        /// Draws the border.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        private void DrawBorder(Canvas canvas)
        {
            var rect = new RectF(Bounds);
            rect.Inset(_borderThickness / 2, _borderThickness / 2);

            if (Shape is OvalShape)
            {
                canvas.DrawOval(rect, _borderPaint);
            }
            else if (Shape is RoundRectShape)
            {
                canvas.DrawRoundRect(rect, _radius, _radius, _borderPaint);
            }
            else
            {
                canvas.DrawRect(rect, _borderPaint);
            }
        }

        /// <summary>
        /// Class Builder.
        /// </summary>
        public class Builder : IConfigBuilder, IShapeBuilder, IBuilder
        {
            /// <summary>
            /// The _border color
            /// </summary>
            internal Color _borderColor;

            /// <summary>
            /// The _font size
            /// </summary>
            internal int _fontSize;

            /// <summary>
            /// The _height
            /// </summary>
            internal int _height;

            /// <summary>
            /// The _shape
            /// </summary>
            internal RectShape _shape;

            /// <summary>
            /// The _text
            /// </summary>
            internal string _text;

            /// <summary>
            /// The _text color
            /// </summary>
            internal Color _textColor;

            /// <summary>
            /// The _to upper case
            /// </summary>
            internal bool _toUpperCase;

            /// <summary>
            /// The _width
            /// </summary>
            internal int _width;

            /// <summary>
            /// The _border thickness
            /// </summary>
            internal int BorderThickness;

            /// <summary>
            /// The _color
            /// </summary>
            internal Color Color;
            /// <summary>
            /// The drawable
            /// </summary>
            internal Drawable Drawable;

            /// <summary>
            /// The _font
            /// </summary>
            internal Typeface Font;
            /// <summary>
            /// The _is bold
            /// </summary>
            internal bool IsBold;

            /// <summary>
            /// The _radius
            /// </summary>
            internal float Radius;
            /// <summary>
            /// Initializes a new instance of the <see cref="Builder" /> class.
            /// </summary>
            internal Builder()
            {
                _text = "";
                Color = Color.Gray;
                _textColor = Color.White;
                _borderColor = Color.Transparent;
                BorderThickness = 0;
                _width = -1;
                _height = -1;
                _shape = new RectShape();
                Font = Typeface.Create(Typeface.SansSerif, TypefaceStyle.Normal);
                _fontSize = -1;
                IsBold = false;
                _toUpperCase = false;
            }

            /// <summary>
            /// Begins the configuration.
            /// </summary>
            /// <returns>IConfigBuilder.</returns>
            public IConfigBuilder BeginConfig()
            {
                return this;
            }

            /// <summary>
            /// Bolds this instance.
            /// </summary>
            /// <returns>IConfigBuilder.</returns>
            public IConfigBuilder Bold()
            {
                IsBold = true;
                return this;
            }

            /// <summary>
            /// Borders the color.
            /// </summary>
            /// <param name="color">The color.</param>
            /// <returns>IConfigBuilder.</returns>
            public IConfigBuilder BorderColor(Color color)
            {
                _borderColor = color;
                return this;
            }

            /// <summary>
            /// Builds the specified text.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable Build(string text, Color color)
            {
                Color = color;
                _text = text;
                return new TextDrawable(this);
            }

            /// <summary>
            /// Builds the specified drawable.
            /// </summary>
            /// <param name="drawable">The drawable.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable Build(Drawable drawable, Color color)
            {
                Color = color;
                Drawable = drawable;
                return new TextDrawable(this);
            }

            /// <summary>
            /// Builds the rect.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable BuildRect(string text, Color color)
            {
                Rect();
                return Build(text, color);
            }

            /// <summary>
            /// Builds the rect.
            /// </summary>
            /// <param name="drawable">The drawable.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable BuildRect(Drawable drawable, Color color)
            {
                Rect();
                return Build(drawable, color);
            }

            /// <summary>
            /// Builds the round.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable BuildRound(string text, Color color)
            {
                Round();
                return Build(text, color);
            }

            /// <summary>
            /// Builds the round.
            /// </summary>
            /// <param name="drawable">The drawable.</param>
            /// <param name="color">The color.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable BuildRound(Drawable drawable, Color color)
            {
                Round();
                return Build(drawable, color);
            }

            /// <summary>
            /// Builds the round rect.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <param name="radius">The radius.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable BuildRoundRect(string text, Color color, int radius)
            {
                RoundRect(radius);
                return Build(text, color);
            }

            /// <summary>
            /// Builds the round rect.
            /// </summary>
            /// <param name="drawable">The drawable.</param>
            /// <param name="color">The color.</param>
            /// <param name="radius">The radius.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable BuildRoundRect(Drawable drawable, Color color, int radius)
            {
                RoundRect(radius);
                return Build(drawable, color);
            }

            /// <summary>
            /// Ends the configuration.
            /// </summary>
            /// <returns>IShapeBuilder.</returns>
            public IShapeBuilder EndConfig()
            {
                return this;
            }

            /// <summary>
            /// Fonts the size.
            /// </summary>
            /// <param name="size">The size.</param>
            /// <returns>IConfigBuilder.</returns>
            /// <exception cref="System.NotImplementedException"></exception>
            public IConfigBuilder FontSize(int size)
            {
                this._fontSize = size;
                return this;
            }

            /// <summary>
            /// Heights the specified height.
            /// </summary>
            /// <param name="height">The height.</param>
            /// <returns>IConfigBuilder.</returns>
            public IConfigBuilder Height(int height)
            {
                _height = height;
                return this;
            }

            /// <summary>
            /// Rects this instance.
            /// </summary>
            /// <returns>IBuilder.</returns>
            public IBuilder Rect()
            {
                _shape = new RectShape();
                return this;
            }

            /// <summary>
            /// Rounds this instance.
            /// </summary>
            /// <returns>IBuilder.</returns>
            public IBuilder Round()
            {
                _shape = new OvalShape();
                return this;
            }

            /// <summary>
            /// Rounds the rect.
            /// </summary>
            /// <param name="radius">The radius.</param>
            /// <returns>IBuilder.</returns>
            public IBuilder RoundRect(int radius)
            {
                Radius = radius;
                float[] radii = { radius, radius, radius, radius, radius, radius, radius, radius };
                _shape = new RoundRectShape(radii, null, null);
                return this;
            }

            /// <summary>
            /// Texts the color.
            /// </summary>
            /// <param name="color">The color.</param>
            /// <returns>IConfigBuilder.</returns>
            public IConfigBuilder TextColor(Color color)
            {
                _textColor = color;
                return this;
            }
            /// <summary>
            /// To the upper case.
            /// </summary>
            /// <returns>IConfigBuilder.</returns>
            public IConfigBuilder ToUpperCase()
            {
                _toUpperCase = true;
                return this;
            }

            /// <summary>
            /// Uses the font.
            /// </summary>
            /// <param name="font">The font.</param>
            /// <returns>IConfigBuilder.</returns>
            public IConfigBuilder UseFont(Typeface font)
            {
                Font = font;
                return this;
            }

            /// <summary>
            /// Widthes the specified width.
            /// </summary>
            /// <param name="width">The width.</param>
            /// <returns>IConfigBuilder.</returns>
            public IConfigBuilder Width(int width)
            {
                _width = width;
                return this;
            }

            /// <summary>
            /// Withes the border.
            /// </summary>
            /// <param name="thickness">The thickness.</param>
            /// <returns>IConfigBuilder.</returns>
            public IConfigBuilder WithBorder(int thickness)
            {
                BorderThickness = thickness;
                return this;
            }

            /// <summary>
            /// Withes the color of the border.
            /// </summary>
            /// <param name="color">The color.</param>
            /// <returns>IConfigBuilder.</returns>
            public IConfigBuilder WithBorderColor(Color color)
            {
                this._borderColor = color;
                return this;
            }
        }
    }
}