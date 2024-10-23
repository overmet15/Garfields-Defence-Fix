using UnityEngine.SceneManagement;

public class LoadingManager
{
	private string sceneId;

	public void Start()
	{
		SceneManager.LoadScene("FD_TitleScene");
	}

	public void setSceneId(string scene)
	{
		sceneId = scene;
	}

	public string GetSceneId()
	{
		return sceneId;
	}
}
