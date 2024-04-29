using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            if(!File.Exists("Key/Settings.json"))
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string screenshotsDirectory = Path.Combine(baseDirectory, "Key");
                if (!Directory.Exists(screenshotsDirectory))
                {
                    Directory.CreateDirectory(screenshotsDirectory);
                }
                string filePath = Path.Combine(screenshotsDirectory, "Settings.json");
                JObject settings = new JObject
                {
                    ["APIKey"] = $"",
                    ["Checked"] = $"false",
                    ["Attempt"] = "6",
                };
                File.WriteAllText(filePath, settings.ToString());
                string readText = File.ReadAllText(filePath);
                Console.WriteLine(readText);
            }
            else
            {
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
                int attempt = (int)settingsFromFile["Attempt"];

                Console.WriteLine("Checked: " + isChecked);

                if(isChecked)
                {
                    cbxSave.Checked = true;
                    tbxAttempt.Text = attempt.ToString();
                }
                else
                {
                    cbxSave.Checked = false;
                }
            }
        }

        private void cbxSave_CheckedChanged(object sender, EventArgs e)
        {
            if(cbxSave.Checked)
            {
                cbxSave.Text = "Save";
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string screenshotsDirectory = Path.Combine(baseDirectory, "Key");
                if (!Directory.Exists(screenshotsDirectory))
                {
                    Directory.CreateDirectory(screenshotsDirectory);
                }
                string filePath = Path.Combine(screenshotsDirectory, "Settings.json");

                if(!File.Exists(filePath))
                {
                    JObject settingsTemp = new JObject
                    {
                        ["APIKey"] = $"",
                        ["Checked"] = $"{cbxSave.Checked}",
                        ["Attempt"] = "6",
                    };
                    File.WriteAllText(filePath, settingsTemp.ToString());
                }

                string readText = File.ReadAllText(filePath);
                JObject settingsFromFile = JObject.Parse(readText);

                string apiKey = (string)settingsFromFile["APIKey"];
                bool isChecked = (bool)settingsFromFile["Checked"];
                int attempt = (int)settingsFromFile["Attempt"];

                Console.WriteLine("API Key: " + apiKey);
                Console.WriteLine("Checked: " + isChecked);
                Console.WriteLine("Attempt: " + attempt);

                JObject settings = new JObject
                {
                    ["APIKey"] = apiKey,
                    ["Checked"] = $"{cbxSave.Checked}",
                    ["Attempt"] = $"{attempt}"
                };
                File.WriteAllText(filePath, settings.ToString());
            }
            else
            {
                cbxSave.Text = "Don't Save";
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string screenshotsDirectory = Path.Combine(baseDirectory, "Key");
                if (!Directory.Exists(screenshotsDirectory))
                {
                    Directory.CreateDirectory(screenshotsDirectory);
                }
                string filePath = Path.Combine(screenshotsDirectory, "Settings.json");

                string readText = File.ReadAllText(filePath);
                JObject settingsFromFile = JObject.Parse(readText);

                int attempt = (int)settingsFromFile["Attempt"];

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine("File deleted successfully.");
                }

                JObject settings = new JObject
                {
                    ["APIKey"] = "",
                    ["Checked"] = $"{cbxSave.Checked}",
                    ["Attempt"] = $"{attempt}"
                };

                File.WriteAllText(filePath, settings.ToString());
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbxAttempt.Text))
            {
                MessageBox.Show("Please enter a value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(int.Parse(tbxAttempt.Text) < 1)
            {
                MessageBox.Show("Please enter a number greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string screenshotsDirectory = Path.Combine(baseDirectory, "Key");
                if (!Directory.Exists(screenshotsDirectory))
                {
                    Directory.CreateDirectory(screenshotsDirectory);
                }
                string filePath = Path.Combine(screenshotsDirectory, "Settings.json");

                string readText = File.ReadAllText(filePath);
                JObject settingsFromFile = JObject.Parse(readText);

                string apiKey = (string)settingsFromFile["APIKey"];
                bool isChecked = (bool)settingsFromFile["Checked"];
                int attempt = (int)settingsFromFile["Attempt"];

                Console.WriteLine("API Key: " + apiKey);
                Console.WriteLine("Checked: " + isChecked);
                Console.WriteLine("Attempt: " + attempt);

                JObject settings = new JObject
                {
                    ["APIKey"] = $"{apiKey}",
                    ["Checked"] = $"{isChecked}",
                    ["Attempt"] = string.IsNullOrEmpty(tbxAttempt.Text) ? "6" : (int.TryParse(tbxAttempt.Text, out int result) ? result.ToString() : "0")
                };
                File.WriteAllText(filePath, settings.ToString());
                MessageBox.Show("Value changed with success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Error changing value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);               
            }
        }
    }
}
