namespace Muneris
{
	public interface IDetectEnvarsCargoChangeCallback : ICallback
	{
		void onDetectEnvarsCargoChange(JsonObject cargo);
	}
}
