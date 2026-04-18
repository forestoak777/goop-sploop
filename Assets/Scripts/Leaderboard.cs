using System.Collections.Generic;
using System.Threading.Tasks;      
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;
using TMPro;

public class Leaderboards: MonoBehaviour
{
    // Create a leaderboard with this ID in the Unity Dashboard
    const string LeaderboardId = "1000mTime";

    string VersionId { get; set; }
    int Offset { get; set; }
    int Limit { get; set; }
    int RangeLimit { get; set; }
    List<string> FriendIds { get; set; }

    public TextMeshProUGUI usernameDisplay, scoreboardDisplay;

    /*
        This code is a modified version of the unity leaderboard sample code. it shows the leaderboard on a TextMeshPro.
    */

    async void Awake()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
            {
        await SignInAnonymously();
        }
        Debug.Log("Singed In!");
        var playerName = await AuthenticationService.Instance.GetPlayerNameAsync();
        usernameDisplay.text = "Username: " + playerName;
        Debug.Log("Signed in as: " + playerName);
        
        GetPaginatedScoresByTier();
    }

    async Task SignInAnonymously()
    {
        AuthenticationService.Instance.SignedIn += async () =>
        {
            
        };
        AuthenticationService.Instance.SignInFailed += s =>
        {
            usernameDisplay.text = "User ID: SIGN IN FAILED. NO SCORE WILL BE POSTED.";
            // Take some action here...
            Debug.Log(s);

        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

    }

    public async void AddScore(float scoreNum)
    {
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, scoreNum);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    public async void GetScores()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    public async void GetPaginatedScores()
    {
        Offset = 10;
        Limit = 10;
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions{Offset = Offset, Limit = Limit});
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    public string[] tiers =
    {
        "Dirt", "Bronze", "Silver", "Gold", "MLG", "God", "Shrek", "Impossible"
    };

    int selectedTier = 0;

    public TextMeshProUGUI tierDisplay;

    public void IncreaseSelectedTier()
    {
        selectedTier++;

        if(selectedTier > tiers.Length - 1)
        {
            selectedTier = 0;
        }

        tierDisplay.text = tiers[selectedTier];

        GetPaginatedScoresByTier();
    }

        public void DecreaseSelectedTier()
    {
        selectedTier--;

        if(selectedTier < 0)
        {
            selectedTier = tiers.Length - 1;
        }

        tierDisplay.text = tiers[selectedTier];

        GetPaginatedScoresByTier();
    }

    struct playerScorePage
    {
        string tier;
        int limit;
        int total;
        public playerScoreResults[] results;
    }

    struct playerScoreResults
    {
        string playerId;
        public string playerName;
        public int rank;
        public float score;
        string tier;
    }

    public async void GetPaginatedScoresByTier()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresByTierAsync(
            LeaderboardId,
            tiers[selectedTier],
            new GetScoresByTierOptions{ Offset = 0, Limit = 50 }
        );
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));


        var scoreTextFinal = "";

        try{
            var scoresData = JsonConvert.DeserializeObject<playerScorePage>(JsonConvert.SerializeObject(scoresResponse));

            if(scoresData.results.Length == 0)
            {
                throw new System.Exception("poop");
            }
        
            foreach (playerScoreResults r in scoresData.results)
            {
                scoreTextFinal += "Rank: "+r.rank+" - Username: " + r.playerName + "- Time: " + r.score;
            }
        }
        catch
        {
            scoreTextFinal = "There are no " + tiers[selectedTier] + " tier scores yet!";
        }


        scoreboardDisplay.text = scoreTextFinal;
    }


    public async void GetPlayerScore()
    {
        var scoreResponse =
            await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    public async void GetVersionScores()
    {
        var versionScoresResponse =
            await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId);
    Debug.Log(JsonConvert.SerializeObject(versionScoresResponse));
    }
}
