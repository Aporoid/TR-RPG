using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The reference for all Tempora spells to be based on, like the Unit script.
/// </summary>
public class TemporaBase : MonoBehaviour
{
	public enum TemporaChoices { Damage, StatUp, StatDown, TimeManipulation};

	public Sprite temporaIcon;
	public string temporaName;
	public string temporaDescription;
	public int spCost;
	public int temporaDamage;

	public TemporaChoices temporaChoices;
}
