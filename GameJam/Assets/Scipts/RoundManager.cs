using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public bool roundOver = false;
	GameObject finalDish;
	int l = 0;
	
	public void QueryDish(){
		// THIS METHOD IS WRITTEN BY SOHUM!!!!!!!

		Dish[] possibleDishes = {
			new Dish("Hot Dog", new List<Ingredient> {Ingredient.Hot_Dog_Buns, Ingredient.Sausages, Ingredient.Ketchup}, new Dictionary<Equipment,float>{} )
			, new Dish("Donut", new List<Ingredient> {Ingredient.Eggs, Ingredient.Dough, Ingredient.Chocolate}, new Dictionary<Equipment, float>(){{Equipment.Mixer, 1}})
			, new Dish("Cake", new List<Ingredient> {Ingredient.Eggs, Ingredient.Flour, Ingredient.Chocolate, Ingredient.Strawberries}, new Dictionary<Equipment, float>(){{Equipment.Mixer, 1},{Equipment.Stove,2}})
			, new Dish("Burger", new List<Ingredient> {Ingredient.Buns,Ingredient.Cheese,Ingredient.Beef}, new Dictionary<Equipment, float>(){{Equipment.Stove,2}})
			, new Dish("IceCream", new List<Ingredient> {Ingredient.Eggs, Ingredient.Milk }, new Dictionary<Equipment, float>(){{Equipment.Mixer, 1}})
			, new Dish("Pizza", new List<Ingredient> {Ingredient.Dough, Ingredient.Pizza_Sauce, Ingredient.Cheese, Ingredient.Toppings }, new Dictionary<Equipment, float>(){{Equipment.Stove, 3}, {Equipment.Mixer, 2}})
			, new Dish("Taco", new List<Ingredient> {Ingredient.Taco_Meat, Ingredient.Taco_Sauce, Ingredient.Cheese, Ingredient.Tortillas }, new Dictionary<Equipment, float>(){{Equipment.Knives, 3},{Equipment.Stove,3}})
			, new Dish("Sushi", new List<Ingredient> {Ingredient.Rice_and_Seaweed, Ingredient.Fish_Fillets, Ingredient.Avocados},new Dictionary<Equipment, float>(){{Equipment.Microwave, 1}, {Equipment.Knives, 2}, {Equipment.Stove, 3}})
		};

		dishRef =  possibleDishes[UnityEngine.Random.Range(0, possibleDishes.Length - 1)];
	}
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		inst = this;
		
	}
	// Start is called before the first frame update
	void Start()
	{
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
			if(GameManager.inst.totalSpawnedDished < 1)
				QueryDish();
			if (GameManager.inst.currentFinishedDishesOnScreen.Count <=0 && GameManager.inst.totalSpawnedDished >=1)
			{
				GameManager.inst.currentRoundState = RoundState.StopScroll;
				
				SpawnIngredients();
			}
			if(GameManager.inst.currentFinishedDishesOnScreen.Count <=0 && GameManager.inst.totalSpawnedDished < 1)
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
				
			if (elapsedTime - stopScrollStartTime >= GameManager.inst.CookingTime)
			{
				
				StartCoroutine(DisappearAll());
				GameManager.inst.currentRoundState = RoundState.ContinueScroll;
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
			beltAnimator.SetSpeedColective(-1.5f, 0.007f);
			if(GameManager.inst.currentFinishedDishesOnScreen.Count <=0){
				finalDish = SpawnFinishedDish(GameManager.inst.currentRoundState);
			}
			if(finalDish.transform.position.x < -120){
				Reset();
			}
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
		GameManager.inst.roundNumber ++;
		GameManager.inst.money += 120;
		elapsedTime = 0;
		dishInstantiated = false;
		CameraMovement.inst.MoveToIndex(0);
		GameManager.inst.totalNumOfIngredients = 0;
		GameManager.inst.currentRoundState = RoundState.ScrollPast;
		GameManager.inst.totalSpawnedDished = 0;
		Destroy(finalDish.gameObject);
		finalDish = null;
	}
	public void ClearIngredients()
	{
		Shuffle(ref instIngredients);
		StartCoroutine(DisappearAll());
	}
	public GameObject SpawnFinishedDish(RoundState state){
	
		
		
		
		
		
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