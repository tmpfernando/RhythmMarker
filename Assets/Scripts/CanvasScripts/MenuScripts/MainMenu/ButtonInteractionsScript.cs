using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractionsScript : MonoBehaviour
{
    public GameObject interactionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        interactionsMenu = Resources.Load<GameObject>("Prefabs/Menus/MenuInteractions");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, 7 * Screen.height / 10);
    }

    public void OnClick()
    {

        Instantiate(interactionsMenu, transform.parent.transform.parent.transform);
        Object.Destroy(transform.parent.gameObject);

    }
}
