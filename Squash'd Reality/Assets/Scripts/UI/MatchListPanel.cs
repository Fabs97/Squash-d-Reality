using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using NetworkingManager;
using UnityEngine.EventSystems;

public class MatchListPanel : MonoBehaviour
{
	[SerializeField] private JoinButton joinButtonPrefab;

	private void Awake() {
		AvailableMatchesList.OnAvailableMatchesChanged += AvailableMatchesList_OnAvailableMatchesChanged;
	}

	private void AvailableMatchesList_OnAvailableMatchesChanged(List<MatchInfoSnapshot> matches) {
        // TODO: this makes the UI lose the pad reference, hence it doesn't allow you to choose with a pad, only with a mouse
		ClearExistingButtons();
		CreateNewJoinGameButtons(matches);
	}

	private void ClearExistingButtons() {
		var buttons = GetComponentsInChildren<JoinButton>();
		foreach (var button in buttons) {
			Destroy(button.gameObject);
		}
	}

	private void CreateNewJoinGameButtons(List<MatchInfoSnapshot> matches) {
		foreach (var match in matches) {
			var button = Instantiate(joinButtonPrefab);
			button.Initialize(match, transform);
			GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(button.gameObject);
		}
	}
	
	private void OnDestroy() {
		AvailableMatchesList.OnAvailableMatchesChanged -= AvailableMatchesList_OnAvailableMatchesChanged;
	}
}