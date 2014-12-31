﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ZkData
{
	partial class News
	{
        [NotMapped]
		public string ImageRelativeUrl {
			get { if (ImageExtension == null) return null; return string.Format("/img/news/{0}{1}", NewsID, ImageExtension); }
		}

        [NotMapped]
        public string ThumbRelativeUrl
        {
            get { if (ImageExtension == null) return null; return string.Format("/img/news/{0}{1}_thumb", NewsID, ImageExtension); }
        }
	}
}
