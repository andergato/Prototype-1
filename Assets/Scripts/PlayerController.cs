using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public int score = 0;

    public CountdownTimer playerTimer = new CountdownTimer();
    public CountdownTimer deathTimer = new CountdownTimer();

    [SerializeField] private TextMeshProUGUI uiTextElement;

    [SerializeField] Image targetImage;
    public TeleportObject teleportObject;

    public Rigidbody rb;

    //Add and Remove Events 
    private void OnEnable()
    {
        InventoryController.OnItemDrop += UpdateScore;
        InventoryController.OnItemPickup += DisplayItem;

        // Subscribe to CountdownTimer's TimerDone event and fire FireTimer when fired.
        //CountdownTimer.TimerDone += FireTimer;
        //playerTimer.StartTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            teleportObject.Teleport(rb);
            ResetCharacter();
        }
    }

    private void OnDisable()
    {
        InventoryController.OnItemDrop -= UpdateScore;
    }

    void ResetCharacter()
    {
        score = -1;
        UpdateScore();
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

    void DisplayItem(Sprite icon)
    {
        targetImage.sprite = icon;
    }

    void FireTimer()
    {
        teleportObject.Teleport(rb);
    }
}
