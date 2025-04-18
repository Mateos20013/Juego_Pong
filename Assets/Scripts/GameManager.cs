using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text paddle1ScoreText;
    [SerializeField] private TMP_Text paddle2ScoreText;
    [SerializeField] private TMP_Text puntoMensaje;

    [SerializeField] private Transform player1Transform;
    [SerializeField] private Transform player2Transform;
    [SerializeField] private Transform BallTransform;

    private int player1Score;
    private int player2Score;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public void player1score()
    {
        player1Score++;
        paddle1ScoreText.text = player1Score.ToString();
        MostrarMensaje("Punto para el Jugador 1");
    }

    public void player2score()
    {
        player2Score++;
        paddle2ScoreText.text = player2Score.ToString();
        MostrarMensaje("Punto para el Jugador 2");
    }

    public void ResetGame()
    {
        player1Transform.position = new Vector3(-7f, player1Transform.position.y, 0f);
        player2Transform.position = new Vector3(7f, player2Transform.position.y, 0f);
        BallTransform.position = new Vector3(0f, BallTransform.position.y, 0f);
    }

    public void MostrarMensaje(string texto)
    {
        puntoMensaje.text = texto;
        puntoMensaje.gameObject.SetActive(true);
        CancelInvoke(nameof(OcultarMensaje));
        Invoke(nameof(OcultarMensaje), 2.5f);
    }

    private void OcultarMensaje()
    {
        puntoMensaje.gameObject.SetActive(false);
    }
}
