using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombCountdownCanvas : MonoBehaviour
{

    private Camera cam;
    private Canvas canvas;
    private string textToDisplay;
    [SerializeField] [Range(0, 300)] private int maxFontSize = 90;
    [SerializeField] [Range(0f, 1f)] private float delay = 0.5f;

    //[SerializeField] private Color textColor;

    private Dictionary<Bomb, TextMeshProUGUI> bombText = new Dictionary<Bomb, TextMeshProUGUI>();

    [SerializeField] private TextMeshProUGUI bombNumberPrefab = null;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        cam = Camera.main;
        List<Bomb> tempList = new List<Bomb>(FindObjectsOfType<Bomb>());
        for (int i = 0; i < tempList.Count; i++)
        {
            bombText.Add(tempList[i], CreateCanvasText());
        }
    }

    public void SetBombPositionAndTime(Vector3 bombPosition, float time, Bomb bomb, bool hasExploded, bool hasBeenPickedUp)
    {

        Vector2 screenPosition = cam.WorldToScreenPoint(bombPosition);

        float scaleFactor = canvas.scaleFactor;

        Vector2 scaledPosition = new Vector2(
                    screenPosition.x / scaleFactor,
                    screenPosition.y / scaleFactor);
        if (hasExploded || hasBeenPickedUp)
        {
            textToDisplay = "";
        }
        else
        {
           
            float decimals = time - (int)time;
            int fontSizeBouncy = 0;

            if (decimals > delay)
            {
                float inverseDecimals = Mathf.InverseLerp(1f, delay, decimals);
                inverseDecimals = Ease.EaseOutBack(inverseDecimals);
                if (time < 4)
                {
                    //bombText[bomb].color = textColor;
                    fontSizeBouncy = (int)Mathf.LerpUnclamped(0, maxFontSize + 80, inverseDecimals);
                }
                else 
                { 
                    fontSizeBouncy = (int)Mathf.LerpUnclamped(0, maxFontSize, inverseDecimals); 
                }

            }

                bombText[bomb].fontSize = fontSizeBouncy;


            if (time < 1)
            {
                textToDisplay = "!";
            }
            else 
            { 
                textToDisplay = ((int)time).ToString(); 
            }

        }
        bombText[bomb].text = textToDisplay;
        bombText[bomb].rectTransform.anchoredPosition = scaledPosition;
    }

    private TextMeshProUGUI CreateCanvasText()
    {
        TextMeshProUGUI newText = Instantiate(bombNumberPrefab, this.gameObject.transform);
        return newText;
    }
}
