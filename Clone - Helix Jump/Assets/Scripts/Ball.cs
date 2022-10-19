using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private ParticleManager _particleManager;

    private Rigidbody _rb;
    [SerializeField] private float _jumpForce;
    private bool _isItFast = false;
    public bool isFinish = false;
    public bool IsMove = false;
    private GameObject _otherGameobject;

    private void Start()
    {
        IsMove = true;
        _isItFast = false;
        _rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 90;
    }

    private void Update()
    {
        IsCombo();
        _particleManager.TrailSetColorAndPlayParticle(_isItFast);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _otherGameobject = collision.gameObject;

        if (_otherGameobject.CompareTag("Finish"))
        {
            isFinish = true;
            IsMove = false;
            _particleManager.PlayConfettiParticle();
            UIManager.Instance.FinishScreen(isFinish);
            _particleManager.SplashImageAndParticleController(_otherGameobject, _isItFast);
            return;
        }

        ToBreakRung();
        CollisionEvent();
    }

    private void CollisionEvent()
    {
        GetComponent<TrailRenderer>().Clear();

        if (_otherGameobject.CompareTag("obstacle"))
        {
            _particleManager.SplashImageAndParticleController(_otherGameobject, _isItFast);
            ObstacleEvent();
        }
        else
        {
            _rb.AddForce(Vector3.up * _jumpForce);
            _particleManager.SplashImageAndParticleController(_otherGameobject, _isItFast);
        }
    }

    private void IsCombo()
    {
        if (_rb.velocity.sqrMagnitude > 80)
        {
            _rb.velocity = Vector3.one * -9f; // max Speed e eþitle;
            _isItFast = true;
            return;
        }
        _isItFast = false;
    }

    private void ToBreakRung()
    {
        if (_isItFast)
        {
            UIManager.Instance.SetScore(16);
            _otherGameobject.transform.parent.gameObject.SetActive(false);
        }
    }

    private void ObstacleEvent()
    {
        if (!_isItFast)
        {
            isFinish = false;
            IsMove = false;
            _particleManager.PlayConfettiParticle();
            UIManager.Instance.FinishScreen(isFinish);
        }
        else
        {
            _rb.AddForce(Vector3.up * _jumpForce);
            _particleManager.SplashImageAndParticleController(_otherGameobject, _isItFast);
        }
    }
}