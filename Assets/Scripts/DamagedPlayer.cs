using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedPlayer : MonoBehaviour
{
    [SerializeField] AudioClip damagedSFX;
    [SerializeField] float sfxVolume;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(damagedSFX, Camera.main.transform.position, sfxVolume);
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
