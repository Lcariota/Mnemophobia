using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogueBlock", menuName = "Dialogue/Block")]
public class DialogueBlock : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] lines;

}