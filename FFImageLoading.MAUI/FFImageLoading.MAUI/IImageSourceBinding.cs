using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FFImageLoading.Work;

namespace FFImageLoading.MAUI
{
    public interface IImageSourceBinding
    {
        FFImageLoading.Work.ImageSource ImageSource { get; }

        string Path { get; }

        Func<CancellationToken, Task<Stream>> Stream { get; }
    }
}
