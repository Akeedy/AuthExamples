using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthExamples2.Context
{
    //IdentityDBContext all user tables
    public class MyDbContext:IdentityDbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
        {


        }
    }
}
