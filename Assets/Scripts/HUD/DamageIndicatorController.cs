﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorController : MonoBehaviour {
    public GameObject IndicatorPrefab;
    public GameObject player;
    public void TriggerIndicator(float damage)
    {
        RectTransform parent = GetComponentInParent<RectTransform>();
        Vector2 pLocation = player.transform.position;
        Vector2 position = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().WorldToScreenPoint(new Vector2(pLocation.x + Random.Range(-.5f, .5f), pLocation.y + Random.Range(-.5f, .5f)));
        GameObject instance = GameObject.Instantiate(IndicatorPrefab, GetComponentInParent<RectTransform>(), true);
        instance.GetComponent<DamageIndicator>().transform.SetParent(parent.transform, false);
        instance.transform.position = position;
        instance.GetComponent<DamageIndicator>().Init(damage);

    }
	// Update is called once per frame
	void Update () {
		
	}
}