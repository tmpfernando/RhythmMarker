using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractionScript : MonoBehaviour
{
    public GameObject interactionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        interactionsMenu = Resources.Load<GameObject>("Prefabs/Menus/MenuInteractions");
        ScreenSizeAdjustments();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        Instantiate(interactionsMenu, transform.parent.transform.parent.transform);
        Object.Destroy(transform.parent.gameObject);
    }

    void ScreenSizeAdjustments()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 6, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(5 * (Screen.width / 6), 9 * (Screen.height / 10));
    }
}
