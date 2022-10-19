using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BallFollower : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    [SerializeField] private List<Color> _backColor;
    [SerializeField] private Camera _cam;
    public static BallFollower Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        transform.position = _ball.transform.position;
    }

    void Update()
    {
        SetPosY();
    }

    private void SetPosY()
    {
        if (transform.position.y > _ball.transform.position.y)
        {
            transform.position = _ball.transform.position;
        }
    }

    public void SetCameraBackroundColor(int i)
    {
        _cam.backgroundColor = _backColor[i];
    }
}