using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text.Json;
using System.Windows.Forms;

namespace Mutifier.Frontend
{
    internal static class Update
    {
        /// <summary>
        /// GitHub JSON Response class
        /// </summary>
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


        [Conditional("RELEASE")]
        public static void CheckForUpdates()
        {
            HttpClient client = new()
            {
                Timeout = TimeSpan.FromSeconds(2)
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
                // TODO: maybe provide proper traceback to the error
                DialogBox.Show("Something went wrong with the update check! No internet?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                client.Dispose();
            }

            int verIndex = response.tag_name.IndexOfAny("0123456789".ToCharArray());
            string version = response.tag_name[verIndex..];

            if (version != Application.ProductVersion)
            {
                DialogResult res = DialogBox.Show("Found a new version! Would you like to be redirected to the GitHub page?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

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

