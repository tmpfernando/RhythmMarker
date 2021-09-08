using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TittleScoreResult : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();
    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.2f, Screen.height / 10);
        GetComponent<RectTransform>().localPosition = new Vector2(0, 4.5f * (Screen.height / 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
