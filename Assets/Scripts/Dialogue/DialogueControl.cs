using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }
    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj;//janela de dialogo
    public Image profileSprite;//sprite do perfil
    public Text speechText;//texto da fala
    public Text actorNameText;//nome do npc

    [Header("Settings")]
    public float typingSpeed;//velocidade da fala

    //Variaveis de Controle
    [SerializeField] private bool isShowing;//se a janela esta visivel
    private int index; //saber a qtd de fala(count)
    private string[] sentences;
    private string[] currentActorName;
    private Sprite[] actorSprite;
    private Player player;

    public static DialogueControl instance;//singleton : Um Singleton é um padrão que garante a existência de apenas uma única cópia do script no jogo inteiro, criando um atalho global para qualquer codigo acessa-lo

    public bool IsShowing { get => isShowing; set => isShowing = value; }

    //awake é chamado antes de tds os starts
    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        
    }

 
    void Update()
    {
        
    }
    IEnumerator TypeSentence()//Corrotina(Uma corrotina é uma função que pode ser pausada e retomada.)
    {
        foreach (char letter in sentences[index].ToCharArray())//lendo a frase do array letra por letra
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);//É o "cronômetro" que cria o atraso entre uma letra e outra para dar o efeito de máquina de escrever.
        }
    }
    //pular pra proxima frase
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                index++;
                profileSprite.sprite = actorSprite[index];
                actorNameText.text = currentActorName[index];
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else//qnd termina o texto
            {
                speechText.text = "";
                actorNameText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
                player.IsPaused = false;

            }
        }
    }
    //chamar a fala
    public void Speech(string[] txt, string[] actorName, Sprite[] actorProfile)
    {
        if (!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            currentActorName = actorName;
            actorSprite = actorProfile;
            profileSprite.sprite = actorSprite[index];
            actorNameText.text = currentActorName[index];
            StartCoroutine(TypeSentence());
            isShowing = true;
            player.IsPaused = true;


        }

    }

}
