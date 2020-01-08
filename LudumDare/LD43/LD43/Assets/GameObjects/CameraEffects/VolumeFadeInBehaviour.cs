using DG.Tweening;
using UnityEngine;

public class VolumeFadeInBehaviour : MonoBehaviour
{
    public float InitialOffset = 100;
    public float TimeToChange = 2;

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        transform.position = Camera.main.transform.position + Vector3.up * InitialOffset;
        transform.SetParent(Camera.main.transform);
        transform.DOLocalMove(Vector3.zero, TimeToChange);
    }

    private void LateUpdate()
    {
        if (!_collider.enabled)
        {
            _collider.enabled = true;
        }
    }
}
