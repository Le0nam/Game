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

    public float tempoInicial = 1f;   // tempo antes de ligar os grupos
    public float tempoAlternancia = 1f; // tempo de alternância entre grupos
    public float duracaoTotal = 5f;   // tempo total antes de desligar tudo

    private bool jaAtivou = false;

    void Start()
    {
        // Inicialmente, todos os objetos desligados
        SetAtivos(grupoX, false);
        SetAtivos(grupoY, false);
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
        // 1️⃣ Anima a gaiola
        if (animatorGaiola != null)
            animatorGaiola.SetTrigger(triggerFechar);

        // 2️⃣ Ajusta iluminação
        RenderSettings.ambientIntensity = intensidadeAmbienteNormal;
        if (spotLuz != null)
            spotLuz.enabled = false;

        // 3️⃣ Espera tempo inicial antes de ligar
        yield return new WaitForSeconds(tempoInicial);

        float tempoPassado = 0f;
        bool estado = true;

        while (tempoPassado < duracaoTotal)
        {
            SetAtivos(grupoX, estado);
            SetAtivos(grupoY, !estado);

            estado = !estado; // alterna para o próximo ciclo
            yield return new WaitForSeconds(tempoAlternancia);
            tempoPassado += tempoAlternancia;
        }

        // 4️⃣ Desliga tudo no final
        SetAtivos(grupoX, false);
        SetAtivos(grupoY, false);
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
