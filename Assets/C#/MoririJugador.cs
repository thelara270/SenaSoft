
using UnityEngine;

public class MoririJugador : MonoBehaviour
{
    [SerializeField] GameObject jugador;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugador.SetActive(false);
        }
    }
}
