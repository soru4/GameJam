using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoundManager : MonoBehaviour
{
	public static RoundManager inst;
	private int RoundTimer; // round is one dish
	public Dish dishRef;
	public BeltManager beltAnimator;

	public float elapsedTime = 0f;
	[SerializeField] Transform[] positions;
	public List<GameObject> instIngredients;
	[SerializeField] int[][] spawnPositions = new int[][] {
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
	public int roundScore = 0;
	[SerializeField] Transform ingredientParent;
	[SerializeField] Vector3 spawnOffset;
	public  AnimationCurve smooth;
	[SerializeField] float slideDuration;
	[SerializeField] float slideGap;
	[SerializeField] float startAngle, finalAngle;

	public int stopScrollStartTime = 0;
	bool dishInstantiated = false;
	[SerializeField] AnimationCurve disappearanceSpeeds;
	[SerializeField] GameObject smokeParticle, dustParticle;
	[SerializeField] GameObject tearEffect; 
	[SerializeField] GameObject particle; 
	public bool roundOver = false;
	[SerializeField] GameObject DONE;
	[SerializeField] TextMeshProUGUI t;
	GameObject finalDish;
	GameObject p,o;
	int l = 0;
	
	public void QueryDish(){
		// THIS METHOD IS WRITTEN BY SOHUM!!!!!!!
		print("getting a new dish!");
		Dish[] possibleDishes = {
			new Dish("HotDog", new List<Ingredient> {Ingredient.Hot_Dog_Buns, Ingredient.Sausages, Ingredient.Ketchup}, new float[6] )
			, new Dish("Donut", new List<Ingredient> {Ingredient.Eggs, Ingredient.Dough, Ingredient.Chocolate}, new float[6])
			, new Dish("Cake", new List<Ingredient> {Ingredient.Eggs, Ingredient.Flour, Ingredient.Chocolate, Ingredient.Strawberries}, new float[6])
			, new Dish("Burger", new List<Ingredient> {Ingredient.Buns,Ingredient.Cheese,Ingredient.Beef}, new float[6])
			, new Dish("IceCream", new List<Ingredient> {Ingredient.Eggs, Ingredient.Milk },new float[6])
			, new Dish("Pizza", new List<Ingredient> {Ingredient.Dough, Ingredient.Pizza_Sauce, Ingredient.Cheese, Ingredient.Toppings }, new float[6])
			, new Dish("Taco", new List<Ingredient> {Ingredient.Taco_Meat, Ingredient.Taco_Sauce, Ingredient.Cheese, Ingredient.Tortillas }, new float[6])
			, new Dish("Sushi", new List<Ingredient> {Ingredient.Rice_and_Seaweed, Ingredient.Fish_Fillets, Ingredient.Avocados},new float[6])
		};

		dishRef =  possibleDishes[UnityEngine.Random.Range(0, possibleDishes.Length )];
			
	}
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		inst = this;
		
	}
	// Start is called before the first frame update
	void Start()
	{
		DONE.SetActive(false);
		tearEffect.SetActive(false);
		beltAnimator = beltAnimator.GetComponent<BeltManager>();
		// we want to start with starting the animcontroller
		RoundTimer += GameManager.inst.CookingTime + GameManager.inst.ScrollPastTime;
		GameManager.inst.currentRoundState = RoundState.ScrollPast;
		//SpawnIngredients();
   
		// we want to start with starting the animcontroller

		RoundTimer = GameManager.inst.CookingTime + GameManager.inst.ScrollPastTime;
		GameManager.inst.currentRoundState = RoundState.ScrollPast;
		//SpawnIngredients();
		elapsedTime = 0;
	}

	// Update is called once per frame
	void Update()
	{
		elapsedTime += Time.deltaTime;
		switch (GameManager.inst.currentRoundState) 
		{
		case RoundState.ScrollPast:
			
			beltAnimator.SetSpeedColective(2, 0.001f);
			if(GameManager.inst.totalSpawnedDished < 1 && GameManager.inst.roundNumber ==0)
				QueryDish();
				
			
			if(GameManager.inst.currentFinishedDishesOnScreen.Count <=0 && GameManager.inst.totalSpawnedDished < 1 && GameManager.inst.roundNumber ==0)
				SpawnFinishedDish(GameManager.inst.currentRoundState);
			if(elapsedTime >= GameManager.inst.ScrollPastTime - slideDuration)
			{
				if (!dishInstantiated)
				{					
					
					dishInstantiated = true;
				}
			}
			
			break;
				
		case RoundState.StopScroll:
			beltAnimator.SetSpeedColective(1, 0.001f);
			if(stopScrollStartTime == 0 )
				stopScrollStartTime = (int)Time.time;
			if(UnityEngine.Random.Range(0,10000) == 10){
				tearEffect.SetActive(!tearEffect.active);
			}
	
			if (elapsedTime - stopScrollStartTime >= GameManager.inst.CookingTime + 3)
			{
				
				StartCoroutine(DisappearAll());
				GameManager.inst.currentRoundState = RoundState.ContinueScroll;
				tearEffect.SetActive(false);
				
			}
			break;
				
		case RoundState.ContinueScroll:
			beltAnimator.SetSpeedColective(10, 0.003f);
			if ( GameManager.inst.ingredientsOnBelt.Count <= 0)
			{
				
				GameManager.inst.currentRoundState = RoundState.ShowScore;
	

			}
			break;
		case RoundState.ShowScore:
			if(!roundOver){
				CameraMovement.inst.MoveToIndex(2); 
				roundOver = true;
			}
			beltAnimator.SetSpeedColective(-1.5f, 0.008f);
			if(beltAnimator.speed < 0)
				finalDish = SpawnFinishedDish(GameManager.inst.currentRoundState);
			
			

			break;
		}

		
	}
    
	public void SpawnIngredients(){

		List<GameObject> ingredients = new List<GameObject>();
		instIngredients = new List<GameObject>();

		// get actual ingredients
		foreach(Ingredient x in dishRef.valuations.Keys){
			print(x.ToString());
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
	public void Reset(){
		print("resetting");
		GameManager.inst.IngredientAmount = new Dictionary<Ingredient, float>();
		GameManager.inst.levelNumber ++;
		GameManager.inst.money += 120;
		if(GameManager.inst.levelNumber >= 5){
			// GameOver!!
			DONE.SetActive(true);
			t.text = roundScore + " / " + "500";
			
		}
		elapsedTime = 0;
		dishInstantiated = false;
		CameraMovement.inst.MoveToIndex(0);
		GameManager.inst.totalNumOfIngredients = 0;
		GameManager.inst.currentRoundState = RoundState.ScrollPast;
		GameManager.inst.totalSpawnedDished = 0;
		GameManager.inst.currentFinishedDishesOnScreen = new List<GameObject>();

		QueryDish();
		SpawnFinishedDish(GameManager.inst.currentRoundState);
		roundOver = false;
	}
	public void ClearIngredients()
	{
		Shuffle(ref instIngredients);
		StartCoroutine(DisappearAll());
	}
	public GameObject SpawnFinishedDish(RoundState state){
	
		
		
		print("dishes");

		if(GameManager.inst.currentFinishedDishesOnScreen.Count <=0){
		
			if(state == RoundState.ScrollPast){
				GameManager.inst.totalSpawnedDished++;
				print(dishRef.name);
				GameObject x = Instantiate((GameObject)Resources.Load("Prefabs/Dishes/"+dishRef.name), new Vector3(-130f,-9.78f,18.86f), Quaternion.identity);
				x.transform.localScale = new Vector3(8,8,8);
				x.AddComponent<PhysicalDish>().onBelt = true;
				GameManager.inst.currentFinishedDishesOnScreen.Add(x);
				return x;
			}else if(state == RoundState.ShowScore){
				GameManager.inst.totalSpawnedDished++;
				GameObject x = Instantiate((GameObject)Resources.Load("Prefabs/Dishes/"+dishRef.name), new Vector3(230,-9.78f,18.86f), Quaternion.identity);
				x.AddComponent<PhysicalDish>().onBelt = true;
				x.transform.localScale = new Vector3(8,8,8);
				GameManager.inst.currentFinishedDishesOnScreen.Add(x);
				return x;
			}
		}
		return null;
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