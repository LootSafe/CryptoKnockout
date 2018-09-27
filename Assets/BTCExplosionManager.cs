using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCExplosionManager : MonoBehaviour {

    public BTCCharacterCollider collider;
    public GameObject explosion;

    public void TriggerExplosion()
    {
        explosion.transform.position = collider.GetLocation();
        explosion.SetActive(true);
    }

    public bool HasLanded()
    {
        if (!collider.gameObject.activeSelf) collider.gameObject.SetActive(true);
        return collider.HasLanded();
    }

    public void Reset()
    {
        collider.Reset();
        explosion.SetActive(false);
        collider.gameObject.SetActive(false);
    }


}
