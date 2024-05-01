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
        StringBuilder leaderboardString = new StringBuilder(); // Create a StringBuilder

        dbReference.OrderByChild("score").LimitToLast(10).GetValueAsync() // Retrieve top 10 scores in descending order
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

              // Iterate through users (child nodes) in descending order
              List<DataSnapshot> users = new List<DataSnapshot>();
              foreach (DataSnapshot userSnapshot in snapshot.Children)
              {
                  users.Add(userSnapshot);
              }

              users.Reverse(); // Reverse the list for top-down order

              int i = 0;
              foreach (DataSnapshot userSnapshot in users)
              {
                  if (i >= 10) // Limit to top 10 entries
                      break;

                  // Retrieve username from the specific field (modify if needed)
                  string username = userSnapshot.Child("username").Value.ToString();
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

                  leaderboardString.AppendLine($"{username} = {score}"); // Add username and score to the string
                  i++;
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