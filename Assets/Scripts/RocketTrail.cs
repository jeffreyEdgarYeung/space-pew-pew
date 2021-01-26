using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTrail : MonoBehaviour
{
    [SerializeField] Player ship;
    [SerializeField] Vector2 tToSVector;

    // Start is called before the first frame update
    void Start()
    {
        tToSVector = transform.position - ship.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        LockTrailToShip();
    }

    private void LockTrailToShip()
    {
        Vector2 shipPosition = ship.transform.position;
        transform.position = shipPosition + tToSVector;
    }
}
