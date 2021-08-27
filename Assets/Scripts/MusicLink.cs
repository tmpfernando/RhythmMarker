using UnityEngine;
using UnityEngine.UI;

public class MusicLink : MonoBehaviour
{
    public Button MusicLinkButton;
    private Text textMusicLinkButton;

    private void Start()
    {
        textMusicLinkButton = GameObject.Find("TextMusicLinkButton").GetComponent<Text>();

        //MusicLinkButton.onClick = new Button.ButtonClickedEvent();
        //MusicLinkButton.onClick.AddListener(() => OpenURL());

    }

    public void OpenURL()
    {
        Application.OpenURL(SoundController.Musics[SoundController.musicIndex].link);
        Debug.Log("Music Link: " + SoundController.Musics[SoundController.musicIndex].link);
    }

    private void Update()
    {
        textMusicLinkButton.text = "Music: " + SoundController.Musics[SoundController.musicIndex].musicName + " by " + SoundController.Musics[SoundController.musicIndex].artistName;
        GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Clamp(30 + 10 * textMusicLinkButton.text.Length, textMusicLinkButton.text.Length, Screen.width / 2) , Screen.height / 15);
    }
}
