using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SplitSpriteFromSheet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        
        
        /*
         *  make the imported sheet readable, Inspector-Advanced-Read/Write Enabled
            move it into your Resources (so you can load it via code)
            in script, load the sprite sheet:
         */
        Sprite[] sheet = Resources.LoadAll<Sprite> ("");

        if (null == sheet)
        {
            return;
        }
        foreach (Sprite sprite in sheet) {
            try
            {
                Texture2D tex = sprite.texture;
                Rect r = sprite.textureRect;
                Texture2D subtex = tex.CropTexture( (int)r.x, (int)r.y, (int)r.width, (int)r.height );
                byte[] data = subtex.EncodeToPNG();
                File.WriteAllBytes (Application.persistentDataPath + "/" + sprite.name + ".png", data);
            }
            catch (Exception e)
            {
                Debug.Log("Exception");
                System.IO.File.Delete(  Application.persistentDataPath + "/" + sprite.name + ".png" );

            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
