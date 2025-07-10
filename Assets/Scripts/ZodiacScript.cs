using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ZodiacScript : MonoBehaviour
{
    public TextAsset file;

    public Text textArea;

    public GameObject buttonPanel;

    private List<Button> buttons = new List<Button>();

    private List<string> lines = new List<string>();

    void Start()
    {
        lines.AddRange(file.text.Split(';'));

        int index = 0;
        foreach (Transform child in buttonPanel.transform)
        {
            Button btn = child.GetComponent<Button>();
            if (btn != null)
            {
                int capturedIndex = index;
                btn.onClick.AddListener(() => Show(capturedIndex));
                buttons.Add(btn);
                index++;
            }
        }
    }


    private void Show(int index)
    {
        textArea.text = lines[index];
        Debug.Log(lines[index]);
        
    }
    

    
}
