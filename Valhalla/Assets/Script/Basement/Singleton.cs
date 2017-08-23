using UnityEngine;
using System.Collections;

namespace Valhalla
{
	public class IMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static bool isApplicationQuitting = false;

		private static T _instance = null;

		private static object _lock = new object();

		public static T Instance
		{
			get
			{
				if(isApplicationQuitting)
				{
					return null;
				}

				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = (T)FindObjectOfType(typeof(T));

						if (FindObjectsOfType(typeof(T)).Length > 1)
						{
							return _instance;
						}

						if (_instance == null)
						{
							GameObject singleton = new GameObject(string.Format("[Singleton]{0}", typeof(T)));
							_instance = singleton.AddComponent<T>();
							//DontDestroyOnLoad(singleton);
						}
					}
				}

				return _instance;
			}
		}

		private void OnDestroy()
		{
			isApplicationQuitting = true;
		}
	}

	public class ISingleton<T> where T : class, new()
	{
		private static T _instance = null;

		private static object _lock = new object();

		public static T Instance
		{
			get
			{
				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = new T();
					}
				}

				return _instance;
			}
		}

		public ISingleton() { }
	}
}

