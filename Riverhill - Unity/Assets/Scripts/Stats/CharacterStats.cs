using System.Collections;
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

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        healingAura_Anim = healingAura.GetComponent<Animator>();
        healingAura_SR = healingAura.GetComponent<SpriteRenderer>();
        healingAura.gameObject.SetActive(false);

        StartCoroutine(AnimCheck());
    }

    public void isHealing()
    {
        animator.SetTrigger("isHealing");
        healingAura_Anim.SetTrigger("isHealing");
    }

    public void isWalking()
    {
        animator.SetBool("isWalking", true);
    }
    
    public void isAttacking()
    {
        animator.SetTrigger("isAttacking");
    }

    public void wasHit()
    {
        animator.SetTrigger("wasHit");
    }

    public void isDead()
    {
        animator.SetBool("isDead", true);
    }

    public void useSpecial()
    {
        animator.SetTrigger("useSpecial");
    }

    IEnumerator AnimCheck()
    {
        Debug.Log("AnimCheck");
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
