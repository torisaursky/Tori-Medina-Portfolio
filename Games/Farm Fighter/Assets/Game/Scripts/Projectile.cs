using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 8.0f;
    [SerializeField] private int projectileID = 0;
    // 0 --> turnip
    // 1 --> triple shot pea
    [SerializeField] private AudioClip ProjectileSound;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(ProjectileSound, Camera.main.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (projectileID == 1)
        {
            speed = 10.0f;
        } else
        {
            speed = 8.0f;
        } //end if

        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y > 6.0f || transform.position.y < -6.0f)
        {
            if (transform.parent == null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(transform.parent.gameObject);
            } //end if

        } //end if
    }
}
