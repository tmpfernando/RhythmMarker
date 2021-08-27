using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxScript : MonoBehaviour
{
    Text dialogueBoxText;
    public Button okButton;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBoxText = gameObject.transform.GetChild(1).GetComponent<Text>();
        okButton = gameObject.transform.GetChild(0).GetComponent<Button>();

        okButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        okButton.GetComponent<RectTransform>().localPosition = new Vector3(0, okButton.GetComponent<RectTransform>().sizeDelta.y - Screen.height / 2, 0);
        okButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Clamp(Screen.width / 5, 100, Screen.width / 5), Mathf.Clamp(Screen.height / 8, 40, Screen.height / 8));

        dialogueBoxText.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 20, Screen.height - okButton.GetComponent<RectTransform>().sizeDelta.y - 40);
        dialogueBoxText.GetComponent<RectTransform>().localPosition = new Vector3(0, (okButton.GetComponent<RectTransform>().sizeDelta.y + 20) / 2, 0);
        
    }

    public void OnCLick() {

        //while (GetComponent<RectTransform>().sizeDelta.x > 0.01f) {
        //    GetComponent<RectTransform>().sizeDelta =
        //    new Vector2(GetComponent<RectTransform>().sizeDelta.x * Time.deltaTime * 0.99f,
        //    GetComponent<RectTransform>().sizeDelta.y * Time.deltaTime * 0.99f);
        //}

        Object.Destroy(gameObject);
    }

}
