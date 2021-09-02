using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMusicListScript : MonoBehaviour
{
    public GameObject musicListMenu;

    // Start is called before the first frame update
    void Start()
    {
        musicListMenu = Resources.Load<GameObject>("Prefabs/Menus/MenuMusicList");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, 6 * Screen.height / 10);
    }

    public void OnClick()
    {

        Instantiate(musicListMenu, transform.parent.transform.parent.transform);
        Object.Destroy(transform.parent.gameObject);

    }
}
