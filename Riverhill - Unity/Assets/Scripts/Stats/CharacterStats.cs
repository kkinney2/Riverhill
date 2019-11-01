﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats : MonoBehaviour
{
    public bool isEnemy;

    //Base Stats
    [HideInInspector] // Name will be given when created based on gameobject
    public string Name;

    public float BaseHP;
    public float CurrentHP;

    public float attack;

    private Animator animator;

    public GameObject healingAura;
    private Animator healingAura_Anim;
    private SpriteRenderer healingAura_SR;

    private float deathPlayTime = 0;

    private void Start()
    {
        CurrentHP = BaseHP;

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
                Debug.Log(clip.length);
                deathPlayTime = clip.length;
            }
        }

        StartCoroutine(AnimCheck());
        #endregion
    }

    private void Update()
    {
        if (CurrentHP <= 0f)
        {
            IsDead();
        }
    }

    public void IsHealing()
    {
        animator.SetTrigger("isHealing");
        healingAura_Anim.SetTrigger("isHealing");
    }

    public void IsWalking()
    {
        animator.SetBool("isWalking", true);
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
