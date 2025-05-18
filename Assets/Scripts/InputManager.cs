using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public Button interactButton;
    public bool isInteractPressed;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (interactButton != null)
            interactButton.onClick.AddListener(() => isInteractPressed = true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteractPressed = true;
        }
    }

    public bool ConsumeInteract()
    {
        if (isInteractPressed)
        {
            isInteractPressed = false;
            return true;
        }
        return false;
    }
}
