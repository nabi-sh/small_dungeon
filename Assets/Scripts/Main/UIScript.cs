using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public List<GameObject> fullHearts = new List<GameObject>();
    
    public List<GameObject> emptyHearts = new List<GameObject>();
    private PlayerControl playerControl;


    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        foreach (Transform child in transform.Find("Hearts").transform)
        {   
            if(child.CompareTag("FullHeart"))
            {
                fullHearts.Add(child.gameObject);
            }
            else if(child.CompareTag("EmptyHeart"))
            {
                emptyHearts.Add(child.gameObject);
            }
        }
    }
    void Update()
    {
        if(playerControl != null && playerControl.animator.GetBool("IsHurt"))
        {
            for (int heartNum = fullHearts.Count - 1; heartNum > playerControl.Health - 1; heartNum--)
            {
                fullHearts[heartNum].SetActive(false);
                emptyHearts[heartNum].SetActive(true);
            }
        }
    }
}
