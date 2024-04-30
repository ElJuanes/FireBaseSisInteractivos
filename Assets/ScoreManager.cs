using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using TMPro;
using Firebase.Extensions;
using System.Text;

public class ScoreboardManager : MonoBehaviour
{
    private DatabaseReference dbReference;

    public GameObject scoreboard; // Reference to the Scoreboard GameObject
    public TextMeshProUGUI text; // Reference to the TextMeshPro object for displaying the leaderboard
    public GameObject LeaderboardUserText;

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
            Debug.Log("Se inicio firebase2");

        });
    }

    public void GetScoreboardData()
    {
        Debug.Log("Se inicio GetScoreboardData");
        StringBuilder leaderboardString = new StringBuilder();
        dbReference.OrderByChild("score").LimitToLast(10).GetValueAsync() // Retrieve top 10 scores
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
                Debug.Log("iniciando limpieza");

                //ClearLeaderboardUI();
                //Debug.Log("Acabando limpieza");

                // Iterate through users (child nodes) in the order they are stored
                int i = 0;
                foreach (DataSnapshot userSnapshot in snapshot.Children)
                {
                    if (i >= 10) // Limit to top 10 entries
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
                    leaderboardString.AppendLine($"{username} = {score}"); // Add username and score to the string
                    i++;
                    // Debug.Log($"Leaderboard entrytest2 {i + 1}: {username} - {score}");
                    // Update UI elements with username and score

                }
                text.text = leaderboardString.ToString();
                
            });
    }

    public void RefrescarLeader()
    {
        Debug.Log("Refrescando la leaderboard");
        LeaderboardUserText.SetActive(false);
        LeaderboardUserText.SetActive(true);
    }

    // Clear leaderboard UI text
    /*  void ClearLeaderboardUI()
      {
          foreach (TextMeshProUGUI text in elements.GetComponentsInChildren<TextMeshProUGUI>())
          {
              text.text = "";
          }

      }*/
}