using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Controller : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private Image waterUI;
    [SerializeField] private Image woodUI;
    [SerializeField] private Image carrotUI;
    [SerializeField] private Image fishUI;

    [Header("Tools")]
    //[serializefield] private image axeui;
    //[serializefield] private image shovelui;
    //[serializefield] private image bucketui;
    public List<Image> toolsUI = new List<Image>();

    [SerializeField] private Color selectColor;
    [SerializeField] private Color alphaColor;

    private Player player;




    private PlayerItens playerItens;
    private void Awake()
    {
        playerItens = FindObjectOfType<PlayerItens>();
        player = playerItens.GetComponent<Player>();
    }
    void Start()
    {
        waterUI.fillAmount = 0f;
        woodUI.fillAmount = 0f;
        carrotUI.fillAmount = 0f;
        fishUI.fillAmount = 0f;
    }

    void Update()
    {
        waterUI.fillAmount = playerItens.TotalWater / playerItens.WaterLimit1;
        woodUI.fillAmount = playerItens.totalWoods / playerItens.WoodLimit;
        carrotUI.fillAmount = playerItens.TotalCarrot / playerItens.CarrotLimit;
        fishUI.fillAmount = playerItens.TotalFish / playerItens.FishLimit;

        for (int i = 0; i < toolsUI.Count; i++)
        {
            if(i == player.HandlingObj)
            {
                toolsUI[i].color = selectColor;
            }
            else
            {
                toolsUI[i].color = alphaColor;
            }
        }
    }
}
