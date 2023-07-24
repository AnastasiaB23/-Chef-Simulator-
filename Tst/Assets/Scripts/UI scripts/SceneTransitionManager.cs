using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
   /* public bool on = false;
    public int ab =0;*/



    public void GoToSceneAsync(int sceneIndex)
    {
        /*on = (sceneIndex == SceneManager.GetActiveScene().buildIndex);
        Debug.Log($"sceneInd {on}");*/
        //GameObject.Find("Fader Screen").SetActive(true);
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
        Debug.Log($"go to scene: {gameObject.active}");
        /*Debug.Log($"sceneInd123 {on}");
        if (on)
        {
            ab = 5;
            SceneEquality();
        }
        Debug.Log($"sceneInd78 {on}");
        ab = 0;*/
        //if (on)
        //{
        //    ab = 5;
        //    Debug.Log($"sceneInd {ab}");
        //}

    }

    //private void FixedUpdate()
    //{
    //    Debug.Log($"sceneInd123 {on}");
    //    if (on) 
    //    {
    //        ab = 5;
    //        SceneEquality();
    //    }
    //    Debug.Log($"sceneInd78 {on}");
    //    ab = 0;
    //}

    //public void SceneEqual(int sceneIndex)
    //{
    //private void FixedUpdate()
    //{
    //    if (on)
    //    {
    //        fadeScreen.EndOfTheDayOn();
    //    }
    //}

    //}

    /*public int SceneEquality()
    {
        Debug.Log($"sceneInd11 {on}");
        return ab;
    }*/

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        

        fadeScreen.FadeOut();

        

        Debug.Log($"GoToSceneRoutine: {gameObject.active}");

        //Launch the new scene
        

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation= false;

        

        float timer=0;

        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

      

        operation.allowSceneActivation= true;
        Debug.Log($"LoadScene: {gameObject.active}"); 
        
        //if (on)
        //{
        //    Debug.Log("before on");
        //    fadeScreen.EndOfTheDayOn();
        //    Debug.Log("after on");


        //}


    }
}
