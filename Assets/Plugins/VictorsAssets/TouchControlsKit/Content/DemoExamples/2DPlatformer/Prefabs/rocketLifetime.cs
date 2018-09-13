using UnityEngine;

public class rocketLifetime : MonoBehaviour
{
    private float lifeTime = 0f;

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if( lifeTime > .75f )
            GameObject.DestroyObject( gameObject );
    }
}
