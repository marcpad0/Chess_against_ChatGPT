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
    public partial class Form1 : Form
    {
        public Form1(bool againtsRobot = false)
        {
            InitializeComponent();
            if(againtsRobot)
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

                if (string.IsNullOrEmpty(apiKey))
                {
                    OpenAiKeyMessageBox openAiKeyMessageBox = new OpenAiKeyMessageBox();
                    openAiKeyMessageBox.ShowDialog();
                }

                readText = File.ReadAllText(filePath);
                settingsFromFile = JObject.Parse(readText);

                apiKey = (string)settingsFromFile["APIKey"];
                isChecked = (bool)settingsFromFile["Checked"];
                attempt = (int)settingsFromFile["Attempt"];

                if (isChecked)
                {
                    Chess chess = new Chess(this, apiKey, attempt, againtsRobot);
                    chess.init();
                    this.FormBorderStyle = FormBorderStyle.FixedSingle;
                    this.MaximizeBox = false;
                    this.MinimizeBox = false;
                }
                else
                {
                    Task.Delay(1000).Wait();
                    string filePath_temp = Path.Combine(screenshotsDirectory, "Temp.json");
                    string readText_temp = File.ReadAllText(filePath_temp);

                    JObject settingsFromFile_temp = JObject.Parse(readText_temp);
                    string apiKey_temp = (string)settingsFromFile_temp["APIKey"];

                    Chess chess = new Chess(this, apiKey_temp, attempt, againtsRobot);
                    chess.init();
                    this.FormBorderStyle = FormBorderStyle.FixedSingle;
                    this.MaximizeBox = false;
                    this.MinimizeBox = false;

                    File.Delete(filePath_temp);
                }
            }
            else
            {
                Chess chess = new Chess(this);
                chess.init();
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
            }
        }
    }
}
