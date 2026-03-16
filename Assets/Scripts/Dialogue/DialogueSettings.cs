using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]//create faz a classe aparecer no project(botao direito)
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;
    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence;

    public List<Sentences> dialogues = new List<Sentences>();

}
[System.Serializable]
public class Sentences
{
        public string actorName;
        public Sprite profile;
        public Languages sentence;

}
[System.Serializable]
public class Languages
{
        public string portuguese;
        public string english;
        public string spanish;
        
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
   public class BuilderEditor : Editor
   {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();//sobrescrever um inspector da unity
        DialogueSettings ds = (DialogueSettings)target;

        Languages l = new Languages();
        l.portuguese = ds.sentence;

        Sentences s = new Sentences();
        s.profile = ds.speakerSprite;
        s.sentence = l;

        if(GUILayout.Button("Create Dialogue"))
        {
            if(ds.sentence != "")
            {
                ds.dialogues.Add(s);
                ds.speakerSprite = null;
                ds.sentence = "";
            }

        }

    }
   
   
   
   }
#endif




