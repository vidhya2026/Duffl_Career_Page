using Microsoft.AspNetCore.Mvc;
using Duffl_career.Models;
using Duffl_career.Data;
using Duffl_career.Service;

namespace Duffl_career.Controllers
{
    public class CareerDetailsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly IEmailService _emailService;

        public CareerDetailsController(ApplicationDbContext db,
                                       IWebHostEnvironment env,
                                       IEmailService emailService)
        {
            _db = db;
            _env = env;
            _emailService = emailService;
        }

        // GET: /CareerDetails/Apply
        public IActionResult Apply()
        {
            return View();
        }

        // POST: /CareerDetails/Apply
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(Career_Detail model,
                                               IFormFile CvFile,
                                               int CaptchaAnswer)
        {
            // ONE TIME APPLY — Check Email already applied
            bool alreadyApplied = _db.ContactTable
                .Any(x => x.Email.ToLower() == model.Email.ToLower());

            if (alreadyApplied)
            {
                ModelState.AddModelError("Email",
                    "This email has already been used to apply. " +
                    "You can only apply once.");
                return View("Apply", model);
            }

            // Captcha check
            if (CaptchaAnswer != 5)
            {
                TempData["CaptchaError"] = "Wrong answer. Hint: 2 + 3 = 5";
                return View("Apply", model);
            }

            // Experienced field validation
            if (model.ExperienceLevel == "Experienced")
            {
                if (string.IsNullOrEmpty(model.CurrentlyEmployed))
                    ModelState.AddModelError("CurrentlyEmployed",
                        "Please select currently employed status.");

                if (model.WorkExperience == null)
                    ModelState.AddModelError("WorkExperience",
                        "Work experience is required.");

                if (model.CurrentlyEmployed == "Y")
                {
                    if (string.IsNullOrEmpty(model.CurrentEmployerName))
                        ModelState.AddModelError("CurrentEmployerName",
                            "Current employer name is required.");
                    if (string.IsNullOrEmpty(model.CurrentDrawnSalary))
                        ModelState.AddModelError("CurrentDrawnSalary",
                            "Current drawn salary is required.");
                    if (string.IsNullOrEmpty(model.NoticePeriod))
                        ModelState.AddModelError("NoticePeriod",
                            "Notice period is required.");
                }

                if (model.CurrentlyEmployed == "N")
                {
                    if (string.IsNullOrEmpty(model.LastEmployerName))
                        ModelState.AddModelError("LastEmployerName",
                            "Last employer name is required.");
                    if (string.IsNullOrEmpty(model.LastDrawnSalary))
                        ModelState.AddModelError("LastDrawnSalary",
                            "Last drawn salary is required.");
                }
            }

            // File validation
            if (CvFile != null && CvFile.Length > 0)
            {
                var allowed = new[] { ".pdf", ".doc", ".docx" };
                var ext = Path.GetExtension(CvFile.FileName).ToLower();

                if (!Array.Exists(allowed, e => e == ext))
                    ModelState.AddModelError("CvFile",
                        "Only .pdf, .doc, .docx files are allowed.");

                if (CvFile.Length > 5 * 1024 * 1024)
                    ModelState.AddModelError("CvFile",
                        "File size must be under 5 MB.");
            }

            // Final model check
            if (!ModelState.IsValid)
                return View("Apply", model);

            // Save CV file
            if (CvFile != null && CvFile.Length > 0)
            {
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadPath);
                var fileName = Guid.NewGuid() + "_" + CvFile.FileName;
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await CvFile.CopyToAsync(stream);
                }

                model.CvFilePath = "/uploads/" + fileName;
            }

            model.SubmittedAt = DateTime.Now;

            // Save to database
            _db.ContactTable.Add(model);
            await _db.SaveChangesAsync();

            // Build HTML email body
            string emailBody = $@"
            <div style='font-family:Arial,sans-serif; max-width:520px;
                        margin:auto; border:1px solid #2a2a2a;
                        border-radius:12px; overflow:hidden;'>

                <!-- Header -->
                <div style='background:#f76200; padding:28px; text-align:center;'>
                    <h2 style='color:#fff; margin:0; font-size:24px;'>
                        ✅ Application Received!
                    </h2>
                </div>

                <!-- Body -->
                <div style='background:#1a1a1a; padding:30px;'>
                    <p style='color:#fff; font-size:16px;'>
                        Hello <strong style='color:#f76200;'>{model.Name}</strong>,
                    </p>
                    <p style='color:#aaa; font-size:14px; line-height:1.7;'>
                        Thank you for applying at
                        <strong style='color:#f76200;'>Duffl Digital</strong>!
                        We have received your application and our team will
                        review it and get back to you shortly.
                    </p>

                    <!-- Details Box -->
                    <div style='background:#111; border:1px solid #2a2a2a;
                                border-radius:8px; padding:20px; margin:20px 0;'>
                        <p style='color:#f76200; font-weight:bold;
                                  margin:0 0 12px; font-size:14px;'>
                            YOUR SUBMITTED DETAILS
                        </p>
                        <table style='width:100%; border-collapse:collapse;'>
                            <tr>
                                <td style='color:#aaa; padding:6px 0;
                                           font-size:13px; width:140px;'>
                                    Name
                                </td>
                                <td style='color:#fff; padding:6px 0; font-size:13px;'>
                                    : {model.Name}
                                </td>
                            </tr>
                            <tr>
                                <td style='color:#aaa; padding:6px 0; font-size:13px;'>
                                    Email
                                </td>
                                <td style='color:#fff; padding:6px 0; font-size:13px;'>
                                    : {model.Email}
                                </td>
                            </tr>
                            <tr>
                                <td style='color:#aaa; padding:6px 0; font-size:13px;'>
                                    Mobile
                                </td>
                                <td style='color:#fff; padding:6px 0; font-size:13px;'>
                                    : {model.MobileNumber}
                                </td>
                            </tr>
                            <tr>
                                <td style='color:#aaa; padding:6px 0; font-size:13px;'>
                                    Location
                                </td>
                                <td style='color:#fff; padding:6px 0; font-size:13px;'>
                                    : {model.Location}
                                </td>
                            </tr>
                            <tr>
                                <td style='color:#aaa; padding:6px 0; font-size:13px;'>
                                    Experience Level
                                </td>
                                <td style='color:#fff; padding:6px 0; font-size:13px;'>
                                    : {model.ExperienceLevel}
                                </td>
                            </tr>
                            <tr>
                                <td style='color:#aaa; padding:6px 0; font-size:13px;'>
                                    Expected Salary
                                </td>
                                <td style='color:#fff; padding:6px 0; font-size:13px;'>
                                    : {model.ExpectedSalary}
                                </td>
                            </tr>
                            {(model.ExperienceLevel == "Experienced" ? $@"
                            <tr>
                                <td style='color:#aaa; padding:6px 0; font-size:13px;'>
                                    Work Experience
                                </td>
                                <td style='color:#fff; padding:6px 0; font-size:13px;'>
                                    : {model.WorkExperience} Years
                                </td>
                            </tr>
                            <tr>
                                <td style='color:#aaa; padding:6px 0; font-size:13px;'>
                                    Currently Employed
                                </td>
                                <td style='color:#fff; padding:6px 0; font-size:13px;'>
                                    : {(model.CurrentlyEmployed == "Y" ? "Yes" : "No")}
                                </td>
                            </tr>
                            <tr>
                                <td style='color:#aaa; padding:6px 0; font-size:13px;'>
                                    {(model.CurrentlyEmployed == "Y" ?
                                        "Current Employer" : "Last Employer")}
                                </td>
                                <td style='color:#fff; padding:6px 0; font-size:13px;'>
                                    : {(model.CurrentlyEmployed == "Y" ?
                                        model.CurrentEmployerName :
                                        model.LastEmployerName)}
                                </td>
                            </tr>
                            <tr>
                                <td style='color:#aaa; padding:6px 0; font-size:13px;'>
                                    {(model.CurrentlyEmployed == "Y" ?
                                        "Current Salary" : "Last Salary")}
                                </td>
                                <td style='color:#fff; padding:6px 0; font-size:13px;'>
                                    : {(model.CurrentlyEmployed == "Y" ?
                                        model.CurrentDrawnSalary :
                                        model.LastDrawnSalary)}
                                </td>
                            </tr>
                            {(model.CurrentlyEmployed == "Y" ? $@"
                            <tr>
                                <td style='color:#aaa; padding:6px 0; font-size:13px;'>
                                    Notice Period
                                </td>
                                <td style='color:#fff; padding:6px 0; font-size:13px;'>
                                    : {model.NoticePeriod}
                                </td>
                            </tr>" : "")}" : "")}
                        </table>
                    </div>

                    <p style='color:#aaa; font-size:13px;'>
                        If you did not submit this application,
                        please ignore this email.
                    </p>
                </div>

                <!-- Footer -->
                <div style='background:#111; border-top:1px solid #2a2a2a;
                            padding:16px; text-align:center;'>
                    <p style='color:#555; font-size:12px; margin:0;'>
                        © {DateTime.Now.Year} Duffl Digital. All rights reserved.
                    </p>
                </div>

            </div>";

            // Send email to applicant using MailKit
            await _emailService.SendEmailAsync(
                model.Email,
                "Application Received – Thank You",
                emailBody
            );

            TempData["Success"] = "true";
            TempData["UserName"] = model.Name;
            return RedirectToAction("Apply");
        }
    }
}