using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    public Image imagemCutscene;
    public Sprite[] imagens;
    public float tempoPorImagem = 2f;
    public string proximaCena = "JogoPrincipal";

    private void Start()
    {
        if (imagemCutscene == null || imagens.Length == 0)
        {
            Debug.LogError("CutsceneController não configurado corretamente!");
            return;
        }

        StartCoroutine(RodarCutscene());
    }

    IEnumerator RodarCutscene()
    {
        foreach (Sprite img in imagens)
        {
            imagemCutscene.sprite = img;
            yield return new WaitForSeconds(tempoPorImagem);
        }

        SceneManager.LoadScene(proximaCena);
    }
}
