using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    public Renderer rend;
    private GameObject _endOfTheDayMenu;
    private SceneTransitionManager SceneTransitionEq = new SceneTransitionManager();


    // Start is called before the first frame update

    void Start()
    {
        
        Debug.Log($"before fade start: {gameObject.active}");
        rend = GetComponent<Renderer>();

        if (fadeOnStart)
        {
            /*if ((GameObject.Find("EndOfTheDayMenu") != null && (SceneTransitionEq.ab!=5)))
            {
                _endOfTheDayMenu = GameObject.Find("EndOfTheDayMenu");
                _endOfTheDayMenu.SetActive(false);
                Debug.Log("off");
            }*/

            //if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
            //{
            //    _endOfTheDayMenu.SetActive(false);
            //}
            //_endOfTheDayMenu.SetActive(false);

            FadeIn();
            Debug.Log($"during fade start: {gameObject.active}");



        }
        Debug.Log($"after fade start: {gameObject.active}");

        Debug.Log($"start: {gameObject.active}");

        //if (Time.timeSinceLevelLoad > 4.0)
        //{

        //}
    }

    public void EndOfTheDayOn()
    {
        //_endOfTheDayMenu.SetActive(true);
        Debug.Log("11111");
    }

    public void FadeIn()
    {
        GameObject.Find("XR Origin").transform.position = Vector3.zero;
        //if(SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
        //{
        //    
        //}

        Fade(1, 0);
        Debug.Log($"fade in: {gameObject.active}");

    }

    public void FadeOut()
    {
        //if(SceneManager.GetActiveScene().buildIndex == 1)
        //{
        //    _endOfTheDayMenu.SetActive(false);
        //}
            
        Fade(0, 1);
        Debug.Log($"fade out: {gameObject.active}");
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
        Debug.Log($"fade: {gameObject.active}");

        //GameObject.Find("Fader Screen").SetActive(false); - выключен всегда
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        Debug.Log($"fade before routine: {gameObject.active}");
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;

        }
        Debug.Log($"fade after routine: {gameObject.active}");

        

        if (timer > 2)
        {
            /*if (SceneTransitionEq.ab==5)
            {
                Debug.Log("before on");
                EndOfTheDayOn();
                Debug.Log("after on");
            }
            else Debug.Log("notequal");*/

            GameObject.Find("Fader Screen").SetActive(false);
            Debug.Log($"timer: {gameObject.active}");
        }
        if (!gameObject.active)
        {
            fadeDuration = 1;
        }



        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);



    }
}
