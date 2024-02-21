using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayManager : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI text;
	[SerializeField] TextMeshProUGUI money;
	[SerializeField] TextMeshProUGUI roundState;
    [SerializeField] CanvasGroup cg;
    float currentOpac;
    float targetOpac;
    [SerializeField] float interp;


    private void Start()
    {
        currentOpac = 0; targetOpac = 0;
    }

    private void Update()
	{
		money.text = "$" + GameManager.inst.money;
		switch(GameManager.inst.currentRoundState){
		case RoundState.ScrollPast:
			roundState.text = "Pay attention to the items on the belt!";
			break;
		case RoundState.StopScroll:
			roundState.text = "Click on the ingredients to add them to the final dish!";
			break;
		case RoundState.ContinueScroll:
			roundState.text = "Your dish is being cooked...Wait For a few seconds!";
			break;
		case RoundState.ShowScore:
			roundState.text = "Your dish has been cooked...the score is displayed below...";
			RoundManager.inst.roundScore += ( (int)RoundManager.inst.dishRef.CalculateScore(GameManager.inst.IngredientAmount, GameManager.inst.equipmentValues));
			text.text =( (int)RoundManager.inst.dishRef.CalculateScore(GameManager.inst.IngredientAmount, GameManager.inst.equipmentValues)).ToString() + "/100";
			targetOpac = 1;
			
			break;
		}
		if(GameManager.inst.currentRoundState != RoundState.ShowScore){
        RaycastHit hit;
        bool hitting = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f, LayerMask.GetMask("FoodItem"));
	    if (hitting)
	    {
		    string str = hit.collider.gameObject.name;
		    Ingredient ing = hit.collider.GetComponent<PhysicalIngredient>().ingredientType;
		    str += " - " + GameManager.inst.GetCount(ing) + "/" + ing.GetMax();
		    text.text = str;
		    targetOpac = 1;
	    }


        else
        {
            targetOpac = 0;
        }

		}

        currentOpac = Mathf.Lerp(currentOpac, targetOpac, interp);
        if (Mathf.Abs(cg.alpha - 0.5f) > 0.49f)
            currentOpac = targetOpac;

        cg.alpha = currentOpac;
    }

}
