﻿using Microsoft.EntityFrameworkCore;
using quest_entity.Models;

namespace quest_entity
{
    public class QuestContext : DbContext
    {
        public QuestContext(DbContextOptions<QuestContext> options) : base(options)
        {
        }

        public DbSet<Player> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
        }
    }
}