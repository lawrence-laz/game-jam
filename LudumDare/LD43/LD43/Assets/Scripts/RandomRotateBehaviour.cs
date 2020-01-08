using DG.Tweening;
using UnityEngine;

public class RandomRotateBehaviour : MonoBehaviour
{
    public float RotationSpeed;

    private Vector3 _rotationDirection;
    private Quaternion _target;

    private void OnEnable()
    {
        _rotationDirection = Random.onUnitSphere;
        var second = new Vector3();

        RotationSpeed += Random.Range(-100f, 100f);
        //transform.GetChild(0).localPosition = Vector3.Cross(_rotationDirection, Random.onUnitSphere) * 0.5f;
        //transform.DOLocalRotate(Random.onUnitSphere * 360, 1).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }

    private void Update()
    {
        //if (_target == transform.localRotation)
        //{
        //    _target = Random.rotation;
        //}

        //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _target, RotationSpeed);

        //transform.RotateAround(transform.position, _rotationDirection, Time.deltaTime * 90f);
        transform.RotateAround(transform.parent.TransformPoint(Vector3.zero), _rotationDirection, RotationSpeed * Time.deltaTime);
        //transform.Rotate(_rotationDirection * , Space.Self);
    }
}
