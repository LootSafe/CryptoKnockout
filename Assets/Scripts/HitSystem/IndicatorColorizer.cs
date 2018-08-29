using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorColorizer : MonoBehaviour {


    public Color[] bgColors;
    public Color[] fgColors;
    public Color[] txtColors;

    public Image bg;
    public Image fg;
    public Text txt;
        
    // Use this for initialization
	void Start () {

        bg.color = bgColors[Random.Range(0, bgColors.Length)];
        fg.color = fgColors[Random.Range(0, fgColors.Length)];
        txt.color = txtColors[Random.Range(0, txtColors.Length)];
	}
	

}
