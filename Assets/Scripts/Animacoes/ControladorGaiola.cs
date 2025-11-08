using UnityEngine;

public class ControladorGaiola : MonoBehaviour
{
    public Animator animator;
    public Transform personagem;
    public float distanciaFechamento = 3f;
    private bool jaFechou = false;

    void Update()
    {
         

        float distancia = Vector3.Distance(personagem.position, transform.position);

        if (distancia <= distanciaFechamento)
        {
            animator.SetTrigger("fechar");
            jaFechou = true; 
        }else
        {
            animator.SetTrigger("fechar");
            jaFechou = false;
        }
    }
}
