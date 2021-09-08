using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudioDelayTest : MonoBehaviour
{
    public GameObject audioDelayTestMenu;

    // Start is called before the first frame update
    void Start()
    {

        audioDelayTestMenu = Resources.Load<GameObject>("Prefabs/Menus/MenuAudioDelay");

        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();

    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected

        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 4, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, 2 * Screen.height / 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        Instantiate(audioDelayTestMenu, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        Object.Destroy(transform.parent.gameObject);
    }
}
