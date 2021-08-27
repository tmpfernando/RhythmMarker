using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxTest : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject menuBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendDialogue(string dialogMessage) {
        Instantiate(dialogueBox, GameObject.Find("Canvas").GetComponent<RectTransform>().localPosition, Quaternion.identity, GameObject.Find("Canvas").GetComponent<Transform>().transform);
        GameObject.Find("DialogueBoxText").GetComponent<Text>().text = dialogMessage;
        GameObject.Find("DialogueBoxText").GetComponent<Text>().fontSize = 40;
    }
}
