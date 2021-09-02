using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class ButtonPlayScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundController.musicIsPlaying.AddListener(MusicIsPlaying);
        SoundController.musicIsNotPlaying.AddListener(MusicIsNotPlaying);
        transform.GetComponent<Button>().Select();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 2, Screen.height / 10);
        GetComponent<RectTransform>().position = new Vector2(Screen.width / 2, Screen.height / 10);
    }

    public void OnClick()
    {
        InputSetup.playButton.Invoke();
    }

    public void MusicIsPlaying()
    {
        Debug.Log("PLAYS");
        transform.GetChild(0).GetComponent<Text>().text = "Music: " + SoundController.Musics[SoundController.musicIndex].musicName + " by " + SoundController.Musics[SoundController.musicIndex].artistName;
    }

    public void MusicIsNotPlaying()
    {
        Debug.Log("NOT PLAYS");
        transform.GetChild(0).GetComponent<Text>().text = "PLAY MUSIC";
    }
}
