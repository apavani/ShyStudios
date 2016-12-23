using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
public class JoyStickController : MonoBehaviour, IPlayerController {
    public event EventHandler<NavigationControlArgs> LeftControl;
    public event EventHandler<NavigationControlArgs> LevitateDown;
    public event EventHandler<NavigationControlArgs> LevitateUp;
    public event EventHandler<NavigationControlArgs> RightControl;
    private float moveX, moveY;
    private NavigationControlArgs nArgs;

    void Start()
    {
        nArgs = new NavigationControlArgs();
    }
    void Update () {
        //TCKInput.BindAxes("leftJoystick", actionHandler, actionPhase);
        moveX = TCKInput.GetAxis("leftJoystick", "Horizontal");
        moveY = TCKInput.GetAxis("leftJoystick", "Vertical");
        if(moveY>0)
        {
            this.GoUp();
        }
        else if(moveY<0)
        {
            this.GoDown();
        }

        if(moveX>0)
        {
            this.GoLeft();
        }
        else if (moveX < 0)
        {
            this.GoRight();
        }
    }

    void GoUp () {
        nArgs.movementVector = new Vector3(moveX, moveY);
        if(LevitateUp!=null)
        {
            LevitateUp(this, nArgs);
        }
	}

    void GoDown()
    {
        nArgs.movementVector = new Vector3(moveX, moveY);

        if (LevitateDown != null)
        {
            LevitateDown(this, nArgs);
        }
    }

    void GoLeft()
    {
        nArgs.movementVector = new Vector3(moveX, moveY);
        if (LeftControl != null)
        {
            LeftControl(this, nArgs);
        }
    }

    void GoRight()
    {
        nArgs.movementVector = new Vector3(moveX, moveY);
        if (RightControl != null)
        {
            RightControl(this, nArgs);
        }
    }
}
