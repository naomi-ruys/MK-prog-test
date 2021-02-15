using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockManager : MonoBehaviour
{
    public static ClockManager cm;

    [SerializeField] private Button addButton;
    [SerializeField] private Clock timeDisplay, stopwatch, countdown;
    [SerializeField] private GameObject newClockPanel;
    [SerializeField] private SetTime setTimePanel;
    [SerializeField] private Canvas canvas; //this determines the scaling of the UI
    [SerializeField] private RectTransform scrollRT;

    private List<Clock> clocks = new List<Clock>();

    private GridLayoutGroup layout;
    private Vector2 spacing;
    private Vector2 clockDimensions;
    private Vector2 screenDimensions;
    private int numClocksX = 3;
    private int scrollHeight;

    private const int MaxClocksXHorizontalDisp = 3;
    private const int MaxClocksXVerticalDisp = 1;
    private const int MinClocks = 1;


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
        setTimePanel.gameObject.SetActive(false);

        AddDisplay(timeDisplay);

        //Set the height of the scroll section
        if(numClocksX == MaxClocksXVerticalDisp)
        {
            scrollHeight = ((int)clockDimensions.y + (int)spacing.y) * 2;
        }
        else
        {
            scrollHeight = (int)clockDimensions.y + ((int)spacing.y * 2);
        }
        scrollRT.sizeDelta = new Vector2(scrollRT.sizeDelta.x, scrollHeight);
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

        if (clocks.Count == MinClocks)
        {
            clocks[0].DeactiveRemoveButton();
        } else
        {
            clocks[0].ActiveRemoveButton();
        }

        UpdateScrollHeight();

        AddButtonToEnd();
        CloseNewClockPanel();
    }

    public void OpenSetTimePanel(Clock display)
    {
        setTimePanel.StartSetTime(display);
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

        if (ratio >= 1)
        { //horizontal display
            numClocksX = MaxClocksXHorizontalDisp;
        }
        else
        { //vertical display
            numClocksX = MaxClocksXVerticalDisp;
        }

        layout.constraintCount = numClocksX;

        //what is the width of the clock display
        //account for spacing/padding
        float clockSpace = screenDimensions.x - spacing.x * (numClocksX + 2); 
        clockDimensions.x = clockSpace / numClocksX;
        clockDimensions.y = clockDimensions.x / 2;

        layout.cellSize = clockDimensions;
    }

    private void UpdateScrollHeight()
    {
        int clockHeight = (int)clockDimensions.y + (int)spacing.y;
        int numLines;

        if (numClocksX == MaxClocksXHorizontalDisp)
        {
            if (clocks.Count % numClocksX == 0)
            {
                numLines = clocks.Count / numClocksX + 1;
                scrollHeight = numLines * clockHeight;
                scrollHeight += (int)spacing.y * 2;
            }
        }
        else if (numClocksX == MaxClocksXVerticalDisp)
        {
            numLines = clocks.Count + 1;
            scrollHeight = numLines * clockHeight;
            scrollHeight += (int)spacing.y * 2;
        }

        scrollRT.sizeDelta = new Vector2(scrollRT.sizeDelta.x, scrollHeight);
    }

    // ----- Remove Clocks -----
    public void RemoveClock(Clock clock)
    {
        clocks.Remove(clock);
        if(clocks.Count == MinClocks)
        {
            clocks[0].DeactiveRemoveButton();
        }

        UpdateScrollHeight();
    }
}
