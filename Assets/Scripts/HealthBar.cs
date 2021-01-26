using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] float originalXPos;
    [SerializeField] float barLengthInUnits;
    [SerializeField] float maxPlayerHealth;

    Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float health = player.GetHealth();
        var deltaX = ((1 - (health / maxPlayerHealth)) * barLengthInUnits);
        transform.position = new Vector3(originalXPos-deltaX, transform.position.y, -1);
    }
}
