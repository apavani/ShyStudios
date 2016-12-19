using UnityEngine;
using TouchControlsKit;

namespace Examples
{
    public class FirstPersonExample : MonoBehaviour
    {
        public enum AxesInputType
        {
            GetAxis = 0,
            BindAxes = 1,
            Broadcasting = 2
        }
        public AxesInputType axesInputType = AxesInputType.GetAxis;
        private bool binded = false;

        public enum GetAxesMethod
        {
            GetByName = 0,
            GetByType = 1
        }
        public GetAxesMethod axesGetType = GetAxesMethod.GetByName;

        //
        private Transform myTransform, cameraTransform;
        private CharacterController controller = null;
        private float rotation = 0f;
        Vector3 moveDirection = Vector3.zero;
        private bool jump, grounded, prevGrounded;
        private float weapReadyTime = 0f;
        private bool weapReady = true;


        // Awake
        void Awake()
        {
            myTransform = transform;
            cameraTransform = Camera.main.transform;
            controller = this.GetComponent<CharacterController>();

            TCKInput.BindAction( "jumpBtn", Jumping, ActionPhase.Down );
        }
        
        // Update
        void Update()
        {
            if( !weapReady )
            {
                weapReadyTime += Time.deltaTime;
                if( weapReadyTime > .15f )
                {
                    weapReady = true;
                    weapReadyTime = 0f;
                }
            }
        }

        // FixedUpdate
        void FixedUpdate()
        {
            if( axesInputType == AxesInputType.BindAxes && !binded )
            {
                TCKInput.BindAxes( "Joystick", BindPlayerAxes );
                binded = true;
                return;
            }

            if( axesInputType != AxesInputType.BindAxes && binded )
            {
                TCKInput.UnBindAxes( "Joystick", BindPlayerAxes );
                binded = false;
                return;
            }
            

            if( axesInputType != AxesInputType.GetAxis )
                return;

            if( axesGetType == GetAxesMethod.GetByName )
            {
                float moveX = TCKInput.GetAxis( "Joystick", "Horizontal" );
                float moveY = TCKInput.GetAxis( "Joystick", "Vertical" );
                PlayerMovement( moveX, moveY );
            }
            else
            {
                float moveX = TCKInput.GetAxis( "Joystick", AxisType.X );
                float moveY = TCKInput.GetAxis( "Joystick", AxisType.Y );
                PlayerMovement( moveX, moveY );
            }
        }


        // Jumping
        private void Jumping()
        {
            if( grounded )
                jump = true;
        }


        private void BindPlayerAxes( float x, float y, TCKTouchPhase tPhase )
        {
            PlayerMovement( x, y );
            //Debug.Log( tPhase );
        }


        // GetPlayerAxes
        public void GetPlayerAxes( float x, float y, TCKTouchPhase tPhase )
        {
            if( axesInputType != AxesInputType.Broadcasting )
                return;

            PlayerMovement( x, y );
            //Debug.Log( tPhase );
        }
        // PlayerMovement
        private void PlayerMovement( float horizontal, float vertical )
        {
            grounded = controller.isGrounded;
            
            moveDirection = myTransform.forward * vertical;
            moveDirection += myTransform.right * horizontal;            

            if( grounded )
            {
                moveDirection *= 7f;
                moveDirection.y = -10f;

                if( jump )
                {
                    jump = false;
                    moveDirection.y = 5f;
                }
            }
            else
            {
                moveDirection += Physics.gravity * 2f * Time.fixedDeltaTime;
            }

            moveDirection.y *= 20f;
            controller.Move( moveDirection * Time.fixedDeltaTime );


            if( !prevGrounded && grounded )
                moveDirection.y = 0f;

            prevGrounded = grounded;
        }

        // PlayerRotation
        public void PlayerRotation( float horizontal, float vertical )
        {
            myTransform.Rotate( 0f, horizontal * 12f, 0f );
            rotation += vertical * 12f;
            rotation = Mathf.Clamp( rotation, -60f, 60f );
            cameraTransform.localEulerAngles = new Vector3( -rotation, cameraTransform.localEulerAngles.y, 0f );
        }
        
        // PlayerFiring
        public void PlayerFiring()
        {
            if( !weapReady )
                return;

            weapReady = false;

            GameObject sphere = GameObject.CreatePrimitive( PrimitiveType.Sphere );
            sphere.transform.position = ( myTransform.position + myTransform.right );
            sphere.transform.localScale = Vector3.one * .15f;
            Rigidbody rBody = sphere.AddComponent<Rigidbody>();
            Transform camTransform = Camera.main.transform;
            rBody.AddForce( camTransform.forward * Random.Range( 25f, 35f ) + camTransform.right * Random.Range( -2f, 2f ) + camTransform.up * Random.Range( -2f, 2f ), ForceMode.Impulse );
            GameObject.Destroy( sphere, 3.5f );
        }

        // PlayerClicked
        public void PlayerClicked()
        {
            //Debug.Log( "PlayerClicked" );
        }
    }
}