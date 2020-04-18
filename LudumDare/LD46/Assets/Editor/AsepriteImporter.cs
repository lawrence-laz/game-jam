using UnityEditor;
using UnityEngine;

public class AsepriteImporter : AssetPostprocessor
{
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
                WorkingDirectory = $"{Application.dataPath}/Resources".Replace('/', '\\'),
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                FileName = "aseprite.exe",
                Arguments = "-b spritesheet.aseprite --ignore-layer Background --save-as {slice}.png",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                
            };

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
