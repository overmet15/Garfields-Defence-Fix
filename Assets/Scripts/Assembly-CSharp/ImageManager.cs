using System.Collections.Generic;
using UnityEngine;

public sealed class ImageManager
{
	private static readonly ImageManager instance = new ImageManager();

	private Dictionary<string, Texture2D> images;

	public static ImageManager Instance
	{
		get
		{
			return instance;
		}
	}

	private ImageManager()
	{
		images = new Dictionary<string, Texture2D>();
	}

	public Texture2D GetImage(string path)
	{
		if (!images.ContainsKey(path))
		{
			Texture2D texture2D = Resources.Load(path) as Texture2D;
			if (texture2D == null)
			{
				return null;
			}
			images.Add(path, texture2D);
		}
		return images[path];
	}

	public void UnloadImage(string path)
	{
		if (images.ContainsKey(path))
		{
			images.Remove(path);
		}
	}
}
