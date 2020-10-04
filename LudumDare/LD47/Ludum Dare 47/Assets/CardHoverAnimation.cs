using DG.Tweening;
using UnityEngine;

public class CardHoverAnimation : MonoBehaviour
{
    private const float _animationOffset = 0.3f;

    public bool IsHovered = false;
    
    private bool _isAnimated = false;
    private float _startingPosition;

    private void Start()
    {
        _startingPosition = transform.localPosition.y;
    }

    private void Update()
    {
        if (IsHovered && !_isAnimated)
        {
            _isAnimated = true;
            DOTween.Kill(transform);
            transform.DOLocalMoveY(_startingPosition + _animationOffset, 0.15f);
        }
        else if (!IsHovered && _isAnimated)
        {
            _isAnimated = false;
            DOTween.Kill(transform);
            transform.DOLocalMoveY(_startingPosition, 0.1f);
        }

        IsHovered = false;
    }
}
