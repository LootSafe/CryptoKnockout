using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CarbonInput;

/// <summary>
/// Interface to the carbon controller input system.
/// </summary>
// ReSharper disable once CheckNamespace
// ReSharper disable InconsistentNaming
public static class GamePad {
	public delegate void OnReloadEvent();
	/// <summary>
	/// This event is fired when a reload has happened.
	/// </summary>
	public static event OnReloadEvent OnReload;
    /// <summary>
    /// Used for lazy initialization.
    /// </summary>
    private static bool IsInitialized;
    /// <summary>
    /// Array of all mappings supporting this platform.
    /// </summary>
    private static CarbonController[] AllMappings;
    /// <summary>
    /// One mapping for each player, including <see cref="PlayerIndex.Any"/> (index 0).
    /// </summary>
    private static ControllerInstance[] PlayerMappings;
    /// <summary>
    /// <see cref="GamePadState"/>s of all players.
    /// </summary>
    private static readonly GamePadState[] States = new GamePadState[CarbonController.PlayerIndices];
    /// <summary>
    /// Number of connected and supported hardware gamepads, without TouchInput/Keyboard.
    /// </summary>
    private static int gamepadCount;
    /// <summary>
    /// Number of connected and supported hardware gamepads, without TouchInput/Keyboard.
    /// </summary>
    public static int GamePadCount { get { if(!IsInitialized) Initialize(); return gamepadCount; } }

    /// <summary>
    /// Used to store settings like <see cref="AnyBehaviour"/> and inverted axes.
    /// </summary>
    private static CarbonSettings settings;
    /// <summary>
    /// Used to store settings like <see cref="AnyBehaviour"/> and inverted axes.
    /// </summary>
    public static CarbonSettings Settings {
        get {
            if(!IsInitialized) Initialize();
            return settings;
        }
        set {
            if(!IsInitialized) Initialize();
            settings = value;
        }
    }

	private static readonly CarbonController disabledInput = CarbonController.CreateDisabledInput();

    /// <summary>
    /// Returns an array of all mappings supported by this platform.
    /// </summary>
    /// <returns></returns>
    public static CarbonController[] GetAllMappings() {
        if(!IsInitialized) Initialize();
        return AllMappings;
    }
    /// <summary>
    /// Returns an array of all player mappings. Index 0 is the mapping for <see cref="PlayerIndex.Any"/> and indices 1 to 8 meant to 
    /// reference <see cref="PlayerIndex.One"/> to <see cref="PlayerIndex.Eight"/>.
    /// </summary>
    /// <returns></returns>
    public static ControllerInstance[] GetPlayerMappings() {
        if(!IsInitialized) Initialize();
        return PlayerMappings;
    }

	/// <summary>
	/// Reinitializes all GamePads.
	/// </summary>
	public static void ReInit() {
		var touchMappings = GetPlayerMappings().Skip(1).Where(x => x.Controller is TouchMapping).ToList();
		Initialize();
		var mappings = GetPlayerMappings();
		int idx = 0;
		for(int i = 1; i < CarbonController.PlayerIndices && idx < touchMappings.Count; i++) {
			if(mappings[i] != null && mappings[i].Controller.Replacable) {
				mappings[i] = touchMappings[idx++];
				if(i == 1) mappings[0] = mappings[1]; // required for PlayerIndex.Any if used with AnyBehaviour.UseMappingOne
			}
		}
		if(OnReload != null)
			OnReload();
	}

	/// <summary>
	/// Initializes this library by loading all mappings from file and matching the given gamepads.
	/// </summary>
	private static void Initialize() {
		if(!IsInitialized) { // first init
			new GameObject("GamePad ReInit").AddComponent<ReInit>();
		}
        List<CarbonController> mappings = new List<CarbonController>(Resources.LoadAll<CarbonController>("Mappings")); // load all mappings
        mappings.RemoveAll(mapping => !mapping.SupportedOnThisPlatform()); // keep only mappings for this platform
        mappings.Sort((a, b) => a.Priority - b.Priority); // sort by priority, lower is better
        AllMappings = mappings.ToArray();
        // now try to match with the names of the connected joysticks
        int nameIndex = 0;
        gamepadCount = 0;
        List<ControllerInstance> matches = new List<ControllerInstance>();
        foreach(string name in Input.GetJoystickNames()) {
	        CarbonController toRemove = null;
            foreach(CarbonController cc in mappings) {
                if(!string.IsNullOrEmpty(cc.RegEx) && Regex.IsMatch(name, cc.RegEx, RegexOptions.IgnoreCase)) {
                    matches.Add(new ControllerInstance(cc, nameIndex));
                    gamepadCount++;
	                if(cc.UseOnce) toRemove = cc;
                    break;
                }
            }
	        if(toRemove != null) mappings.Remove(toRemove);
            nameIndex++;
        }
		// add fallbacks (keyboard)
		var fallbacks = AllMappings.Where(x => x.IsFallback()).ToList();
		fallbacks.Add(disabledInput);
		
        PlayerMappings = new ControllerInstance[CarbonController.PlayerIndices];
        for(int i = 1; i < CarbonController.PlayerIndices; i++) {
            int idx = i - 1;
	        if(idx < matches.Count) PlayerMappings[i] = matches[idx]; // real GamePad
	        else { // Keyboard Fallback
		        var fallback = fallbacks.First();
				PlayerMappings[i] = new ControllerInstance(fallback, idx);
		        if(fallback.UseOnce) fallbacks.RemoveAt(0);
	        }
        }
		PlayerMappings[0] = PlayerMappings[1]; // always use first found mapping as the "global" mapping for Anyone
		for(int i = 0; i < CarbonController.PlayerIndices; i++) States[i] = new GamePadState((PlayerIndex)i);

        settings = CarbonSettings.Default();
        IsInitialized = true;
	}

	/// <summary>
	/// Returns the mapping used by player <paramref name="id"/>.
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public static ControllerInstance GetMapping(PlayerIndex id) {
        if(!IsInitialized) Initialize();
        return PlayerMappings[(int)id];
    }

	/// <summary>
	/// Returns true if there is any real gamepad connected.
	/// </summary>
	/// <returns></returns>
	public static bool AnyConnected() {
		if(!IsInitialized) Initialize();
		return gamepadCount > 0;
	}

    /// <summary>
    /// Returns the state of button <paramref name="btn"/> of player <paramref name="id"/>.
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool GetButton(CButton btn, PlayerIndex id = PlayerIndex.Any) {
        if(!IsInitialized) Initialize();
        if(id == PlayerIndex.Any) {
            switch(settings.Behaviour) {
                case AnyBehaviour.UseControllerOne: return PlayerMappings[1].GetButton(btn);
                case AnyBehaviour.CheckAll:
                    for(int i = 1; i < CarbonController.PlayerIndices; i++) {
                        if(PlayerMappings[i].GetButton(btn)) return true;
                    }
                    return false;
            }
        }
        return PlayerMappings[(int)id].GetButton(btn);
    }

    /// <summary>
    /// Returns the state of button <paramref name="btn"/> of player <paramref name="id"/> using a playstation controller layout.
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool GetButton(PSButton btn, PlayerIndex id = PlayerIndex.Any) {
        return GetButton((CButton)btn, id);
    }

    /// <summary>
    /// Returns the <paramref name="axis"/> of player <paramref name="id"/>. The result is in range [-1, 1], except for the two triggers. 
    /// They are in range [0, 1].
    /// </summary>
    /// <param name="axis"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static float GetAxis(CAxis axis, PlayerIndex id = PlayerIndex.Any) {
        if(!IsInitialized) Initialize();
        if(Settings[axis]) return -GetAxisRaw(axis, id);
        return GetAxisRaw(axis, id);
    }
    private static float GetAxisRaw(CAxis axis, PlayerIndex id) {
        if(id == PlayerIndex.Any) {
            switch(settings.Behaviour) {
                case AnyBehaviour.UseControllerOne: return PlayerMappings[1].GetAxis(axis);
                case AnyBehaviour.CheckAll:
                    for(int i = 1; i < CarbonController.PlayerIndices; i++) {
                        float value = PlayerMappings[i].GetAxis(axis);
                        if(Mathf.Abs(value) > 0.02f) return value;
                    }
                    return 0f;
            }
        }
        return PlayerMappings[(int)id].GetAxis(axis);
    }

    /// <summary>
    /// Returns a <see cref="Vector2"/> for the specified stick of player <paramref name="id"/>.
    /// </summary>
    /// <param name="stick"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Vector2 GetStick(CStick stick, PlayerIndex id = PlayerIndex.Any) {
        switch(stick) {
            case CStick.Left: return GetLeftStick(id);
            case CStick.Right: return GetRightStick(id);
            default: return GetDPad(id);
        }
    }

    /// <summary>
    /// Returns a vector for the left thumbstick of player <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Vector2 GetLeftStick(PlayerIndex id = PlayerIndex.Any) {
        return new Vector2(GetAxis(CAxis.LX, id), GetAxis(CAxis.LY, id));
    }

    /// <summary>
    /// Returns a vector for the right thumbstick of player <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Vector2 GetRightStick(PlayerIndex id = PlayerIndex.Any) {
        return new Vector2(GetAxis(CAxis.RX, id), GetAxis(CAxis.RY, id));
    }

    /// <summary>
    /// Returns the left trigger of player <paramref name="id"/>. Result is in range [0, 1].
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static float GetLeftTrigger(PlayerIndex id = PlayerIndex.Any) {
        return GetAxis(CAxis.LT, id);
    }

    /// <summary>
    /// Returns the right trigger of player <paramref name="id"/>. Result is in range [0, 1].
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static float GetRightTrigger(PlayerIndex id = PlayerIndex.Any) {
        return GetAxis(CAxis.RT, id);
    }

    /// <summary>
    /// Returns a vector for the dpad of player <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static Vector2 GetDPad(PlayerIndex id = PlayerIndex.Any) {
        return new Vector2(GetAxis(CAxis.DX, id), GetAxis(CAxis.DY, id));
    }

    /// <summary>
    /// Returns the state of player <paramref name="id"/>. 
    /// A <see cref="GamePadState"/> contains all pressed buttons and axes values.
    /// It also stores information from the last frame in order to distinguish between a single press and a continuous pressing.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static GamePadState GetState(PlayerIndex id = PlayerIndex.Any) {
        if(!IsInitialized) Initialize();
        GamePadState state = States[(int)id];
        state.Update();
        return state;
    }
}
