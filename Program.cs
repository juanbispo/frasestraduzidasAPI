using ConsumindoAPIs;
using ConsumindoAPIs.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Mime;

HttpClient client = new HttpClient();

client.BaseAddress = new Uri("https://api.adviceslip.com/");

var response = await client.GetAsync("advice");

var content = await response.Content.ReadAsStringAsync();

var fraseAleatoria = JsonConvert.DeserializeObject<Root>(content);

Console.WriteLine(fraseAleatoria.slip.advice);

Console.WriteLine("E a tradução da Frase é.... aguarde....");


var fraseATraduzir = new Tradutor()
{
    q = fraseAleatoria.slip.advice,
    source = "en",
    target = "pt"
};

HttpClient cliente = new HttpClient();


var request = new HttpRequestMessage();

request.RequestUri = new Uri("https://deep-translate1.p.rapidapi.com/language/translate/v2");

request.Method = HttpMethod.Post;

request.Headers.Add(
    "x-rapidapi-key",
    "2430430d0emsh2fc6f0062df48f6p1f3db4jsnc8768818c7ab");

var jsonContent = JsonConvert.SerializeObject(fraseATraduzir);

request.Content = new StringContent(jsonContent);

//Console.WriteLine(jsonContent);


var responseTraducao = await client.SendAsync(request);
response.EnsureSuccessStatusCode();
var body = await responseTraducao.Content.ReadAsStringAsync();
//Console.WriteLine(body);

var jsonTraduzido = JsonConvert.DeserializeObject<RaizAPI>(body);

Console.WriteLine(jsonTraduzido.data.translations.translatedText);


