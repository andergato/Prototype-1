using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int score = 0;

    [SerializeField] private TextMeshProUGUI uiTextElement;

    //Add and Remove Events 
    private void OnEnable()
    {
        InventoryController.OnItemDrop += UpdateScore;
    }
    private void OnDisable()
    {
        InventoryController.OnItemDrop -= UpdateScore;
    }

    // Initialize text on screen.
    void Start()
    {
        uiTextElement.text = score.ToString();
    }

    // Increment score by one.
    public void UpdateScore()
    {
        score += 1;
        uiTextElement.text = score.ToString();
    }
}
