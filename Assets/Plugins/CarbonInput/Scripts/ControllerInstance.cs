namespace CarbonInput {
    public class ControllerInstance {
        public CarbonController Controller;
        public int Index;

        public ControllerInstance(CarbonController controller, int index) {
            Controller = controller;
            Index = index + 1;
        }

        public bool GetButton(CButton button) {
            return Controller.GetButton(button, Index);
        }

        public float GetAxis(CAxis axis) {
            return Controller.GetAxis(axis, Index);
        }
    }
}