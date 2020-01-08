using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BoltShooterBehaviour : MonoBehaviour
{
    public GameObject BoltPrefab;
    public GameObject[] VisualCharges;
    public UnityEvent OnCast;

    [SerializeField]
    private int _charges = 0;
    private Transform _model;
    private Vector3 _startingPosition;
    private Vector3 _startingRotation;
    private bool _cooldownReady = true;
    private Vector3 _startPositionOffset = new Vector3(0, -0.2f, 0); // Used to avoid hitting unintenional things

    public int Charges
    {
        get { return _charges; }
        set
        {
            _charges = value;
            UpdateCharges();
        }
    }

    private void Start()
    {
        _model = transform.Find("Orb_Model");
        _startingPosition = _model.localPosition;
        _startingRotation = _model.localRotation.eulerAngles;
    }

    public void Shoot(Vector3 target)
    {
        if (!_cooldownReady)
        {
            Debug.Log("Cooldown not ready to shoot bolt.");
            return;
        }

        if (Charges <= 0)
        {
            Debug.Log("Out of charges");
            // TODO: Should say something like "I need a sacrifice....";
            return;
        }

        _cooldownReady = false;
        var animation = DOTween.Sequence()
            .Append(_model.DOLocalMove(_model.InverseTransformDirection(Camera.main.transform.forward) * -0.2f, 0.5f).SetRelative(true).SetEase(Ease.OutCubic))
            .AppendCallback(() => 
            {
                var bolt = Instantiate(BoltPrefab).GetComponent<MagicBoltBehaviour>();

                bolt.transform.position = Camera.main.transform.position; //transform.position + _startPositionOffset;
                bolt.transform.Translate(_startPositionOffset, Space.Self);
                bolt.SetTarget(target);
                DecreaseCharge();
                OnCast.Invoke();
            })
            .Append(_model.DOLocalMove(_model.InverseTransformDirection(Camera.main.transform.forward), 0.3f).SetRelative(true).SetEase(Ease.OutElastic))
            .Append(_model.DOLocalMove(_startingPosition, 0.1f))
            .AppendCallback(() =>
            {
                _cooldownReady = true;
            });
    }

    public void IncreaseCharge()
    {
        Charges++;
    }

    public void DecreaseCharge()
    {
        Charges--;
    }

    public void UpdateCharges()
    {
        for (int i = 0; i < VisualCharges.Length; i++)
        {
            VisualCharges[i].SetActive(Charges > i);
        }
    }
}
