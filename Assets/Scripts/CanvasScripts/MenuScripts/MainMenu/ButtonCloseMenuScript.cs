using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCloseMenuScript : MonoBehaviour
{
    GameObject blackSurfaceOverlay;

    // Start is called before the first frame update
    void Start()
    {
        blackSurfaceOverlay = Resources.Load<GameObject>("Prefabs/Overlay/BlackSurfaceOverlay");
        InputSetup.menuButton.AddListener(OnClick);
        GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 6, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 6, (Screen.height / 10));
    }

    public void OnClick() {
        Instantiate(blackSurfaceOverlay, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
        Object.Destroy(transform.parent.gameObject);
    }
}
