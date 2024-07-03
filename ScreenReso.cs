using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenReso : MonoBehaviour
{
    private static readonly Vector2 iPhoneResolution = new Vector2(2778, 1284);

    // Bottom text feedback offset for iPhone 13 Pro Max
    private static float bottomTextFeedbackiPhoneOffset = -320;

    // Window Dimension Scale Controls
    public GameObject[] UIComponents;

    private void Start()
    {
        Vector3 scale = GetScale();
        foreach (GameObject go in UIComponents)
        {
            go.transform.localScale = scale;
        }
    }

    public static float GetBottomTextFeedbackOffset()
    {
        return bottomTextFeedbackiPhoneOffset;
    }

    private static Vector3 GetScale()
    {
        // Calculate scale based on iPhone 13 Pro Max resolution
        float widthScale = Screen.width / iPhoneResolution.x;
        float heightScale = Screen.height / iPhoneResolution.y;

        // Use the smaller scale factor to maintain aspect ratio
        float scale = Mathf.Min(widthScale, heightScale);
        return new Vector3(scale, scale, 1);
    }
}
