using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlePlayer : MonoBehaviour
{
    private CharacterController controlador;
    private Vector3 direcao;
    
    public float velocidadeFrente;
    public float velocidadeMaxima;
    public const float ModificadorVelocidade = 0.2f;
    public float velocidadeExibida = 0; 

    private int faixaDesejada = 1;
    public float distanciaEntreFaixas = 4;

    public float forcaPulo;
    public float gravidade;

    void Start()
    {
        controlador = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!GerenciadorPlayer.jogoIniciado)
            return;

        AumentarVelocidade();

        direcao.z = velocidadeFrente;
        
        ExecutarVirada();

        Vector3 posicaoAlvo = transform.position.z * transform.forward
                               + transform.position.y * transform.up;

        if (faixaDesejada == 0)
            posicaoAlvo += Vector3.left * distanciaEntreFaixas;
        else if (faixaDesejada == 2)
            posicaoAlvo += Vector3.right * distanciaEntreFaixas;

        if (transform.position == posicaoAlvo)
            return;

        Vector3 diff = posicaoAlvo - transform.position;
        Vector3 dirMovimento = diff.normalized * 25 * Time.deltaTime;

        if (dirMovimento.sqrMagnitude < diff.sqrMagnitude)
            controlador.Move(dirMovimento);
        else
            controlador.Move(diff);
    }

    private void FixedUpdate()
    {
        if (!GerenciadorPlayer.jogoIniciado)
            return;

        controlador.Move(direcao * Time.fixedDeltaTime);
    }

    private void ExecutarVirada()
    {
        VirarDireita();
        VirarEsquerda();
    }

    private void VirarEsquerda()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Movement.deslizeEsquerda)
        {
            faixaDesejada--;
            if (faixaDesejada == -1)
                faixaDesejada = 0;
        }
    }

    private void VirarDireita()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Movement.deslizeDireita)
        {
            faixaDesejada++;
            if (faixaDesejada == 3)
                faixaDesejada = 2;
        }
    }

    private void AumentarVelocidade()
    {
        if (velocidadeFrente < velocidadeMaxima)
        {
            velocidadeFrente += ModificadorVelocidade * Time.deltaTime;
            velocidadeExibida = velocidadeFrente * 10;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstaculo")
        {
            GerenciadorPlayer.jogoEncerrado = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        Vector3 novaPosicao = other.transform.localPosition;
        novaPosicao.z = Mathf.Lerp(other.transform.localPosition.z, transform.localPosition.z, Time.deltaTime * 1);

        other.transform.localPosition = novaPosicao;
    }
}
