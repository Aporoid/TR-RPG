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
	public Button meleeButton;
	public Button gunButton;
	public Button temporaButton;

	#endregion

	#region audio
	private AudioSource audio;
    public AudioClip hurtSound;
    public AudioClip killSound;
	public AudioClip gunshot;
	#endregion

	Unit playerunit;
    Unit enemyUnit;
	Unit allyUnit;

	private int rng;
	private bool isPlayerDead;
	private bool isAllyDead;
	private bool isEnemyDead;

	private int tripletapGuarantee;

    //AnimationController animCon = new AnimationController();

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        audio = GetComponent<AudioSource>();
        StartCoroutine(SetupBattle());

		playerHealthBG.color = Color.grey;
		allyHealthBG.color = Color.grey;
		isEnemyDead = enemyUnit.currentHP == 0;
		isPlayerDead = playerunit.currentHP == 0;
		tripletapGuarantee = 0;
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
		meleeButton.interactable = true;
		gunButton.interactable = true;
		temporaButton.interactable = true;
	}

	void AllyTurn1()
	{
		playerHealthBG.color = Color.gray;
		allyHealthBG.color = Color.white;
		meleeButton.interactable = true;
		gunButton.interactable = true;
		temporaButton.interactable = true;

		Debug.Log("Ally is in control");
	}

    IEnumerator PlayerAttack()
    {
		//bool isDead = enemyUnit.TakeDamage(playerunit.damage);
		meleeButton.interactable = false;

		enemyUnit.TakeDamage(playerunit.damage / enemyUnit.defense);

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

		meleeButton.interactable = true;
		meleeButton.Select();

        if (isEnemyDead)
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
		//bool isDead = enemyUnit.TakeDamage(allyUnit.damage);
		meleeButton.interactable = false;

		enemyUnit.TakeDamage(allyUnit.damage);
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

		meleeButton.interactable = true;
		meleeButton.Select();

		if (isEnemyDead)
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

		//bool isDead = playerunit.currentHP == 0;
		playerHealthBG.color = Color.gray;
		allyHealthBG.color = Color.gray;

		enemyImage.color = Color.gray;
		yield return new WaitForSeconds(0.1f);
		enemyImage.color = Color.white;
		yield return new WaitForSeconds(0.1f);
		enemyImage.color = Color.gray;
		yield return new WaitForSeconds(0.1f);
		enemyImage.color = Color.white;
		yield return new WaitForSeconds(0.2f);

		dialoguePopup.SetActive(true);
        dialogueText.text = "The " + enemyUnit.name + " attacks!";

		meleeButton.interactable = false;
		gunButton.interactable = false;
		temporaButton.interactable = false;

		yield return new WaitForSeconds(2f);

        audio.PlayOneShot(hurtSound, 1);

		rng = Random.Range(1, 12);

		if(rng < 2)
		{
			dialogueText.text = "The attack missed!";
		}
		else if (rng < 6 && rng > 2)
		{
			playerunit.TakeDamage(enemyUnit.damage / playerunit.defense);
			playerHUD.SetHP(playerunit.currentHP);
			Debug.Log("The enemy attacked the player");

			dialogueText.text = playerunit.name + " took " + enemyUnit.damage.ToString() + " damage!";
		}
		else if(rng > 6)
		{
			allyUnit.TakeDamage(enemyUnit.damage / allyUnit.defense);
			allyHUD.SetHP(allyUnit.currentHP);
			Debug.Log("The enemy attacked the ally");

			yield return new WaitForSeconds(2f);

			dialogueText.text = allyUnit.name + " took " + enemyUnit.damage.ToString() + " damage!";
		}

        yield return new WaitForSeconds(2f);
        dialoguePopup.SetActive(false);

		meleeButton.interactable = true;
		gunButton.interactable = true;
		temporaButton.interactable = true;
		meleeButton.Select();

		if (isPlayerDead)
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

	public void OnGunSelect()
	{
		if (state == BattleState.PLAYERTURN)
		{
			StartCoroutine(PlayerGunfire());
		}
		else if (state == BattleState.ALLYTURN)
		{
			StartCoroutine(AllyGunfire());
		}
		else
		{
			// do nothing
		}
	}

	IEnumerator PlayerGunfire()
	{
		gunButton.interactable = false;
		if (playerunit.ammo > 1)
		{
			rng = Random.Range(0, 12);
			if (rng >= 2 && rng <= 9) // the gunshot hits
			{
				audio.PlayOneShot(gunshot, 1);
				enemyUnit.TakeDamage(playerunit.gunDamage);
				enemyImage.enabled = false;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = true;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = false;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = true;
				yield return new WaitForSeconds(0.1f);

				dialoguePopup.SetActive(true);
				dialogueText.text = "1 shot hit for " + playerunit.gunDamage + " damage!";
				yield return new WaitForSeconds(2f);
				dialoguePopup.SetActive(false);

				playerunit.ammo--;
				tripletapGuarantee++;
			}
			else if (rng < 2) // the gunshot misses
			{
				audio.PlayOneShot(gunshot, 1);
				dialoguePopup.SetActive(true);
				dialogueText.text = "Attack missed!";
				yield return new WaitForSeconds(2f);
				dialoguePopup.SetActive(false);
				playerunit.ammo--;
			}
			else if (rng > 9 || tripletapGuarantee == 5) // fire 3 shots
			{
				Debug.Log("Triple tap ready to fire");

				audio.PlayOneShot(gunshot, 1);
				yield return new WaitForSeconds(0.2f);
				audio.PlayOneShot(gunshot, 1);
				yield return new WaitForSeconds(0.2f);
				audio.PlayOneShot(gunshot, 1);
				yield return new WaitForSeconds(0.2f);
				enemyUnit.TakeDamage(playerunit.gunDamage * 3);

				enemyImage.enabled = false;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = true;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = false;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = true;
				yield return new WaitForSeconds(0.1f);

				dialoguePopup.SetActive(true);
				dialogueText.text = "3 shots hit for " + playerunit.gunDamage*3 + " damage!";
				yield return new WaitForSeconds(2f);
				dialoguePopup.SetActive(false);

				playerunit.ammo-= 3;
				tripletapGuarantee = 0;
			}
		}
		else
		{
			dialoguePopup.SetActive(true);
			dialogueText.text = "No ammo available!";
			yield return new WaitForSeconds(2f);
			dialoguePopup.SetActive(false);
		}

		gunButton.interactable = true;
		gunButton.Select();

		if (isEnemyDead)
		{
			state = BattleState.WON;
			enemyPanel.SetActive(false);
			audio.PlayOneShot(killSound, 1);
			EndBattle();
		}
		else
		{
			if (allyUnit.isAnAlly1 == true)
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

	IEnumerator AllyGunfire()
	{
		gunButton.interactable = false;
		if (allyUnit.ammo > 1)
		{
			rng = Random.Range(0, 12);
			if (rng >= 2 && rng <= 9) // the gunshot hits
			{
				audio.PlayOneShot(gunshot, 1);
				enemyUnit.TakeDamage(playerunit.gunDamage);
				enemyImage.enabled = false;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = true;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = false;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = true;
				yield return new WaitForSeconds(0.1f);

				dialoguePopup.SetActive(true);
				dialogueText.text = "1 shot hit for " + playerunit.gunDamage + " damage!";
				yield return new WaitForSeconds(2f);
				dialoguePopup.SetActive(false);
			}
			else if (rng < 2) // the gunshot misses
			{
				audio.PlayOneShot(gunshot, 1);
				dialoguePopup.SetActive(true);
				dialogueText.text = "Attack missed!";
				yield return new WaitForSeconds(2f);
				dialoguePopup.SetActive(false);
			}
			else if (rng > 9 && playerunit.ammo == 3) // fire 3 shots
			{
				audio.PlayOneShot(gunshot, 1);
				yield return new WaitForSeconds(0.2f);
				audio.PlayOneShot(gunshot, 1);
				yield return new WaitForSeconds(0.2f);
				audio.PlayOneShot(gunshot, 1);
				yield return new WaitForSeconds(0.2f);
				enemyUnit.TakeDamage(playerunit.gunDamage * 3);

				enemyImage.enabled = false;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = true;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = false;
				yield return new WaitForSeconds(0.1f);
				enemyImage.enabled = true;
				yield return new WaitForSeconds(0.1f);

				dialoguePopup.SetActive(true);
				dialogueText.text = "3 shots hit for " + playerunit.gunDamage * 3 + " damage!";
				yield return new WaitForSeconds(2f);
				dialoguePopup.SetActive(false);
			}
		}
		else
		{
			dialoguePopup.SetActive(true);
			dialogueText.text = "No ammo available!";
			yield return new WaitForSeconds(2f);
			dialoguePopup.SetActive(false);
		}

		gunButton.interactable = true;
		gunButton.Select();

		if (isEnemyDead)
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
}
