using UnityEngine;
using UnityEditor;

namespace Valhalla
{
	public static class ExtentionMethods
	{
		/// <summary>
		/// 移除組件.
		/// </summary>
		/// <typeparam name="Component"></typeparam>
		/// <param name="obj"></param>
		public static void RemoveComponent<Component>(this GameObject obj)
		{
			Component component = obj.GetComponent<Component>();
			if (component != null)
			{
				Object.Destroy(component as Object);
			}
		}

		/// <summary>
		/// 關閉Unity內建MonoBehaviour型別. (ex.Animator, CharacterController, ...)
		/// </summary>
		/// <typeparam name="T">Unity內建泛型型別.</typeparam>
		/// <param name="enabled">開啟or關閉.</param>
		public static void EnabledUnityBehaviour<T>(this ICharacter character, bool enabled) where T : Behaviour
		{
			T component = character.GetGameObject().GetComponent<T>();

			if (component)
			{
				component.enabled = enabled;
				return;
			}

			Debug.Log("[ SetUnityBehaviourEnabled ] Can't Find Component '" + component.name + "'.");
		}

		/// <summary>
		/// Animator.SetFloat連線版.
		/// </summary>
		/// <param name="anim"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetFloat_RPC(this Animator anim, string name, float value)
		{
			anim.SetFloat(name, value);
		}

		/// <summary>
		/// Animator.SetBool連線版.
		/// </summary>
		/// <param name="anim"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public static void SetBool_RPC(this Animator anim, string name, bool value)
		{
			anim.SetBool(name, value);
		}
		
		/// <summary>
		/// Animator.SetTrigger連線版.
		/// </summary>
		/// <param name="anim"></param>
		/// <param name="name"></param>
		public static void SetTrigger_RPC(this Animator anim, string name)
		{
			anim.SetTrigger(name);
		}
	}
}