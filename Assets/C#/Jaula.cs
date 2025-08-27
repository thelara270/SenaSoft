using UnityEngine;

public class Jaula : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 ubicacionOriginal;
    [SerializeField] GameObject otroTrigger; 

    [SerializeField] bool esElEnemigo;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ubicacionOriginal = transform.position;
        otroTrigger.SetActive(true);
    }

    public void CaeJaula()
    {
        rb.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //ComparaElEnemigo();
        if(other.CompareTag("Enemigo") == true)
        {
            esElEnemigo = true;
            Debug.Log("verdadero");
            otroTrigger.SetActive(false);
        }
        else
        {
            transform.position = ubicacionOriginal;
            rb.isKinematic = true;
            esElEnemigo = false;
            Debug.Log("falso");
        }
    }

    public void ComparaElEnemigo()
    {
    }
}
