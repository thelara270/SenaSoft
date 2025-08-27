using UnityEngine;

public class Interactuar : MonoBehaviour
{
    public bool puedeInteractuar;
    public bool interactuo;

    [SerializeField] GameObject jaula;

    Interactuar interactuar;
    Jaula jaulaRb;


    private void Start()
    {
        if (jaula != null)
        {
            jaulaRb = jaula.GetComponent<Jaula>();
        }
        puedeInteractuar = false;
        interactuo = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && puedeInteractuar == true)
        {
            interactuo = true;
            jaulaRb.CaeJaula();
            Debug.Log("Holisiji");
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        puedeInteractuar = true;
        Debug.Log("holis");



    }

    private void OnTriggerExit(Collider other)
    {
        puedeInteractuar = false;
    }
}
