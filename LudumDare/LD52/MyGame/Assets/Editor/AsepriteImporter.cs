using UnityEditor;
using UnityEngine;

public class AsepriteImporter : AssetPostprocessor
{
    public AsepriteImporter()
    {
        // Unity.Collections.LowLevel.Unsafe.UnsafeUtility.ForgiveLeaks();
    }

    static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        var refresh = false;

        foreach (string asset in importedAssets)
        {
            if (!asset.EndsWith(".aseprite"))
                continue;

            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WorkingDirectory = $"{Application.dataPath}/Sprites",
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                FileName = "aseprite",
                Arguments = "-b spritesheet.aseprite --ignore-layer Background --save-as {slice}.png",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,

            };

            // aseprite -b spritesheet.aseprite --ignore-layer Background --save-as {slice}.png

            System.Diagnostics.Process.Start(startInfo).WaitForExit();

            refresh = true;
        }

        if (refresh)
        {
            EditorApplication.delayCall += ImportSprites;
        }
    }

    private static void ImportSprites()
    {
        EditorApplication.delayCall -= ImportSprites;

        AssetDatabase.Refresh();
    }
}
