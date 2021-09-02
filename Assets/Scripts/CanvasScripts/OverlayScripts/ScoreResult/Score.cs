public class Score
{
    public string musicName;

    public bool highestRhythmScore;
    public bool highestMelodyScore;
    public bool highestTotalScore;

    public int rhythmScore;
    public int melodyScore;
    public int totalScore;

    public Score(string musicName, int rhythmScore, int melodyScore) {

        this.musicName = musicName;
        this.rhythmScore = rhythmScore;
        this.melodyScore = melodyScore;
        this.totalScore = rhythmScore + melodyScore;

        if (rhythmScore > this.rhythmScore)
            this.highestRhythmScore = true;
        else
            this.highestRhythmScore = false;

        if (melodyScore > this.melodyScore)
            this.highestMelodyScore = true;
        else
            this.highestMelodyScore = false;

        int totalScore = rhythmScore + melodyScore;

        if (totalScore > this.totalScore)
            this.highestTotalScore = true;
        else
            this.highestTotalScore = false;
    }

}
