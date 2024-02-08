using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
	public static RoundManager inst;
	private int RoundTimer; // round is one dish
	public Dish dishRef;
	public float beltSpeed = 0.2f;
	public float elapsedTime = 0f;
	[SerializeField] Transform[] positions;
	[SerializeField]
	int[][] spawnPositions = new int[][] {
		new int[] { // 4 ing
			0, 1, 2, 3
		},
		new int[] { // 6 ing
			0, 1, 2, 3, 4, 8
		},
		new int[] { // 9 ing
			0, 1, 2, 3, 4, 5, 6, 7, 8
		},
		new int[] { // 11 ing
			0, 1, 2, 3, 4, 5, 6, 7, 8, 10, 11
		},
		new int[] { // 13 ing
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12
		},
	};
	[SerializeField] Transform ingredientParent;
	[SerializeField] Vector3 spawnOffset;
	public AnimationCurve smooth;
	[SerializeField] float slideDuration;
	[SerializeField] float slideGap;
	[SerializeField] float startAngle, finalAngle;
	bool dishInstantiated = false;

	List<GameObject> instIngredients;

	[SerializeField] AnimationCurve disappearanceSpeeds;
	[SerializeField] GameObject smokeParticle, dustParticle;

	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		inst = this;
	}
    // Start is called before the first frame update
    void Start()
    {
		// we want to start with starting the animcontroller
		RoundTimer += GameManager.inst.CookingTime + GameManager.inst.ScrollPastTime;
		GameManager.inst.currentGameState = GameState.ScrollPast;
	    //SpawnIngredients();
    }

    // Update is called once per frame
    void Update()
    {
		elapsedTime += Time.deltaTime;
		switch (GameManager.inst.currentGameState)
		{
			case GameState.ScrollPast:
				if (elapsedTime >= GameManager.inst.ScrollPastTime - slideDuration)
				{
					if (!dishInstantiated)
					{
						Dish d = new Dish(new List<Ingredient> { Ingredient.Eggs, Ingredient.Avocados, Ingredient.Bread, Ingredient.Flour }, new Dictionary<Equipments, float> { }, 1);
						dishRef = d;
						SpawnIngredients();
						dishInstantiated = true;
					}
				}
				if (elapsedTime >= GameManager.inst.ScrollPastTime)
				{
					GameManager.inst.currentGameState = GameState.StopScroll;
				}
				break;
			case GameState.StopScroll:
				if (elapsedTime - GameManager.inst.ScrollPastTime >= GameManager.inst.CookingTime)
				{
					GameManager.inst.currentGameState = GameState.ContinueScroll;
				}
				break;
			case GameState.ContinueScroll:
				beltSpeed *= 1.01f;
				if (beltSpeed >= 10)
				{
					beltSpeed = 10;
				}
				if (elapsedTime - (GameManager.inst.ScrollPastTime + GameManager.inst.CookingTime) >= 2f && GameManager.inst.ingredientsOnBelt.Count <= 0)
				{
					StartCoroutine(DisappearAll());
					GameManager.inst.currentGameState = GameState.ShowScore;
				}
				break;
			case GameState.ShowScore:

				if (beltSpeed >= 0.1f)
				{
					beltSpeed *= 0.99f;
				}
				else if (beltSpeed <= 0.1f)
				{
					beltSpeed = -MathF.Abs(beltSpeed);
					if (beltSpeed < 0)
					{
						beltSpeed *= 1.006f;
					}

				}
				if (beltSpeed <= -0.75)
				{
					beltSpeed = -0.75f;
				}
				break;
		}

		/*
		if (Input.GetKeyDown(KeyCode.Space))
        {
			Dish d = new Dish(new List<Ingredient> { Ingredient.Eggs, Ingredient.Cheese, Ingredient.Bread, Ingredient.Flour }, new Dictionary<Equipments, float> { }, 1);
			dishRef = d;
			SpawnIngredients();
        }
		*/

    }
    
	public void SpawnIngredients(){

		List<GameObject> ingredients = new List<GameObject>();
		instIngredients = new List<GameObject>();

		// get actual ingredients
		foreach(Ingredient x in dishRef.valuations.Keys){
			ingredients.Add(Resources.Load("Prefabs/Ingredients/" + x.ToString().Replace("_", " ")) as GameObject);
		}

		int maxIngredientsLevel = GameManager.inst.ingredientsPerLevel[GameManager.inst.levelNumber];
		int ingredientCount = Enum.GetNames(typeof(Ingredient)).Length;

		// get random ingredients
		for (int i = ingredients.Count; i < Mathf.Min(maxIngredientsLevel, ingredientCount); i++)
        {
			//print(Enum.GetNames(typeof(Ingredient)).ToString() + "   " + );
			Ingredient x = (Ingredient)UnityEngine.Random.Range(0, ingredientCount);
			GameObject obj = Resources.Load("Prefabs/Ingredients/" + x.ToString().Replace("_", " ")) as GameObject;
			//print(x.ToString() + " " + obj.name);
			if (ingredients.Contains(obj))
				i--;
			else
				ingredients.Add(obj);
		}

        Shuffle(ref ingredients);

		for(int i = 0; i < ingredients.Count; i++)
        {
			instIngredients.Add(Instantiate(ingredients[i], positions[spawnPositions[GameManager.inst.levelNumber][i]].position + spawnOffset, Quaternion.Euler(0, startAngle, 0), ingredientParent));
//			print(instIngredients[i].transform.position);
        }

		StartCoroutine(ChainSlide(instIngredients, slideGap));

	}

	public void ClearIngredients()
    {
		Shuffle(ref instIngredients);
		StartCoroutine(DisappearAll());
    }

	public static void Shuffle<T>(ref List<T> list)
	{
		System.Random rng = new System.Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	IEnumerator ChainSlide(List<GameObject> objs, float gap)
    {
		foreach(GameObject obj in objs)
        {
			IEnumerator slide = Slide(obj.transform, slideDuration);
			StartCoroutine(slide);
			yield return new WaitForSeconds(gap);
		}
    }

	IEnumerator Slide(Transform t, float dur)
    {
		Vector3 startPos = t.position;
		Vector3 endPos = t.position - spawnOffset;
		Quaternion startRot = Quaternion.Euler(0, startAngle, 0), endRot = Quaternion.Euler(0, finalAngle, 0);
		float start = Time.time;
		while(Time.time < start + dur)
        {
			float tRaw = (Time.time - start) / dur;
			float time = smooth.Evaluate(tRaw);
			t.SetPositionAndRotation(
				Vector3.Lerp(startPos, endPos, time),
				Quaternion.Slerp(startRot, endRot, time)
			);
			yield return null;
        }
		transform.SetPositionAndRotation(endPos, endRot);
    }

	IEnumerator DisappearAll()
    {
		Shuffle(ref instIngredients);
		for(int i = 0; i < instIngredients.Count; i++)
        {
			GameObject particle1 = Instantiate(smokeParticle, instIngredients[i].transform.position + new Vector3(0, 0, 0), Quaternion.Euler(-90,0,0));
			GameObject particle2 = Instantiate(dustParticle, instIngredients[i].transform.position + new Vector3(0, 5, 0), Quaternion.identity);
			Destroy(instIngredients[i]);
			Destroy(particle1, 5f);
			Destroy(particle2, 5f);
			yield return new WaitForSeconds(disappearanceSpeeds.Evaluate(i));
		}
		instIngredients.Clear();
    }
}

