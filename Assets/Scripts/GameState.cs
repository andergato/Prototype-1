using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    [SerializeField] private Camera menuCam;
    [SerializeField] private Camera gameCam;

    // [SerializeField] private Button startButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameCam.enabled = false;
        menuCam.enabled = true;
    }

    public void OnButtonPressed()
    {
        gameCam.enabled = true;
        menuCam.enabled = false;
    }
}
