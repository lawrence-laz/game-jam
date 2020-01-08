using Libs.Base.GameLogic;
using UnityEngine;

public class GoUpText : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime);

        var rect = (RectTransform)transform;
        if (rect.rect.height / 2 - rect.position.y + Screen.height < 0)
            FindObjectOfType<SceneLoadingBehaviour>().LoadScene();
    }
}
