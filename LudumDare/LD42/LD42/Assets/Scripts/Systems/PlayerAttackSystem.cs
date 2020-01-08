using DG.Tweening;
using UnityEngine;


public class PlayerAttackSystem : MonoBehaviour
{
    private GameObject _swordGameObject;
    private Transform _swordTransform;
    private Transform _swordSpriteTransform;
    private WeaponComponent _weapon;

    private Vector3 _initialSwordSpritePosition;
    private Sequence _attackSequence;

    private Vector3 _HitOffset { get { return Vector3.up * _weapon.Range; } }

    private void Start()
    {
        _swordGameObject = GameObject.FindGameObjectWithTag("Player").transform.Find("Sword").gameObject;
        _swordTransform = _swordGameObject.transform;
        _swordSpriteTransform = _swordTransform.Find("Sword_Sprite").transform;
        _weapon = _swordGameObject.GetComponent<WeaponComponent>();

        _initialSwordSpritePosition = _swordSpriteTransform.localPosition;
    }

    private void Update()
    {
        HandleAttackActionBasedOnInput();
    }

    private void HandleAttackActionBasedOnInput()
    {
        if (Input.GetMouseButtonDown(0) && _weapon.IsReady && !GameOverSystem.Instance.GameOver)
        {
            DoAttackAnimation();
        }
    }

    private void DoAttackAnimation()
    {
        if (_attackSequence != null)
        {
            _attackSequence.Kill();
            _attackSequence = null;
            _swordSpriteTransform.localPosition = _initialSwordSpritePosition;
        }

        _attackSequence = DOTween.Sequence()
            .AppendCallback(() => _swordSpriteTransform.GetComponent<Collider2D>().enabled = true)
            .AppendCallback(() => _weapon.LastTimeAttackedAt = Time.time)
            .Append(_swordSpriteTransform.DOLocalMove(_HitOffset, _weapon.Speed))
            .Append(_swordSpriteTransform.DOLocalMove(_HitOffset, 0.1f))
            .AppendCallback(() => _swordSpriteTransform.GetComponent<Collider2D>().enabled = false)
            .Append(_swordSpriteTransform.DOLocalMove(_initialSwordSpritePosition, _weapon.Speed * 2))
            .OnComplete(() => _attackSequence = null)
            .Play();
    }
}
