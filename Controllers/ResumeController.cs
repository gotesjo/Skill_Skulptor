using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSkulptor.Models;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace SkillSkulptor.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ILogger<ResumeController> _logger;
        private SsDbContext _dbContext;
        private UserManager<AppUser> userManager;

        public ResumeController(ILogger<ResumeController> logger, SsDbContext db, UserManager<AppUser> userManager)
        {
            _dbContext = db;
            _logger = logger;
            this.userManager = userManager;
        }

        //Visar ett CV, skickas inget id med visas den inloggades CV
        [HttpGet]
        public async Task<IActionResult> Index(string? id)
        {
            string _id = null;
            ResumeViewModel viewModel = new ResumeViewModel();

            if (id == null)
            {
                AppUser loggedInUser = await userManager.GetUserAsync(User);
                _id = loggedInUser.Id;
            }
            else
            {
                _id = id;
            }
            AppUser choosenUser;
            CV userCV;
            List<Project> projects;

            //Lägger till alla projekt man medverkar till ViewModeln
            try
            {
                choosenUser = _dbContext.Users.Find(_id);
                userCV = choosenUser.userCV;
                List<int> projectIdsForUser = _dbContext.ProjectMembers.Where(pm => pm.UserId == choosenUser.Id).Select(pm => pm.ProjectId).ToList();
				projects = _dbContext.Projects.Where(p => projectIdsForUser.Contains(p.ProjectId)).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Index", "Home");
            }


            if (choosenUser != null)
            {
                viewModel.UserCV = userCV;
                viewModel.User = choosenUser;
                viewModel.Projects = projects;
                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        [HttpGet]
        public IActionResult CreateCV() 
        {
            CreateResumeModel model = new CreateResumeModel();

            return View(model);
        }

        //Skapar ett cv för en inloggad användare 
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateCV(CreateResumeModel model)
        {
            AppUser loggedInUser = await userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {

                if (loggedInUser != null)
                {
                    CV _cv = new CV();
                    _cv.fkUser = loggedInUser;
                    _cv.Summary = model.Summary;
                    _cv.PersonalLetter = model.PersonalLetter;
                    _cv.Clicks = 0;

                    //Education
                    if (model.Education != null)
                    {
                        //Använder privat metod för att mappa modellen till ett Education objekt
                        _cv.Educations.Add(MakeEducation(model.Education));
                    }
                    //Experience
                    if (model.Experience != null)
                    {
                        _cv.Experiences.Add(MakeExperience(model.Experience));
                    }
                    //Qualification
                    if (model.Qualification != null)
                    {
                        _cv.Qualifications.Add(MakeQualification(model.Qualification));
                    }
                    
                    _dbContext.CVs.Add(_cv);
                    await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Ingen inloggad användare hittades.");
                }
            }
			// Om valideringsfel finns, visa formuläret igen
			return View(model); 
        }
        
        [HttpGet]
        public IActionResult CreateExperience()
        {
            CreateExperienceModel model = new CreateExperienceModel();
            return View(model);

        }

        [HttpPost]
        public IActionResult CreateExperience(CreateExperienceModel model)
        {
            if (ModelState.IsValid)
            {
                Experience exp = MakeExperience(model);

                try
                {
                    exp.Cv = GetLoggedInUser().userCV.CVID;
                    _dbContext.Experiences.Add(exp);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult CreateEducation()
        {
            CreateEducationModel model = new CreateEducationModel();
            return View(model);

        }

        [HttpPost]
        public IActionResult CreateEducation(CreateEducationModel model)
        {
            if (ModelState.IsValid)
            {
                Education edu = MakeEducation(model);

                try
                {
                    edu.CvId = GetLoggedInUser().userCV.CVID;
                    _dbContext.Educations.Add(edu);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return View(model);
            }

        }

        [HttpGet]
        public IActionResult CreateQualification()
        {

            CreateQualificationModel model = new CreateQualificationModel();
            return View(model);

        }

        [HttpPost]
        public IActionResult CreateQualification(CreateQualificationModel model)
        {
            if (ModelState.IsValid)
            {
                Qualification qu = MakeQualification(model);

                try
                {
                    qu.CvId = GetLoggedInUser().userCV.CVID;
                    _dbContext.Qualifications.Add(qu);
                    _dbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction("Index");
                }

            }
            else
            {
                return View(model);
            }

            
        }

        //Hämtar och fyller modell med befintliga data från summary och personalletter

        [HttpGet]
        public async Task<IActionResult> EditSummaryLetter(int id)
        {
            var cv = await _dbContext.CVs.FirstOrDefaultAsync(c => c.CVID == id);
            if (cv == null)
            {
               
                return RedirectToAction("Index"); 
            }

            var model = new EditSummaryLetterModel
            {
                Summary = cv.Summary,
                PersonalLetter = cv.PersonalLetter
            };

            return View(model);
        }

        //Uppdaterar summary eller personligtbrev från modellen

        [HttpPost]
        public async Task<IActionResult> EditSummaryLetter(EditSummaryLetterModel model)
        {
            if (ModelState.IsValid)
            {
                var cv = await _dbContext.CVs.FirstOrDefaultAsync(c => c.CVID == GetLoggedInUser().userCV.CVID);
                if (cv != null)
                {
                    cv.Summary = model.Summary;
                    cv.PersonalLetter = model.PersonalLetter;

                    _dbContext.Update(cv);
                    var result = await _dbContext.SaveChangesAsync();
                    if (result > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Ingen summary eller personligtbrev hittades för användaren!";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    
        [HttpGet]
        public async Task<IActionResult> EditEducation(int edid)

        {
            //hämtar id:et för education från databasen
            var education =  await _dbContext.Educations.FindAsync(edid);

          if(education == null)
            {
                TempData["ErrorMessage"] = "Ingen erfarenhet hittades för användaren";
                return RedirectToAction("Index");
            }
            //Fyller modellen
            var model = new CreateEducationModel

            {
                Institution = education.Institution,
                Course = education.Course,
                Degree = education.Degree,
                EdStartDate = education.StartDate,
                EdEndDate = education.EndDate,



            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditEducation(CreateEducationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEducation", model);
            }

            Education education;
            if (model.EdID.HasValue && model.EdID > 0)
            {
                education = await _dbContext.Educations.FindAsync(model.EdID.Value);
                if (education == null)
                {
                    TempData["ErrorMessage"] = "Utbildningen hittades inte.";
                    return RedirectToAction("Index");
                }

                //uppdaterar befintlig utbildning från modellen
                education.Institution = model.Institution;
                education.Course = model.Course;
                education.Degree = model.Degree;
                education.StartDate = model.EdStartDate;
                education.EndDate = model.EdEndDate;
            }
          

            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Resume");
        }

        [HttpGet]
        public async Task<IActionResult> EditExperience(int exid)

        {
            var experience = await _dbContext.Experiences.FindAsync(exid);

            if (experience == null)
            {
                TempData["ErrorMessage"] = "Ingen erfarenhet hittades för användaren";
                return RedirectToAction("Index", "Resume");
            }

            //Fyller modellen
            var model = new CreateExperienceModel
            {

                Position = experience.Position,
                ExDescription = experience.Description,
                Employer = experience.Employer,
                ExStartDate = experience.StartDate,
                ExEndDate = experience.EndDate



            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditExperience(CreateExperienceModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateExperience", model);
            }

            Experience experience;
            if (model.ExID.HasValue && model.ExID > 0)
            {
                experience = await _dbContext.Experiences.FindAsync(model.ExID.Value);
                if (experience == null)
                {
                    TempData["ErrorMessage"] = "Erfarenhet hittades inte.";
                    return RedirectToAction("Index");
                }

                //Uppdatera befintlig erfarenhet från modellen
                
                experience.Position = model.Position;
                experience.Employer = model.Employer;
                experience.Description = model.ExDescription;
                experience.StartDate = model.ExStartDate;
                experience.EndDate = model.ExEndDate;
            }


            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Resume");
        }

        [HttpGet]
        public async Task<IActionResult> EditQualification(int qid)

        {
            var qualification = await _dbContext.Qualifications.FindAsync(qid);

            if (qualification == null)
            {
                TempData["ErrorMessage"] = "Ingen färdighet hittades för användaren";
                return RedirectToAction("Index", "Resume");
            }
            //Fyller modellen
            var model = new CreateQualificationModel

            {
                QName = qualification.QName,
                QDescription = qualification.Description
                


            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditQualification(CreateQualificationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateQualification", model);
            }

            Qualification qualification;
            if (model.QID.HasValue && model.QID > 0)
            {
                qualification= await _dbContext.Qualifications.FindAsync(model.QID.Value);
                if (qualification == null)
                {
                    TempData["ErrorMessage"] = "Färdighet hittades inte.";
                    return RedirectToAction("Index", "Resume");
                }
                //uppdaterar befintliga färdigheter från modellen
               
                qualification.QName = model.QName;
                qualification.Description = model.QDescription;
                
            }


            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Resume");
        }

        [HttpPost]
        public async Task<IActionResult>DeleteExperience(int id)
        {
            var experience = await _dbContext.Experiences.FindAsync(id);

            if(experience != null)
            {
                _dbContext.Experiences.Remove(experience);
                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Erfarenheten har tagits bort!";
            }
            else
            {
                TempData["ErrorMessage"] = "Ingen erfarenhet hittades!";
            }
            return RedirectToAction("Index", "Resume");
        }

        [HttpPost]
        public async Task<IActionResult>DeleteEducation(int id)
        {
            var education = await _dbContext.Educations.FindAsync(id);
                if(education!=null)
            {
                _dbContext.Educations.Remove(education);
                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Utbildning har tagits bort!";
            }
                else
            {
                TempData["ErrorMessage"] = "Ingen utbildning hittades!";

            }
            return RedirectToAction("Index", "Resume");
            
        }

        [HttpPost]
        public async Task<IActionResult>DeleteQualification(int id)
        {
            var qualification = await _dbContext.Qualifications.FindAsync(id);
            if(qualification!=null)
            {
                _dbContext.Qualifications.Remove(qualification);
                await _dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = "Färdighet har tagits bort!";
            }
            else
            {
                TempData["ErrorMessage"] = "Ingen färdighet hittades!";
            }
            return RedirectToAction("Index", "Resume");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCV()
        {
            var loggedInUser = await userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                
                return RedirectToAction("Login", "Account"); 
            }

            var userCV = _dbContext.CVs.FirstOrDefault(cv => cv.BelongsTo == loggedInUser.Id);
            if (userCV != null)
            {



                _dbContext.CVs.Remove(userCV);
                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Ditt CV har tagits bort.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Inget CV hittades att ta bort.";
            }
            return RedirectToAction("Index", "Home");
        }

		//Metod för att mappa modellen till en DatabasEntitet
		private Education MakeEducation(CreateEducationModel model)
		{
			Education ed = new Education();
			if (model.EdID.HasValue && model.EdID > 0)
			{
				ed.EdID = model.EdID.Value;
			}

			ed.StartDate = model.EdStartDate;
			ed.EndDate = model.EdEndDate;
			ed.Institution = model.Institution;
			ed.Course = model.Course;
			ed.Degree = model.Degree;

			return ed;
		}
		//Metod för att mappa modellen till en DatabasEntitet
		private Experience MakeExperience(CreateExperienceModel model)
		{
			Experience ex = new Experience();

			ex.StartDate = model.ExStartDate;
			ex.EndDate = model.ExEndDate;
			ex.Position = model.Position;
			ex.Description = model.ExDescription;
			ex.Employer = model.Employer;

			return ex;
		}
		//Metod för att mappa modellen till en DatabasEntitet
		private Qualification MakeQualification(CreateQualificationModel model)
		{
			Qualification qu = new Qualification();
			qu.QName = model.QName;
			qu.Description = model.QDescription;

			return qu;
		}

        //Metod för att hämta inloggad användare
		private AppUser GetLoggedInUser()
		{
			AppUser loggedInUser = userManager.GetUserAsync(User).Result;
			return loggedInUser;

		}
	}
}

