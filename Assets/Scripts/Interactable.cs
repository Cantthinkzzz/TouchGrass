public interface IInteractable
{
    public void Interact();
    public Sense getSense();

    public int GetIndex();

    public bool IsInteractable();
    public void SetInteractable(bool value);
}

public enum Sense
{
    SIGHT,
    HEARING,
    SMELL,
    TOUCH,
    TASTE
}