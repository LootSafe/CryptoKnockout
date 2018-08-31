using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorController : MonoBehaviour {
    public GameObject indicatorPrefab;
    public GameObject criticalPrefab;
    public GameObject player;
    public Vector2 randomRange;

    public void TriggerIndicator(float damage, Player source, bool critical)
    {
        RectTransform parent = GetComponentInParent<RectTransform>();
        Vector2 pLocation = player.transform.position;
        Vector2 position = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(new Vector2(pLocation.x + Random.Range(-randomRange.x, randomRange.x), pLocation.y + Random.Range(-randomRange.y, randomRange.y)));
        GameObject instance;

        if (critical)
        {
            instance = GameObject.Instantiate(criticalPrefab, GetComponentInParent<RectTransform>(), true);
        }
        else
        {
            instance = GameObject.Instantiate(indicatorPrefab, GetComponentInParent<RectTransform>(), true);
        }
        
        instance.GetComponent<DamageIndicator>().transform.SetParent(parent.transform, false);
        instance.transform.position = position;

        instance.GetComponent<DamageIndicator>().Init(damage, player.GetComponent<Player>(), critical, source);

    }

	// Update is called once per frame
	void Update () {
		
	}
}
