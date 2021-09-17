using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//For HTTP Request
using UnityEngine.Networking;
//For easy json creation and parsing
using Newtonsoft.Json;
//For encoding the request to bytes
using System.Text;

public class WebHandlerScript : MonoBehaviour
{
    [HideInInspector] public static WebHandlerScript Instance;
    public void Awake()
    {
        //assigns the one instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //destroys the duplicate gameObject 
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public string BaseURL
    {
        //Base URL of our requests
        get { return "https://my-user-scoreboard.herokuapp.com/api/"; }
    }

    public void AddGroup()
    {
        Debug.Log("Creating Group");
        StartCoroutine(SampleAddGroup());
    }

    IEnumerator SampleAddGroup()
    {
        //Dictionary to contain the parameters to create a player
        Dictionary<string, string> PlayerParams =
            new Dictionary<string, string>();

        //A group needs these 3 parameters
        //group number of the group in Canvas
        PlayerParams.Add("group_num", "1");
        //group name
        PlayerParams.Add("group_name", "God Bless Studios");
        //game name
        PlayerParams.Add("game_name", "Alien Invaders");

        //Turns the Dictionary above to a JSON string
        string requestString = JsonConvert.SerializeObject(PlayerParams);
        //Convert the json string to a byte array
        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        //Create a request to the "players" resource
        //Using the verb "POST"
        UnityWebRequest request = new UnityWebRequest(BaseURL + "groups", "POST");
        //Set what type of data is in the request
        request.SetRequestHeader("Content-Type", "application/json");
        //Add the requestData using UploadHandlerRaw
        request.uploadHandler = new UploadHandlerRaw(requestData);
        //Create a receiver for the data later
        request.downloadHandler = new DownloadHandlerBuffer();

        //When ready- send the web request
        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        //Check if there are no errors
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Created Group: {request.downloadHandler.text}");
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }

    public void DeletePlayer()
    {
        Debug.Log("Deleting");
        StartCoroutine(SampleDeletePlayer());
    }

    IEnumerator SampleDeletePlayer()
    {
        //Always check the available ID in the server data to correctly access the info
        //Create a request to the player with ID 23 resource
        //Using the verb "DELETE"
        UnityWebRequest request = new UnityWebRequest(BaseURL + "players/23", "DELETE");
        //Create a receiver for the data later
        request.downloadHandler = new DownloadHandlerBuffer();

        //When ready- send the web request
        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        //Check if theres no errors
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Deleted Player: {request.downloadHandler.text}");
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }

    public void EditPlayer()
    {
        Debug.Log("Editing");
        StartCoroutine(SampleEditPlayerRoutine());
    }

    IEnumerator SampleEditPlayerRoutine()
    {
        //Dictionary to contain the parameters of an update data of the player
        Dictionary<string, string> PlayerParams = 
            new Dictionary<string, string>();

        //edit the name in the data
        //Update the player's name
        PlayerParams.Add("user_name", "mc");

        //Turns the Dictionary above to a JSON string
        string requestString = JsonConvert.SerializeObject(PlayerParams);
        //Convert the json string to a byte array
        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        //Create a request to the player with ID 23
        //Using the verb PUT
        UnityWebRequest request = new UnityWebRequest(BaseURL + "get_scores/1/1", "PUT");
        //Set what type of data is in the request
        request.SetRequestHeader("Content-Type", "application/json");
        //Add the requestData using UploadHandlerRaw
        request.uploadHandler = new UploadHandlerRaw(requestData);
        //Create a receiver for the data later
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        //Check if theres no errors
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");

            //Convert the response into a Dictionary
            Dictionary<string, string> player =
                JsonConvert.DeserializeObject<Dictionary<string, string>>
                                (request.downloadHandler.text);

            Debug.Log($"Got player: {player["nickname"]}");
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }

    public void GetPlayer()
    {
        Debug.Log("Getting the player");
        StartCoroutine(SampleGetPlayerRoutine());
    }

    IEnumerator SampleGetPlayerRoutine()
    {
        //Create a request to the player with ID 22 resource
        //Using the verb GET
        UnityWebRequest request = new UnityWebRequest(BaseURL + "players/23", "GET");
        //Create a receiver for the data later
        request.downloadHandler = new DownloadHandlerBuffer();

        //When ready- send the web request
        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        //Check if thres no errors
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");

            //Convert the response into a Dictionary
            Dictionary<string, string> player = 
                JsonConvert.DeserializeObject<Dictionary<string, string>>
                                (request.downloadHandler.text);

            Debug.Log($"Got player: {player["nickname"]}");
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }

    public void GetPlayers()
    {
        Debug.Log("Getting Players");
        StartCoroutine(SampleGetPlayersRoutine());
    }
    public void GetPlayersLeaderboard()
    {
        Debug.Log("Getting Leaderboard");
        StartCoroutine(SampleGetLeaderBoard());
    }
    //Gets all the players names
    IEnumerator SampleGetLeaderBoard()
    {
        //Create a request to the "players" resource
        //Using the verb GET
        UnityWebRequest request = new UnityWebRequest(BaseURL + "get_scores/1", "GET");
        //Create a receiver for the data later
        request.downloadHandler = new DownloadHandlerBuffer();

        //When ready- send the web request
        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        //Check if theres no errors
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");

            //Conver the response into a list of Dictionaries
            List<Dictionary<string, string>> playerListRaw = JsonConvert.DeserializeObject<
                                List<Dictionary<string, string>>
                                >(request.downloadHandler.text);

            LeaderBoard leaderBoard = GameObject.FindObjectOfType<LeaderBoard>();

            int max_get = leaderBoard.leaderBoardCount;
            int count = 0;

            //Iterate for testing
            foreach (Dictionary<string, string> player in playerListRaw)
            {
                leaderBoard.playersLeaderboard.Add(player["user_name"]);
                leaderBoard.scores.Add(player["score"]);
                //gets the top 5 player and their scores
                if (++count >= max_get)
                {
                    break;
                }
            }
            for (int i = 0; i < leaderBoard.leaderBoardCount; i++)
            {
                leaderBoard.playersRanks[i].SetText($"{i}. {leaderBoard.playersLeaderboard[i]} = {leaderBoard.scores[i]}");
            }

        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }
    //Gets all the players names
    IEnumerator SampleGetPlayersRoutine()
    {
        //Create a request to the "players" resource
        //Using the verb GET
        UnityWebRequest request = new UnityWebRequest(BaseURL + "get_scores/1", "GET");
        //Create a receiver for the data later
        request.downloadHandler = new DownloadHandlerBuffer();

        //When ready- send the web request
        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        //Check if theres no errors
        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Message: {request.downloadHandler.text}");

            //Conver the response into a list of Dictionaries
            List<Dictionary<string, string>> playerListRaw = JsonConvert.DeserializeObject<
                                List<Dictionary<string, string>>
                                >(request.downloadHandler.text);

            int max_get = 5;
            int count = 0;

            //Iterate for testing
            foreach (Dictionary<string, string> player in playerListRaw)
            {
                Debug.Log($"Got player: {player["user_name"]}");
                //gets the top 5 player
                if (++count >= max_get)
                {
                    break;
                }
            }
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }

    public void CreatePlayer()
    {
        Debug.Log($"Creating Player");
        StartCoroutine(SamplePostRoutine());
    }

    IEnumerator SamplePostRoutine()
    {
        //Dictionary to contain the parameters to create a player
        Dictionary<string, string> PlayerParams = 
            new Dictionary<string, string>();

        //A player needs these 4 parameters
        //Nickname of the player
        PlayerParams.Add("group_num", "1");
        //Name of the player
        PlayerParams.Add("user_name", UserAccountSc.Instance.UserName);
        Debug.Log($"User name: {UserAccountSc.Instance.UserName}");
        //Score of the player
        PlayerParams.Add("score", UserAccountSc.Instance.UserGameScore.ToString());
        Debug.Log($"User name: {UserAccountSc.Instance.UserGameScore.ToString()}");

        //Turns the Dictionary above to a JSON string
        string requestString = JsonConvert.SerializeObject(PlayerParams);
        //Convert the json string to a byte array
        byte[] requestData = new UTF8Encoding().GetBytes(requestString);

        //Create a request to the "players" resource
        //Using the verb "POST"
        UnityWebRequest request = new UnityWebRequest(BaseURL + "scores", "POST");
        //Set what type of data is in the request
        request.SetRequestHeader("Content-Type", "application/json");
        //Add the requestData using UploadHandlerRaw
        request.uploadHandler = new UploadHandlerRaw(requestData);
        //Create a receiver for the data later
        request.downloadHandler = new DownloadHandlerBuffer();

        //When ready- send the web request
        yield return request.SendWebRequest();

        Debug.Log($"Status Code: {request.responseCode}");
        //Check if there are no errors
        if(string.IsNullOrEmpty(request.error))
        {
            Debug.Log($"Created Player: {request.downloadHandler.text}");
        }
        else
        {
            Debug.LogError($"Error: {request.error}");
        }
    }
}
