using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("IFrames")]
    [SerializeField]private float inFrameDuration;
    [SerializeField]private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Component")]
    [SerializeField] Behaviour[] components;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim=GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if(invulnerable) return;
        currentHealth= Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if(!dead)
            {
                foreach(Behaviour component in components)    
                    component.enabled = false;

                anim.SetBool("grounded", true);
                anim.SetTrigger("die");

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth=Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    public void Respown()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("idle");
        StartCoroutine(Invulnerability());

        foreach(Behaviour component in components)
            component.enabled = true;
    }
    private IEnumerator Invulnerability()
    {
        invulnerable=true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(inFrameDuration/(numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(inFrameDuration/(numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }
    private void Deactive()
    {
        gameObject.SetActive(false);
    }
}
