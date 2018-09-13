/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using UnityEngine;
using UnityEngine.UI;

namespace TouchControlsKit
{
    [DisallowMultipleComponent]
    [RequireComponent( typeof( Image ) )]
    public class TCKDPadArrow : MonoBehaviour
    {
        public EArrowType arrowType = EArrowType.none;

        [SerializeField]
        private RectTransform baseRect = null;
        [SerializeField]
        private Image baseImage = null;

        internal bool isPressed { get; private set; }

        internal float axisValue { get; private set; }


        // Init
        internal void Init()
        {
            baseRect = transform as RectTransform;
            baseImage = GetComponent<Image>();            
        }

        // SetSprite AndColor
        internal void SetSpriteAndColor( Sprite sprite, Color color )
        {
            baseImage.sprite = sprite;
            baseImage.color = color;
        }


        // SetArrowColor
        internal void SetArrowEnable( bool value )
        {
            baseImage.enabled = value;
        }

        // SetArrowActive
        internal void SetArrowPhase( Sprite sprite, Color color, bool pressed )
        {
            baseImage.sprite = sprite;
            baseImage.color = color;
            isPressed = pressed;
        }


        // CheckBoolPosition
        internal bool CheckTouchPosition( Vector2 touchPos, RectTransform touchZone )
        {
            if( CheckBoolPosition( touchPos, touchZone ) )
            {
                switch( arrowType )
                {
                    case EArrowType.UP:
                    case EArrowType.RIGHT:
                        axisValue = 1f;
                        break;

                    case EArrowType.DOWN:
                    case EArrowType.LEFT:
                        axisValue = -1f;
                        break;

                    case EArrowType.none:
                        Debug.LogError( "Arrow type for '" + name + "' is not assigned!" );
                        axisValue = 0f;
                        return false;
                }
                return true;
            }
            else
            {
                axisValue = 0f;
                return false;
            }
        }
        

        // CheckBoolPosition
        private bool CheckBoolPosition( Vector2 touchPos, RectTransform touchZone )
        {
            Vector2 pos = baseRect.position;            
            Vector2 size = touchZone.sizeDelta * .1f;
            Rect rect = Rect.zero;

            switch( arrowType )
            {
                case EArrowType.UP:
                    rect.position = new Vector2( pos.x - size.x, pos.y );
                    size.x *= 2f;
                    rect.size = size;
                    break;
                case EArrowType.DOWN:                    
                    rect.position = new Vector2( pos.x + size.x, pos.y );
                    size.x *= 2f;
                    rect.size = -size;
                    break;
                case EArrowType.LEFT:                    
                    rect.position = new Vector2( pos.x, pos.y + size.y );
                    size.y *= 2f;
                    rect.size = -size;
                    break;
                case EArrowType.RIGHT:
                    rect.position = new Vector2( pos.x, pos.y - size.y );
                    size.y *= 2f;
                    rect.size = size;
                    break;

                default: break;
            }

            return rect.Contains( touchPos, true );
        }
    };
}



/*
Debug.DrawLine( new Vector2( myData.baseRect.position.x + sizeX / 2f, myData.baseRect.position.y + halfSizeY / 2f ),
                new Vector2( myData.baseRect.position.x - sizeX / 2f, myData.baseRect.position.y - halfSizeY / 2f ), Color.red );
 
Debug.DrawLine( new Vector2( myData.baseRect.position.x + halfSizeX / 2f, myData.baseRect.position.y + sizeY / 2f ),
                new Vector2( myData.baseRect.position.x - halfSizeX / 2f, myData.baseRect.position.y - sizeY / 2f ), Color.green );
*/