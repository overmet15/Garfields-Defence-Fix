using System.Collections;
using System.Xml;

public class FD_ObjectLevelDefinition
{
	private string levelNum;

	private Hashtable attributeMap;

	private Hashtable childObjectMap;

	private static string OBJECT_ELEMENT = "level";

	private static string ATTRIBUTE_ELEMENT = "attribute";

	public FD_ObjectLevelDefinition(XmlNode node)
	{
		StarStrike_Assertion.Assert(OBJECT_ELEMENT.Equals(node.Name), "The specified node must be an '" + OBJECT_ELEMENT + "' node.");
		levelNum = node.Attributes.GetNamedItem("levelNum").Value;
		attributeMap = new Hashtable();
		XmlNodeList childNodes = node.ChildNodes;
		foreach (XmlNode item in childNodes)
		{
			if (ATTRIBUTE_ELEMENT.Equals(item.Name))
			{
				ParseAttribute(item);
			}
		}
	}

	private void ParseAttribute(XmlNode node)
	{
		StarStrike_Assertion.Assert(ATTRIBUTE_ELEMENT.Equals(node.Name), "The specified node must be an '" + ATTRIBUTE_ELEMENT + "' node.");
		XmlAttributeCollection attributes = node.Attributes;
		string value = attributes.GetNamedItem("name").Value;
		StarStrike_Assertion.Assert(!StarStrike_Utils.IsEmpty(value), "name must not be null or empty.");
		string value2 = attributes.GetNamedItem("value").Value;
		StarStrike_Assertion.Assert(!StarStrike_Utils.IsEmpty(value2), "value must not be null or empty.");
		attributeMap.Add(value, value2);
	}

	public bool HasAttribute(string name)
	{
		return attributeMap[name] != null;
	}

	public string GetAttributeValue(string name)
	{
		if (attributeMap[name] != null)
		{
            //if (name == "mineralsPerSecond") attributeMap[name].ToString().Replace(".", ",");
            //else return attributeMap[name].ToString();
            return attributeMap[name].ToString().Replace(".", ",");
        }
		return string.Empty;
	}

	public string GetLevelNum()
	{
		return levelNum;
	}
}
