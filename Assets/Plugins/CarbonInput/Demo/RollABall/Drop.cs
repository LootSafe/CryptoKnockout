using UnityEngine;
using System.Collections;

namespace CarbonInput.Demo {
    /// <summary>
    /// Example script used for the rotating drops in the CarbonInput RollABall demo.
    /// </summary>
    public class Drop : MonoBehaviour {
        public Vector3 Axis = new Vector3(1, 1, 1);
        void Update() {
            transform.Rotate(Axis, 100 * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other) {
            BallController.UpdateScore(true);
            Destroy(gameObject);
        }
    }
}
