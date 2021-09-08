using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDelaysScript : MonoBehaviour
{
    public GameObject delayMenu;
    // Start is called before the first frame update
    void Start()
    {
        delayMenu = Resources.Load<GameObject>("Prefabs/Menus/MenuDelays");

        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();
    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, 5 * Screen.height / 10);
    }

    public void OnClick() {

        Instantiate(delayMenu, transform.parent.transform.parent.transform);
        Object.Destroy(transform.parent.gameObject);
    }
}
