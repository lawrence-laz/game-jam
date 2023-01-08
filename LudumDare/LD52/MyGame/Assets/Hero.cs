using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private Rigidbody2D _body;
    private float _originalMass;
    private Holder _holder;

    private void Start() {
        _body = GetComponent<Rigidbody2D>();
        _originalMass = _body.mass;
        _holder = GetComponentInChildren<Holder>();
    }

    private void Update() 
    {
        _body.mass = _originalMass + _holder.Items.Sum(item => item.GetComponent<Rigidbody2D>()?.mass ?? 0);
    }
}
