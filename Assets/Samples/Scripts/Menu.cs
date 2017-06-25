using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
	public Canvas menuButtons;
	public Canvas returnButton;

	string currentSceneName;

    public void OpenScene(string sceneName)
    {
		menuButtons.gameObject.SetActive(false);
		currentSceneName = sceneName;
		SceneManager.LoadScene("Samples/Scenes/" + sceneName, LoadSceneMode.Additive);
		StartCoroutine(WaitAndActive(returnButton.gameObject));
    }

	public void ReturnToMenu(){
		returnButton.gameObject.SetActive(false);
		SceneManager.UnloadSceneAsync(currentSceneName);
		StartCoroutine(WaitAndActive(menuButtons.gameObject));
	}

	/// <summary>誤タップ防止で少し待つ</summary>
	IEnumerator WaitAndActive(GameObject go){
		yield return new WaitForSeconds(1);
		go.SetActive(true);
	}
}
