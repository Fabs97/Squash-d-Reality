using TMPro;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinButton : MonoBehaviour
{
	private TextMeshProUGUI buttonText;
	private MatchInfoSnapshot match;

	private void Awake()
	{
		buttonText = GetComponentInChildren<TextMeshProUGUI>();
		GetComponent<Button>().onClick.AddListener(JoinMatch);
	}

	public void Initialize(MatchInfoSnapshot match, Transform panelTransform)
	{
		this.match = match;
		buttonText.text = match.name;
		transform.SetParent(panelTransform);
		transform.localScale = Vector3.one;
		transform.localRotation = Quaternion.identity;
		transform.localPosition = Vector3.zero;
	}

	private void JoinMatch()
	{
		FindObjectOfType<NetworkingManager.NetworkingManager>().JoinMatch(match);
	}
}