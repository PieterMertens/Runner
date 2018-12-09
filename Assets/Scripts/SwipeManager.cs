using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public enum Swipe { None, Up, Down, Left, Right, TapMiddle, TapRight, TapLeft };

public class SwipeManager : MonoBehaviour
{
    public float minSwipeLength = 20f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    private int screenWidth = Screen.width;
    //private int screenHeight = Screen.height;

    public static Swipe swipeDirection;

    void Update()
    {
        DetectSwipe();
    }

    public void DetectSwipe()
    {
        if (Input.touches.Length > 0) {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began) {
                firstPressPos = new Vector2(t.position.x, t.position.y);
                Debug.Log("--- First Touch on =" + firstPressPos.ToString());
            }

            if (t.phase == TouchPhase.Ended) {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                Debug.Log("--- Second Touch on =" + secondPressPos.ToString());
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.magnitude < minSwipeLength) {
                    if (firstPressPos.x < screenWidth / 3) {
                        swipeDirection = Swipe.TapLeft;
                        AnalyticsEvent.Custom("Controls", new Dictionary<string, object> { { "Tap", "Left" } });
                    } else if (firstPressPos.x > screenWidth * 2 / 3) {
                        swipeDirection = Swipe.TapRight;
                        AnalyticsEvent.Custom("Controls", new Dictionary<string, object> { { "Tap", "Right" } });
                    } else {
                        swipeDirection = Swipe.TapMiddle;
                        AnalyticsEvent.Custom("Controls", new Dictionary<string, object> { { "Tap", "Middle" } });
                    }
                    AnalyticsEvent.Custom("User Control", new Dictionary<string, object> { { PlayerPrefs.GetString("id"), "Tap" } });
                    AnalyticsEvent.Custom("User Control2", new Dictionary<string, object> { { "Tap", PlayerPrefs.GetString("id") } });
                    return;
                }

                currentSwipe.Normalize();

                // Swipe up
                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                    swipeDirection = Swipe.Up;
                    AnalyticsEvent.Custom("Controls", new Dictionary<string, object> { { "Swipe", "Up" } });
                    AnalyticsEvent.Custom("User Control", new Dictionary<string, object> { { PlayerPrefs.GetString("id"), "Swipe" } });
                    AnalyticsEvent.Custom("User Control2", new Dictionary<string, object> { { "Swipe", PlayerPrefs.GetString("id") } });
                    // Swipe down
                } else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                    swipeDirection = Swipe.Down;
                    AnalyticsEvent.Custom("Controls", new Dictionary<string, object> { { "Swipe", "Down" } });
                    AnalyticsEvent.Custom("User Control", new Dictionary<string, object> { { PlayerPrefs.GetString("id"), "Swipe" } });
                    AnalyticsEvent.Custom("User Control2", new Dictionary<string, object> { { "Swipe", PlayerPrefs.GetString("id") } });
                    // Swipe left
                } else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    swipeDirection = Swipe.Left;
                    AnalyticsEvent.Custom("Controls", new Dictionary<string, object> { { "Swipe", "Left" } });
                    AnalyticsEvent.Custom("User Control", new Dictionary<string, object> { { PlayerPrefs.GetString("id"), "Swipe" } });
                    AnalyticsEvent.Custom("User Control2", new Dictionary<string, object> { { "Swipe", PlayerPrefs.GetString("id") } });
                    // Swipe right
                } else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    swipeDirection = Swipe.Right;
                    AnalyticsEvent.Custom("Controls", new Dictionary<string, object> { { "Swipe", "Right" } });
                    AnalyticsEvent.Custom("User Control", new Dictionary<string, object> { { PlayerPrefs.GetString("id"), "Swipe" } });
                    AnalyticsEvent.Custom("User Control2", new Dictionary<string, object> { { "Swipe", PlayerPrefs.GetString("id") } });
                }
            }
        } else {
            swipeDirection = Swipe.None;
        }
    }
}
