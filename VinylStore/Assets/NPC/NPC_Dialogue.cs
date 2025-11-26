using UnityEngine;

[CreateAssetMenu(fileName = "New_NPC_Dialogue", menuName = "NPC Dialogue")]
public class NPC_Dialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    public AudioClip dialogueSound;
    public float voicePitch = 1.0f;

}
