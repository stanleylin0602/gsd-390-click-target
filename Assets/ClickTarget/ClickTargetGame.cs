using UnityEngine;

public class ClickTargetGame : MonoBehaviour
{
    int clicks = 0;
    float timeLeft = 15f;
    Rect target;

    void Update()
    {
        target = new Rect(Screen.width / 2f - 80, Screen.height / 2f - 50, 160, 160);
        if (Input.GetKeyDown(KeyCode.R)) Restart();
        if (clicks >= 10 || timeLeft <= 0) return;

        // The first click starts the timer.
        if (clicks > 0) timeLeft = Mathf.Max(0, timeLeft - Time.deltaTime);
        if (timeLeft <= 0) return;

        // Mouse coordinates start at the bottom; GUI coordinates start at the top.
        Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        if (Input.GetMouseButtonDown(0) && target.Contains(mouse)) clicks++;
    }

    void Restart()
    {
        clicks = 0;
        timeLeft = 15f;
    }

    void OnGUI()
    {
        GUIStyle text = new GUIStyle(GUI.skin.label);
        text.fontSize = 24;
        text.alignment = TextAnchor.MiddleCenter;

        GUI.Label(new Rect(0, 25, Screen.width, 50), "CLICK THE TARGET", text);
        GUI.Label(new Rect(0, 80, Screen.width, 35), "Click the square 10 times in 15 seconds.", text);
        GUI.Label(new Rect(0, 120, Screen.width, 35),
            "Clicks: " + clicks + " / 10     Time: " + timeLeft.ToString("0.0") + "s", text);

        string message = "Your first click starts the timer.";
        GUI.color = new Color(0.25f, 0.8f, 1f);
        if (clicks >= 10)
        {
            message = "You Win!";
            GUI.color = Color.green;
        }
        else if (timeLeft <= 0)
        {
            message = "You Lose!";
            GUI.color = Color.gray;
        }
        else if (clicks > 0) message = "Keep clicking!";

        GUI.DrawTexture(target, Texture2D.whiteTexture);
        GUI.color = Color.black;
        GUI.Label(target, "CLICK ME", text);
        GUI.color = Color.white;
        GUI.Label(new Rect(0, target.yMax + 20, Screen.width, 45), message, text);
        if (GUI.Button(new Rect(Screen.width / 2f - 90, target.yMax + 80, 180, 40), "Restart (R)"))
            Restart();
    }
}
