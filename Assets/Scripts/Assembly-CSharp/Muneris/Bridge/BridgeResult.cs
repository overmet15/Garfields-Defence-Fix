namespace Muneris.Bridge
{
	public class BridgeResult<T>
	{
		public enum Type
		{
			Value = 0,
			Exception = 1
		}

		public Type Kind { get; private set; }

		public T Value { get; private set; }

		public BridgeException Exception { get; private set; }

		public BridgeResult(T value, BridgeException exception)
		{
			Value = value;
			Exception = exception;
			Kind = ((exception != null) ? Type.Exception : Type.Value);
		}
	}
}
