using System;
using UnityEngine;

public class PlayerAimSystem : MonoBehaviour
{
    private Transform _player;
    private Transform _playerSprite;
    private GameObject _swordGameObject;
    private Transform _swordTransform;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerSprite = _player.Find("Player_Sprite");
        _swordGameObject = _player.Find("Sword").gameObject;
        _swordTransform = _swordGameObject.transform;
    }

    private void Update()
    {
        if (GameOverSystem.Instance.GameOver)
            return;

        UpdateSwordRotationBasedOnMouse();
        UpdateFacingSprite();
    }

    private void UpdateFacingSprite()
    {
        Vector3 facingDirection = InputX.MouseWorldPosition - _player.position;
        Vector3 scale = _playerSprite.localScale;
        scale.x = (facingDirection.x < 0 ? 1 : -1);
        _playerSprite.transform.localScale = scale;
    }

    private void UpdateSwordRotationBasedOnMouse()
    {
        _swordTransform.LookAt2D(InputX.MouseWorldPosition);
    }
}
