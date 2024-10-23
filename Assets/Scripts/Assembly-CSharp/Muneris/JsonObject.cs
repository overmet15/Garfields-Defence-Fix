using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris
{
	public class JsonObject : Dictionary<string, object>
	{
		public JsonObject()
		{
		}

		public JsonObject(IDictionary<string, object> list)
			: base(list)
		{
		}

		public override string ToString()
		{
			return JsonHelper.Serialize(this);
		}
	}
}
