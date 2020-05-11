using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ALLYTURN, ENEMYTURN, WON, LOST };

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
	public GameObject allyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public CombatHUD playerHUD;
    public CombatHUD enemyHUD;
	public CombatHUD allyHUD;

	#region UI elements
	public GameObject dialoguePopup;
    public Text dialogueText;
    public Text enemyDamageText;
    public GameObject enemyPanel;
    public Image enemyImage;
    public GameObject playerHPGuage;
	public GameObject allyHPGuage;
	public Image playerHealthBG;
	public Image allyHealthBG;
	#endregion

	#region audio
	private AudioSource audio;
    public AudioClip hurtSound;
    public AudioClip killSound;
	#endregion

	Unit playerunit;
    Unit enemyUnit;
	Unit allyUnit;

	private int rng;

    //AnimationController animCon = new AnimationController();

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        audio = GetComponent<AudioSource>();
        StartCoroutine(SetupBattle());
		playerHealthBG.color = Color.grey;
		allyHealthBG.color = Color.grey;
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerunit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

		GameObject allyGO = Instantiate(allyPrefab, playerBattleStation);
		allyUnit = allyGO.GetComponent<Unit>();

		dialogueText.text = "A " + enemyUnit.name + " approaches!";

		//the HUDs for the respective members are setup here
        playerHUD.SetHUD(playerunit);
        enemyHUD.SetHUD(enemyUnit);
		if(allyUnit.isAnAlly1 == true)
		{
			allyHUD.SetHUD(allyUnit);
			allyHPGuage.SetActive(true);
		}
		else if(allyUnit.isAnAlly1 == false)
		{
			allyHPGuage.SetActive(false);
		}

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        Playerturn();
    }

    void Playerturn()
    {
        dialoguePopup.SetActive(false);
		playerHealthBG.color = Color.white;
		allyHealthBG.color = Color.gray;
	}

	void AllyTurn1()
	{
		playerHealthBG.color = Color.gray;
		allyHealthBG.color = Color.white;
		Debug.Log("Ally is in control");
	}

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerunit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialoguePopup.SetActive(true);
        dialogueText.text = playerunit.name + " attacks!";

		#region enemyhurt flash
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
		#endregion

		dialogueText.text = playerunit.name + " dealt " + playerunit.damage + " damage!";
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
			if(allyUnit.isAnAlly1 == true)
			{
				state = BattleState.ALLYTURN;
				AllyTurn1();
			}
			else
			{
	            state = BattleState.ENEMYTURN;
	            StartCoroutine(EnemyTurn());
			}
        }
    }

	IEnumerator AllyAttack()
	{
		bool isDead = enemyUnit.TakeDamage(allyUnit.damage);

		enemyHUD.SetHP(enemyUnit.currentHP);
		dialoguePopup.SetActive(true);
		dialogueText.text = allyUnit.name + " attack!";

		#region enemyhurt flash
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
		#endregion

		dialogueText.text = allyUnit.name + " dealt " + allyUnit.damage + " damage!";
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

			//if (allyUnit.isAnAlly1 == true && allyUnit.isAnAlly2 == false)
			//{
			//	state = BattleState.ALLYTURN;
			//	AllyTurn1();
			//}
			//else if(allyUnit.isAnAlly2 == true && allyUnit.isAnAlly1 == false)
			//{

			//}
			//else
			//{
			//	state = BattleState.ENEMYTURN;
			//	StartCoroutine(EnemyTurn());
			//}
		}
	}

	IEnumerator EnemyTurn()
    {
		bool isDead = playerunit.currentHP == 0;
		playerHealthBG.color = Color.gray;
		allyHealthBG.color = Color.gray;

		dialoguePopup.SetActive(true);
        dialogueText.text = "The " + enemyUnit.name + " attacks!";

        audio.PlayOneShot(hurtSound, 1);

		rng = Random.Range(1, 6);
		if(rng < 2)
		{
			playerunit.TakeDamage(enemyUnit.damage);
			playerHUD.SetHP(playerunit.currentHP);
			Debug.Log("The enemy attacked the player");

			dialogueText.text = playerunit.name + " took " + enemyUnit.damage.ToString() + " damage!";
		}
		else if(rng > 2)
		{
			allyUnit.TakeDamage(enemyUnit.damage);
			allyHUD.SetHP(allyUnit.currentHP);
			Debug.Log("The enemy attacked the ally");

			yield return new WaitForSeconds(2f);

			dialogueText.text = allyUnit.name + " took " + enemyUnit.damage.ToString() + " damage!";
		}

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
        //if (state != BattleState.PLAYERTURN || state != BattleState.ALLYTURN)
        //    return;

        //StartCoroutine(PlayerAttack());
		if(state == BattleState.PLAYERTURN)
		{
			StartCoroutine(PlayerAttack());
		}
		else if (state == BattleState.ALLYTURN)
		{
			StartCoroutine(AllyAttack());
		}
		else
		{
			// do nothing
		}
    }
}
