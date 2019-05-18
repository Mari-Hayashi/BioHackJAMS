using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class QuestionController : MonoBehaviour
{
    [SerializeField]
    private ButtonClass[] buttons;
    [SerializeField]
    private TextMeshPro questionText;
    [SerializeField]
    private InputField textbox;
    private TextAsset csvFile;
    private const string parentFileName = "QuestionsForParent";
    private const string kidFileName = "QuestionsForKids";

    private List<Question> parentQuestions;
    private int curQuestionIndex;
    private Question currentQuestion;

    private void Start()
    {
        parentQuestions = new List<Question>();
        readParentCSV();
        curQuestionIndex = 0;
        loadQuestion();
    }

    private void readParentCSV()
    {
        csvFile = new TextAsset();
        csvFile = Resources.Load<TextAsset>(parentFileName);
        StringReader reader = new StringReader(csvFile.text);
        string line = reader.ReadLine(); // extraline
        line = reader.ReadLine();

        while (line != null)
        {
            string[] valuesInString;
            valuesInString = line.Split(',');

            Question question = new Question(int.Parse(valuesInString[1]));

            question.questionStr = valuesInString[0];
            for (int i = 0; i < int.Parse(valuesInString[1]); ++i)
            {
                question.choice[i] = valuesInString[2 + i];
            }

            parentQuestions.Add(question);

            line = reader.ReadLine();
        }

        reader.Close();
    }
    private void getAnswer(int choice)
    {
        if (currentQuestion.numChoices == 0)
        {
            if (textbox.text == "") return;
            parentQuestions[curQuestionIndex].answer = textbox.text;
        }
        else
        {
            parentQuestions[curQuestionIndex].answer = parentQuestions[curQuestionIndex].choice[choice - 1];
        }
        curQuestionIndex++;
        loadQuestion();
    }
    private void loadQuestion()
    {
        currentQuestion = parentQuestions[curQuestionIndex];
        questionText.text = currentQuestion.questionStr;
        if (currentQuestion.numChoices == 0)
        {
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            buttons[2].changeText("OK");
            textbox.gameObject.SetActive(true);
        }
        else
        {
            int i;
            for (i = 0; i < currentQuestion.numChoices; ++i)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].changeText(currentQuestion.choice[i]);
            }
            while (i < 3)
            {
                buttons[i].gameObject.SetActive(false);
                i++;
            }
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clicklocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collision2d = Physics2D.OverlapPoint(clicklocation);
            if (collision2d)
            {
                string layername = LayerMask.LayerToName(collision2d.gameObject.layer);
                if (layername == "Choice1") getAnswer(1);
                else if (layername == "Choice2") getAnswer(2);
                else if (layername == "Choice3") getAnswer(3);
            }
        }
    }
}
