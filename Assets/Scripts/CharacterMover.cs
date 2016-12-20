using System;
using UnityEngine;
using TouchControls;

public enum VelocityType
{
    constant,
    accelerating
}
public class CharacterMover : MonoBehaviour {
    
    public float movementSpeed;
    public VelocityType velocityType;
    private float halfSizeX;
    private float halfSizeY;
    private CharacterController _controller;

    IPlayerController _control;
	// Use this for initialization
	void OnEnable () {
        halfSizeX = gameObject.GetComponent<Renderer>().bounds.extents.x/2;
        halfSizeY = gameObject.GetComponent<Renderer>().bounds.extents.y / 2;
        _control = GameObject.Find("Controller").GetComponent<AccelerometerControl>();
        _control.LeftControl += new EventHandler<NavigationControlArgs>(GoLeft);
        _control.RightControl += new EventHandler<NavigationControlArgs>(GoRight);
        _control.LevitateUp += new EventHandler<NavigationControlArgs>(GoUp);
        _control.LevitateDown += new EventHandler<NavigationControlArgs>(GoDown);
        _controller = GetComponent<CharacterController>();

    }


    private void GoLeft(object sender, NavigationControlArgs e){
        if(velocityType==VelocityType.constant)
            _controller.Move(movementSpeed * Time.deltaTime * new Vector3(e.movementVector.x, e.movementVector.y).normalized);
        else if(velocityType==VelocityType.accelerating)
            _controller.Move(movementSpeed * new Vector3(e.movementVector.x, e.movementVector.y));

        this._keepWithinBounds();
    }

    private void GoRight(object sender, NavigationControlArgs e){
        if (velocityType == VelocityType.constant)
            _controller.Move(movementSpeed * Time.deltaTime * new Vector3(e.movementVector.x, e.movementVector.y).normalized);
        else if (velocityType == VelocityType.accelerating)
            _controller.Move(movementSpeed * new Vector3(e.movementVector.x, e.movementVector.y));

        this._keepWithinBounds();
    }

    private void GoUp(object sender, NavigationControlArgs e) {
        if (velocityType == VelocityType.constant)
            _controller.Move(movementSpeed * Time.deltaTime * new Vector3(e.movementVector.x, e.movementVector.y).normalized);
        else if (velocityType == VelocityType.accelerating)
            _controller.Move(movementSpeed * new Vector3(e.movementVector.x, e.movementVector.y));

        this._keepWithinBounds();
    }

    private void GoDown(object sender, NavigationControlArgs e){
        if (velocityType == VelocityType.constant)
            _controller.Move(movementSpeed * Time.deltaTime * new Vector3(e.movementVector.x, e.movementVector.y).normalized);
        else if (velocityType == VelocityType.accelerating)
            _controller.Move(movementSpeed * new Vector3(e.movementVector.x, e.movementVector.y));

        this._keepWithinBounds();
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
}
