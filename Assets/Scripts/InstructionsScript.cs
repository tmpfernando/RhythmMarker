using UnityEngine;
using UnityEngine.UI;

public class InstructionsScript : MonoBehaviour
{
    public Button InstructionsButton;
    public GameObject dialogueBox;
    public string[] dialogueMessage;
    public int index;

    private void Start()
    {
        dialogueMessage = new string[4];

        index = 0;

        dialogueMessage[0] = "Hello!!! Thank you for reading instructions!" +
            "\nUse WASD to walk (each hit correspond to one step)." +
            "\nUse UIJK to raise your arms." +
            "\nUse P to play and stop music." +
            "\nUse , (comma) and . (dot) to change actual music." +
            "\nUse Esc to open menu (we have a volume control there).";

        dialogueMessage[1] = "You have to do steps at the rhythm time." +
            "\nThe grade flashes to help you realise the rhythm (or can it confuse you if not synchronized with the sound...)" +
            "\nAbout melody, just raise your arms to represent music energy variations." +
            "\nIf you are playing browser version, unfortunately the melody mechanics will not work." +
            "\nBut you can download an executable for windows and enjoy all musics.";

        dialogueMessage[2] = "On next update we will certainly have a method to player correct video delay." +
            "\nDiferent equipment have diferent delay..." +
            "\nMaybe we will have someone else to dance together here." +
            "\nMaybe a RED, GREEN or BLUE NPC...";

        dialogueMessage[3] = "Do not worry about those information on top left of your screen." +
            "\nI am using it to verify if algorithm is working properly." +
            "\nIf you find some bug, or have any sugestions, you can type on ITHC.IO page.";

    }

    public void OnClick()
    {
        if (GameObject.Find("DialogueBox(Clone)") != null) {
            Object.Destroy(GameObject.Find("DialogueBox(Clone)"));
        }
        else {
            if (index >= dialogueMessage.Length)
                index = 0;

            Instantiate(dialogueBox, GameObject.Find("Canvas").GetComponent<RectTransform>().localPosition, Quaternion.identity, GameObject.Find("Canvas").GetComponent<Transform>().transform);
            GameObject.Find("DialogueBoxText").GetComponent<Text>().text = dialogueMessage[index];

            index++;
        }
               
    }

    private void Update()
    {

        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 4, Screen.height / 15);
        GetComponent<RectTransform>().position = new Vector3(Screen.width - GetComponent<RectTransform>().sizeDelta.x - 10, 10, 0);
    }
}
