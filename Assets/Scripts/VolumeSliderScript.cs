using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeSliderScript : MonoBehaviour
{
    public GameObject volumeSlider;
    public Slider mainSlider;
    // Start is called before the first frame update
    void Start()
    {
        mainSlider.value = AudioListener.volume;
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    private void Update()
    {
        volumeSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 3, Mathf.Clamp(Screen.height / 5, Screen.height / 5, 100));
        gameObject.transform.GetChild(3).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 3, Mathf.Clamp(Screen.height / 5, Screen.height / 5, 100));
    }

    public void ValueChangeCheck()
    {
        AudioListener.volume = mainSlider.value;
    }
}
