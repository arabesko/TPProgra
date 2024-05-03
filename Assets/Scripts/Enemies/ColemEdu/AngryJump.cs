using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryJump : MonoBehaviour
{
    [SerializeField] private bool _isInZone = false;
    [SerializeField] private int _timeOutsideMax = 5;
    [SerializeField] private float _timeOutsideMaxCount;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!_isInZone)
        {
            _timeOutsideMaxCount += Time.deltaTime;

            if (_timeOutsideMaxCount >= _timeOutsideMax)
            {
                _animator.SetTrigger("isJumpAttacking");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            _isInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isInZone = false;
    }
}
