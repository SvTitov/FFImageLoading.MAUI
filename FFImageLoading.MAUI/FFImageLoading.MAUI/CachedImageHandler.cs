using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFImageLoading.MAUI
{
#if ANDROID

    public partial class CachedImageHandler
    {

        public static PropertyMapper<ICachedImage, CachedImageHandler> CustomEntryMapper = new PropertyMapper<ICachedImage, CachedImageHandler>(ViewHandler.ViewMapper)
        {
            [nameof(ICachedImage.Source)] = MapSource,
            [nameof(ICachedImage.Aspect)] = MapAspect
        };
     

        public CachedImageHandler() : base(CustomEntryMapper)
        {

        }

        public CachedImageHandler(PropertyMapper? mapper = null) : base(mapper ?? CustomEntryMapper)
        {

        }

    }

#endif

}

