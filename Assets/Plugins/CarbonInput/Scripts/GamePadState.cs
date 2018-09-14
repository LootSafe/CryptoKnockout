using UnityEngine;
using CarbonInput;

/// <summary>
/// Represents the current state of a specific gamepad. 
/// The state of any button can be accessed by the attributes <see cref="A"/>, <see cref="B"/>,... or via the method <see cref="Button(CButton)"/>. <para/>
/// <see cref="Pressed(CButton)"/> returns true during the frame it was pressed. <para/>
/// <see cref="Released(CButton)"/> returns true during the frame it was released. <para/>
/// <see cref="Left"/>, <see cref="Right"/> and <see cref="DPad"/> will give you direct access to the corresponding axes. 
/// The trigger can be accessed by <see cref="LT"/> and <see cref="RT"/>.
/// </summary>
// ReSharper disable once CheckNamespace
// ReSharper disable InconsistentNaming
public class GamePadState {
	/// <summary>
	/// Any axis is being considered "pressed" if it's absolute value is greater than this threshold.
	/// </summary>
	private const float AxisPressedThreshold = 0.3f;

	#region Buttons
	/// <summary>
	/// Stores the state of all buttons from the last frame.
	/// </summary>
	private bool[] LastFrameButtons = new bool[CarbonController.ButtonCount];
    /// <summary>
    /// Stores the state of all buttons from this frame.
    /// </summary>
    private bool[] Buttons = new bool[CarbonController.ButtonCount];

	/// <summary>
	/// Is true while <see cref="CButton.A"/> is pressed.
	/// </summary>
	public bool A { get { return Buttons[(int)CButton.A]; } }
    /// <summary>
    /// Is true while <see cref="CButton.B"/> is pressed.
    /// </summary>
    public bool B { get { return Buttons[(int)CButton.B]; } }
    /// <summary>
    /// Is true while <see cref="CButton.X"/> is pressed.
    /// </summary>
    public bool X { get { return Buttons[(int)CButton.X]; } }
    /// <summary>
    /// Is true while <see cref="CButton.Y"/> is pressed.
    /// </summary>
    public bool Y { get { return Buttons[(int)CButton.Y]; } }
    /// <summary>
    /// Is true while <see cref="CButton.Back"/> is pressed.
    /// </summary>
    public bool Back { get { return Buttons[(int)CButton.Back]; } }
    /// <summary>
    /// Is true while <see cref="CButton.Start"/> is pressed.
    /// </summary>
    public bool Start { get { return Buttons[(int)CButton.Start]; } }
    /// <summary>
    /// Is true while <see cref="CButton.LB"/> is pressed.
    /// </summary>
    public bool LB { get { return Buttons[(int)CButton.LB]; } }
    /// <summary>
    /// Is true while <see cref="CButton.RB"/> is pressed.
    /// </summary>
    public bool RB { get { return Buttons[(int)CButton.RB]; } }
    /// <summary>
    /// Is true while <see cref="CButton.LS"/> is pressed.
    /// </summary>
    public bool LS { get { return Buttons[(int)CButton.LS]; } }
    /// <summary>
    /// Is true while <see cref="CButton.RS"/> is pressed.
    /// </summary>
    public bool RS { get { return Buttons[(int)CButton.RS]; } }
	#endregion

	#region Axis
	/// <summary>
	/// Stores the state of all axis values from the last frame.
	/// </summary>
	private float[] LastAxis = new float[CarbonController.AxisCount];
	/// <summary>
	/// Stores the state of all axis values from this frame.
	/// </summary>
	private float[] Axis = new float[CarbonController.AxisCount];

	/// <summary>
	/// X and Y axis of the left thumbstick.
	/// </summary>
	public Vector2 Left { get { return new Vector2(Axis[(int)CAxis.LX], Axis[(int)CAxis.LY]); } }
    /// <summary>
    /// X and Y axis of the right thumbstick.
    /// </summary>
    public Vector2 Right { get { return new Vector2(Axis[(int)CAxis.RX], Axis[(int)CAxis.RY]); } }
	/// <summary>
	/// Left trigger.
	/// </summary>
	public float LT { get { return Axis[(int)CAxis.LT]; } }
	/// <summary>
	/// Right trigger.
	/// </summary>
	public float RT { get { return Axis[(int)CAxis.RT]; } }
	/// <summary>
	/// X and Y axis of the dpad.
	/// </summary>
	public Vector2 DPad { get { return new Vector2(Axis[(int)CAxis.DX], Axis[(int)CAxis.DY]); } }
	#endregion

	/// <summary>
	/// Defines the owner of this <see cref="GamePadState"/>.
	/// </summary>
	private readonly PlayerIndex Index;
    /// <summary>
    /// Number of the last frame, used to determine if we're in a new frame or not.
    /// </summary>
    private int LastFrame;

	/// <summary>
	/// Returns true if the button state has changed since the last frame.
	/// </summary>
	/// <param name="btn"></param>
	/// <returns></returns>
	public bool HasChanged(CButton btn) { return Buttons[(int)btn] != LastFrameButtons[(int)btn]; }

	/// <summary>
	/// Returns true while the button is pressed.
	/// </summary>
	/// <param name="btn"></param>
	/// <returns></returns>
	public bool Button(CButton btn) { return Buttons[(int)btn]; }
    /// <summary>
    /// Returns true during the frame the user pressed the button.
    /// </summary>
    /// <param name="btn"></param>
    /// <returns></returns>
    public bool Pressed(CButton btn) { return Buttons[(int)btn] && !LastFrameButtons[(int)btn]; }
    /// <summary>
    /// Returns true during the frame the user released the button.
    /// </summary>
    /// <param name="btn"></param>
    /// <returns></returns>
    public bool Released(CButton btn) { return !Buttons[(int)btn] && LastFrameButtons[(int)btn]; }

	/// <summary>
	/// Returns true while the axis is "pressed", which is if the absolute value of this axis is greater than a certain threshold.
	/// </summary>
	/// <param name="axis"></param>
	/// <returns></returns>
	public bool Button(CAxis axis) {
		return Mathf.Abs(Axis[(int)axis]) > AxisPressedThreshold;
	}
	/// <summary>
	/// Returns true during the frame the axis is "pressed", which is if the absolute value of this axis is greater than a certain threshold.
	/// </summary>
	/// <param name="axis"></param>
	/// <returns></returns>
	public bool Pressed(CAxis axis) {
		bool pressedNow = Mathf.Abs(Axis[(int)axis]) > AxisPressedThreshold;
		bool pressedLastFrame = Mathf.Abs(LastAxis[(int)axis]) > AxisPressedThreshold;
		return pressedNow && !pressedLastFrame;
	}
	/// <summary>
	/// Returns true during the frame the axis is no longer "pressed", which is if the absolute value of this axis is greater than a certain threshold.
	/// </summary>
	/// <param name="axis"></param>
	/// <returns></returns>
	public bool Released(CAxis axis) {
		bool pressedNow = Mathf.Abs(Axis[(int)axis]) > AxisPressedThreshold;
		bool pressedLastFrame = Mathf.Abs(LastAxis[(int)axis]) > AxisPressedThreshold;
		return !pressedNow && pressedLastFrame;
	}

	/// <summary>
	/// Returns the value of the given axis.
	/// </summary>
	/// <param name="axis"></param>
	/// <returns></returns>
	public float GetAxis(CAxis axis) {
		return Axis[(int)axis];
	}

	#region PSButton
	/// <summary>
	/// Returns true while the button is pressed.
	/// </summary>
	/// <param name="btn"></param>
	/// <returns></returns>
	public bool Button(PSButton btn) { return Button((CButton)btn); }
	/// <summary>
	/// Returns true during the frame the user pressed the button.
	/// </summary>
	/// <param name="btn"></param>
	/// <returns></returns>
	public bool Pressed(PSButton btn) { return Pressed((CButton)btn); }
	/// <summary>
	/// Returns true during the frame the user released the button.
	/// </summary>
	/// <param name="btn"></param>
	/// <returns></returns>
	public bool Released(PSButton btn) { return Released((CButton)btn); }
	#endregion

	/// <summary>
	/// Returns true if any button is currently pressed.
	/// </summary>
	public bool AnyButton { get; private set; }
	/// <summary>
	/// Returns true if any axis is currently not zero.
	/// </summary>
	public bool AnyAxis { get; private set; }
	/// <summary>
	/// Returns true if any button is currently pressed or if any axis is currently not zero.
	/// </summary>
	public bool AnyButtonOrAxis { get { return AnyButton || AnyAxis; } }

	/// <summary>
	/// Returns a button that is currently pressed or null if no buttons are pressed.
	/// </summary>
	/// <returns></returns>
	public CButton? GetAnyButton() {
		for(int i = 0; i < CarbonController.ButtonCount; i++)
			if(Buttons[i]) return (CButton)i;
		return null;
	}

	/// <summary>
	/// Returns an axis that is not zero or null if all axis are zero.
	/// </summary>
	/// <returns></returns>
	public CAxis? GetAnyAxis() {
		for(int i = 0; i < CarbonController.AxisCount; i++)
			if(Mathf.Abs(Axis[i]) > AxisPressedThreshold) return (CAxis)i;
		return null;
	}

	public GamePadState(PlayerIndex id) {
        Index = id;
    }

    /// <summary>
    /// This will update all buttons and axes of this instance.
    /// Multiple calls in the same frame won't have any effect.
    /// </summary>
    public void Update() {
        if(LastFrame == Time.frameCount) return;
        LastFrame = Time.frameCount;
        SwapArrays();
	    AnyButton = false;
        for(int i = 0; i < Buttons.Length; i++) {
			AnyButton |= (Buttons[i] = GamePad.GetButton((CButton)i, Index));
        }
	    AnyAxis = false;
	    for(int i = 0; i < Axis.Length; i++) {
		    AnyAxis |= Mathf.Abs(Axis[i] = GamePad.GetAxis((CAxis)i, Index)) > AxisPressedThreshold;
	    }
    }
    
    private void SwapArrays() {
        bool[] tmp = LastFrameButtons;
        LastFrameButtons = Buttons;
        Buttons = tmp;

	    float[] axis = LastAxis;
	    LastAxis = Axis;
	    Axis = axis;
    }
}
