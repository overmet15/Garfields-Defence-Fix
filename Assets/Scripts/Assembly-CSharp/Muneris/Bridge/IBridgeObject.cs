namespace Muneris.Bridge
{
	public interface IBridgeObject
	{
		long GetObjectId();

		string GetNativeClassName();

		new string ToString();

		new bool Equals(object obj);

		new int GetHashCode();
	}
}
