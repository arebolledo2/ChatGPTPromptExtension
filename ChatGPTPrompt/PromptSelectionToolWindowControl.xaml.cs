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
            Templates.Add(new DefaultTemplate());
            Templates.Add(new UnitTestTemplate());
        }

        public ObservableCollection<ITemplate> Templates { get; } = new ObservableCollection<ITemplate>();
        public ITemplate SelectedTemplate { get; set; }

        private async void sendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IsEnabled = false;

                ////var items = relevantClassesListBox.Items.OfType<string>();

                ////string relevant = string.Empty;
                ////if (items.Any())
                ////{
                ////    // TODO
                ////}

                var prompt = new PromptFactory().Create(SelectedTemplate.Prompt, txtCode.Text, txtRelated.Text);

                var response = await new OpenAIQuery().GetOpenAIResponseAsync(prompt);
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
                txtPrompt.Text = string.Empty;
            }
            else
            {
                txtPrompt.Text = SelectedTemplate.Prompt;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtPrompt.Clear();
            txtRelated.Clear();
            txtCode.Clear();
            resultTextBox.Clear();
        }

        private void txtPrompt_TextChanged(object sender, TextChangedEventArgs e)
        {
            SelectedTemplate.Prompt = txtPrompt.Text;
        }
    }
}