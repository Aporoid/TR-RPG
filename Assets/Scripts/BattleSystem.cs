using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public CombatHUD playerHUD;
    public CombatHUD enemyHUD;

    public GameObject dialoguePopup;
    public Text dialogueText;
    public Text enemyDamageText;

    private AudioSource audio;
    public AudioClip hurtSound;
    public AudioClip killSound;

    Unit playerunit;
    Unit enemyUnit;

    public GameObject enemyPanel;
    public Image enemyImage;
    public GameObject playerHPGuage;

    //AnimationController animCon = new AnimationController();

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        audio = GetComponent<AudioSource>();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerunit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "Eyes up! Ambush!";

        playerHUD.SetHUD(playerunit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        Playerturn();
    }

    void Playerturn()
    {
        dialoguePopup.SetActive(false);
    }


    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerunit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialoguePopup.SetActive(true);
        dialogueText.text = "You attack!";

        audio.PlayOneShot(hurtSound, 1);
        enemyImage.enabled = false;
        yield return new WaitForSeconds(0.1f);
        enemyImage.enabled = true;
        yield return new WaitForSeconds(0.1f);
        enemyImage.enabled = false;
        yield return new WaitForSeconds(0.1f);
        enemyImage.enabled = true;
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(2f);

        dialogueText.text = "You dealt " + playerunit.damage + " damage!";
        yield return new WaitForSeconds(2f);
        dialoguePopup.SetActive(false);

        if (isDead)
        {
            state = BattleState.WON;
            enemyPanel.SetActive(false);
            audio.PlayOneShot(killSound, 1);
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        bool isDead = playerunit.TakeDamage(enemyUnit.damage);

        dialoguePopup.SetActive(true);
        dialogueText.text = "The enemy attacks!";

        audio.PlayOneShot(hurtSound, 1);
        playerHUD.SetHP(playerunit.currentHP);

        #region image flash enemy
        playerHPGuage.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        playerHPGuage.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerHPGuage.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        playerHPGuage.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        #endregion

        yield return new WaitForSeconds(2f);

        dialogueText.text = "You took " + enemyUnit.damage.ToString() + " damage!";

        yield return new WaitForSeconds(2f);
        dialoguePopup.SetActive(false);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            Playerturn();
        }

    }

    void EndBattle()
    {
        dialoguePopup.SetActive(true);
        if (state == BattleState.WON)
        {
            StartCoroutine(WinText());
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated.";
        }
    }

    IEnumerator WinText()
    {
        dialogueText.text = "You won the battle!";
        yield return new WaitForSeconds(2f);
        dialogueText.text = "You gained " + enemyUnit.experienceGiven.ToString() + " experiece!";
        yield return new WaitForSeconds(2f);
        dialoguePopup.SetActive(false);
    }

    public void OnMeleeSelect()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }
}
