/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TouchControlsKit
{
    public class TCKSteeringWheel : AxesBasedController,
        IPointerUpHandler, IPointerDownHandler, IDragHandler, IPointerClickHandler
    {
        [Range( 36f, 360f )]
        public float maxSteeringAngle = 120f;
        [Range( 25f, 150f )]
        public float releasedSpeed = 45f;

        float wheelAngle, wheelPrevAngle;


        // UpdatePosition
        protected override void UpdatePosition( Vector2 touchPos )
        {
            if( !axisX.enabled )
                return;

            base.UpdatePosition( touchPos );

            UpdateCurrentPosition( touchPos );

            if( touchDown )
            {
                float wheelNewAngle = Vector2.Angle( Vector2.up, currentPosition - defaultPosition );

                if( currentPosition.x > defaultPosition.x )
                    wheelAngle += wheelNewAngle - wheelPrevAngle;
                else
                    wheelAngle -= wheelNewAngle - wheelPrevAngle;

                wheelAngle = Mathf.Clamp( wheelAngle, -maxSteeringAngle, maxSteeringAngle );
                wheelPrevAngle = wheelNewAngle;

                UptateWheelRotation();

                SetAxes( wheelAngle / maxSteeringAngle * sensitivity
                        , 0f );
            }
            else
            {
                touchDown = true;
                touchPhase = ETouchPhase.Began;

                StopCoroutine( "WheelReturnRun" );
                wheelPrevAngle = Vector2.Angle( Vector2.up, currentPosition - defaultPosition );

                UpdatePosition( touchPos );

                // Broadcasting
                DownHandler();
            }
        }

        // GetCurrentPosition
        private void UpdateCurrentPosition( Vector2 touchPos )
        {
            defaultPosition = currentPosition = baseRect.position;
            currentPosition = GuiCamera.ScreenToWorldPoint( touchPos );
        }

        // UptateWheelRotation
        private void UptateWheelRotation()
        {
            baseRect.localEulerAngles = new Vector3( 0f, 0f, -wheelAngle );
        }

        // ControlReset
        protected override void ControlReset()
        {
            base.ControlReset();

            StopCoroutine( "WheelReturnRun" );
            StartCoroutine( "WheelReturnRun" );

            UpHandler();
        }        

        // WheelReturnRun
        private IEnumerator WheelReturnRun()
        {
            while( Mathf.Approximately( 0f, wheelAngle ) == false )
            {
                float deltaAngle = releasedSpeed * Time.smoothDeltaTime * 10f;
                
                if( Mathf.Abs( deltaAngle ) > Mathf.Abs( wheelAngle ) ) {
                    wheelAngle = 0f;
                }                                 
                else if( wheelAngle > 0f ) {
                    wheelAngle -= deltaAngle;
                }                                   
                else {
                    wheelAngle += deltaAngle;
                }                    
                
                UptateWheelRotation();
                yield return null;
            }
        }

        
        // OnPointerDown
        public void OnPointerDown( PointerEventData pointerData )
        {
            if( touchDown == false )
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

        // OnPointerUp
        public void OnPointerUp( PointerEventData pointerData )
        {
            UpdatePosition( pointerData.position );
            ControlReset();
        }

        // OnPointerClick
        public void OnPointerClick( PointerEventData pointerData )
        {
            ClickHandler();
        }


        // ShowHide TouchZone
        protected override void OnApplyShowTouchZone()
        { }
    };
}