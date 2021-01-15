using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    public static ClockManager cm;

    public Button addButton;
    public GameObject timeDisplay, stopwatch, countdown;
    public GameObject newClockPanel;
    public Canvas canvas; //this determines the scaling of the UI

    private List<Clock> clocks = new List<Clock>();

    private GridLayoutGroup layout;
    private Vector2 spacing;
    private Vector2 clockDimensions;
    private Vector2 screenDimensions;
    private int numClocksX = 3;


    private void Awake()
    {
        if (cm == null)
        {
            cm = this;
        }
        else if (cm != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        layout = transform.GetComponent<GridLayoutGroup>();
        spacing = layout.spacing;

        CalculateGridSize();
        CloseNewClockPanel();
    }

    private void Update()
    {
        if(clocks.Count == 1)
        {
            Debug.Log("1");
        }
    }

    // ----- Buttons -----

    public void OpenNewClockPanel()
    {
        newClockPanel.SetActive(true);
    }

    public void CloseNewClockPanel()
    {
        newClockPanel.SetActive(false);
    }

    public void AddDisplay(Clock display)
    {
        Clock newClock = Instantiate(display);
        newClock.transform.SetParent(transform, false);

        clocks.Add(newClock);

        AddButtonToEnd();
        CloseNewClockPanel();
    }


    // ----- Formatting -----

    //Add button will always appear after the last clock
    private void AddButtonToEnd()
    {
        addButton.transform.SetAsLastSibling();
    }

    private void CalculateGridSize()
    {
        screenDimensions.x = canvas.GetComponent<RectTransform>().rect.width;
        screenDimensions.y = canvas.GetComponent<RectTransform>().rect.height;

        float ratio = screenDimensions.x / screenDimensions.y;

        if (ratio >= 0)
        { //horizontal
            numClocksX = 3;
        }
        else
        { //vertical
            numClocksX = 1;
        }

        layout.constraintCount = numClocksX;

        //what is the width of the clock display
        float clockSpace = screenDimensions.x - spacing.x * (numClocksX + 2); //account for spacing/padding
        clockDimensions.x = clockSpace / numClocksX;
        clockDimensions.y = clockDimensions.x / 2;

        layout.cellSize = clockDimensions;
    }

    // ----- Remove Clocks -----
    public void RemoveClock(Clock clock)
    {
        clocks.Remove(clock);
        if(clocks.Count == 1)
        {
            clocks[0].DeactiveRemoveButton();
        }
    }
}
