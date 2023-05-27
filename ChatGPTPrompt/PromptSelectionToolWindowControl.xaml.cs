using ChatGPTPrompt.Templates;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ChatGPTPrompt
{
    /// <summary>
    /// Interaction logic for PromptSelectionToolWindowControl.
    /// </summary>
    public partial class PromptSelectionToolWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PromptSelectionToolWindowControl"/> class.
        /// </summary>
        public PromptSelectionToolWindowControl()
        {
            this.InitializeComponent();
            this.DataContext = this; // If the properties are in code-behind
            Templates.Add(null);
            Templates.Add(new UnitTestTemplate());
        }

        public ObservableCollection<ITemplate> Templates { get; } = new ObservableCollection<ITemplate>();
        public ITemplate SelectedTemplate { get; set; }

        private async void sendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsEnabled = false;
                var response = await new OpenAIQuery().GetOpenAIResponseAsync(txtPrompt.Text);
                resultTextBox.Text = response;
            }
            finally
            {
                IsEnabled = true;
            }
        }

        private void templateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedTemplate == null)
            {
                txtPrompt.IsEnabled = true;
                txtPrompt.Text = string.Empty;
            }
            else
            {
                txtPrompt.IsEnabled = false;
                txtPrompt.Text = SelectedTemplate.Prompt;
            }
        }
    }
}