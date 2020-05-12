using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporaButton : MonoBehaviour
{
	public Button temporaButton;
	public Text temporaName;
	public Text temporaCost;
	public Image iconImage;

	private Tempora tempora;
	private TemporaScrollList scrollList;

    // Start is called before the first frame update
    void Start()
    {
		
    }

	public void Setup(Tempora currentTempora, TemporaScrollList currentScrollList)
	{
		tempora = currentTempora;
		temporaName.text = tempora.temporaName;
		temporaCost.text = tempora.spCost.ToString();
		iconImage.sprite = tempora.temporaIcon;
		//scrollList.temporaDescriptionText.text = tempora.temporaDescription;

		scrollList = currentScrollList;
	}
}
