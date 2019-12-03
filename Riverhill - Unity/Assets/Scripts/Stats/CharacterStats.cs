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
    [HideInInspector] // Name will be given when created based on gameobject
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

    public Text currentHealthText;
    public Image HPBarFill;

    private void Start()
    {
        // No longer self reports
        //BattleManager.Instance.characterStats.Add(this);

        ResetHealth();

        ///* EMILY ADDED SETTING THIS STUFF IN BATTLEMANAGER SCRIPT
        //currentHealthText.text = ("Health: " + CurrentHP);
        //HPBarFill.fillAmount = ((CurrentHP) / 100);
        //*/

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
        currentHealthText.text = ("Health: " + CurrentHP);
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
    }

    public void WasHit()
    {
        animator.SetTrigger("wasHit");
    }

    public void IsDead()
    {
        StartCoroutine(Died());
    }

    IEnumerator Died()
    {
        animator.SetBool("isDead", true);

        yield return new WaitForSeconds(deathPlayTime);

        gameObject.SetActive(false);
    }

    public void useSpecial()
    {
        animator.SetTrigger("useSpecial");
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
