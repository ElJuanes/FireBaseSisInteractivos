using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;
using Firebase.Extensions;

public class ScoreboardManager : MonoBehaviour
{
    private DatabaseReference dbReference;

    public GameObject scoreboard; // Reference to the Scoreboard GameObject
    public GameObject elements;
    public TextMeshProUGUI[] leaderboardUserText; // Array to hold username TextMeshPro objects
    public TextMeshProUGUI[] leaderboardScoreText; // Array to hold score TextMeshPro objects

    void Start()
    {
        // Ensure Firebase is properly initialized before accessing the database
        FirebaseApp.CheckDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Firebase initialization failed: " + task.Exception);
                return;
            }
           
            dbReference = FirebaseDatabase.DefaultInstance.RootReference.Child("users");
            Debug.Log("Se inicio firebase");
            GetScoreboardData();    
        });
    }

    public void GetScoreboardData()
    {
        Debug.Log("Se inicio GetScoreboardData");
        dbReference.OrderByChild("score").LimitToFirst(10).GetValueAsync() // Retrieve top 10 scores
            .ContinueWith(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError("Error retrieving score data: " + task.Exception);
                    return;
                }

                DataSnapshot snapshot = task.Result;
                if (snapshot.Value == null)
                {
                    Debug.LogWarning("No score data found in the database.");
                    return;
                }

                // Clear existing UI data
                ClearLeaderboardUI();

                // Iterate through users (child nodes) in the order they are stored
                int i = 0;
                foreach (DataSnapshot userSnapshot in snapshot.Children)
                {
                    if (i >= leaderboardUserText.Length || i >= leaderboardScoreText.Length)
                        break;

                    string username = userSnapshot.Key; // Key is the username
                    int score = 0;
                    try
                    {
                        score = int.Parse(userSnapshot.Child("score").Value.ToString());
                    }
                    catch (System.FormatException)
                    {
                        Debug.LogError("Invalid score format for user: " + username);
                        Debug.LogError("Score data: " + userSnapshot.Child("score").Value.ToString());
                        continue; // Skip this user if score parsing fails
                    }
                    Debug.Log($"Leaderboard entry {i + 1}: {username} - {score}");
                    // Update UI elements with username and score
                    leaderboardUserText[i].text = username;
                    leaderboardScoreText[i].text = score.ToString();
                   

                    i++;
                }
            });
    }

    // Clear leaderboard UI text
    void ClearLeaderboardUI()
    {
        foreach (TextMeshProUGUI text in elements.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.text = "";
        }
    }
}