using System;
using System.IO;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RTS___Zendesk_Get_Users
{
    class Program
    {
        static async Task Main(string[] args)
        {
			// Console app to read all the users from Zendesk API
			// References: https://developer.zendesk.com/api-reference/
			//             https://developer.zendesk.com/api-reference/ticketing/users/users/
			var nextPage = "https://mydomain.zendesk.com/api/v2/users.json";
			var execount = 0;
			// Write the specified text asynchronously to a new file named "users.csv".
			using (StreamWriter outputFile = File.AppendText("users.csv"))
			{
				Console.WriteLine("name, email, updated_at, verified, active" );
				await outputFile.WriteLineAsync("name, email, updated_at, verified, active");				    
			}
			while (nextPage != null && nextPage != "") {
				
				//Use a programmatic browser (HttpClient) to get the info from Zendesk
				var client = new HttpClient();
				var request = new HttpRequestMessage(
					method: HttpMethod.Get, 
					requestUri: nextPage
				);
				var authenticationString = "username@domain.com/token:TokenGoesHere";
				var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
				request.Headers.Add("Authorization", "Basic " + base64EncodedAuthenticationString);
				HttpResponseMessage response = await client.SendAsync(request);

				// Proces the response
				if (response.IsSuccessStatusCode)
				{
					string responseContent = await response.Content.ReadAsStringAsync();

					dynamic json = JsonSerializer.Deserialize<ExpandoObject>(responseContent);	
					//Display in screen
					Console.WriteLine(responseContent);					
					Console.WriteLine("name: " + json.users[0].GetProperty("name").GetString() + " email: " + json.users[0].GetProperty("email").GetString() + " updated_at: " + json.users[0].GetProperty("updated_at").GetString() + " verified: " + json.users[0].GetProperty("verified").GetBoolean() + " active: " + json.users[0].GetProperty("active").GetBoolean() );
					
					var jsonDom = JsonDocument.Parse(responseContent);
					var users = jsonDom.RootElement.GetProperty("users");
					
					//Write original content
					using (StreamWriter outputFile = File.AppendText("users.json"))
					{
						await outputFile.WriteLineAsync(responseContent);				    
					}
					
					// Write the specified text asynchronously to a new file named "users.csv".
					using (StreamWriter outputFile = File.AppendText("users.csv"))
					{
						foreach(var user in users.EnumerateArray()) 
						{	
							Console.WriteLine("\"" + user.GetProperty("name").GetString() + "\", " + user.GetProperty("email").GetString() + ", " + user.GetProperty("updated_at").GetString() + ", " + user.GetProperty("verified").GetBoolean() + ", " + user.GetProperty("active").GetBoolean() );
							await outputFile.WriteLineAsync("\"" + user.GetProperty("name").GetString() + "\", " + user.GetProperty("email").GetString() + ", " + user.GetProperty("updated_at").GetString() + ", " + user.GetProperty("verified").GetBoolean() + ", " + user.GetProperty("active").GetBoolean() );
						}				    
					}
					
					//Keep retrieving pages if there is more data. Wait 1 second between calls.
					nextPage = jsonDom.RootElement.GetProperty("next_page").ToString();
					
					Console.WriteLine(jsonDom.RootElement.GetProperty("next_page"));
					execount++;
					Console.WriteLine("Execount: " + execount.ToString());
					System.Threading.Thread.Sleep(1000);
				}
			}
			Console.WriteLine("COMPLETED");
        }
    }
}
