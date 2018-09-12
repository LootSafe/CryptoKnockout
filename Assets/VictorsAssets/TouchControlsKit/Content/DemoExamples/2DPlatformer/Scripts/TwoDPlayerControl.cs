using UnityEngine;
using TouchControlsKit;

namespace Examples
{
    public class TwoDPlayerControl : MonoBehaviour
    {
        public float moveForce = 365f;
        public float maxSpeed = 5f;
        public float jumpForce = 1000f;

        internal bool facingRight { get; private set; }
        private bool jump = false;

        private Transform groundCheck = null;
        private bool grounded = false;
        private Animator anim = null;
        private Rigidbody2D m_Rigidbody2D = null;

        // Awake
        void Awake()
        {
            facingRight = true;
            groundCheck = transform.Find( "groundCheck" );
            anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update
        void Update()
        {
            grounded = Physics2D.Linecast( transform.position, groundCheck.position, 1 << LayerMask.NameToLayer( "Default" ) );

            if( TCKInput.GetAction( "jumpButton", EActionEvent.Down ) && grounded )            
                jump = true;                       
        }

        // FixedUpdate
        void FixedUpdate()
        {
            float horizontal = TCKInput.GetAxis( "DPad", EAxisType.Horizontal );
            horizontal = Mathf.Clamp( horizontal, -1f, 1f );

            anim.SetFloat( "Speed", Mathf.Abs( horizontal ) );

            if( horizontal * m_Rigidbody2D.velocity.x < maxSpeed )
                m_Rigidbody2D.AddForce( Vector2.right * horizontal * moveForce );

            if( Mathf.Abs( m_Rigidbody2D.velocity.x ) > maxSpeed )
                m_Rigidbody2D.velocity = new Vector2( Mathf.Sign( m_Rigidbody2D.velocity.x ) * maxSpeed, m_Rigidbody2D.velocity.y );

            if( horizontal > 0f && !facingRight )
                Flip();
            else if( horizontal < 0f && facingRight )
                Flip();

            if( jump )
            {
                anim.SetTrigger( "Jump" );
                m_Rigidbody2D.AddForce( new Vector2( 0f, jumpForce * 1.5f ) );
                jump = false;
            }
        }

        // Flip
        void Flip()
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
