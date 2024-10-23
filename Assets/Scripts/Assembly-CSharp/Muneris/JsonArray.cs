using System.Collections.Generic;
using Muneris.Bridge;

namespace Muneris
{
	public class JsonArray : List<object>
	{
		public JsonArray()
		{
		}

		public JsonArray(IList<object> list)
			: base((IEnumerable<object>)list)
		{
		}

		public override string ToString()
		{
			return JsonHelper.Serialize(this);
		}
	}
}
