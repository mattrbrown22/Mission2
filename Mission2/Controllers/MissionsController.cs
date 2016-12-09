using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mission2.DAL;
using Mission2.Models;

namespace Mission2.Controllers
{
    [Authorize]
    public class MissionsController : Controller
    {
        private Mission2Context db = new Mission2Context();
        public IEnumerable<MissionQuestions> MissionQuestions;
        // GET: Missions
        public ActionResult Index()
        {
            return View(db.Mission.ToList());
        }

        // GET: Missions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Missions missions = db.Mission.Find(id);
            if (missions == null)
            {
                return HttpNotFound();
            }
            return View(missions);
        }

        // GET: Missions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Missions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MissionID,MissionPresidentName,MissionName,MissionAddress,Language,Climate,Religion,Flag")] Missions missions)
        {
            if (ModelState.IsValid)
            {
                db.Mission.Add(missions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(missions);
        }

        // GET: Missions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Missions missions = db.Mission.Find(id);
            if (missions == null)
            {
                return HttpNotFound();
            }
            return View(missions);
        }

        // POST: Missions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MissionID,MissionPresidentName,MissionName,MissionAddress,Language,Climate,Religion,Flag")] Missions missions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(missions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(missions);
        }

        // GET: Missions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Missions missions = db.Mission.Find(id);
            if (missions == null)
            {
                return HttpNotFound();
            }
            return View(missions);
        }

        // POST: Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Missions missions = db.Mission.Find(id);
            db.Mission.Remove(missions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult FAQ(int? ID)
        {
            ID = 3;
            MissionQuestions = db.Database.SqlQuery<MissionQuestions>("SELECT * FROM MissionQuestions WHERE (MissionID = " + ID + ");");
            return View(MissionQuestions);
        }

        public ActionResult SingleMission(int? missionID)
        {
            List<MissionQuestions> questions = new List<MissionQuestions> { };
            foreach (var item in db.MissionsQuestion)
            {
                if (item.MissionID == missionID)
                {
                    questions.Add(item);
                }
            }
            //all of the information for each mission that will populate when we go
            Missions mission = db.Mission.Find(missionID);
            ViewBag.MissionID = mission.MissionID;
            ViewBag.missionName = mission.MissionName;
            ViewBag.presName = mission.MissionPresidentName;
            ViewBag.misAddress = mission.MissionAddress;
            ViewBag.language = mission.Language;
            ViewBag.climate = mission.Climate;
            ViewBag.domReligion = mission.Religion;
            ViewBag.flag = mission.Flag;
            return View(questions);
        }
        //code for when we will post a question on the website
        [HttpPost]
        public ActionResult postQuestion(FormCollection form)
        {
            
            int tempMissionIDs = Convert.ToInt32(form["hdnMission"]);
            int tempUserID = 1;
            string tempQuestion = form["Question"].ToString();
            string tempAnswer = "No answer has been given.";
            MissionQuestions temp = new MissionQuestions { MissionID = tempMissionIDs, UserID = tempUserID, Question = tempQuestion, Answer = tempAnswer };
            db.MissionsQuestion.Add(temp);
            db.SaveChanges();
            return RedirectToAction("SingleMission", "Missions", new { missionID = tempMissionIDs });
        }
        //code for when we will answer/update a question
        public ActionResult answerQuestion(FormCollection form, int missionQuestionID, int missionID)
        {
           //load the db object based on the id given in parameters (missionQuestionID)
            MissionQuestions tempQuestion = db.MissionsQuestion.Find(missionQuestionID);
            int tempMissionIDs = missionID;
            string tempAnswer = form["Answer"].ToString();
            tempQuestion.Answer = tempAnswer;
            db.Entry(tempQuestion).State = EntityState.Modified;
            db.SaveChanges();  
            return RedirectToAction("SingleMission", "Missions", new { missionID = tempMissionIDs });
        }
    }
}
