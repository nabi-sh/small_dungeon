using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ReadNameInput(string name)
    {
        MainManager.Instance.playerName = name;
    }
}
