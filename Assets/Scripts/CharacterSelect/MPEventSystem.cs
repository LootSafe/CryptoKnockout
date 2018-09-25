using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MPEventSystem : EventSystem {

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        EventSystem originalCurrent = EventSystem.current;
        current = this;
        base.Update();
        current = originalCurrent;
    }
}
