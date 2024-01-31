using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDeath : MonoBehaviour
{
    [SerializeField] private AudioClip EnemyDeathSound;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(EnemyDeathSound, Camera.main.transform.position);
        Destroy(this.gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
