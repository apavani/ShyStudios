/*******************************************************
 * 													   *
 * Asset:		 Touch Controls Kit         		   *
 * Script:		 TCKTouchpad.cs                        *
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

namespace TouchControlsKit
{
    [RequireComponent( typeof( Image ) )]
    public class TCKTouchpad : AxesBasedController,
        IPointerUpHandler, IPointerDownHandler, IDragHandler, IPointerClickHandler, IPointerEnterHandler
    {
        private GameObject prevPointerPressGO = null;


        // Set Visible
        protected override void SetVisible()
        { }


        // Update Position
        protected override void UpdatePosition( Vector2 touchPos )
        {
            if( !axisX.enabled && !axisY.enabled )
                return;

            base.UpdatePosition( touchPos );

            if( touchDown )
            {
                if( axisX.enabled ) 
                    currentPosition.x = touchPos.x;
                if( axisY.enabled ) 
                    currentPosition.y = touchPos.y;

                currentDirection = currentPosition - defaultPosition;
                
                float touchForce = Vector2.Distance( defaultPosition, currentPosition ) * 2f;
                defaultPosition = currentPosition;

                float aX = currentDirection.normalized.x * touchForce / 100f * sensitivity;
                float aY = currentDirection.normalized.y * touchForce / 100f * sensitivity;
                
                SetAxis( aX, aY );
            }
            else
            {
                touchDown = true;
                touchPhase = TCKTouchPhase.Began;

                currentPosition = defaultPosition = touchPos;
                UpdatePosition( touchPos );

                // Broadcasting
                DownHandler();
            }
        }

        // Control Reset
        protected override void ControlReset()
        {
            base.ControlReset();

            // Broadcasting
            UpHandler();
        }
        
        
        // OnPointer Enter
        public void OnPointerEnter( PointerEventData pointerData )
        {
            if( !pointerData.pointerPress )
                return;

            if( pointerData.pointerPress == gameObject )
            {
                OnPointerDown( pointerData );
                return;
            }

            TCKButton btn = pointerData.pointerPress.GetComponent<TCKButton>();
            if( btn && btn.swipeOut )
            {
                prevPointerPressGO = pointerData.pointerPress;
                pointerData.pointerDrag = gameObject;
                pointerData.pointerPress = gameObject;
                OnPointerDown( pointerData );
            }
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
            {
                UpdatePosition( pointerData.position );
                StopCoroutine( "UpdateEndPosition" );
                StartCoroutine( "UpdateEndPosition", pointerData.position );
            }
        }


        // Update EndPosition
        private System.Collections.IEnumerator UpdateEndPosition( Vector2 position )
        {
            yield return new WaitForSeconds( .0025f );

            if( touchDown )
                UpdatePosition( position );
            else
                ControlReset();
        }

        // OnPointer Up
        public void OnPointerUp( PointerEventData pointerData )
        {
            if( prevPointerPressGO )
            {
                ExecuteEvents.Execute<IPointerUpHandler>( prevPointerPressGO, pointerData, ExecuteEvents.pointerUpHandler );
                prevPointerPressGO = null;
            }

            ControlReset();
        }

        // OnPointer Click
        public void OnPointerClick( PointerEventData pointerData )
        {
            ClickHandler();
        }
    }
}