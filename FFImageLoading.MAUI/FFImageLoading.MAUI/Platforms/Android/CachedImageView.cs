﻿using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFImageLoading.MAUI.Platforms.Android
{
    public class CachedImageView : ImageView
    {
        bool _skipInvalidate;

        public CachedImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public CachedImageView(Context context) : base(context)
        {
        }

        public CachedImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }


        public override void Invalidate()
        {
            if (_skipInvalidate)
            {
                _skipInvalidate = false;
                return;
            }

            base.Invalidate();
        }

        public void SkipInvalidate()
        {
            _skipInvalidate = true;
        }
    }
}
