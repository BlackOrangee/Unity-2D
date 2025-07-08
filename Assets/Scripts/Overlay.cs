using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Overlay : MonoBehaviour
{
    // Hearts
    public Sprite heartFull;
    public Sprite heartBlank;
    public GameObject heartObj;
    public Transform heartsPanel;
    public float spacing = 40f;
    private List<Image> hearts = new List<Image>();

    // Items
    public GameObject itemObj;

    public void Initialize(int maxHealth)
    {
        GenerateHearts(maxHealth);
        SetHealth(maxHealth);
    }

    public void GenerateHearts(int count)
    {
        foreach (Transform child in heartsPanel)
        {
            Destroy(child.gameObject);
        }
        hearts.Clear();

        RectTransform firstRect = null;

        for (int i = 0; i < count; i++)
        {
            GameObject heart = Instantiate(heartObj, heartsPanel);

            Image heartImage = heart.GetComponent<Image>();

            RectTransform rectTransform = heart.GetComponent<RectTransform>();

            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 1);

            if (i == 0)
            {
                rectTransform.anchoredPosition = new Vector2(10f, -10f);
                firstRect = rectTransform;
            }
            else
            {
                float xOffset = i * spacing;
                rectTransform.anchoredPosition = firstRect.anchoredPosition + new Vector2(xOffset, 0f);
            }

            heartImage.enabled = true;

            hearts.Add(heartImage);
        }
    }

    public void SetHealth(int health)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = i < health ? heartFull : heartBlank;
        }
    }

    public void SetItems(int items)
    {
        Text text = itemObj.GetComponentInChildren<Text>();

        if (text != null)
        {
            text.text = items.ToString();
        }
    }
}

