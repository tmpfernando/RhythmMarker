using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAudioDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("audioDelay");
        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();
    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 5);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, 4 * Screen.height / 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ValueChangeCheck()
    {
        SoundController.audioDelay = GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("audioDelay", GetComponent<Slider>().value);
        PlayerPrefs.Save();
    }
}
