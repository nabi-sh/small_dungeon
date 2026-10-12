using UnityEngine;

public class PlayerDeathAnimationScript : MonoBehaviour
{
    public GameObject restartScreen;
    private void ActivateRestartScreen()
    {
        Instantiate(restartScreen);
        Debug.Log("RESTART");
    }
}
