using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Lost.Model;

namespace Lost.DataAccess.Context
{
    public class LostDbContext : DbContext
    {
        public LostDbContext(DbContextOptions<LostDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<AchatVente> AchatVente { get; set; }
        public DbSet<Billet> Billet { get; set; }
        public DbSet<CompteClubTransaction> CompteClubTransaction { get; set; }
        public DbSet<Sac> Sac { get; set; }
        public DbSet<Voiture> Voiture { get; set; }
        public DbSet<VoiturePersonne> VoiturePersonne { get; set; }
        public DbSet<Groupe> Groupe { get; set; }
        public DbSet<Personne> Personne { get; set; }
        public DbSet<Semaine> Semaine { get; set; }
        public DbSet<TauxBlanchiment> TauxBlanchiment { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Utilisateur> Utilisateur { get; set; }
        public DbSet<StatistiqueGroupe> StatistiqueGroupe { get; set; }
        public DbSet<StatistiquePersonne> StatistiquePersonne { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StatistiqueGroupe>(v =>
            {
                v.HasNoKey();
                v.ToView(Lost.Model.StatistiqueGroupe.viewName);
            });

            modelBuilder.Entity<StatistiquePersonne>(v =>
            {
                v.HasNoKey();
                v.ToView(Lost.Model.StatistiquePersonne.viewName);
            });
        }
    }
}
