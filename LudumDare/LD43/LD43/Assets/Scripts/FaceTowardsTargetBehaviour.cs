using UnityEngine;

public class FaceTowardsTargetBehaviour : MonoBehaviour
{
    private NoticeBehaviour _notice;

    private void Start()
    {
        _notice = GetComponent<NoticeBehaviour>();
    }

    private void Update()
    {
        if (_notice != null )
        {
            if (_notice.HasNoticed)
            {
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    Quaternion.LookRotation(transform.position.DirectionTo(_notice.LastSightPosition), Vector3.up),
                    50);
                var rotfix = transform.rotation.eulerAngles;
                rotfix.x = 0;
                rotfix.z = 0;
                transform.rotation = Quaternion.Euler(rotfix);
            }
            else if (_notice.SecondaryAttraction)
            {
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    Quaternion.LookRotation(transform.position.DirectionTo(_notice.SecondaryAttractionPosition), Vector3.up),
                    50);
                var rotfix = transform.rotation.eulerAngles;
                rotfix.x = 0;
                rotfix.z = 0;
                transform.rotation = Quaternion.Euler(rotfix);
            }
        }
    }
}
