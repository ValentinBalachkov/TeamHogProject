using System;
using System.Collections;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Action<Transform> OnDestroy;
    private bool _isGround;
    private bool _stopMove;

    public bool StopMoveProp
    {
        get => _stopMove;
        set => _stopMove = value;
    }

    private void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }
    

    public void Init(bool isGround)
    {
        _isGround = isGround;
    }

    private void FixedUpdate()
    {
        if (_stopMove)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, -300, 0), 0.1f);
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(30f);
        if (!_isGround)
        {
            OnDestroy?.Invoke(transform);
            OnDestroy = null;
            Destroy(gameObject);
        }
        else
        {
            _stopMove = true;
        }
        
    }
}
