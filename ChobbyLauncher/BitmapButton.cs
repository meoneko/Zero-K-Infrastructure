﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChobbyLauncher
{
    public class BitmapButton : Button
    {

        public BitmapButton()
        {
            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Cursor = Cursors.Hand;
            BackgroundImage = null;
            BackgroundImageLayout = ImageLayout.None;
            DoubleBuffered = true;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.MouseDownBackColor = Color.Transparent;
            FlatAppearance.MouseOverBackColor = Color.Transparent;
            FlatAppearance.CheckedBackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Selectable, false);

            ButtonStyle = FrameBorderRenderer.StyleType.Shraka;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color ForeColor { get { return base.ForeColor; } set { base.ForeColor = value; } }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Color BackColor { get { return base.BackColor; } set { base.BackColor = value; } }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Image BackgroundImage { get { return base.BackgroundImage; } set { base.BackgroundImage = value; } }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        protected override bool DoubleBuffered { get { return base.DoubleBuffered; } set { base.DoubleBuffered = value; } }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override Cursor Cursor { get { return base.Cursor; } set { base.Cursor = value; } }

        public FrameBorderRenderer.StyleType ButtonStyle { get; set; }


        bool mouseOver;

        protected override void OnMouseEnter(EventArgs e)
        {
            mouseOver = true;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseOver = false;
        }

        /// <summary>
        /// Disable border when button is default button of form
        /// </summary>
        public override void NotifyDefault(bool value)
        {
            //base.NotifyDefault(value);
        }


        protected override void OnPaint(PaintEventArgs pevent)
        {
            BackgroundImage = FrameBorderRenderer.Instance.GetImageWithCache(DisplayRectangle,
                ButtonStyle, mouseOver ? FrameBorderRenderer.Styles[ButtonStyle].HoverStyle : null);
            base.OnPaint(pevent);
        }

    }
}