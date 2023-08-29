using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    
    public void StartNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShowInstructions()
    {
        panel.SetActive(true);
    }
    
    public void HideInstructions()
    {
        panel.SetActive(false);
    }
}
