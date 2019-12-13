using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class CharacterStats : MonoBehaviour
{
    public bool isEnemy;

    //Base Stats
    public string Name;

    public float BaseHP;
    public float CurrentHP;

    Vector3 startPos;

    public Vector2 meleeAttackRange = new Vector2(1, 1);
    public Vector2 rangedAttackRange = new Vector2(1, 3);

    public float attack;
    public float healingAuraHP; //amount of healing
    public float knockbackAD; //amount of knockback damage

    private Animator animator;

    public GameObject healingAura;
    private Animator healingAura_Anim;
    private SpriteRenderer healingAura_SR;

    private float deathPlayTime = 0;

    public Image HPBarFill;

    //sound stuff
    public AudioSource charSounds;
    public AudioClip charAttack;
    public AudioClip charSpecial;
    public AudioClip charInjury;
    public AudioClip charDeath;

    public bool hasAnimPlaying = false;

    private void Start()
    {
        HPBarFill.fillAmount = ((CurrentHP) / 100);

        AudioSource charSounds = GetComponent<AudioSource>();

        #region Animations
        animator = gameObject.GetComponent<Animator>();

        healingAura_Anim = healingAura.GetComponent<Animator>();
        healingAura_SR = healingAura.GetComponent<SpriteRenderer>();
        healingAura.gameObject.SetActive(false);

        // Gets Animation Time(s)
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == name + "_Dies")
            {
                //Debug.Log(clip.length);
                deathPlayTime = clip.length;
            }
        }

        StartCoroutine(HealingCheck());
        #endregion
    }

    private void Awake()
    {
        ResetHealth();
        startPos = transform.position;
    }

    // Once not active, reset it for next play through
    private void OnDisable()
    {
        ResetHealth();
        transform.position = startPos;
    }

    private void Update()
    {
        //currentHealthText.text = ("Health: " + CurrentHP);
        HPBarFill.fillAmount = ((CurrentHP) / 100);

        if (CurrentHP <= 0f)
        {
            IsDead();
        }
    }

    public void ResetHealth()
    {
        CurrentHP = BaseHP;
    }

    IEnumerator AnimCheck()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                yield return new WaitForSeconds(1f);    // Adds a slight delay to ensure the anim is played out
                hasAnimPlaying = false;
            }
            else
            {
                hasAnimPlaying = true;
            }
        }
    }

    #region Walk
    public void IsWalking(bool set)
    {
        animator.SetBool("isWalking", set);
    }
    #endregion

    #region Attack
    public void IsAttacking()
    {
        animator.SetTrigger("isAttacking");

        StartCoroutine(AttackSound());
    }

    IEnumerator AttackSound()
    {
        //wanting to adjust sound delay for different character...
        if (Name.Contains("Alyss"))
        {
            yield return new WaitForSeconds(0.7f); //around 0.7f delay seems good for Alyss attack (also tried 0.5f and 0.6f)
        }

        if (Name.Contains("Dayana"))
        {
            yield return new WaitForSeconds(1f); //1f seems good for Dayana attack
        }

        //attack sound
        charSounds.clip = charAttack;
        charSounds.Play();
    }
    #endregion

    #region Hit
    public void WasHit()
    {
        animator.SetTrigger("wasHit");

        StartCoroutine(MakeOof());
    }

    IEnumerator MakeOof()
    {
        //wanting to adjust sound delay for different character...
        if (Name.Contains("Alyss"))
        {
            yield return new WaitForSeconds(1f); //?? any delay is no good?? //Alyss seems to react before actually getting hit (Dayana's anim is pretty slow)
            //1f looks fine, tbh, aside from Alyss taking damage before Dayana finishes her attack lol
        }

        if (Name.Contains("Dayana"))
        {
            yield return new WaitForSeconds(0.5f); //around 0.5f good for Dayana react
        }
        
        //injury sound
        charSounds.clip = charInjury;
        charSounds.Play();
    }
    #endregion

    #region Dead
    public void IsDead()
    {
        StartCoroutine(Died());
    }

    IEnumerator Died()
    {
        animator.SetBool("isDead", true);

        yield return new WaitForSeconds(deathPlayTime);
        //need more of a delay for sound? idk, I think so though

        //death sound
        charSounds.clip = charDeath;
        charSounds.Play();

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
    }
    #endregion

    #region Special
    public void useSpecial()
    {
        animator.SetTrigger("useSpecial");

        //special sound //may need to add wait for seconds also?
        charSounds.clip = charSpecial;
        charSounds.Play();
    }
    #endregion

    #region Healing Effect
    public void IsHealing()
    {
        //Alyss special, may need to move special healing sound here?

        animator.SetTrigger("isHealing");
        healingAura_Anim.SetTrigger("isHealing");
    }

    IEnumerator HealingCheck()
    {
        while (true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Healing"))
            {
                healingAura.gameObject.SetActive(true);
            }
            else healingAura.gameObject.SetActive(false);

            yield return new WaitForEndOfFrame();
        }
    }
    #endregion
}
