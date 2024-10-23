namespace Muneris
{
	public interface IDenyAccessCallback : ICallback
	{
		void onDenyAccess(MunerisException exception);
	}
}
