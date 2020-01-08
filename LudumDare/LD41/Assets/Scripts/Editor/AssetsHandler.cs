using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetsHandler : AssetPostprocessor
{
    private void OnPreprocessTexture()
    {
        //var importer = assetImporter as TextureImporter;
        //TextureImporterSettings settings = new TextureImporterSettings
        //{
        //    filterMode = FilterMode.Point,
        //    textureType = TextureImporterType.Sprite,
        //    spriteMode = 0
        //};
        //importer.SetTextureSettings(settings);
    }
}
