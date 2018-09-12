/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using UnityEngine;
using UnityEngine.EventSystems;

namespace TouchControlsKit
{
    public class TCKButton : ControllerBase,
        IPointerExitHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerClickHandler
    {
        private ActionEventHandler 
            downHandler = () =>{ }
            , pressHandler = () => { }
            , upHandler = () => { }
            , clickHandler = () => { };

        private ActionAlwaysHandler alwaysHandler = tf => { };
        
        public ActionEvent 
            downEvent = new ActionEvent()
            , pressEvent = new ActionEvent()
            , upEvent = new ActionEvent()
            , clickEvent = new ActionEvent();

        public AlwaysActionEvent alwaysEvent = new AlwaysActionEvent();

        public bool swipeOut = false;

        [Label( "Normal Button" )]
        public Sprite normalSprite;
        [Label( "Pressed Button" )]
        public Sprite pressedSprite;

        public Color32 pressedColor = new Color32( 255, 255, 255, 165 );
               
        int pressedFrame = -1
            , releasedFrame = -1
            , clickedFrame = -1;


        // isPRESSED
        internal bool isPRESSED {  get { return touchDown; } }
        // isDOWN
        internal bool isDOWN { get { return ( pressedFrame == Time.frameCount - 1 ); } }
        // isUP
        internal bool isUP { get { return ( releasedFrame == Time.frameCount - 1 ); } }
        // isCLICK
        internal bool isCLICK { get { return ( clickedFrame == Time.frameCount - 1 ); } }


        // Bind Action
        internal void BindAction( ActionEventHandler handler, EActionEvent actionEvent )
        {
            switch( actionEvent )
            {
                case EActionEvent.Down:
                    if( downHandler != handler ) {
                        downHandler += handler;
                    }                        
                    break;
                case EActionEvent.Press:
                    if( pressHandler != handler ) {
                        pressHandler += handler;
                    }                        
                    break;
                case EActionEvent.Up:
                    if( upHandler != handler ) {
                        upHandler += handler;
                    }                        
                    break;
                case EActionEvent.Click:
                    if( clickHandler != handler ) {
                        clickHandler += handler;
                    }                        
                    break;

                default: break;
            }
        }
        // Bind Action
        internal void BindAction( ActionAlwaysHandler handler )
        {
            if( alwaysHandler != handler ) {
                alwaysHandler += handler;
            }                
        }
        
        // UnBind Action
        internal void UnBindAction( ActionEventHandler handler, EActionEvent actionEvent )
        {
            switch( actionEvent )
            {
                case EActionEvent.Down:
                    if( downHandler == handler ) {
                        downHandler -= handler;
                    }
                    break;
                case EActionEvent.Press:
                    if( pressHandler == handler ) {
                        pressHandler -= handler;
                    }
                    break;
                case EActionEvent.Up:
                    if( upHandler == handler ) {
                        upHandler -= handler;
                    }
                    break;
                case EActionEvent.Click:
                    if( clickHandler == handler ) {
                        clickHandler -= handler;
                    }
                    break;

                default: break;
            }
        }
        // UnBind Action
        internal void UnBindAction( ActionAlwaysHandler handler )
        {
            if( alwaysHandler == handler ) {
                alwaysHandler -= handler;
            }
        }

        
        // Update Position
        protected override void UpdatePosition( Vector2 touchPos )
        {
            base.UpdatePosition( touchPos );

            if( touchDown == false )
            {
                touchDown = true;
                touchPhase = ETouchPhase.Began;
                pressedFrame = Time.frameCount;

                ButtonDown();
                DownHandler();
            }            
        }
                

        // Button Down
        protected void ButtonDown()
        {
            baseImage.sprite = pressedSprite;
            baseImage.color = visible ? pressedColor : ( Color32 )Color.clear;
        }

        // Button Up
        protected void ButtonUp()
        {
            baseImage.sprite = normalSprite;
            baseImage.color = visible ? baseImageColor : ( Color32 )Color.clear;
        }

        // Control Reset
        protected override void ControlReset()
        {
            base.ControlReset();

            releasedFrame = Time.frameCount;
            ButtonUp();            

            UpHandler();            
        }        

        // OnPointer Down
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
            {
                UpdatePosition( pointerData.position );
            }
        }

        // OnPointer Exit
        public void OnPointerExit( PointerEventData pointerData )
        {
            if( swipeOut == false ) {
                OnPointerUp( pointerData );
            }                
        }

        // OnPointer Up
        public void OnPointerUp( PointerEventData pointerData )
        {
            ControlReset();
        }

        // OnPointer Click
        public void OnPointerClick( PointerEventData pointerData )
        {
            clickedFrame = Time.frameCount;
            ClickHandler();
        }


        // Down Handler
        protected override void DownHandler()
        {
            downHandler.Invoke();
            downEvent.Invoke();
        }

        // Press Handler
        protected override void PressHandler()
        {
            alwaysHandler.Invoke( touchPhase );
            alwaysEvent.Invoke( touchPhase );

            if( touchDown )
            {
                pressHandler.Invoke();
                pressEvent.Invoke();
            }
        }

        // Up Handler
        protected override void UpHandler()
        {
            upHandler.Invoke();
            upEvent.Invoke();
        }

        // Click Handler
        protected override void ClickHandler()
        {
            clickHandler.Invoke();
            clickEvent.Invoke();
        }
    };
}