using System.Collections;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    [SerializeField] private LevelController controller;
    [SerializeField] private GameObject endPanel = null;
    public bool Instruction;
    public bool Forest;
    public bool CaveTimeline;
    public bool Cave;
    public bool Lobby;
    public bool levelOne;
    public bool levelTwo;
    public bool levelThree;
    public bool End;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.DeleteKey("x");
            PlayerPrefs.DeleteKey("y");
            if (Instruction)
            {
                controller.Instruction();
            }
            else if (Forest)
            {
                controller.ForestLocation();
            }
            else if (CaveTimeline)
            {
                controller.CaveTimelineLocation();
            }
            else if (Cave)
            {
                controller.CaveLocation();
            }
            else if (Lobby)
            {
                controller.LobbyLocation();
            }
            else if (levelOne)
            {
                controller.FirstLocation();
            }
            else if (levelTwo)
            {
                controller.SecondLocation();
            }
            else if (levelThree)
            {
                controller.ThirdLocation();
            }
            else if (End)
            {
                endPanel.SetActive(true);
                StartCoroutine(TheEnd());

            }
            else
            {
                controller.BackToMenu();
            }
        }
    }

    private IEnumerator TheEnd()
    {
        yield return new WaitForSeconds(5);

        controller.BackToMenu();
    }
}
