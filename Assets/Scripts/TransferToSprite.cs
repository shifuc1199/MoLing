using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEngine.UI;
#if UNITY_EDITOR
public class TransferToSprite : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
       
            TextureImporter texImpoter = assetImporter as TextureImporter;
            texImpoter.textureType = TextureImporterType.Sprite;
        
    }
          
}
#endif
