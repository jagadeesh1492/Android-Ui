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



namespace TextDrawable
{
    /// <summary>
    /// Class TextDrawable. This class cannot be inherited.
    /// </summary>
    public sealed class TextDrawable : ShapeDrawable
    {
        /// <summary>
        /// The _shade factor
        /// </summary>
        private static readonly float ShadeFactor = 0.9f;

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
        /// The _bordercolor
        /// </summary>
        private readonly Color _bordercolor;
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
        /// The _shape
        /// </summary>
        private RectShape _shape;

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
            _borderPaint = new Paint { Color = _bordercolor };
            _borderPaint.SetStyle(Paint.Style.Stroke);
            _borderPaint.StrokeWidth = _borderThickness;

            // drawable paint color
            var paint = Paint;
            paint.Color = _color;
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
            /// <param name="borderColor">Color of the border.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable Build(string text, Color color, Color borderColor);
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

            IConfigBuilder BorderColor(Color color);
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
            /// <param name="borderColor">Color of the border.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable BuildRect(string text, Color color, Color borderColor);

            /// <summary>
            /// Builds the round.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <param name="borderColor">Color of the border.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable BuildRound(string text, Color color, Color borderColor);

            /// <summary>
            /// Builds the round rect.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <param name="radius">The radius.</param>
            /// <param name="borderColor">Color of the border.</param>
            /// <returns>TextDrawable.</returns>
            TextDrawable BuildRoundRect(string text, Color color, int radius, Color borderColor);

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
        /// Gets the text drwable builder.
        /// </summary>
        /// <value>The text drwable builder.</value>
        public static IShapeBuilder TextDrwableBuilder => new Builder();


        public override int IntrinsicHeight => _width;


        public override int IntrinsicWidth => _height;


        public override int Opacity => (int)Format.Translucent;

        /// <summary>
        ///     Sets the alpha.
        /// </summary>
        /// <param name="alpha">The alpha.</param>
        /// <summary>
        ///     Sets the alpha.
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
            canvas.Translate(r.Left, r.Top);

            // draw text
            var width = _width < 0 ? r.Width() : _width;
            var height = _height < 0 ? r.Height() : _height;
            var fontSize = _fontSize < 0 ? Math.Min(width, height) / 2 : _fontSize;
            _textPaint.TextSize = _fontSize;
            Rect textBounds = new Rect();
            _textPaint.GetTextBounds(_text, 0, _text.Length, textBounds);
            canvas.DrawText(_text, width / 2, height / 2 - textBounds.ExactCenterY(), _textPaint);
            //canvas.DrawText(_text, width/2, height/2 - (_textPaint.Descent() + _textPaint.Ascent())/2, _textPaint);

            canvas.RestoreToCount(count);
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

        ///// <summary>
        /////     Gets the darker shade.
        ///// </summary>
        ///// <param name="color">The color.</param>
        ///// <returns>Color.</returns>
        //private Color GetDarkerShade(Color color)
        //{
        //    return new Color((int) (_shadeFactor*new Color(color)),
        //        (int) (_shadeFactor*new Color(color)),
        //        (int) (_shadeFactor*new Color(color)));
        //}

        /// <summary>
        /// Class Builder.
        /// </summary>
        public class Builder : IConfigBuilder, IShapeBuilder, IBuilder
        {
            /// <summary>
            /// The _border thickness
            /// </summary>
            internal int BorderThickness;

            /// <summary>
            /// The _color
            /// </summary>
            internal Color Color;


            /// <summary>
            /// The _border color
            /// </summary>
            internal Color _borderColor;
            /// <summary>
            /// The _font
            /// </summary>
            internal Typeface Font;

            /// <summary>
            /// The _font size
            /// </summary>
            internal int _fontSize;

            /// <summary>
            /// The _height
            /// </summary>
            internal int _height;

            /// <summary>
            /// The _is bold
            /// </summary>
            internal bool IsBold;

            /// <summary>
            /// The _radius
            /// </summary>
            internal float Radius;

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
            /// Initializes a new instance of the <see cref="Builder" /> class.
            /// </summary>
            internal Builder()
            {
                _text = "";
                Color = Color.Gray;
                _textColor = Color.White;
                _borderColor = Color.Red;
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
            /// Builds the specified text.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <param name="borderColor">Color of the border.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable Build(string text, Color color, Color borderColor)
            {
                Color = color;
                _text = text;
                _borderColor = borderColor;
                return new TextDrawable(this);
            }

            /// <summary>
            /// Builds the rect.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <param name="borderColor">Color of the border.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable BuildRect(string text, Color color, Color borderColor)
            {
                Rect();
                return Build(text, color, borderColor);
            }

            /// <summary>
            /// Builds the round.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <param name="borderColor">Color of the border.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable BuildRound(string text, Color color, Color borderColor)
            {
                Round();
                return Build(text, color, borderColor);
            }

            /// <summary>
            /// Builds the round rect.
            /// </summary>
            /// <param name="text">The text.</param>
            /// <param name="color">The color.</param>
            /// <param name="radius">The radius.</param>
            /// <param name="borderColor">Color of the border.</param>
            /// <returns>TextDrawable.</returns>
            public TextDrawable BuildRoundRect(string text, Color color, int radius, Color borderColor)
            {
                RoundRect(radius);
                return Build(text, color, borderColor);
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

            public IConfigBuilder BorderColor(Color color)
            {
                _borderColor = color;
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
        }
    }
}