using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentInteractionsMenu : MonoBehaviour
{

    public static List<Message> messageList = new List<Message>();
    public TextAsset messages;

    public GameObject interactionRecord;
    public float instaceYposition;

    public Button firstSelected;
    public float positionY;

    // Start is called before the first frame update
    void Start()
    {

        instaceYposition = 0;
        interactionRecord = Resources.Load<GameObject>("Prefabs/Menus/RecordInteraction");

        LoadMessages();
        InstanceMessages();

        ScreenResolutionCheck.screenResolutionChange.AddListener(ScreenSizeAdjustments);
        ScreenSizeAdjustments();

    }

    void ScreenSizeAdjustments()
    {
        //Make size and position adjusts if any screen resolution was detected
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (messageList.Count + 2) * Screen.height / 5);

        positionY = (messageList.Count + 2) / 2 * Screen.height / 5 - Screen.height / 5;

        for (int i = messageList.Count - 1; i >= 0 ; i--)
        {
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, positionY);
            transform.GetChild(i).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height / 5);
            transform.GetChild(i).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width / 1.4f);
            positionY -= Screen.height / 5;
        }
    }

    public void LoadMessages()
    {
        messageList.Clear();

        messages = Resources.Load<TextAsset>("Messages");

        string[] data = messages.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { '~' });

            if (row[1] != "")
            {
                Message m = new Message();
                m.messageIndex = int.Parse(row[0]);
                m.receivedFrom = row[1];
                m.messageText = row[2];

                messageList.Add(m);
                Debug.Log(m);
            }
        }
    }

    public void InstanceMessages()
    {
        foreach (Message m in messageList)
        {
            interactionRecord.transform.GetChild(0).GetComponent<Text>().text = "Message From: " + m.receivedFrom + "\n" + m.messageText;

            if (m.receivedFrom == "RED")
                interactionRecord.transform.GetChild(0).GetComponent<Text>().color = new Color(0.3f, 0, 0, 1);
            else if (m.receivedFrom == "GREEN")
                interactionRecord.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0.3f, 0, 1);
            else if (m.receivedFrom == "BLUE")
                interactionRecord.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 0, 0.3f, 1);
            else if (m.receivedFrom == "INVERSE WAVE")
                interactionRecord.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 1);

            instaceYposition += Screen.height / 5;

            Instantiate(interactionRecord, new Vector2(Screen.width / 2, instaceYposition), Quaternion.identity, transform);
        }
    }
}
