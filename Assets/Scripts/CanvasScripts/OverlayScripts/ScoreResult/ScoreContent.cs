using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class ScoreContent : MonoBehaviour
{
    public bool showScore;

    GameObject scoreResults;

    public string path;
    public static string fileName;

    public static string[] scoreHistory;

    public static int historySize = 10;


    // Start is called before the first frame update
    void Start()
    {

        //Instantiate score history
        scoreHistory = new string[historySize];

        showScore = false;

        //Load the prefab of overlay to show scores
        scoreResults = Resources.Load<GameObject>("Prefabs/Overlay/ScoreResult");

        //Listener to hear when the music plays and stops
        SoundController.musicIsPlaying.AddListener(MusicIsPlaying);
        SoundController.musicIsNotPlaying.AddListener(MusicIsNotPlaying);

        //Define the path to arquive that contains the information about score history
        path = Application.persistentDataPath + "/SaveState";
        fileName = Application.persistentDataPath + "/SaveState/ScoreHistory.txt";

        //Create directory if it do not exist
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        if (!File.Exists(fileName))
        {
            File.Create(fileName);
        }
            

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MusicIsPlaying()
    {
        //If music is playing it verifies if already reached the last song second
        if (SoundController.audioSource.clip.length - SoundController.audioSource.time < 1)
        {
            showScore = true;

            scoreHistory[0] = SoundController.Musics[SoundController.musicIndex].musicName
                    + " by " + SoundController.Musics[SoundController.musicIndex].artistName + "~" +
                    SoundController.rhythmScore + "~" + SoundController.melodyScore;
            
            //Load the score information stored in TXT file
            
        }

    }
    public void MusicIsNotPlaying()
    {
        //If Reached the end of music, then the score result rescreen have to be spawned
        if (showScore)
        {
            LoadScoreHistory();
            //if already spawned, does nothing
            if (GameObject.FindGameObjectsWithTag("scoreResult").Length < 1)
            {
                //Instatiate score result panel
                Instantiate(scoreResults, GameObject.Find("Canvas").transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            }
            showScore = false;
        }
    }

    public static void LoadScoreHistory()
    {

        //Instantiate a stream reader to read the TXT with score information
        StreamReader reader = new StreamReader(fileName);
        int i = 1;
        while (reader.ReadLine() != null && i < historySize)
        {
            scoreHistory[i] = reader.ReadLine();
            Debug.Log("scoreHistory[" + i + "]: " + scoreHistory[i]);
            i++;
        }
        reader.Close();


        //Instantiate a Stream Writer to write on TXT file
        StreamWriter writer = new StreamWriter(fileName);
        for (int k = 0; k < historySize; k++)
        {
            if (scoreHistory[k] != null)
                writer.WriteLine(scoreHistory[k]);
        }
        writer.Close();
        //Do not forget to close Stream Writer
    }

    public void LoadScoreHistoryFirsTime()
    {
        StreamReader reader = new StreamReader(fileName);
        int i = 0;
        while (reader.ReadLine() != null && i < historySize)
        {
            scoreHistory[i] = reader.ReadLine();
            Debug.Log("scoreHistory[i]: " + scoreHistory[i]);
            i++;
        }
        reader.Close();
    }

}
