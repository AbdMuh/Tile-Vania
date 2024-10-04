using System.Collections;
using UnityEngine;




public class GravityChange : MonoBehaviour
{
    private bool _changeComplete = true;
    private float gravity;
    
    public void Start()
    {
        gravity = Physics2D.gravity.y;
    }

    IEnumerator ChangeGravityOverTime(float duration)
    {
        float startGravity = Physics2D.gravity.y;
        float targetGravity = -Physics2D.gravity.y; 
        float time = 0;

        while (time < duration)
        {
            Physics2D.gravity = new Vector2(Physics2D.gravity.x, Mathf.Lerp(startGravity, targetGravity, time / duration));
            time += Time.deltaTime;
            yield return null;
        }

        // Set the full gravity vector here as well
        Physics2D.gravity = new Vector2(Physics2D.gravity.x, targetGravity);
        Debug.Log("Gravity Changed");
        _changeComplete = true;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void TriggerGravityChange()
    {
        StartCoroutine(ChangeGravityOverTime(0.2f)); 
    }

    void Update()
    {
        Debug.Log("Gravity is: "+Physics2D.gravity);
        if (Input.GetKeyDown(KeyCode.G) && _changeComplete == true)
        {
            _changeComplete = false;
            TriggerGravityChange();
            FlipSpriteY();
        }
    }

    private void FlipSpriteY()
    {
        transform.localScale = new Vector2(transform.localScale.x, Mathf.Sign(Physics2D.gravity.y)); 
        Debug.Log("Sprite Flipped!!");
    }
}
