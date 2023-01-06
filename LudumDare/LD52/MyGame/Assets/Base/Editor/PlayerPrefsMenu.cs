using UnityEditor;
using UnityEngine;

namespace Libs.Base.Editor
{
    public class PlayerPrefsMenu : MonoBehaviour
    {
        [MenuItem("DreamBit/Reset Player Prefs")]
        static void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}