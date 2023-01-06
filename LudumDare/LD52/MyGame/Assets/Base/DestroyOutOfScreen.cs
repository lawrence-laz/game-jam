using UnityEngine;

public class DestroyOutOfScreen : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void OnEnable()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_renderer.isVisible)
            Destroy(gameObject);
    }
}
