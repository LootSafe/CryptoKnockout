namespace CarbonInput {
    /// <summary>
    /// Internal wrapper class, used to access touch input.
    /// </summary>
    public class TouchMapping : CarbonController {
        /// <summary>
        /// Currently pressed buttons.
        /// </summary>
        private readonly bool[] buttonMap = new bool[ButtonCount];
        /// <summary>
        /// Current values of all axes.
        /// </summary>
        private readonly float[] axisMap = new float[AxisCount];

        private void OnEnable() {
            name = "TouchInput";
        }

        /// <summary>
        /// Gets or sets if the specific button is pressed or not.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool this[CButton button] {
            get { return buttonMap[(int)button]; }
            set { buttonMap[(int)button] = value; }
        }
        /// <summary>
        /// Gets or sets the value of the given axis.
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public float this[CAxis axis] {
            get { return axisMap[(int)axis]; }
            set { axisMap[(int)axis] = value; }
        }

        public override bool GetButton(CButton btn, int id) {
            return this[btn];
        }
        public override float GetAxis(CAxis axis, int id) {
            return this[axis];
        }
    }
}
