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
        GetComponent<Slider>().value = PlayerPrefs.GetFloat("gradeDelay");
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
        SoundController.gradeDelay = GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("gradeDelay", GetComponent<Slider>().value);
        PlayerPrefs.Save();
    }
}
