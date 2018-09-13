/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TouchControlsKit
{
    public class TCKDPad : AxesBasedController,
        IPointerUpHandler, IPointerDownHandler, IDragHandler, IPointerClickHandler
    {
        [Label( "Normal Arrow" )]
        public Sprite normalSprite;
        [Label( "Pressed Arrow" )]
        public Sprite pressedSprite;

        public Color32 
            normalColor = new Color32( 255, 255, 255, 165 )
            , pressedColor = new Color32( 255, 255, 255, 165 );

        private Color nvColor { get { return visible ? GetActiveColor( normalColor ) : Color.clear; } }
        private Color32 pvColor { get { return visible ? pressedColor : ( Color32 )Color.clear; } }

        [SerializeField]
        private TCKDPadArrow[] m_Arrows = new TCKDPadArrow[ 0 ];
        

        // Control Awake
        public override void OnAwake()
        {
            base.OnAwake();

            m_Arrows = GetComponentsInChildren<TCKDPadArrow>();
            Array.ForEach( m_Arrows, a => 
            {
                a.Init();
                a.SetSpriteAndColor( normalSprite, nvColor );
            } );
        }


        // Set Enable
        protected override void OnApplyEnable()
        {
            base.OnApplyEnable();
            Array.ForEach( m_Arrows, a => a.SetArrowEnable( enable ) );
        }

        // Set Visible
        protected override void OnApplyVisible()
        {
            base.OnApplyVisible();
            UpdateArrowsData();
        }

        // OnRefresh ActiveColors
        protected override void OnApplyActiveColors()
        {
            base.OnApplyActiveColors();
            UpdateArrowsData();
        }


        // Update ArrowsData
        private void UpdateArrowsData()
        {
            Array.ForEach( m_Arrows, a => a.SetSpriteAndColor( normalSprite, nvColor ) );
        }



        // Update Position
        protected override void UpdatePosition( Vector2 touchPos )
        {
            if( !axisX.enabled && !axisY.enabled )
                return;

            base.UpdatePosition( touchPos );

            if( touchDown )
            {
                UpdateCurrentPosition( touchPos );

                currentDirection = currentPosition - defaultPosition;

                Vector2 borderSize = GetBorderSize();                

                float currentDistance = Vector2.Distance( defaultPosition, currentPosition );

                if( currentDistance > borderSize.x || currentDistance > borderSize.y )
                {
                    currentPosition.x = defaultPosition.x + currentDirection.normalized.x * borderSize.x;
                    currentPosition.y = defaultPosition.y + currentDirection.normalized.y * borderSize.y;
                }                    
                
                //Debug.DrawLine( defaultPosition, currentPosition );

                Vector2 axes = Vector2.zero;

                for( int i = 0; i < m_Arrows.Length; i++ )
                {
                    if( m_Arrows[ i ].CheckTouchPosition( currentPosition, baseRect ) )
                    {
                        //ArrowDown
                        if( m_Arrows[ i ].isPressed == false ) {
                            m_Arrows[ i ].SetArrowPhase( pressedSprite, pvColor, true );
                        }

                        if( m_Arrows[ i ].arrowType == EArrowType.UP || m_Arrows[ i ].arrowType == EArrowType.DOWN ) {
                            axes.y = m_Arrows[ i ].axisValue * sensitivity;
                        }

                        if( m_Arrows[ i ].arrowType == EArrowType.RIGHT || m_Arrows[ i ].arrowType == EArrowType.LEFT ) {
                            axes.x = m_Arrows[ i ].axisValue * sensitivity;
                        }

                        continue;
                    }

                    //ArrowUp
                    if( m_Arrows[ i ].isPressed ) {
                        m_Arrows[ i ].SetArrowPhase( normalSprite, nvColor, false );
                    }

                    if( m_Arrows[ i ].arrowType == EArrowType.UP && m_Arrows[ i ].axisValue == 0f )
                    {
                        for( int j = 0; j < m_Arrows.Length; j++ )
                        {
                            if( m_Arrows[ j ].arrowType == EArrowType.DOWN && m_Arrows[ j ].axisValue == 0f )
                            {
                                axes.y = m_Arrows[ j ].axisValue * sensitivity;
                                break;
                            }
                        }
                    }

                    if( m_Arrows[ i ].arrowType == EArrowType.RIGHT && m_Arrows[ i ].axisValue == 0f )
                    {
                        for( int j = 0; j < m_Arrows.Length; j++ )
                        {
                            if( m_Arrows[ j ].arrowType == EArrowType.LEFT && m_Arrows[ j ].axisValue == 0f )
                            {
                                axes.x = m_Arrows[ j ].axisValue * sensitivity;
                                break;
                            }
                        }
                    }
                }

                SetAxes( axes );
            }
            else
            {
                touchDown = true;
                touchPhase = ETouchPhase.Began;

                UpdatePosition( touchPos );
                DownHandler();
            }            
        }

        // UpdateCurrent Position
        protected void UpdateCurrentPosition( Vector2 touchPos )
        {
            Vector2 worldPoint = GuiCamera.ScreenToWorldPoint( touchPos );

            if( axisX.enabled ) currentPosition.x = worldPoint.x;
            if( axisY.enabled ) currentPosition.y = worldPoint.y;

            defaultPosition = baseRect.position;
        }

        // Calculate BorderSize
        protected Vector2 GetBorderSize()
        {
            return baseRect.sizeDelta * .09f;
        }


        // Control Reset
        protected override void ControlReset()
        {
            base.ControlReset();

            Array.ForEach( m_Arrows, a => 
            {
                if( a.isPressed ) {
                    a.SetArrowPhase( normalSprite, nvColor, false );
                }                    
            } );

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
            if( Input.touchCount >= touchId && touchDown ) {
                UpdatePosition( pointerData.position );
            }                
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
    };
}