using UnityEngine;

public class Gasolina : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(70 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GerenciadorPlayer.numeroDeGasolinas += 1;
            Destroy(gameObject);
        }
    }
}
