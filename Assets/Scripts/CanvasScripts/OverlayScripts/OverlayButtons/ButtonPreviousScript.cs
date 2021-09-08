using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPreviousScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        InputSetup.previousMusicButton.Invoke();
    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 8, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 8, Screen.height / 10);
    }
}
