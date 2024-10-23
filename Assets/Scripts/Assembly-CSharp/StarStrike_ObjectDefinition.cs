using System.Collections;
using System.Xml;

public class StarStrike_ObjectDefinition
{
	private string name;

	private int id;

	private Hashtable attributeMap;

	private Hashtable childObjectMap;

	private ArrayList levelArray;

	private static string OBJECT_ELEMENT = "object";

	private static string ATTRIBUTE_ELEMENT = "attribute";

	public StarStrike_ObjectDefinition(XmlNode node)
	{
		StarStrike_Assertion.Assert(OBJECT_ELEMENT.Equals(node.Name), "The specified node must be an '" + OBJECT_ELEMENT + "' node.");
		name = node.Attributes.GetNamedItem("name").Value;
		if (node.Attributes.GetNamedItem("id") != null)
		{
			id = int.Parse(node.Attributes.GetNamedItem("id").Value);
		}
		else
		{
			id = -1;
		}
		StarStrike_Assertion.Assert(!StarStrike_Utils.IsEmpty(name), "The name of the object must not be null.");
		attributeMap = new Hashtable();
		levelArray = new ArrayList();
		XmlNodeList childNodes = node.ChildNodes;
		foreach (XmlNode item in childNodes)
		{
			if (OBJECT_ELEMENT.Equals(item.Name))
			{
				ParseChildObject(item);
			}
			else if (ATTRIBUTE_ELEMENT.Equals(item.Name))
			{
				ParseAttribute(item);
			}
			else if ("level".Equals(item.Name))
			{
				ParseLevel(item);
			}
		}
	}

	public static StarStrike_ObjectDefinition ParseObject(string xmlText)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xmlText);
		XmlNode firstChild = xmlDocument.FirstChild;
		StarStrike_Assertion.Assert(OBJECT_ELEMENT.Equals(firstChild.Name), "The root element must be '" + OBJECT_ELEMENT + "'.");
		return new StarStrike_ObjectDefinition(firstChild);
	}

	private void ParseLevel(XmlNode node)
	{
		FD_ObjectLevelDefinition value = new FD_ObjectLevelDefinition(node);
		levelArray.Add(value);
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

	private void ParseChildObject(XmlNode node)
	{
		if (childObjectMap == null)
		{
			childObjectMap = new Hashtable();
		}
		StarStrike_ObjectDefinition starStrike_ObjectDefinition = new StarStrike_ObjectDefinition(node);
		childObjectMap.Add(starStrike_ObjectDefinition.Name(), starStrike_ObjectDefinition);
	}

	public string Name()
	{
		return name;
	}

	public string GetAttributeValue(string name)
	{
		string text = (string)attributeMap[name];
		StarStrike_Assertion.Assert(text != null, "Value for the specified attribute name was not found.");
		return text;
	}

	public ArrayList GetLevelArray(string name)
	{
		return levelArray;
	}

	public bool HasAttribute(string name)
	{
		return attributeMap[name] != null;
	}

	public StarStrike_ObjectDefinition GetChild(string name)
	{
		StarStrike_Assertion.Assert(childObjectMap != null, "Trying to retrieve a child object when this object has no children.");
		return (StarStrike_ObjectDefinition)childObjectMap[name];
	}

	public string GetChildById(int id)
	{
		StarStrike_Assertion.Assert(childObjectMap != null, "Trying to retrieve a child object when this object has no children.");
		int num = 0;
		foreach (DictionaryEntry item in childObjectMap)
		{
			if (id == num)
			{
				return item.Key.ToString();
			}
			num++;
		}
		return null;
	}

	public int GetIdByIndex(int id)
	{
		StarStrike_Assertion.Assert(childObjectMap != null, "Trying to retrieve a child object when this object has no children.");
		int num = 0;
		foreach (DictionaryEntry item in childObjectMap)
		{
			if (id == num)
			{
				StarStrike_ObjectDefinition starStrike_ObjectDefinition = (StarStrike_ObjectDefinition)childObjectMap[item.Key.ToString()];
				return starStrike_ObjectDefinition.id;
			}
			num++;
		}
		return -1;
	}

	public int GetId()
	{
		return id;
	}

	public int GetTotalChildren()
	{
		return childObjectMap.Count;
	}
}
