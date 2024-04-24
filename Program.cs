// See https://aka.ms/new-console-template for more information
using PuppeteerSharp;


namespace PuppeteerSharp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Set my Google search terms
            var googleSearch = "James Spencer Locknet";
            await new BrowserFetcher().DownloadAsync();
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false
            });
            // Attempting to search for James Spencer Locknet on Google
            var page = await browser.NewPageAsync();
            await page.GoToAsync("https://www.google.com");
            await page.TypeAsync("textarea.gLFyf", googleSearch);
            await page.Keyboard.PressAsync("Enter");
            Console.WriteLine();

            // Take it a step further and click on the result! This should lead to my LinkedIn profile
            string selector = "h3.LC20lb.MBeuO.DKV0Md";
            await page.WaitForSelectorAsync(selector);
            var elements = await page.QuerySelectorAllAsync(selector);
            if (elements.Length > 0)
            {
                //Iterating through the search results to read the text and print it to console
                List<string> searchResults = new List<string>();
                foreach (var element in elements)
                {
                    var resultText = await element.EvaluateFunctionAsync<string>("node => node.textContent");
                    searchResults.Add(resultText);
                }

                foreach (var result in searchResults)
                {
                    Console.WriteLine(result);
                    
                }

                //Clicking on linkedIn profile
                await elements[0].ClickAsync();
                await Task.Delay(2000);
            }

        }

        }
    }

