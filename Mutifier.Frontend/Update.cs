using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mutifier.Frontend
{
    internal static class Update
    {
        internal static readonly string? programVersion = FileVersionInfo.GetVersionInfo(Environment.ProcessPath).FileVersion;

        private class GitHubResponse
        {
            public int id { get; set; } = 0;
            public string tag_name { get; set; } = string.Empty;
            public string update_url { get; set; } = string.Empty;
            public string update_authenticity_token { get; set; } = string.Empty;
            public string delete_url { get; set; } = string.Empty;
            public string delete_authenticity_token { get; set; } = string.Empty;
            public string edit_url { get; set; } = string.Empty;
        }


        public static void CheckForUpdates()
        {
            HttpClient client = new()
            {
                Timeout = TimeSpan.FromSeconds(1)
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Uri link = new("https://github.com/TorniX0/Mutifier/releases/latest");
            GitHubResponse response = new();

            try
            {
                string result = client.GetStringAsync(link).Result;
                response = JsonSerializer.Deserialize<GitHubResponse>(result)!;
            }
            catch
            {
                MessageBox.Show("Something went wrong with the update checking!", "Mutifier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                client.Dispose();
            }

            int verIndex = response.tag_name.IndexOfAny("0123456789".ToCharArray());
            string version = response.tag_name[verIndex..];

            if (programVersion != null && version != programVersion)
            {
                DialogResult res = MessageBox.Show("Found a new version! Would you like to be redirected to the GitHub page?", "Mutifier", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = "https://github.com/TorniX0/Mutifier/releases/latest",
                        UseShellExecute = true
                    });
                }
            }
        }
    }
}
