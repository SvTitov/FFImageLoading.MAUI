using System;

namespace FFImageLoading.MAUI
{
	public interface ICacheKeyFactory
	{
		string GetKey(ImageSource imageSource, object bindingContext);
	}
}

