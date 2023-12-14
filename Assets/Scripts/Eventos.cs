using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Eventos : MonoBehaviour
{
    public Text textoGameOver;
    public Text textoVelocidade;
    public GameObject painelGameOver;

    public void Awake()
    {
        painelGameOver.SetActive(false);
    }

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene("Level");
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }

    private void EsconderElementosUI()
    {
        this.textoGameOver.gameObject.SetActive(false);
        this.textoVelocidade.gameObject.SetActive(false);
       
    }

    public void ExibirElementosUI()
    {
        this.textoGameOver.gameObject.SetActive(true);
        this.textoVelocidade.gameObject.SetActive(true);
    }

    public void OnClickBotaoPular()
    {
        this.painelGameOver.SetActive(true);
    }

    public void ExibirPainelGameOver()
    {
        this.painelGameOver.SetActive(true);
    }

    public void EsconderPainelGameOver()
    {
        this.painelGameOver.SetActive(false);
    }

    public void RetornarAoMenuPrincipal()
    {
        SceneManager.LoadScene("Menu");
    }
}
