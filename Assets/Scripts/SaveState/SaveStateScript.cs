[System.Serializable]
public class SaveStateScript
{

    #region VARIABLES_FOR_GAME_CONFIGURATION
    public float globalVolume;
    public string inputMethod;
    public float audioDelay;
    public float videoDelay;
    #endregion

    #region VARIABLES_FOR_SAVE_STATE
    public MusicInfo[] unlockedMusics;
    public MusicInfo[] playlist;
    public Message[] receivedMessages;
    public Score[] scoreHistory;
    #endregion


    public SaveStateScript(

        float globalVolume,
        string inputMethod,
        float audioDelay,
        float videoDelay,

        MusicInfo[] unlockedMusics,
        MusicInfo[] playlist,
        Message[] receivedMessages,
        Score[] scoreHistory

        )
    {

        this.globalVolume = globalVolume;
        this.inputMethod = inputMethod;
        this.audioDelay = audioDelay;
        this.videoDelay = videoDelay;

        this.unlockedMusics = unlockedMusics;
        this.playlist = playlist;
        this.receivedMessages = receivedMessages;
        this.scoreHistory = scoreHistory;

    }
}
