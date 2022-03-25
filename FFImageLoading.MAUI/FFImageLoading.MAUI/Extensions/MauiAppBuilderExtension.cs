using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFImageLoading.MAUI.Extensions
{
    public static class MauiAppBuilderExtension
    {
        public static MauiAppBuilder UseFFImageLoading(this MauiAppBuilder builder)
        {
            return builder.ConfigureMauiHandlers(collection =>
            {
#if ANDROID
                collection.AddHandler(typeof(CachedImage), typeof(CachedImageHandler));
#endif
            });
        }
    }
}
