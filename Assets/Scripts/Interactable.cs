public interface IInteractable
{
    public bool Interact();
    public Sense getSense();
}

public enum Sense
{
    SIGHT,
    HEARING,
    SMELL,
    TOUCH,
    TASTE
}