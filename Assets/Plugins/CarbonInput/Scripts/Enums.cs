using System;
using UnityEngine;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

/// <summary>
/// Specifies the game controller associated with a player.
/// </summary>
public enum PlayerIndex {
    Any, One, Two, Three, Four, Five, Six, Seven, Eight
}

/// <summary>
/// Describes a single button of a gamepad using the common XBox layout.
/// </summary>
public enum CButton {
    A, B, X, Y,
    Back, Start,
    LB, RB,
    LS, RS,
}

/// <summary>
/// Describes a single button of a gamepad using the playstation layout.
/// </summary>
public enum PSButton {
    Cross, Circle, Square, Triangle,
    Select, Start,
    L1, R1,
    //L2 and R2 are mapped by LT and RT
    L3, R3
}

/// <summary>
/// Describes a single axis of a gamepad. The dpad is also considered an axis.
/// </summary>
public enum CAxis {
    LX, LY,
    RX, RY,
    LT, RT,
    DX, DY
}

/// <summary>
/// Flag mapping used to define all supported platforms.
/// </summary>
[Flags]
public enum CPlatform {
    Windows = 1 << 0,
    Linux = 1 << 1,
    OSX = 1 << 2,
    WSA = 1 << 3,
    Android = 1 << 4,
    IOS = 1 << 5,
    WP8 = 1 << 6,
    Wii = 1 << 7,
    XBox360 = 1 << 8,
    XBoxOne = 1 << 9,
    PS3 = 1 << 10,
    PS4 = 1 << 11,
    PSP2 = 1 << 12
}

/// <summary>
/// Enumeration of all sticks of a gamepad. Used to get a <see cref="Vector2"/> consisting of the corresponding x and y values of a given axis.
/// </summary>
public enum CStick {
    Left, Right, DPad
}

/// <summary>
/// Describes the different behaviours of <see cref="PlayerIndex.Any"/>.
/// </summary>
public enum AnyBehaviour {
    /// <summary>
    /// Use the same mapping <see cref="PlayerIndex.One"/> uses, but listen on any gamepad for that mapping.
    /// </summary>
    UseMappingOne,
    /// <summary>
    /// Always use <see cref="PlayerIndex.One"/> whenever <see cref="PlayerIndex.Any"/> is used.
    /// </summary>
    UseControllerOne,
    /// <summary>
    /// Go over all players and use first match.
    /// Slightly slower than the other two behaviours, but it is the most accurate.
    /// </summary>
    CheckAll
}

