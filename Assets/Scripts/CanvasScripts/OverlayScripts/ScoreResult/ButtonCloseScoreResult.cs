using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCloseScoreResult : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        InputSetup.menuButton.AddListener(OnClick);
        GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 6, Screen.height / 10);
        GetComponent<RectTransform>().localPosition = new Vector2(0, - 4 * (Screen.height / 10));
    }

    public void OnClick()
    {
        Object.Destroy(transform.parent.gameObject);
    }
}
