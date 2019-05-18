using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class QuestionController : MonoBehaviour
{
    [SerializeField]
    private TextAsset csvFile;
    private const string parentFileName = "QuestionsForParent";
    private const string kidFileName = "QuestionsForKids";

    private List<Question> parentQuestions;
    private int curQuestion;

    private void Start()
    {
        parentQuestions = new List<Question>();
        readParentCSV();
        curQuestion = 0;
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 clicklocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collision2d = Physics2D.OverlapPoint(clicklocation);
            if (collision2d)
            {
                if (LayerMask.LayerToName(collision2d.gameObject.layer) == "Choice1")
                {
                    Debug.Log("YES!!");
                }
            }
        }
    }
}
