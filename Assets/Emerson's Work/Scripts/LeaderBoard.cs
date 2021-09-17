using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
    //For leaderboard
    [HideInInspector] public List<string> playersLeaderboard;
    [HideInInspector] public List<string> scores;
    public List<TextMeshProUGUI> playersRanks; //LeaderBoard

    [HideInInspector] public int leaderBoardCount = 5;

    private void OnEnable()
    {
        updateRank();
    }

    public void updateRank()
    {
        playersLeaderboard.Clear();
        scores.Clear();
        WebHandlerScript.Instance.GetPlayersLeaderboard();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
