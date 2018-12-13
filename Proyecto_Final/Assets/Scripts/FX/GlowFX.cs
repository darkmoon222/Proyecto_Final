using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowFX : MonoBehaviour {


    //SerializeField -> hace que las variables privadas se puedan ver desde el editor
    [SerializeField]
    private SpriteRenderer sRenderer;

    private Color currentColor;

    [Header("Alpha Values")]
    public float maxAlpha, minAlpha;

    public bool up = false;

    float currentTime = 0;
    public float maxTime;

	// Use this for initialization
	void Start () {
        currentColor = sRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {

        #region TimeCounter
        currentTime += Time.deltaTime;

        if (currentTime >= maxTime)
        {
            up = !up;
            currentTime = 0;
        }
        #endregion

        #region Color Change
        if (up)
        {
            currentColor.a = Mathf.Lerp(currentColor.a, minAlpha, currentTime * 0.1f);
            
        }
        else
        {
            currentColor.a = Mathf.Lerp(currentColor.a, maxAlpha, currentTime * 0.1f);
        }

        sRenderer.color = currentColor;

        #endregion
    }
}
