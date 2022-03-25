using System;
using System.Threading;
using System.Threading.Tasks;
using FFImageLoading.Work;

#if ANDROID

namespace FFImageLoading.MAUI.Handlers
{
	extern alias ffdroid;
	public abstract class HandlerBase<TNativeView>
	{
		protected virtual Task<IImageLoaderTask> LoadImageAsync(IImageSourceBinding binding, Microsoft.Maui.Controls.ImageSource imageSource, TNativeView imageView, CancellationToken cancellationToken)
		{
			TaskParameter parameters = default;

			if (binding.ImageSource == FFImageLoading.Work.ImageSource.Url)
			{
				var urlSource = (Microsoft.Maui.Controls.UriImageSource)((imageSource as IVectorImageSource)?.ImageSource ?? imageSource);
				parameters = ffdroid.FFImageLoading.ImageService.Instance.LoadUrl(binding.Path, urlSource.CacheValidity);

				if (!urlSource.CachingEnabled)
				{
					parameters.WithCache(Cache.CacheType.None);
				}
			}
			else if (binding.ImageSource == FFImageLoading.Work.ImageSource.CompiledResource)
			{
				parameters = ffdroid.FFImageLoading.ImageService.Instance.LoadCompiledResource(binding.Path);
			}
			else if (binding.ImageSource == FFImageLoading.Work.ImageSource.ApplicationBundle)
			{
				parameters = ffdroid.FFImageLoading.ImageService.Instance.LoadFileFromApplicationBundle(binding.Path);
			}
			else if (binding.ImageSource == FFImageLoading.Work.ImageSource.Filepath)
			{
				parameters = ffdroid.FFImageLoading.ImageService.Instance.LoadFile(binding.Path);
			}
			else if (binding.ImageSource == FFImageLoading.Work.ImageSource.Stream)
			{
				parameters = ffdroid.FFImageLoading.ImageService.Instance.LoadStream(binding.Stream);
			}
			else if (binding.ImageSource == FFImageLoading.Work.ImageSource.EmbeddedResource)
			{
				parameters = ffdroid.FFImageLoading.ImageService.Instance.LoadEmbeddedResource(binding.Path);
			}

			if (parameters != default)
			{
				// Enable vector image source
				if (imageSource is IVectorImageSource vect)
				{
					parameters.WithCustomDataResolver(vect.GetVectorDataResolver());
				}

				var tcs = new TaskCompletionSource<IImageLoaderTask>();

				parameters
					.FadeAnimation(false, false)
					.Error(ex => {
						tcs.TrySetException(ex);
					})
					.Finish(scheduledWork => {
						tcs.TrySetResult(scheduledWork as IImageLoaderTask);
					});

				if (cancellationToken.IsCancellationRequested)
					return Task.FromResult<IImageLoaderTask>(null);

				var task = GetImageLoaderTask(parameters, imageView);

				if (cancellationToken != default)
					cancellationToken.Register(() =>
					{
						try
						{
							task?.Cancel();
						}
						catch { }
					});

				return tcs.Task;
			}

			return Task.FromResult<IImageLoaderTask>(null);
		}

		protected abstract IImageLoaderTask GetImageLoaderTask(TaskParameter parameters, TNativeView imageView);
	}
}

#endif