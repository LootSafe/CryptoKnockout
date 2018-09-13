using UnityEngine;
using TouchControlsKit;


public class CarMove : MonoBehaviour
{
    private CharacterController controller = null;
    private Transform m_Transform = null;

    // Awake
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        m_Transform = transform;
    }

    // Update
    void Update()
    {
        float horizontal = TCKInput.GetAxis( "steeringWheel", EAxisType.Horizontal ) + Input.GetAxis( "Horizontal" );
        float vertical = TCKInput.GetAxis( "moveJoystick", EAxisType.Vertical ) + Input.GetAxis( "Vertical" );
        
        if( vertical != 0f )
            m_Transform.Rotate( 0f, horizontal, 0f );

        Vector3 moveVector = ( m_Transform.forward * vertical ) * 5f;
        moveVector *= Time.deltaTime;
        controller.Move( moveVector );
    }
}
