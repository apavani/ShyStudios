using System;
using UnityEngine;
using TouchControls;

public enum VelocityType
{
    constant,
    accelerating
}

public enum ControllerType
{
    accelerometer,
    joystick,
    button
}
public class CharacterMover : MonoBehaviour {

    public ControllerType controllerType;
    public VelocityType velocityType;
    public float movementSpeed;
    private float halfSizeX;
    private float halfSizeY;
    private CharacterController _controller;

    IPlayerController _control;
	// Use this for initialization
	void OnEnable () {
        halfSizeX = gameObject.GetComponent<Renderer>().bounds.extents.x/2;
        halfSizeY = gameObject.GetComponent<Renderer>().bounds.extents.y / 2;
        switch(controllerType)
        {
            case(ControllerType.accelerometer):
            {
                    _control = GameObject.Find("Controller").GetComponent<AccelerometerControl>();
                    break;
            }
            case (ControllerType.joystick):
            {
                    _control = GameObject.Find("Controller").GetComponent<JoyStickController>();
                    break;
            }
            case (ControllerType.button):
            {
                    _control = GameObject.Find("Controller").GetComponent<ButtonController>();
                    break;
            }
            default:
                break;
        }

        _control.LeftControl += new EventHandler<NavigationControlArgs>(GoLeft);
        _control.RightControl += new EventHandler<NavigationControlArgs>(GoRight);
        _control.LevitateUp += new EventHandler<NavigationControlArgs>(GoUp);
        _control.LevitateDown += new EventHandler<NavigationControlArgs>(GoDown);
        _controller = GetComponent<CharacterController>();

    }


    private void GoLeft(object sender, NavigationControlArgs e){
        this._updatePosition(e);
    }

    private void GoRight(object sender, NavigationControlArgs e){
        this._updatePosition(e);
    }

    private void GoUp(object sender, NavigationControlArgs e) {
        this._updatePosition(e);
    }

    private void GoDown(object sender, NavigationControlArgs e){
        this._updatePosition(e);
    }

    private void _keepWithinBounds()
    {
        if (transform.position.x < MapBounds.minX + halfSizeX)
        {
            transform.position = new Vector3(MapBounds.minX + halfSizeX, transform.position.y);
        }

        if (transform.position.x > MapBounds.maxX - halfSizeX)
        {
            transform.position = new Vector3(MapBounds.maxX - halfSizeX, transform.position.y);
        }

        if (transform.position.y > MapBounds.maxY - halfSizeY)
        {
            transform.position = new Vector3(transform.position.x, MapBounds.maxY - halfSizeY);
        }

        if (transform.position.y < MapBounds.minY + halfSizeY)
        {
            transform.position = new Vector3(transform.position.x, MapBounds.minY + halfSizeY);
        }
    }

    private void _updatePosition(NavigationControlArgs e)
    {
        if (velocityType == VelocityType.constant)
        {
            _controller.Move(movementSpeed * Time.deltaTime * e.movementVector.normalized);
        }
        else if (velocityType == VelocityType.accelerating)
        {
            _controller.Move(movementSpeed * e.movementVector);
        }
        this._keepWithinBounds();
        GameplayMessageCenter.Instance.SendPlayerPositionData(transform.position);

    }
        void OnDisable(){
            _control.LeftControl -= new EventHandler<NavigationControlArgs>(GoLeft);
            _control.RightControl -= new EventHandler<NavigationControlArgs>(GoRight);
            _control.LevitateUp -= new EventHandler<NavigationControlArgs>(GoUp);
            _control.LevitateDown -= new EventHandler<NavigationControlArgs>(GoDown);
        }
}
