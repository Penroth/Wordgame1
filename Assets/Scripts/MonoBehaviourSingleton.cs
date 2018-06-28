using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

/// <summary>
///  Singleton-Pattern for MonoBehaviour
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T _instance;

	public static T Instance
	{
		get
		{
			// If the Singleton has not been initialized yet, create an empty GameObject and add the Singleton to it.
			if (!_instance)
			{
				_instance = InstantiateSingleton();
			}
			return _instance;
		}
	}

	public static bool Initialized
	{
		get { return _instance != null; }
	}

	protected virtual void Awake()
	{
		if (!_instance && _instance != this)
		{
			_instance = (T) System.Convert.ChangeType(this, typeof(T));
		}
		else
		{
			Debug.Log(this.GetType() + " Singleton._instance already set, destroy this.");
			Object.Destroy(this.gameObject);
			// _instance = (T)System.Convert.ChangeType(this, typeof(T));
		}
	}

	protected static T InstantiateSingleton()
	{
		GameObject go;

		go = new GameObject();
		go.name = typeof(T) + "";
		go.AddComponent<T>();

		return go.GetComponent<T>();
	}

	// Use this for initialization
	protected virtual void Start()
	{
	}


	// Update is called once per frame
	protected virtual void Update()
	{
	}

	protected virtual void OnEnable()
	{
	}

	protected virtual void OnDisable()
	{
	}
}