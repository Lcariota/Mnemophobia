using UnityEngine;

public abstract class Interaction : ScriptableObject
{
    public abstract void Interact(GameObject interactor, GameObject interactable);
}