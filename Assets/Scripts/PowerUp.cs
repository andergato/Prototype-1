using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Power
    {
        SpeedBoost
    }

    [SerializeField] public Power power;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (power)
        {
            case Power.SpeedBoost: 
                Debug.Log("Speed boost");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
