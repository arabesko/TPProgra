using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PisoFlotante : MonoBehaviour
{
    private MeshRenderer _meshR;
    private Collider _collider;
    [SerializeField] bool isOnTheFlor;
    [SerializeField] private float _timeBeforeBlink = 2;
    [SerializeField] private float _timeBeforeBlinkCount;
    [SerializeField] private float _timeBetweenBlink = 0.35f;
    [SerializeField] private float _timeBetweenBlinkCount;
    [SerializeField] private int _timeToRevive = 4;
    private float _countBlink;
    public Material rxSolida;
    public Material rxTrans;
    public Material rxTransAll;
    private bool isSolid = true;
    private bool isDead;
    [SerializeField] private float _deadCount;


    void Start()
    {
        _meshR = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (isOnTheFlor)
        {
            _timeBeforeBlinkCount += Time.deltaTime;
        }
        else
        {
            if (isDead)
            {
                _meshR.material = rxTransAll;
            }
            else
            {
                _meshR.material = rxSolida;
            }
            _timeBeforeBlinkCount = 0;
            _countBlink = 0;
        }

        if (_timeBeforeBlinkCount > _timeBeforeBlink)
        {
            Blink();
        }

        if (isDead)
        {
            _deadCount += Time.deltaTime;
            if (_deadCount > _timeToRevive)
            {
                _collider.enabled = true;
                _meshR.material = rxSolida;
                isDead = false;
                _deadCount = 0;

            }
        }
    }


    void Blink()
    {
        _timeBetweenBlinkCount += Time.deltaTime;
        if (_timeBetweenBlinkCount > _timeBetweenBlink)
        {
            //cambiar material alternante
            if (isSolid)
            {
                //material trans
                _meshR.material = rxTrans;
                isSolid = false;
            }
            else
            {
                //material solido
                _meshR.material = rxSolida;
                isSolid = true;
            }
            _timeBetweenBlinkCount = 0;
            _countBlink += 1;

            if (_countBlink > 5)
            {
                _meshR.material = rxTransAll;
                _collider.enabled = false;
                isDead = true;
                isOnTheFlor = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnTheFlor = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isOnTheFlor = false;
    }
}