using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    private float speed = 6.0f;
    private float fireRate = 0.25f;
    private float canFire = 0.05f;

    public bool canTripleShot = false;
    [SerializeField] private GameObject TurnipPrefab;
    [SerializeField] private GameObject TripleShotPrefab;

    public bool canFlame = false;
    [SerializeField] private GameObject FlameLeft;
    [SerializeField] private GameObject FlameRight;
    [SerializeField] private GameObject HeroDeath;

    public bool canShield = false;

    private GameManager GM;
    private SpawnManager SM;
    private UIManager UI;
    private Animator heroAnimator;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        SM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        UI = GameObject.Find("UIManager").GetComponent<UIManager>();
        heroAnimator = gameObject.GetComponent<Animator>();
    } //end Start

    // Update is called once per frame
    void Update()
    {
        Movement();
        Bounds();
        Shoot();
        SpeedFlames();
    } //end Update

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
    } //end Movement

    private void Bounds()
    {
        float lowerBound = -3.5f;
        float horizBound = 9.0f;
        
        if (transform.position.y < lowerBound)
        {
            transform.position = new Vector3(transform.position.x, lowerBound, 0);
        } //end if

        if (transform.position.x > horizBound)
        {
            transform.position = new Vector3(horizBound * -1, transform.position.y, 0);
        }
        else if (transform.position.x < horizBound * -1)
        {
            transform.position = new Vector3(horizBound, transform.position.y, 0);
        } //end if

    } //end Bounds

    private void Shoot()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !GM.isPaused)
        {
            if (Time.time > canFire)
            {
                if (!canTripleShot)
                {
                    Instantiate(TurnipPrefab, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
                    canFire = Time.time + fireRate;
                }
                else
                {
                    Instantiate(TripleShotPrefab, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
                    canFire = Time.time + fireRate;
                } //end if

            } //end if

        } //end if
    } //end shoot

    //Triple Shot Power Up/Down
    public IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    } //end TripleShotPowerDown
    public void TripleShotPowerUp()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDown());
    } //end TripleShotPowerUp

    //Speed Boost Power Up/Down
    public IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        speed = 5.0f;
        canFlame = false;
    } //
    public void SpeedBoostPowerUp()
    {
        speed = 12.0f;
        canFlame = true;
        StartCoroutine(SpeedBoostPowerDown());
    } //end SpeedBoostPowerUp

    public void SpeedFlames()
    {
        if (canFlame && (heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("HeroTurnLeft") || heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("PumpkinLeft")))
        {
            FlameLeft.SetActive(true);
        } else 
        {
            FlameLeft.SetActive(false);
        } //end if 

        if (canFlame && (heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("HeroTurnRight") || heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("PumpkinRight")))
        {
            FlameRight.SetActive(true);
        } else
        {
            FlameRight.SetActive(false);
        } //end if
    } //end SpeedFlames

    public void UpdateShield()
    {
        if (canShield)
        {
            heroAnimator.SetBool("HasPumpkin", true);
        } else
        {
            heroAnimator.SetBool("HasPumpkin", false);
        }
    } //end UpdateShield

    public void HeroDamage()
    {
        if (GM.lives > 0 && !canShield)
        {
            GM.lives--;
            UI.UpdateHeroHealth(GM.lives);

            heroAnimator.SetTrigger("HeroHit");
            StartCoroutine(DamageAnim());

            if (GM.lives == 0)
            {
                Instantiate(HeroDeath, transform.position, Quaternion.identity);
                UI.ShowTitle();
                GM.gameOver = true;
                Destroy(this.gameObject);
            }
        }

        if (canShield)
        {
            canShield = false;
            UpdateShield();
        }
    } //end HeroDamage

    public IEnumerator DamageAnim()
    {
        yield return new WaitForSeconds(0.5f);
        if (heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("HeroDamageLeft"))
        {
            heroAnimator.Play("HeroIdleLeft");
        } else if (heroAnimator.GetCurrentAnimatorStateInfo(0).IsName("HeroDamageRight"))
        {          
            heroAnimator.Play("HeroIdleRight");
        }
        heroAnimator.ResetTrigger("HeroHit");
    }


} //end class
