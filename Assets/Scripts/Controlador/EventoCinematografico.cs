using System.Collections;
using UnityEngine;

public class EventoCinematografico : MonoBehaviour
{
    [Header("Iluminação")]
    public Light spotLuz;
    public float intensidadeAmbienteNormal = 1f;

    [Header("Gaiola")]
    public Animator animatorGaiola;
    public string triggerFechar = "fechar";

    [Header("Player")]
    public Transform jogador;
    public float distanciaAcao = 2f;

    [Header("Objetos a alternar")]
    public GameObject[] grupoX;
    public GameObject[] grupoY;

    [Header("UI")]
    public GameObject panelPerguntas; 

    public float tempoInicial = 1f;
    public float tempoAlternancia = 1f;
    public float duracaoTotal = 5f;

    private bool jaAtivou = false;

    private Light led1;
    private Light led2;
    private Light led3;

    void Start()
    {
        led1 = GameObject.Find("led1").GetComponent<Light>();
        led2 = GameObject.Find("led2").GetComponent<Light>();
        led3 = GameObject.Find("led3").GetComponent<Light>();

        SetAtivos(grupoX, false);
        SetAtivos(grupoY, false);

        if (panelPerguntas != null)
            panelPerguntas.SetActive(false);
    }

    void Update()
    {
        if (jaAtivou || jogador == null) return;

        float distancia = Vector3.Distance(jogador.position, transform.position);
        if (distancia <= distanciaAcao)
        {
            jaAtivou = true;
            StartCoroutine(AtivarEventoComAlternancia());
            
        }
    }

    IEnumerator AtivarEventoComAlternancia()
    {
        led1.color = Color.green; led2.color = Color.green; led3.color = Color.green;
        if (animatorGaiola != null)
            animatorGaiola.SetTrigger(triggerFechar);

        RenderSettings.ambientIntensity = intensidadeAmbienteNormal;
        if (spotLuz != null)
            spotLuz.enabled = false;

        yield return new WaitForSeconds(tempoInicial);

        float tempoPassado = 0f;
        bool estado = true;

        while (tempoPassado < duracaoTotal)
        {
            SetAtivos(grupoX, estado);
            SetAtivos(grupoY, !estado);

            estado = !estado;
            yield return new WaitForSeconds(tempoAlternancia);
            tempoPassado += tempoAlternancia;
        }

        
        SetAtivos(grupoX, false);
        SetAtivos(grupoY, false);

        
        yield return new WaitForSeconds(1f);

        if (panelPerguntas != null)
        {
            panelPerguntas.SetActive(true);

            
            Time.timeScale = 0f;

            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void SetAtivos(GameObject[] objetos, bool ativo)
    {
        foreach (var obj in objetos)
        {
            if (obj != null)
                obj.SetActive(ativo);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaAcao);
    }
}
