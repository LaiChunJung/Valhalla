using UnityEngine;
using System.Collections;

namespace Valhalla
{
	public abstract class IMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance = null;

		private static object _lock = new object();

		public static T Instance
		{
			get
			{
				if (applicationIsQuitting)
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

		private static bool applicationIsQuitting = false;

		void OnDestroy()
		{
			applicationIsQuitting = true;
		}
	}

	public abstract class ISingleton<T> where T : class, new()
	{
		private static bool applicationIsQuitting = false;

		private static T _instance = null;

		private static object _lock = new object();

		public static T Instance
		{
			get
			{
				if (applicationIsQuitting)
				{
					return null;
				}

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

		~ISingleton()
		{
			applicationIsQuitting = true;
		}
	}
}

