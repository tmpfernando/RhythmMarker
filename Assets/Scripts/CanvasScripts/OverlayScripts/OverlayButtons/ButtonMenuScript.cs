using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMenuScript : MonoBehaviour
{
    public GameObject menuPanel;
    public Button firstSelected;

    // Start is called before the first frame update
    void Start()
    {
        InputSetup.menuButton.AddListener(OnClick);
        menuPanel = Resources.Load<GameObject>("Prefabs/Menus/MenuMain");
        firstSelected = menuPanel.transform.GetChild(0).GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 6, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 6, 9 * (Screen.height / 10));
    }

    public void OnClick()
    {
        if (GameObject.FindGameObjectsWithTag("menu").Length == 0)
        {
            Instantiate(menuPanel, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            firstSelected.Select();
            Object.Destroy(transform.parent.gameObject);
        }
    }
}
