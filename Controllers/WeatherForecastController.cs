using Microsoft.AspNetCore.Mvc;
using core;
using hackattack.core.Services;
using hackattack.core.Support;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Configuration;
using hackattack.core.Models;
using Microsoft.Extensions.Configuration;

namespace HackAttackWebAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private AuthenticationHelper _authHelper;
		public IConfiguration Configuration { get; private set; }
		public WorkflowService WorkflowService { get; private set; }
		public AssistyClient assistyClient;

		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly hackattack.core.Support.ILogger _logger;

		public WeatherForecastController(hackattack.core.Support.ILogger logger)
		{


			_logger = logger;


			var environment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "dev1";
			Configuration = ConfigurationHelper.GetConfiguration(environment);
			_authHelper = new AuthenticationHelper(Configuration);
			var baseurl = "https://assisty-api-dev.netdocuments.com";
			assistyClient = new AssistyClient(baseurl);
			this.WorkflowService = new WorkflowService(assistyClient, Configuration, _logger);






		}

		[HttpGet(Name = "GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}

		//[HttpGet(Name = "GetAIdata")]
		//[Route("api/[controller]/ai")]
		//public IEnumerable<WeatherForecast> GetAIdata()
		//{
		//	return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		//	{
		//		Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
		//		TemperatureC = Random.Shared.Next(-20, 55),
		//		//Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		//		Summary = "I am a summary 1"
		//	})
		//	.ToArray();
		//}

		//[HttpGet]
		//public IEnumerable<WeatherForecast> Get()
		//{

		//	return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		//	{
		//		Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
		//		TemperatureC = Random.Shared.Next(-20, 55),
		//		Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		//	})
		//	.ToArray();
		//}

		//[HttpGet]
		////[Route("{name}")]
		//[Route("something")]
		//public IEnumerable<WeatherForecast> GetByName([FromRoute] string name)
		//{
		//	return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		//	{
		//		Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
		//		TemperatureC = Random.Shared.Next(-20, 55),
		//		Summary = "I am a summary 2"
		//	})
		//	.ToArray();
		//}

		[HttpGet]
		//[Route("{name}")]
		[Route("something")]
		public IEnumerable<WeatherForecast> GetByName()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = "I am a summary 2"
			})
			.ToArray();
		}

		[HttpGet]
		//[Route("{name}")]
		[Route("ai")]
		public async Task<string> GetAI()
		{

			//var tt1 = core.WorkflowService.CreateThreadWithFileAsync();



			//var service = new WorkflowService();

			await WorkflowService.SetupToken();
			var threadId = await WorkflowService.CreateThreadWithFileAsync("4825-2943-2258");
			var response = await WorkflowService.ProcessThreadConversation(threadId, "new message");

			Console.WriteLine(response);

			return "THis is a string 1000";

			//return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			//{
			//	Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			//	TemperatureC = Random.Shared.Next(-20, 55),
			//	Summary = "I am a summary 3"
			//})
			//.ToArray();
		}
	}
}
