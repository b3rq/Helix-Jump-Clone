using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    #region Materials
    [Header("Materials")]
    [SerializeField] private Material _trailMaterial;
    [SerializeField] private Material _myMaterial;
    [SerializeField] private Material _lerpMaterial;
    #endregion

    [Header("Particles")]
    [SerializeField] private ParticleSystem _confetti;
    [SerializeField] private ParticleSystem _confetti2;
    [SerializeField] private ParticleSystem _splashParticle;
    [SerializeField] private GameObject _fireLineParticle;
    [SerializeField] private GameObject _ballSplashImage;
    [SerializeField] public ParticleSystem _FastTriggerParticle;

    private Transform _BallPos;

    private void Start()
    {
        _BallPos = _ball.transform;

        _myMaterial.color = _ball.GetComponent<MeshRenderer>().material.color;
    }

    public void SplashImageAndParticleController(GameObject other, bool SpeedCheck)
    {
        if (!SpeedCheck)
        {
            Vector3 ballSplashImagePos = new(_ball.transform.position.x, _ball.transform.position.y - 0.14f, _ball.transform.position.z);
            StartSplashParticleAndSplashImage(ballSplashImagePos, null, other, _ballSplashImage, false);

            Vector3 ballSplashParticlePos = new(_BallPos.position.x, _BallPos.position.y - 0.12f, _BallPos.position.z - 1);
            StartSplashParticleAndSplashImage(ballSplashParticlePos, _splashParticle, other, null, true);
        }
        else
        {
            Vector3 FastTriggerParticlePos = new(_ball.transform.position.x, _ball.transform.position.y - 0.14f, _ball.transform.position.z);
            ParticleSystem ParticlePlay = Instantiate(_FastTriggerParticle, FastTriggerParticlePos, Quaternion.identity);
            ParticlePlay.Play();
        }
    }

    public void TrailSetColorAndPlayParticle(bool SpeedCheck)
    {
        if (SpeedCheck)
        {
            _fireLineParticle.SetActive(true);
            _trailMaterial.color = Color.Lerp(_trailMaterial.color, _lerpMaterial.color, 0.5f);
            _ball.GetComponent<MeshRenderer>().material.color = Color.Lerp(_ball.GetComponent<MeshRenderer>().material.color, _lerpMaterial.color, 0.5f);
        }
        else
        {
            _fireLineParticle.SetActive(false);
            _trailMaterial.color = _myMaterial.color;
            _ball.GetComponent<MeshRenderer>().material.color = _myMaterial.color;
        }
    }

    private void StartSplashParticleAndSplashImage(Vector3 instantiatePos, ParticleSystem splashParticle, GameObject other, GameObject splashImage, bool isParticle)
    {
        if (isParticle)
        {
            ParticleSystem PlayParticle = Instantiate(splashParticle, instantiatePos, Quaternion.identity);
            PlayParticle.transform.SetParent(other.transform);
            PlayParticle.Play();
            return;
        }

        GameObject splashImages = Instantiate(_ballSplashImage, instantiatePos, _ball.transform.rotation);
        splashImages.transform.SetParent(other.transform);
    }

    public void PlayConfettiParticle()
    {
        _confetti.Play();
        _confetti2.Play();
    }
}