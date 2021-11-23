using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FundooModels;

namespace FundooRepository.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) :base(options){}
        public DbSet<SignUpModel> Users { get; set; }
        public DbSet<NotesModel> Notes { get; set; }
    }
}
