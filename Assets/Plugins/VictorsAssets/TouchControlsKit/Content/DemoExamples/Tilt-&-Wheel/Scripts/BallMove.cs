using UnityEngine;
using TouchControlsKit;

public class BallMove : MonoBehaviour
{
    private Rigidbody m_Rigidbody = null;
    private Vector3 tiltVector = Vector3.zero;
    private Transform m_Transform, cameraTransform;


    // Awake
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.maxAngularVelocity = 25f;
        m_Transform = transform;
        cameraTransform = Camera.main.transform;
    }

    // Update
    void Update()
    {
        tiltVector.x = TCKTilt.forwardAxis + TCKInput.GetAxis( "dPad", EAxisType.Horizontal );
        tiltVector.z = -TCKTilt.sidewaysAxis + TCKInput.GetAxis( "dPad", EAxisType.Vertical );
        cameraTransform.position = new Vector3( m_Transform.position.x, cameraTransform.position.y, m_Transform.position.z - 5f );
    }

    // FixedUpdate
    void FixedUpdate()
    {
        m_Rigidbody.AddForce( tiltVector * 5f * Time.fixedDeltaTime, ForceMode.Impulse );
    }
}
