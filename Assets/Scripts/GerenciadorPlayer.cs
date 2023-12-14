using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorPlayer : MonoBehaviour
{
    public static bool jogoEncerrado;
    public GameObject painelGameOver;

    public static bool jogoIniciado;
    public GameObject textoInicial;

    public static int numeroDeGasolinas;
    public int tempoDeJogo;
    public float cronometro;

    public Text textoGasolinas;
    public Text textoTempo;
    public Text textoVelocidade;

    public int velocidade;

    private static string nomeDoPlayer;
    bool jaFeito = false;

    // Start is called before the first frame update
    void Start()
    {
        cronometro = 0.0f;
        tempoDeJogo = 0;

        velocidade = 0;

        jogoEncerrado = false;
        Time.timeScale = 1;
        jogoIniciado = false;
        numeroDeGasolinas = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AtualizarTempo();
        AtualizarVelocidade();

        if (jogoEncerrado)
        {
            Time.timeScale = 0;
            if (!jaFeito)
            {
                Eventos eventosObjeto = FindObjectOfType<Eventos>();
                jaFeito = true;

                // Ativar o painel de Game Over
                if (painelGameOver != null)
                {
                    painelGameOver.SetActive(true);
                }
            }
        }

        textoGasolinas.text = "Gasolina: " + numeroDeGasolinas;
        textoTempo.text = "Tempo: " + FormatarTextoTempo();
        textoVelocidade.text = "Velocidade: " + FormatarTextoVelocidade();

        StartCoroutine(IniciarJogo());
    }

    void AtualizarTempo()
    {
        if (jogoIniciado)
        {
            cronometro += Time.deltaTime;
            tempoDeJogo = Convert.ToInt32(cronometro);
        }
    }

    void AtualizarVelocidade()
    {
        GameObject Player = GameObject.Find("Player");
        ControlePlayer controle = Player.GetComponent<ControlePlayer>();
        velocidade = Convert.ToInt32(controle.velocidadeExibida);
    }

    string FormatarTextoVelocidade()
    {
        Color laranja = new Color(1.0f, 0.64f, 0.0f);

        switch (velocidade)
        {
            case int s when (s < 220 && s >= 150):
                textoVelocidade.color = laranja;
                textoVelocidade.fontSize = 55;
                break;

            case int s when (s <= 300 && s >= 220):
                textoVelocidade.color = Color.red;
                textoVelocidade.fontSize = 65;
                break;

            default:
                break;
        }

        return string.Format("{0} km/h", velocidade);
    }

    string FormatarTextoTempo()
    {
        return (tempoDeJogo.ToString()).PadLeft(3, ' ') + "s";
    }

    private IEnumerator IniciarJogo()
    {
        if (Movement.toque)
        {
            if (!jogoIniciado)
            {

                yield return new WaitForSeconds(1);

                jogoIniciado = true;

                Destroy(textoInicial);
            }
        }
    }


}
