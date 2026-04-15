using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<Evento> Eventos { get; set; } = null!;
        public DbSet<Invitado> Invitados { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<Mesa> Mesas { get; set; } = null!;
        public DbSet<IngresosLog> IngresosLogs { get; set; } = null!;
        public DbSet<Acompañante> Acompañantes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Invitado>()
                .HasOne(i => i.Evento)
                .WithMany(e => e.Invitados)
                .HasForeignKey(i => i.EventoId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Invitado>()
                .HasOne(i => i.Mesa)
                .WithMany(m => m.Invitados)
                .HasForeignKey(i => i.MesaId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Invitado>()
            .Property(i => i.Estado)
            .HasConversion<int>();

            modelBuilder.Entity<Acompañante>()
                .HasOne(m => m.Invitado)
                .WithMany(i => i.Acompañantes)
                .HasForeignKey(m => m.InvitadoId);


            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Invitado)
                .WithOne(i => i.Ticket)
                .HasForeignKey<Ticket>(t => t.InvitadoId);

            modelBuilder.Entity<IngresosLog>()
                .HasOne(il => il.Invitado)
                .WithMany(i => i.IngresosLogs)
                .HasForeignKey(il => il.InvitadoId);

        }
    }
}
