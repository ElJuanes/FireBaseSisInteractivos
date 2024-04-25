using UnityEngine;
using TMPro;
public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // Obtener la referencia al componente TextMeshPro
        scoreText = GameObject.Find("ScoreMinijuego").GetComponent<TextMeshProUGUI>();

        // Actualizar el texto inicialmente
        UpdateScoreText(0); // Asumiendo que el puntaje inicial es 0
    }

    // Método para actualizar el texto del puntaje
    public void UpdateScoreText(int score)
    {
        scoreText.text = "Puntos: " + score.ToString();
    }
}