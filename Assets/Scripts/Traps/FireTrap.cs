using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [Header("Firetrap Timers")]
    [SerializeField] private float damage;

    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer spriteRend;

    [Header("SFX")]
    [SerializeField] private AudioClip firetrapSound;

    private bool triggered;
    private bool active;

    private Health playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if( playerHealth != null && active )        
            playerHealth.TakeDamage(damage);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
                playerHealth= collision.GetComponent<Health>();
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if(active)
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = null;
        }
    }
    private IEnumerator ActivateFireTrap()
    {
        triggered = true;
        spriteRend.color = Color.red;

        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firetrapSound);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated",true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered= false;
        anim.SetBool("activated", false);    
    }
}
