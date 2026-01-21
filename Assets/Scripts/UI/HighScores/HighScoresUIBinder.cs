using System;
using System.Linq;
using Core.Score;
using UnityEngine.UIElements;

namespace UI.HighScores
{
	public sealed class HighScoresUIBinder : IDisposable
	{
		private readonly HighScoresViewModel _viewModel;
		private readonly ListView _listView;
		private readonly VisualTreeAsset _rowTemplate;
		private readonly Button _restartButton;

		public event Action RestartClicked;

		public HighScoresUIBinder(
			VisualElement root,
			HighScoresViewModel viewModel,
			VisualTreeAsset rowTemplate)
		{
			_viewModel = viewModel;
			_rowTemplate = rowTemplate;

			_listView = root.Q<ListView>("highScoresList");
			_restartButton = root.Q<Button>("restart");
			_restartButton.clicked += OnRestartClicked;

			ConfigureListView();
		}

		public void Dispose()
		{
			_restartButton.clicked -= OnRestartClicked;
		}

		private void OnRestartClicked()
		{
			RestartClicked?.Invoke();
		}

		private void ConfigureListView()
		{
			_listView.itemsSource = _viewModel.Entries.ToList();
			_listView.selectionType = SelectionType.None;

			_listView.makeItem = CreateItem;
			_listView.bindItem = BindItem;
		}

		private VisualElement CreateItem()
		{
			return _rowTemplate.CloneTree();
		}

		private void BindItem(VisualElement element, int index)
		{
			ScoreEntry entry = _viewModel.Entries[index];

			element.Q<Label>("username").text = entry._username;
			element.Q<Label>("score").text = entry._score.ToString();
		}
	}
}
