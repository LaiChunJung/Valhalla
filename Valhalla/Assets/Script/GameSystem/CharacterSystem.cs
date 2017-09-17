using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Valhalla
{
	public class CharacterSystem : IGameSystem, ISystemUpdatable
	{
		private ICharacter player;
		private Dictionary<int, ICharacter> playerDic;  // 存放所有角色參考的Dictionary.


		public CharacterSystem()
		{
			playerDic = new Dictionary<int, ICharacter>();
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public void SystemUpdate() { }

		public override void Release()
		{
			base.Release();
			playerDic.Clear();
		}

		/// <summary>
		/// 建立玩家角色. (未完成)
		/// </summary>
		/// <param name="fileName">資源檔案名稱.</param>
		/// <param name="position">初始位置.</param>
		/// <param name="rotation">初始角度.</param>
		/// <returns></returns>
		public ICharacter CreateCharacter(ICharacter character)
		{
			playerDic.Add(character.GetGameObject().GetInstanceID(), character);

			return character;
		}

		public void SetMyPlayer(ICharacter character)
		{
			player = character;
		}

		public ICharacter GetMyPlayer()
		{
			return player;
		}
	}
}
