/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using UnityEngine;

namespace TouchControlsKit
{
    [DisallowMultipleComponent]
    public class TCKTilt : MonoBehaviour
    {
        public EUpdateMode updateMode = EUpdateMode.Normal;

        [Range( 1f, 10f )]
        public float sensitivity = 4f;

        [Range( 0f, 90f )]
        public float fullTiltAngle = 25f;

        [Range( -50f, 50f )]
        public float centreAngleOffset = 0f;
        

        public static float forwardAxis { get; private set; }
        public static float sidewaysAxis { get; private set; }


        
        // Update
        void Update()
        {
            if( updateMode == EUpdateMode.Normal ) {
                InputsUpdate();
            }                
        }
        // Late Update
        void LateUpdate()
        {
            if( updateMode == EUpdateMode.Late ) {
                InputsUpdate();
            }                
        }
        // Fixed Update
        void FixedUpdate()
        {
            if( updateMode == EUpdateMode.Fixed ) {
                InputsUpdate();
            }                
        }

        // InputsUpdate
        private void InputsUpdate()
        {
            Vector3 acceleration = Input.acceleration;

            if( acceleration == Vector3.zero )
            {
                forwardAxis = sidewaysAxis = 0f;
                return;
            }

            float forwardAngle = Mathf.Atan2( acceleration.x, -acceleration.y ) * Mathf.Rad2Deg + centreAngleOffset;
            float sidewaysAngle = Mathf.Atan2( acceleration.z, -acceleration.y ) * Mathf.Rad2Deg + centreAngleOffset;
            forwardAxis = ( Mathf.InverseLerp( -fullTiltAngle, fullTiltAngle, forwardAngle ) * 2f - 1f ) * sensitivity;
            sidewaysAxis = ( Mathf.InverseLerp( -fullTiltAngle, fullTiltAngle, sidewaysAngle ) * 2f - 1f ) * sensitivity;
        }
    };
}