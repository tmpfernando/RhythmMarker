using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVideoDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Slider>().value = AudioListener.volume;
        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();
    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 5);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, 8 * Screen.height / 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValueChangeCheck()
    {
        //AudioListener.volume = Mathf.Clamp(GetComponent<Slider>().value, 0.01f, 0.99f);
    }
}
