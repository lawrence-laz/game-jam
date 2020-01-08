using DG.Tweening;
using System;
using UnityEngine;

public class GroundShrinkingSystem : Singleton<GroundShrinkingSystem>
{
    private GroundComponent _ground;
    private ColliderToMesh _groundMeshBuilder;
    private ColliderToMesh _groundShadowMeshBuilder;
    private EllipseCollider2D _groundCollider;
    private EllipseCollider2D _groundShadowCollider;

    private void Start()
    {
        _ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<GroundComponent>();
        _groundMeshBuilder = _ground.transform.Find("Ground_Sprite").GetComponent<ColliderToMesh>();
        _groundCollider = _groundMeshBuilder.GetComponent<EllipseCollider2D>();
        _groundShadowMeshBuilder = _ground.transform.Find("GroundShadow_Sprite").GetComponent<ColliderToMesh>();
        _groundShadowCollider = _groundShadowMeshBuilder.GetComponent<EllipseCollider2D>();

        DOTween.To(() => _ground.Size, x => _ground.Size = x, 0.1f, _ground.Duration);
    }

    private void Update()
    {
        if (GameOverSystem.Instance.GameOver || VictorySystem.Instance.Victory)
            return;

        RebuildGround();
    }

    private void RebuildGround()
    {
        _groundCollider.radiusX = _ground.Width;
        _groundCollider.radiusY = _ground.Height;
        _groundCollider.ResetPoints();
        _groundMeshBuilder.BuildMesh();
        _groundShadowCollider.radiusX = _ground.Width;
        _groundShadowCollider.radiusY = _ground.Height;
        _groundShadowCollider.ResetPoints();
        _groundShadowMeshBuilder.BuildMesh();
    }

    //UNUSED
    private void Shrink()
    {
        _ground.Size = Mathf.MoveTowards(_ground.Size, 0, _ground.Duration);
    }
}
