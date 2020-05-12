using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Tempora
{
	public Sprite temporaIcon;
	public string temporaName;
	[TextArea(3, 5)]
	public string temporaDescription;
	public int spCost;
	public int temporaDamage;
}

public class TemporaScrollList : MonoBehaviour
{
	public List<Tempora> temporaList;
	public Transform contentPanel;
	public TemporaScrollList secondaryTemporaScrollList;
	public TemporaObjectPool buttonObjectPool;

	public Text temporaDescriptionText;

    // Start is called before the first frame update
    void Start()
    {
		RefreshDisplay();
    }

	public void RefreshDisplay()
	{
		AddButtons();
	}

	private void AddButtons()
	{
		for(int i = 0; i < temporaList.Count; i++)
		{
			Tempora tempora = temporaList[i];
			GameObject newButton = buttonObjectPool.GetObject();
			newButton.transform.SetParent(contentPanel);

			TemporaButton temporaButton = newButton.GetComponent<TemporaButton>();
			temporaButton.Setup(tempora, this);
		}
	}

	private void AddTempora(Tempora temporatoAdd, TemporaScrollList magicList)
	{
		magicList.temporaList.Add(temporatoAdd);
	}

	private void RemoveTempora(Tempora itemToRemove, TemporaScrollList scrollList)
	{
		for (int i = scrollList.temporaList.Count - 1; i >= 0; i--)
		{
			if(scrollList.temporaList[i] == itemToRemove)
			{
				scrollList.temporaList.RemoveAt(i);
			}
		}
	}
}
