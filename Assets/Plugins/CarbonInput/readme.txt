Getting started
1. If not already done, download and import CarbonInput from Unity Asset Store
2. Initialize the input system by clicking the following menut item:
	Edit > Project Settings > Carbon Input > Create Carbon Input Axes
	Now the necessary axes are automatically set up and you can start using CarbonInput
3. Query a button or axis directly:
	bool button = GamePad.GetButton(CButton.A);
	float axis = GamePad.GetAxis(CAxis.LX);
	You could also query a Vector2:
	Vector2 leftStick = GamePad.GetStick(CStick.Left);
	// alternative:
	Vector2 leftStick = GamePad.GetLeftStick();
4. You can also query the complete state of a gamepad:
	GamePadState state = GamePad.GetState();
	bool fire = state.A;
	Vector2 move = state.Left;
	The GamePadState does not only store the current state, but also the state from the last frame.
	Therefor you can also check if a button was pressed or released during this frame:
	bool pressed = state.Pressed(CButton.A);
	bool released = state.Released(CButton.A);
5. The mentioned methods are able to accept a PlayerIndex as an optional parameter. 
	If you don't specify an index, the special index PlayerIndex.Any is used. 
	Example for second player: GamePad.GetButton(CButton.A, PlayerIndex.Two)

That's basically all. 
p.s.
If you want to have icons for the CarbonInput asset files, you have to move the content of the CarbonInput/Gizmos folder to the Gizmos folder in your assets root directory.
