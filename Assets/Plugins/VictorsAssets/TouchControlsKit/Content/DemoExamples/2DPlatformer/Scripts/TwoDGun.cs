using UnityEngine;
using TouchControlsKit;

namespace Examples
{
    public class TwoDGun : MonoBehaviour
    {
        public Rigidbody2D rocket = null;
        public float speed = 20f;
        private TwoDPlayerControl playerCtrl = null;
        private Animator anim = null;

        // Awake
        void Awake()
        {
            anim = transform.root.gameObject.GetComponent<Animator>();
            playerCtrl = transform.root.GetComponent<TwoDPlayerControl>();
        }

        // Update
        void Update()
        {
            if( TCKInput.GetAction( "shootButton", EActionEvent.Down ) )
            {
                anim.SetTrigger( "Shoot" );

                if( playerCtrl.facingRight )
                {
                    Rigidbody2D bulletInstance = Instantiate( rocket, transform.position, Quaternion.Euler(  Vector3.zero ) ) as Rigidbody2D;
                    bulletInstance.velocity = new Vector2( speed, 0f );
                }
                else
                {
                    Rigidbody2D bulletInstance = Instantiate( rocket, transform.position, Quaternion.Euler( new Vector3( 0f, 0f, 180f ) ) ) as Rigidbody2D;
                    bulletInstance.velocity = new Vector2( -speed, 0f );
                }
            }
        }
        //
    }
}
