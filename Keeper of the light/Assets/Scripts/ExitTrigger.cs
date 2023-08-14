using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    [SerializeField] private LevelController controller;
    public bool Forest;
    public bool CaveTimeline;
    public bool Cave;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Forest)
            {
                controller.ForestLocation();
            }else if (CaveTimeline)
            {
                controller.CaveTimelineLocation();
            }
            else if (Cave)
            {
                controller.CaveLocation();
            }
            else
            {
                controller.BackToMenu();
            }
        }
    }
}
