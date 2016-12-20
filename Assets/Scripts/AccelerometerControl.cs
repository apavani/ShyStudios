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

        public float centerRangeX;
        public float centerRangeY;
        
        private float forwardAxis;
        private float sidewaysAxis;
        private bool jetPressed;

        private NavigationControlArgs nArgs;
        public event EventHandler<NavigationControlArgs>LeftControl;
        public event EventHandler<NavigationControlArgs>RightControl;
        public event EventHandler<NavigationControlArgs> LevitateUp;
        public event EventHandler<NavigationControlArgs> LevitateDown;

        void Start()
        {
            nArgs = new NavigationControlArgs();
        }
        // Update
        void Update()
        {
            //Return Values to the subscribing class
            if (Input.acceleration.x < -centerRangeX/2)
            {
                this._goLeft();
            }
            else if(Input.acceleration.x >= centerRangeX / 2)
            {
                this._goRight();
            }

            if (Input.acceleration.y < -centerRangeY / 2)
            {
                this._goDown();
            }
            else if (Input.acceleration.y >= centerRangeY / 2)
            {
                this._goUp();
            }
            /*
            if(jetPressed)
            {
                this._goUp();
            }
            else
            {
                this._goDown();
            }
            */

        }

        private void _goLeft()
        {
            nArgs.movementVector = Input.acceleration;
            if(LeftControl!=null)
                LeftControl(this,nArgs);
        }

        private void _goRight()
        {
            nArgs.movementVector = Input.acceleration;
            if (RightControl != null)
                RightControl(this, nArgs);
        }

        //ManagedByUI
        private void _goUp()
        {
            nArgs.movementVector = Input.acceleration;
            if (LevitateUp != null)
                LevitateUp(this, nArgs);
        }


        private void _goDown()
        {
            nArgs.movementVector = Input.acceleration;
            if (LevitateDown != null)
                LevitateDown(this, nArgs);
        }

        private void GoUp()
        {
            if (LevitateUp != null)
                LevitateUp(this, null);
        }

        private void GoDown()
        {
            if (LevitateDown != null)
                LevitateDown(this, null);
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