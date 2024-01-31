using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 2.4f;

    [SerializeField] private GameObject FlyDeath;

    private GameManager GM;
    private UIManager UI;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        UI = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = Random.Range(-7.5f, 7.5f);

        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -6.0f)
        {
            GM.Damage();
            Destroy(this.gameObject);
        } //end if

        if (GM.gameOver)
        {
            Destroy(this.gameObject);
        } //end if
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject); //destroys laser
            Instantiate(FlyDeath, transform.position, Quaternion.identity);
            UI.UpdateScore();
            Destroy(this.gameObject); //destroys enemy

        }
        else if (collision.CompareTag("Player"))
        {
            Hero H = collision.GetComponent<Hero>();
            if (H != null)
            {
                H.HeroDamage();
            } //end if

            Instantiate(FlyDeath, transform.position, Quaternion.identity);
            Destroy(this.gameObject); //destroys enemy
        }//end if
    } //end OnTrigger


}
