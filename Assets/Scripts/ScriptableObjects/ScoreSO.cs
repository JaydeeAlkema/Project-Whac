using Unity.Properties;
using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "ScoreSO", menuName = "ScriptableObjects/New Score SO", order = 0)]
	public class ScoreSO : ScriptableObject
	{
		[DontCreateProperty]
		[SerializeField]
		private int _value;

		[CreateProperty]
		public int Value
		{
			get => _value;
			set => _value = value;
		}
	}
}
