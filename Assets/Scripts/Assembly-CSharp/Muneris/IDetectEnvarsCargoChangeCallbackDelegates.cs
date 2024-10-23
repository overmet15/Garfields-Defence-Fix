namespace Muneris
{
	public class IDetectEnvarsCargoChangeCallbackDelegates : ICallback, IDetectEnvarsCargoChangeCallback
	{
		public delegate void onDetectEnvarsCargoChangeDelegate(JsonObject cargo);

		private onDetectEnvarsCargoChangeDelegate m_onDetectEnvarsCargoChangeDelegate;

		public IDetectEnvarsCargoChangeCallbackDelegates(onDetectEnvarsCargoChangeDelegate _onDetectEnvarsCargoChangeDelegate)
		{
			m_onDetectEnvarsCargoChangeDelegate = _onDetectEnvarsCargoChangeDelegate;
		}

		public void onDetectEnvarsCargoChange(JsonObject cargo)
		{
			if (m_onDetectEnvarsCargoChangeDelegate != null)
			{
				m_onDetectEnvarsCargoChangeDelegate(cargo);
			}
		}
	}
}
