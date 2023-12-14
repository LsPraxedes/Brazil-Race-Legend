using UnityEngine;

public class Movement : MonoBehaviour
{
    public static bool toque, deslizeEsquerda, deslizeDireita, deslizeCima, deslizeBaixo;
    private bool estaArrastando = false;
    private Vector2 toqueInicial, deltaDeslize;

    private void Update()
    {
        toque = deslizeEsquerda = deslizeDireita = deslizeCima = deslizeBaixo = false;

        #region Entradas Standalone
        if (Input.GetMouseButtonDown(0))
        {
            toque = true;
            estaArrastando = true;
            toqueInicial = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            estaArrastando = false;
            Resetar();
        }
        #endregion

        #region Entradas Mobile
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                toque = true;
                estaArrastando = true;
                toqueInicial = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                estaArrastando = false;
                Resetar();
            }
        }
        #endregion

        // Calcular distância
        deltaDeslize = Vector2.zero;
        if (estaArrastando)
        {
            if (Input.touches.Length > 0)
                deltaDeslize = Input.touches[0].position - toqueInicial;
            else if (Input.GetMouseButton(0))
                deltaDeslize = (Vector2)Input.mousePosition - toqueInicial;
        }

        // Cruzamos a distância?
        if (deltaDeslize.magnitude > 125)
        {
            // Direção
            float x = deltaDeslize.x;
            float y = deltaDeslize.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                    deslizeEsquerda = true;
                else
                    deslizeDireita = true;
            }
            else
            {
                if (y < 0)
                    deslizeBaixo = true;
                else
                    deslizeCima = true;
            }

            Resetar();
        }
    }

    private void Resetar()
    {
        toqueInicial = deltaDeslize = Vector2.zero;
        estaArrastando = false;
    }
}
