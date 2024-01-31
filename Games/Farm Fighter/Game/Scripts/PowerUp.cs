using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private float speed = 2.0f;
    [SerializeField] private int powerUpID = 0;
    // 0 --> triple
    // 1 --> speed
    // 2 --> sheild
    [SerializeField] private AudioClip PowerUpSound;

    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        } //end if

        if (GM.gameOver)
        {
            Destroy(this.gameObject);
        } //end if

    } //end Update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Hero H = collision.GetComponent<Hero>();
            if (H != null)
            {
                AudioSource.PlayClipAtPoint(PowerUpSound, GameObject.Find("Main Camera").transform.position, 1f);

                if (powerUpID == 0) {
                    H.TripleShotPowerUp();
                } else if (powerUpID == 1)
                {
                    H.SpeedBoostPowerUp();
                } else if (powerUpID == 2)
                {
                    H.canShield = true;
                    H.UpdateShield();
                } //end if

                Destroy(this.gameObject);
            } //end if

        } //end if

    } //end trigger
}
