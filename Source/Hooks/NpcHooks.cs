#region License Information (MIT)
// MIT License
// 
// Copyright (c) 2021-2024 Analog Feelings
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using CharacterRadar.Attributes;
using CharacterRadar.Loaders;
using HarmonyLib;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace CharacterRadar.Hooks
{
	[HookContainer]
	internal class NpcHooks
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(NPC), "Awake")]
		// ReSharper disable once InconsistentNaming
		public static void AwakePostfix(NPC __instance)
		{
			Character character = __instance.Character;
			Color color;

			if ((int)character > (int)Character.DrReflex)
			{
				if (ConfigLoader.Instance.UseHashColors.Value)
				{
					using (SHA256 sha = SHA256.Create())
					{
						byte[] npcName = Encoding.UTF8.GetBytes(__instance.GetType().Name);
						byte[] hash = sha.ComputeHash(npcName);

						int red = hash[4];
						int green = hash[6];
						int blue = hash[8];

						red = Mathf.Clamp(red, 48, byte.MaxValue);
						green = Mathf.Clamp(green, 48, byte.MaxValue);
						blue = Mathf.Clamp(blue, 48, byte.MaxValue);

						color = new Color(red / 255f, green / 255f, blue / 255f);
					}
				}
				else
				{
					color = ConfigLoader.Instance.CharacterColors[Character.Null];
				}
			}
			else
			{
				color = ConfigLoader.Instance.CharacterColors[character];
			}
			
			Singleton<BaseGameManager>.Instance.Ec.map.AddArrow(__instance.transform, color);
		}
	}
}
