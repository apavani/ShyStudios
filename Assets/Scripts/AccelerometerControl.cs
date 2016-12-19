using System;
using UnityEngine;

namespace TouchControls
{
    public class AccelerometerControl : MonoBehaviour, IPlayerController
    {
        [ Range( 1f, 10f ) ]
        public float sensitivity = 4f;

        [Range( 0f, 90f )]
        public float fullTiltAngle = 25f;

        [Range( -50f, 50f )]
        public float centreAngleOffset = 0f;

        public float centerRange;


        private float forwardAxis;
        private float sidewaysAxis;
        private bool jetPressed;

        private NavigationControlArgs nArgs;
        public event EventHandler<NavigationControlArgs>LeftControl;
        public event EventHandler<NavigationControlArgs>RightControl;
        public event EventHandler<NavigationControlArgs>Levitate;

        void Start()
        {
            nArgs = new NavigationControlArgs();
        }
        // Update
        void Update()
        {
            if( Input.acceleration != Vector3.zero )
            {
                float forwardAngle = Mathf.Atan2( Input.acceleration.x, -Input.acceleration.y ) * Mathf.Rad2Deg + centreAngleOffset;
                float sidewaysAngle = Mathf.Atan2( Input.acceleration.z, -Input.acceleration.y ) * Mathf.Rad2Deg + centreAngleOffset;
                forwardAxis = ( Mathf.InverseLerp( -fullTiltAngle, fullTiltAngle, forwardAngle ) * 2f - 1f ) * sensitivity;
                sidewaysAxis = ( Mathf.InverseLerp( -fullTiltAngle, fullTiltAngle, sidewaysAngle ) * 2f - 1f ) * sensitivity;
            }
            else
            {
                forwardAxis = 0f;
                sidewaysAxis = 0f;
            }

            //Return Values to the subscribing class
            if (Input.acceleration.x < -centerRange/2)
            {
                this._goLeft();
            }
            else if(Input.acceleration.x > centerRange / 2)
            {
                this._goRight();
            }

            if(jetPressed)
            {
                this.GoUp();
            }
            else
            {
                this.GoDown();
            }

        }

        private void _goLeft()
        {
            nArgs.value = sidewaysAxis;
            if(LeftControl!=null)
            LeftControl(this,nArgs);
        }

        private void _goRight()
        {
            nArgs.value = sidewaysAxis;
            if (RightControl != null)
                RightControl(this, nArgs);
        }

        //ManagedByUI
        private void GoUp()
        {
            nArgs.levitate = true;
            if (Levitate != null)
                Levitate(this, nArgs);
        }


        private void GoDown()
        {
            nArgs.levitate = false;
            if (Levitate != null)
                Levitate(this, nArgs);
        }
        public void JetButtonPressedDown()
        {
            jetPressed = true;
        }

        public void JetButtonPressedUp()
        {
            jetPressed = false;
        }


    }
}

public class NavigationControlArgs : EventArgs
{
    public float value;
    public bool levitate;
}