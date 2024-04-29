using UnityEngine;
using TMPro;
public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI usernameText;

    void Start()
    {
        // Update the score text initially
        UpdateScoreText(0); // Asumiendo que el puntaje inicial es 0
    }

    // Método para actualizar el texto del puntaje
    public void UpdateScoreText(int score)
    {
        // Get the username from the 'Usuario' TextMeshProUI-Text
        string username = usernameText.text;

        // Update the score text with the username and score
        scoreText.text = $"{username}: Puntos: {score}";
    }
} 