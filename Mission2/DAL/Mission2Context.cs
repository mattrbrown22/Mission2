using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mission2.Models;
using System.Data.Entity;

namespace Mission2.DAL
{//set up the dbcontext
    public class Mission2Context : DbContext
    {
        public Mission2Context() : base ("Mission2Context")
        {

        }

        //ADD get; set; methods here
        public DbSet<MissionQuestions> MissionsQuestion { get; set; }
        public DbSet<Missions> Mission { get; set; }
        public DbSet<MissionUsers> MissionsUser { get; set; }

    }
}