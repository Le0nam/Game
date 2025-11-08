using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // novo sistema de input
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    public Image imagemCutscene;
    public Sprite[] imagens;
    public float tempoPorImagem = 2f;
    public string proximaCena = "JogoPrincipal";
    public Button botaoPular;
    public Key teclaPular = Key.Space; // tecla do novo input system

    private int indiceAtual = 0;
    private bool pular = false;

    private void Start()
    {
        if (imagemCutscene == null || imagens.Length == 0)
        {
            Debug.LogError("CutsceneController não configurado corretamente!");
            return;
        }

        if (botaoPular != null)
        {
            botaoPular.gameObject.SetActive(true);
            botaoPular.onClick.AddListener(PularImagem);

            Text textoBotao = botaoPular.GetComponentInChildren<Text>();
            if (textoBotao != null)
            {
                textoBotao.text = "Pular (Espaço)";
            }
        }

        StartCoroutine(RodarCutscene());
    }

    private void Update()
    {
        // novo sistema de input
        if (Keyboard.current != null && Keyboard.current[teclaPular].wasPressedThisFrame)
        {
            PularImagem();
        }
    }

    private void PularImagem()
    {
        pular = true;
    }

    IEnumerator RodarCutscene()
    {
        for (indiceAtual = 0; indiceAtual < imagens.Length; indiceAtual++)
        {
            imagemCutscene.sprite = imagens[indiceAtual];
            pular = false;

            float tempoPassado = 0f;
            while (tempoPassado < tempoPorImagem && !pular)
            {
                tempoPassado += Time.deltaTime;
                yield return null;
            }
        }

        SceneManager.LoadScene(proximaCena);
    }
}
