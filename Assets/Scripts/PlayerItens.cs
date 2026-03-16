using UnityEngine;

public class PlayerItens : MonoBehaviour
{
   
    [SerializeField] private int _totalWoods;
    [SerializeField] private float _totalWater;
    [SerializeField] private int _totalCarrot;
    [SerializeField] private int _totalFish;

    //limites
    private float waterLimit = 50;
    private float woodLimit = 10;
    private float carrotLimit = 10;
    private float  fishLimit = 10; 

    
    public int totalWoods { get => _totalWoods; set => _totalWoods = value; }
    public float TotalWater { get => _totalWater; set => _totalWater = value; }
    public int TotalCarrot { get => _totalCarrot; set => _totalCarrot = value; }
    public int TotalFish { get => _totalFish; set => _totalFish = value; }



    public float WaterLimit1 { get => waterLimit; set => waterLimit = value; }
    public float WoodLimit { get => woodLimit; set => woodLimit = value; }
    public float CarrotLimit { get => carrotLimit; set => carrotLimit = value; }
    public float FishLimit { get => fishLimit; set => fishLimit = value; }

    public void WaterLimit(float water)
    {
        if(TotalWater < waterLimit)
        {
            TotalWater += water;
        }
    }
}
