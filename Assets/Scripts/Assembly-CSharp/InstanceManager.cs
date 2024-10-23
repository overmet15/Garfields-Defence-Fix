using Outblaze;
using UnityEngine;

public class InstanceManager : SingletonMonoBehaviour<InstanceManager>
{
	private LanguageManager m_languageManager;

	private LoadingManager m_loadingManager;

	private UserProfileManager m_userProfileManager;

	[SerializeField]
	private FontManager m_fontManager;

	[SerializeField]
	private string hack_lang = "en";

	public LanguageManager LanguageManagerInstance
	{
		get
		{
			return m_languageManager;
		}
	}

	public LoadingManager LoadingManagerInstance
	{
		get
		{
			return m_loadingManager;
		}
	}

	public UserProfileManager UserProfileManagerInstance
	{
		get
		{
			return m_userProfileManager;
		}
	}

	public FontManager FontManagerInstance
	{
		get
		{
			return m_fontManager;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		m_userProfileManager = new UserProfileManager(hack_lang);
		m_languageManager = new LanguageManager();
		m_loadingManager = new LoadingManager();
	}

	private void Update()
	{
		m_userProfileManager.Update();
	}

	protected void Start()
	{
		m_userProfileManager.Start();
		m_loadingManager.Start();
	}
}
