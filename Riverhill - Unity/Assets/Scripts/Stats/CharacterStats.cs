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

    public Vector2 meleeAttackRange = new Vector2(1, 1);
    public Vector2 rangedAttackRange = new Vector2(1, 3);

    /* Emily, you big dumb dumb!! It's easier than you think.
    public float CurrentP1HP;
    public float CurrentE1HP;
    */

    public float attack;

    private Animator animator;

    public GameObject healingAura;
    private Animator healingAura_Anim;
    private SpriteRenderer healingAura_SR;

    private float deathPlayTime = 0;

    //for health bars
    /* Emily is big dumb dumb :-)
    public Text currentP1Health;
    public Text currentE1Health;
    public Image p1HPBarFill;
    public Image e1HPBarFill;
    */

    //public Image P1HPBar;
    //public Image E1HPBar;
    //public Image p1HPBarFill;
    //public Image e1HPBarFill;
    //public Text currentP1Health;
    //public Text currentE1Health;

    //public Text currentHealthText;
    public Image HPBarFill;

    //sound stuff
    public AudioSource charSounds;
    public AudioClip charAttack;
    public AudioClip charSpecial;
    public AudioClip charInjury;
    public AudioClip charDeath;

    private void Start()
    {
        // No longer self reports
        //BattleManager.Instance.characterStats.Add(this);

        ResetHealth();

        ///* EMILY ADDED SETTING THIS STUFF IN BATTLEMANAGER SCRIPT
        //currentHealthText.text = ("Health: " + CurrentHP);
        //HPBarFill.fillAmount = ((CurrentHP) / 100);
        //*/

        //currentHealthText.text = ("Health: " + CurrentHP);
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

        StartCoroutine(AnimCheck());
        #endregion
    }

    private void Awake()
    {

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

    public void IsHealing()
    {
        //Alyss special, may need to move special healing sound here?

        animator.SetTrigger("isHealing");
        healingAura_Anim.SetTrigger("isHealing");
    }

    public void IsWalking(bool set)
    {
        animator.SetBool("isWalking", set);
    }

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

    public void useSpecial()
    {
        animator.SetTrigger("useSpecial");

        //special sound //may need to add wait for seconds also?
        charSounds.clip = charSpecial;
        charSounds.Play();
    }

    IEnumerator AnimCheck()
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
}
