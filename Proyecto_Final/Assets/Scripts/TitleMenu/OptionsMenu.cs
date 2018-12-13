using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    public Dropdown resDropdown;
    public Dropdown qualityDropdown;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(resDropdown.value)
        {
            case 0:
                {
                    Screen.SetResolution(1280, 720, true);
                    break;
                }
            case 1:
                {
                    Screen.SetResolution(1600, 1200, true);
                    break;
                }
            case 2:
                {
                    Screen.SetResolution(1920, 1080, true);
                    break;
                }
            default:
                {
                    Screen.SetResolution(1280, 720, true);
                    break;
                }
        }

        QualitySettings.SetQualityLevel(qualityDropdown.value, true);
    }
}
