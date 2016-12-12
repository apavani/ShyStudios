/*******************************************************
 * 													   *
 * Asset:		 Touch Controls Kit         		   *
 * Script:		 AxesBasedController.cs                *
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

namespace TouchControlsKit
{
    public abstract class AxesBasedController : ControllerBase
    {
        public float sensitivity = 1f;        

        public Axis axisX = new Axis( "Horizontal" );
        public Axis axisY = new Axis( "Vertical" );

        private AxesEventHandler downHandler, pressHandler, upHandler, clickHandler;
        private AxesAlwaysHandler alwaysHandler;
        
        [SerializeField] public AxesEvent downEvent, pressEvent, upEvent, clickEvent;
        [SerializeField] public AlwaysAxesEvent alwaysEvent;

        [SerializeField]
        private bool showBaseImage = true;

        protected Vector2 defaultPosition, currentPosition, currentDirection;
        
        // Show TouchZone
        public bool ShowTouchZone
        {
            get { return showBaseImage; }
            set
            {
                if( showBaseImage == value )
                    return;

                showBaseImage = value;
                ShowHideTouchZone();
            }
        }

        // ShowHide TouchZone
        private void ShowHideTouchZone()
        {
            if( showBaseImage )
            {
                baseImage.color = baseImageNativeColor;
            }
            else
            {
                baseImageNativeColor = baseImage.color;
                baseImage.color = ( Color32 )Color.clear;
            }
        }
        

        // Bind Action
        internal void BindAxes( AxesEventHandler m_Handler, ActionPhase actionPhase )
        {
            switch( actionPhase )
            {
                case ActionPhase.Down:
                    useDown = true;
                    if( downHandler != m_Handler )
                        downHandler += m_Handler;
                    break;
                case ActionPhase.Pressed:
                    usePress = true;
                    if( pressHandler != m_Handler )
                        pressHandler += m_Handler;
                    break;
                case ActionPhase.Up:
                    useUp = true;
                    if( upHandler != m_Handler )
                        upHandler += m_Handler;
                    break;
                case ActionPhase.Click:
                    useClick = true;
                    if( clickHandler != m_Handler )
                        clickHandler += m_Handler;
                    break;
            }
        }
        // Bind Axes
        internal void BindAxes( AxesAlwaysHandler m_Handler )
        {
            useAlways = true;
            if( alwaysHandler != m_Handler )
                alwaysHandler += m_Handler;
        }
        
        // UnBind Action
        internal void UnBindAxes( AxesEventHandler m_Handler, ActionPhase actionPhase )
        {
            switch( actionPhase )
            {
                case ActionPhase.Down:
                    if( downHandler == m_Handler )
                    {
                        downHandler -= m_Handler;
                        useDown = ( downHandler != null );
                    }
                    break;
                case ActionPhase.Pressed:
                    if( pressHandler == m_Handler )
                    {
                        pressHandler -= m_Handler;
                        usePress = ( pressHandler != null );
                    }
                    break;
                case ActionPhase.Up:
                    if( upHandler == m_Handler )
                    {
                        upHandler -= m_Handler;
                        useUp = ( upHandler != null );
                    }
                    break;
                case ActionPhase.Click:
                    if( clickHandler == m_Handler )
                    {
                        clickHandler -= m_Handler;
                        useClick = ( clickHandler != null );
                    }
                    break;
            }
        }
        // UnBind Axes
        internal void UnBindAxes( AxesAlwaysHandler m_Handler )
        {
            if( alwaysHandler == m_Handler )
            {
                alwaysHandler -= m_Handler;
                useAlways = ( clickHandler != null );
            }
        }


        // Set Axis
        protected void SetAxis( float x, float y )
        {
            axisX.SetValue( x );
            axisY.SetValue( y );
        }


        // Control Reset
        protected override void ControlReset()
        {
            base.ControlReset();
            SetAxis( 0f, 0f );
        }


        // Down Handler
        protected override void DownHandler()
        {
            if( useDown )
                downHandler.Invoke( axisX.value, axisY.value );

            if( broadcast )
                downEvent.Invoke( axisX.value, axisY.value );
        }

        // Press Handler
        protected override void PressHandler()
        {
            if( useAlways )
                alwaysHandler.Invoke( axisX.value, axisY.value, touchPhase );

            if( broadcast )
                alwaysEvent.Invoke( axisX.value, axisY.value, touchPhase );

            if( touchDown )
            {
                if( usePress )
                    pressHandler.Invoke( axisX.value, axisY.value );

                if( broadcast )
                    pressEvent.Invoke( axisX.value, axisY.value );
            }
        }

        // Up Handler
        protected override void UpHandler()
        {
            if( useUp )
                upHandler.Invoke( axisX.value, axisY.value );

            if( broadcast )
                upEvent.Invoke( axisX.value, axisY.value );
        }

        // Click Handler
        protected override void ClickHandler()
        {
            if( useClick )
                clickHandler.Invoke( axisX.value, axisY.value );

            if( broadcast )
                clickEvent.Invoke( axisX.value, axisY.value );
        }
    }
}