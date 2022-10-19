using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMove : MonoBehaviour
{
    [SerializeField] public enum Inputs { MouseInput, TouchInput }
    public Inputs inputs;

    [SerializeField] private Ball _ball;
    private Touch _firstTouchPos;
    private Vector2 _xMove;
    [SerializeField] float _speed;

    private void Update()
    {
        SelectInput();
    }
    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            _firstTouchPos = Input.GetTouch(0);
        }
        if (_firstTouchPos.phase == TouchPhase.Moved)
        {
            _xMove = _firstTouchPos.deltaPosition;
            _firstTouchPos = Input.GetTouch(0);

            PlayerMoveSwerve();
        }
        if (Input.GetMouseButtonUp(0))
        {
            _xMove = Vector2.zero;
        }
    }

    private void PlayerMoveSwerve()
    {
        float playerMoveX = Time.deltaTime * _speed * -_xMove.x;
        Vector3 direction = new(0, playerMoveX, 0);
        transform.Rotate(direction);

        //this.transform.Translate(direction);
    }

    private void MouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            float asd = Input.GetAxis("Mouse X") * _speed;
            transform.Rotate(0, -asd, 0);
        }
    }

    private void SelectInput()
    {
        if (!_ball.IsMove) return;

        switch (inputs)
        {
            case Inputs.MouseInput:
                MouseInput();
                break;
            case Inputs.TouchInput:
                TouchInput();
                break;
            default:
                break;
        }
    }
}