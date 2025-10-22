using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    
    public Button buttonA;
    public Button buttonB;
    public Button buttonC;
    public Button buttonD;
    public Button buttonE;

    
    public GameObject panel;

    void Start()
    {
        
        buttonA.onClick.AddListener(() => CheckAnswer("A"));
        buttonB.onClick.AddListener(() => CheckAnswer("B"));
        buttonC.onClick.AddListener(() => CheckAnswer("C"));
        buttonD.onClick.AddListener(() => CheckAnswer("D"));
        buttonE.onClick.AddListener(() => CheckAnswer("E"));
    }

    void CheckAnswer(string choice)
    {
        
        string correctAnswer = "E";

        if (choice == correctAnswer)
        {
            Debug.Log("Resposta correta!");

            HighlightButton(choice, new Color(0f, 1f, 0f, 0.2f));
        }
        else
        {
            Debug.Log("Resposta errada!");

            HighlightButton(choice, new Color(1f, 0f, 0f, 0.2f));
            
        }
    }

    void HighlightButton(string buttonName, Color color)
    {
        Button btn = null;

        switch (buttonName)
        {
            case "A": btn = buttonA; break;
            case "B": btn = buttonB; break;
            case "C": btn = buttonC; break;
            case "D": btn = buttonD; break;
            case "E": btn = buttonE; break;
        }

        if (btn != null)
            btn.GetComponent<Image>().color = color;
    }
}
