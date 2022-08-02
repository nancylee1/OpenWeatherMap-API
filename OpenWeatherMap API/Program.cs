using Newtonsoft.Json.Linq;

//URL to openweathermap.org
//Gitignore to hide my API

var client = new HttpClient();

// grabs API keys from appsettings.json file
string apiJson = File.ReadAllText("appsettings.json");

// grabs key from DefaultKey via appsettings
string defaultKey = JObject.Parse(apiJson).GetValue("DefaultKey").ToString();

Console.WriteLine("Please provide a 5-digit zip code: ");
int userInput = int.Parse(Console.ReadLine());

// grabs response of URL from API and returns a JSON string
string weatherAppURL = $"https://api.openweathermap.org/data/2.5/weather?zip={userInput},US&appid={defaultKey}";
string weatherJSONResponse = client.GetStringAsync(weatherAppURL).Result;

// parsing JObject

var weatherJSON = JObject.Parse(weatherJSONResponse);   // Returns to me an object of the results containing city, temp, humidity, etc.
string city = weatherJSON.GetValue("name").ToString();
double tempKelvin = double.Parse(weatherJSON["main"]["temp"].ToString());
double tempF = (1.8 * (tempKelvin - 273) + 32);  // converting Kelvin to Fahrenheit
string description = weatherJSON["weather"].First["description"].ToString();
string humidity = weatherJSON["main"]["humidity"].ToString();
double feelsLike = double.Parse(weatherJSON["main"]["feels_like"].ToString());
double feelsLikeF = (1.8 * (feelsLike - 273) + 32); // converting Kelvin to Fahrenheit

// output of my weather app! 
Console.WriteLine($"The current weather in the city of {city} is {Math.Round(tempF,2)}°F with {description}.");
Console.WriteLine($"The humidity is {humidity} and it actually feels like {Math.Round(feelsLikeF,2)}°F outside.");
