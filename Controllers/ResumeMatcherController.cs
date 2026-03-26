// Controllers/ResumeMatcherController.cs

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Duffl_career.Models;

namespace Duffl_career.Controllers
{
    public class ResumeMatcherController : Controller
    {
        // -------------------------------------------------------
        // Paste your ngrok URL here after running Colab
        // Example: "https://abc123.ngrok.io"
        // -------------------------------------------------------
        private readonly string _flaskApiUrl = "https://cf70-34-53-43-129.ngrok-free.app";

        // GET: /ResumeMatcher/Index
        // Loads the HR upload form
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // POST: /ResumeMatcher/Match
        // Called when HR submits the form
        [HttpPost]
        public async Task<IActionResult> Match(
            string jobDescription,
            List<IFormFile> resumes)
        {
            if (string.IsNullOrWhiteSpace(jobDescription))
            {
                ViewBag.Error = "Please enter the Job Description.";
                return View("Index");
            }

            if (resumes == null || resumes.Count == 0)
            {
                ViewBag.Error = "Please upload at least one resume.";
                return View("Index");
            }

            if (resumes.Count > 20)
            {
                ViewBag.Error = "Maximum 10 resumes allowed at once.";
                return View("Index");
            }

            try
            {
                using var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(120);

                using var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(jobDescription), "jd");

                foreach (var resume in resumes)
                {
                    var fileContent = new StreamContent(resume.OpenReadStream());
                    formData.Add(fileContent, "resumes", resume.FileName);
                }

                var response = await httpClient.PostAsync($"{_flaskApiUrl}/match", formData);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = $"ML service error: {json}";
                    return View("Index");
                }

                // Store result in TempData so Results action can read it
                TempData["MatchResult"] = json;

                return RedirectToAction("Results");
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Cannot reach ML service. Is Colab running? ({ex.Message})";
                return View("Index");
            }
            catch (TaskCanceledException)
            {
                ViewBag.Error = "Request timed out. BERT may still be loading — try again in 30 seconds.";
                return View("Index");
            }
        }

        // GET: /ResumeMatcher/Results
        // Reads from TempData and renders Results view
        [HttpGet]
        public IActionResult Results()
        {
            var json = TempData["MatchResult"] as string;

            if (string.IsNullOrEmpty(json))
            {
                // Direct access without submitting — send back to form
                return RedirectToAction("Index");
            }

            var apiResult = JsonSerializer.Deserialize<MatchApiResponse>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return View(apiResult);
        }
    }
}