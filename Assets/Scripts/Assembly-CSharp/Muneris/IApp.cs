using System;

namespace Muneris
{
	public interface IApp
	{
		string getAppId();

		string getName();

		string getPackageName();

		Uri getAppUrl();

		Uri getImageUrl();

		string getAppStoreId();

		void launch();
	}
}
