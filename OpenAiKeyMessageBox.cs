using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Chess
{
    public partial class OpenAiKeyMessageBox : Form
    {
        public OpenAiKeyMessageBox()
        {
            InitializeComponent();
            this.FormClosing += Message;
        }

        private void airButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bigTextBox1.Text))
            {
                MessageBox.Show("Please enter an API Key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                bool isValidKey = ValidateOpenAIKey(bigTextBox1.Text);

                if (isValidKey)
                {
                    MessageBox.Show("Valid OpenAI API Key", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invalid OpenAI API Key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.FormClosing -= Message;

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string screenshotsDirectory = Path.Combine(baseDirectory, "Key");
                if (!Directory.Exists(screenshotsDirectory))
                {
                    Directory.CreateDirectory(screenshotsDirectory);
                }
                string filePath = Path.Combine(screenshotsDirectory, "Settings.json");

                string readText = File.ReadAllText(filePath);
                JObject settingsFromFile = JObject.Parse(readText);

                bool isChecked = (bool)settingsFromFile["Checked"];

                Console.WriteLine("Checked: " + isChecked);

                if (isChecked)
                {
                    int attempt = (int)settingsFromFile["Attempt"];

                    Console.WriteLine("API Key: " + bigTextBox1.Text);
                    Console.WriteLine("Checked: " + isChecked);
                    Console.WriteLine("Attempt: " + attempt);

                    JObject settings = new JObject
                    {
                        ["APIKey"] = bigTextBox1.Text,
                        ["Checked"] = $"{isChecked}",
                        ["Attempt"] = $"{attempt}"
                    };
                    File.WriteAllText(filePath, settings.ToString());
                }
                else
                {
                    string filePath2 = Path.Combine(screenshotsDirectory, "Temp.json");
                    JObject settings = new JObject
                    {
                        ["APIKey"] = bigTextBox1.Text
                    };
                    File.WriteAllText(filePath2, settings.ToString());
                }
                this.Close();
            }
        }

        private void Message(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                MessageBox.Show("You can't close until you insert a valid key, if you really want to exit click on refuse", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateOpenAIKey(string apiKey)
        {
            try
            {
                string requestUrl = $"https://api.openai.com/v1/engines";

                WebRequest request = WebRequest.Create(requestUrl);
                request.Headers.Add("Authorization", $"Bearer {apiKey}");
                request.Method = "GET";

                using (WebResponse response = request.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void airButton2_Click(object sender, EventArgs e)
        {
            this.FormClosing -= Message;
            Environment.Exit(0);
        }
    }
}
