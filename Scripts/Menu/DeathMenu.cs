using UnityEngine;
using UnityEngine.SceneManagement;
using CombatCharacter.Player;

namespace Menu
{
public class DeathMenu : MonoBehaviour 
{
    private void Start() 
    {
        FindObjectOfType<Player>().OnDeath += ActivateDeathScreen;
        gameObject.SetActive(false);
    }

    void ActivateDeathScreen()
    {
        gameObject.SetActive(true);
    }

    public void GoToMenu() 
    {
        SceneManager.LoadScene(0);
    }
}
}