using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform alvo;
    private Vector3 deslocamento;

    void Start()
    {
        deslocamento = transform.position - alvo.position;
    }

    void LateUpdate()
    {
        Vector3 novaPosicao = new Vector3(transform.position.x, transform.position.y, deslocamento.z + alvo.position.z);
        transform.position = Vector3.Lerp(transform.position, novaPosicao, 10 * Time.deltaTime);
    }
}
