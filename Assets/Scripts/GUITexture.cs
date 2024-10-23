using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITexture : UnityEngine.UI.Image
{
    private Texture _texture;
    public Texture texture  
    { 
        get 
        { 
            return _texture; 
        } 
        set 
        {
            _texture = value;
            sprite = Sprite.Create(_texture as Texture2D, Rect.zero, new Vector2(0.5f, 0.5f));
        } 
    }
}
