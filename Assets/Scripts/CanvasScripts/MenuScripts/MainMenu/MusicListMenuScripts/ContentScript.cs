using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentScript : MonoBehaviour
{
    public GameObject buttonMusicLink;
    public float positionY;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (SoundController.Musics.Count + 2) * Screen.height / 10);

        positionY = SoundController.Musics.Count * Screen.height / 20;

        buttonMusicLink = Resources.Load<GameObject>("Prefabs/Menus/ButtonMusicLink");

        for (int i = 0; i < SoundController.Musics.Count; i++)
        {
            positionY -= Screen.height / 10;
            Instantiate(buttonMusicLink, new Vector2(0, positionY), Quaternion.identity, transform);

        }

        for (int i = 0; i < SoundController.Musics.Count; i++)
        {
            transform.GetChild(i).name = "MusicLink" + i;
            transform.GetChild(i).GetComponent<ButtonMusicLinkScript>().musicName = SoundController.Musics[i].musicName;
            transform.GetChild(i).GetComponent<ButtonMusicLinkScript>().artistName = SoundController.Musics[i].artistName;
            transform.GetChild(i).GetComponent<ButtonMusicLinkScript>().link = SoundController.Musics[i].link;

            Debug.Log("added to music list menu: " + buttonMusicLink.name);
        }
    }

    // Update is called once per frame
    void Update()
    {

        positionY = SoundController.Musics.Count * Screen.height / 20;

        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (SoundController.Musics.Count + 2) * Screen.height / 10);

        for (int i = 0; i < SoundController.Musics.Count; i++)
        {
            positionY -= Screen.height / 10;
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, positionY);
            transform.GetChild(i).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SoundController.Musics.Count * Screen.height / 10);
        }
    }
}
