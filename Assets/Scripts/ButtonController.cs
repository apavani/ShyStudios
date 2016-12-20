using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour, IPlayerController {
    public event EventHandler<NavigationControlArgs> LeftControl;
    public event EventHandler<NavigationControlArgs> LevitateDown;
    public event EventHandler<NavigationControlArgs> LevitateUp;
    public event EventHandler<NavigationControlArgs> RightControl;

    private bool leftPressed, rightPressed, upPressed, downPressed;
    Button button;
    private NavigationControlArgs nArgs;
    // Use this for initialization
    void Start () {
        nArgs = new NavigationControlArgs();
	}
	
    void Update()
    {
        if (downPressed)
            GoDown();
        if (upPressed)
            GoUp();
        if (leftPressed)
            GoLeft();
        if (rightPressed)
            GoRight();
    }

	public void GoLeft () {
        nArgs.movementVector = Vector3.left;
        if (LeftControl != null)
            LeftControl(this, nArgs);
    }

    public void GoRight()
    {
        nArgs.movementVector = Vector3.right;

        if (RightControl != null)
            RightControl(this, nArgs);
    }

    public void GoUp()
    {
        nArgs.movementVector = Vector3.up;

        if (LevitateUp != null)
            LevitateUp(this, nArgs);
    }

    public void GoDown()
    {
        nArgs.movementVector = Vector3.down;

        if (LevitateDown != null)
            LevitateDown(this, nArgs);
    }

    public void LeftDown()
    {
        this.leftPressed = true;
    }

    public void LeftUp()
    {
        this.leftPressed = false;
    }

    public void RightDown()
    {
        this.rightPressed = true;
    }

    public void RightUp()
    {
        this.rightPressed = false;

    }
    public void UpDown()
    {
        this.upPressed = true;
    }

    public void UpUp()
    {
        this.upPressed = false;

    }
    public void DownDown()
    {
        this.downPressed = true;
    }

    public void DownUp()
    {
        this.downPressed = false;
    }
}
