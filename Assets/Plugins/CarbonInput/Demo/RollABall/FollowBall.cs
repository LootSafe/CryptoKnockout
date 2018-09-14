using UnityEngine;
using System.Collections;

namespace CarbonInput.Demo {
    /// <summary>
    /// Script used to move the camera in the CarbonInput RollABall demo
    /// </summary>
    public class FollowBall : MonoBehaviour {
        public GameObject Player;
        private Vector3 Offset;

        void Start() {
            Offset = transform.position - Player.transform.position;
        }

        void LateUpdate() {
            transform.position = Player.transform.position + Offset;
        }
    }
}
