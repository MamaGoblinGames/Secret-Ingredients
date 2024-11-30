public class ResultData
{
    public int disguiseRating;
    public string MissionScore
    {
        get
        {
            return CalculateMissionRating(disguiseRating);
        }
    }

    public string DisguiseRatingText
    {
        get
        {
            return "Your disguise was " + disguiseRating.ToString() + "% effective.";
        }
    }

    public ResultData(int disguiseRating) {
        this.disguiseRating = disguiseRating;
    }

    private string CalculateMissionRating(int disguiseRating) {
        if (disguiseRating == 100) {
            return "A++";
        } else if (disguiseRating >= 95) {
            return "A+";
        } else if (disguiseRating >= 90) {
            return "A";
        } else if (disguiseRating >= 85) {
            return "B";
        } else if (disguiseRating >= 75) {
            return "C";
        } else if (disguiseRating >= 65) {
            return "D";
        } else {
            return "F";
        }
    }
}