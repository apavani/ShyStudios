/*******************************************************
 * 													   *
 * Asset:		 Touch Controls Kit         		   *
 * Script:		 TCKJoystick.cs                        *
 * 													   *
 * Copyright(c): Victor Klepikov					   *
 * Support: 	 http://bit.ly/vk-Support			   *
 * 													   *
 * mySite:       http://vkdemos.ucoz.org			   *
 * myAssets:     http://u3d.as/5Fb                     *
 * myTwitter:	 http://twitter.com/VictorKlepikov	   *
 * 													   *
 *******************************************************/


using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TouchControlsKit.Utils;

namespace TouchControlsKit
{
    /// <summary>
    /// isStatic = true;  - Switches a joystick in a static mode, in which it is only at the specified position.
    /// isStatic = false; - Switches a joystick in the dynamic mode, in this mode, it operates within the touch zone.
    /// </summary>

    [RequireComponent( typeof( Image ) )]
    public class TCKJoystick : AxesBasedController,
        IPointerUpHandler, IPointerDownHandler, IDragHandler, IPointerClickHandler
    {
        public Image joystickImage, joystickBackgroundImage;
        public RectTransform joystickRT, joystickBackgroundRT;
        
        [SerializeField]
        private bool isStatic = true;
                
        public float borderSize = 5.85f;
        private Vector2 borderPosition = Vector2.zero;

        public bool smoothReturn = false;
        public float smoothFactor = 7f;

        private float xVel, yVel;

        private Color32 joystickNativeColor, backgroundNativeColor;


        // Joystick Mode
        public bool IsStatic
        {
            get { return isStatic; }
            set
            {
                if( isStatic == value ) return;
                isStatic = value;
            }
        }

        
        // Control Awake
        public override void ControlAwake()
        {
            base.ControlAwake();

            if( Application.isPlaying )
                SetTransparency();
        }


        // Set Enable
        protected override void SetEnable()
        {
            base.SetEnable();
            joystickImage.enabled = enable;
            joystickBackgroundImage.enabled = enable;
        }

        // Set Visible
        protected override void SetVisible()
        {
            if( visible )
            {
                joystickImage.color = joystickNativeColor;
                joystickBackgroundImage.color = backgroundNativeColor;
            }
            else
            {
                joystickNativeColor = joystickImage.color;
                backgroundNativeColor = joystickBackgroundImage.color;

                joystickImage.color = ( Color32 )Color.clear;
                joystickBackgroundImage.color = ( Color32 )Color.clear;
            }
        }


        // Update Position
        protected override void UpdatePosition( Vector2 touchPos )
        {
            if( !axisX.enabled && !axisY.enabled )
                return;

            base.UpdatePosition( touchPos );

            if( touchDown )
            {
                GetCurrentPosition( touchPos );

                currentDirection = currentPosition - defaultPosition;

                float currentDistance = Vector2.Distance( defaultPosition, currentPosition );
                float touchForce = 100f;

                float calculatedBorderSize = ( joystickBackgroundRT.sizeDelta.magnitude / 2f ) * borderSize / 8f;

                borderPosition = defaultPosition;
                borderPosition += currentDirection.normalized * calculatedBorderSize;

                if( currentDistance > calculatedBorderSize )                
                    currentPosition = borderPosition;                
                else 
                    touchForce = ( currentDistance / calculatedBorderSize ) * 100f;

                UpdateJoystickPosition();

                float aX = currentDirection.normalized.x * touchForce / 100f * sensitivity;
                float aY = currentDirection.normalized.y * touchForce / 100f * sensitivity;
                
                SetAxis( aX, aY );
            }
            else
            {
                touchDown = true;
                touchPhase = TCKTouchPhase.Began;

                UpdatePosition( touchPos );
                SetAxis( 0f, 0f );

                if( !isStatic ) 
                    UpdateTransparencyAndPosition( touchPos );

                // Broadcasting
                DownHandler();
            }
        }

        // Get CurrentPosition
        private void GetCurrentPosition( Vector2 touchPos )
        {
            defaultPosition = currentPosition = joystickBackgroundRT.position;
            if( axisX.enabled ) currentPosition.x = GuiCamera.ScreenToWorldPoint( touchPos ).x;
            if( axisY.enabled ) currentPosition.y = GuiCamera.ScreenToWorldPoint( touchPos ).y;
        }
        
        // Update JoystickPosition
        private void UpdateJoystickPosition()
        {
            joystickRT.position = currentPosition;
        }

        //Update Transparency And Position for Dynamic Joystick
        private void UpdateTransparencyAndPosition( Vector2 touchPos )
        {
            joystickImage.enabled = joystickBackgroundImage.enabled = true;
            joystickRT.position = joystickBackgroundRT.position = GuiCamera.ScreenToWorldPoint( touchPos );
        }

        // SmoothReturn Run
        private System.Collections.IEnumerator SmoothReturnRun()
        {
            bool smoothReturnIsRun = true;

            while( smoothReturnIsRun && !touchDown && isStatic )
            {
                int dpMag = Mathf.RoundToInt( defaultPosition.sqrMagnitude );
                int cpMag = Mathf.RoundToInt( currentPosition.sqrMagnitude );

                currentPosition.x = Mathf.SmoothDamp( currentPosition.x, defaultPosition.x, ref xVel, smoothFactor * Time.smoothDeltaTime );
                currentPosition.y = Mathf.SmoothDamp( currentPosition.y, defaultPosition.y, ref yVel, smoothFactor * Time.smoothDeltaTime );

                if( cpMag == dpMag )
                {
                    currentPosition = defaultPosition;
                    smoothReturnIsRun = false;
                    xVel = yVel = 0f;
                }

                UpdateJoystickPosition();
                yield return null;
            }
        }


        // Set Transparency
        private void SetTransparency()
        {
            joystickImage.enabled = joystickBackgroundImage.enabled = isStatic;
        }

        // Contro lReset
        protected override void ControlReset()
        {
            base.ControlReset();

            SetTransparency();

            if( smoothReturn && isStatic )
            {
                StopCoroutine( "SmoothReturnRun" );
                StartCoroutine( "SmoothReturnRun" );
            }
            else
            {
                currentPosition = defaultPosition;
                UpdateJoystickPosition();
            }

            // Broadcasting
            UpHandler();
        }


        // OnPointer Down
        public void OnPointerDown( PointerEventData pointerData )
        {
            if( !touchDown )
            {
                touchId = pointerData.pointerId;
                UpdatePosition( pointerData.position );
            }
        }

        // OnDrag
        public void OnDrag( PointerEventData pointerData )
        {
            if( Input.touchCount >= touchId && touchDown )
                UpdatePosition( pointerData.position );
        }

        // OnPointer Up
        public void OnPointerUp( PointerEventData pointerData )
        {
            UpdatePosition( pointerData.position );
            ControlReset();
        }

        // OnPointer Click
        public void OnPointerClick( PointerEventData pointerData )
        {
            ClickHandler();
        }
    }
}