using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionLocks {

    PlayerEntity player;

    bool punch, kick, block, super;

    public ActionLocks(PlayerEntity parent)
    {
        player = parent;
    }

    public bool AnyLock()
    {
        return punch || kick || block || super;
    }

    public void Reset()
    {
        punch = false;
        kick = false;
        block = false;
        super = false;
    }

    public enum Locks
    {
        PUNCH,
        KICK,
        BLOCK,
        SUPER,
        ALL
    }

    public void Lock(Locks action){
        switch (action)
        {
            case Locks.PUNCH:
                punch = true;
                break;
            case Locks.BLOCK:
                block = true;
                break;
            case Locks.KICK:
                kick = true;
                break;
            case Locks.SUPER:
                super = true;
                break;
        }
        return;
    }

    public void Release(Locks action)
    {
        switch (action)
        {
            case Locks.PUNCH:
                punch = false;
                break;
            case Locks.BLOCK:
                block = false;
                break;
            case Locks.KICK:
                kick = false;
                break;
            case Locks.SUPER:
                super = false;
                break;
            case Locks.ALL:
                Reset();
                break;
        }
        return;
    }


}
