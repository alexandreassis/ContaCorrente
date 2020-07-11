using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ContaCorrente.Repositorio.Entities;

namespace ContaCorrente.Repositorio.Model
{
    public partial class CCDbContext : DbContext
    {
        public CCDbContext()
        {
        }

        public CCDbContext(DbContextOptions<CCDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conta> Conta { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<RendimentoDiarioCc> RendimentoDiarioCc { get; set; }
        public virtual DbSet<TaxaCdi> TaxaCdi { get; set; }
        public virtual DbSet<TipoTransacao> TipoTransacao { get; set; }
        public virtual DbSet<Transacao> Transacao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=DESKTOP-KR8LN7B\\SQLEXPRESS;Database=ContaCorrente;User Id=dev;Password=dev;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conta>(entity =>
            {
                entity.HasKey(e => e.IdConta)
                    .HasName("PK__Conta__F35D5E9B944C82E4");

                entity.HasOne(d => d.IdPessoaNavigation)
                    .WithMany(p => p.Conta)
                    .HasForeignKey(d => d.IdPessoa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conta__Id_Pessoa__4589517F");
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(e => e.IdPessoa)
                    .HasName("PK__Pessoa__172EA62E6B05CD5C");

                entity.Property(e => e.Cpf).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);
            });

            modelBuilder.Entity<RendimentoDiarioCc>(entity =>
            {
                entity.HasKey(e => e.IdRendimentoDiarioCc)
                    .HasName("PK__Rendimen__F9181186808C6113");

                entity.HasOne(d => d.IdContaNavigation)
                    .WithMany(p => p.RendimentoDiarioCc)
                    .HasForeignKey(d => d.IdConta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rendiment__Id_Co__5006DFF2");

                entity.HasOne(d => d.IdTaxaCdiNavigation)
                    .WithMany(p => p.RendimentoDiarioCc)
                    .HasForeignKey(d => d.IdTaxaCdi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rendiment__Id_Ta__50FB042B");
            });

            modelBuilder.Entity<TaxaCdi>(entity =>
            {
                entity.HasKey(e => e.IdTaxaCdi)
                    .HasName("PK__TaxaCDI__E06634EC5B6D3402");
            });

            modelBuilder.Entity<TipoTransacao>(entity =>
            {
                entity.HasKey(e => e.IdTipoTransacao)
                    .HasName("PK__TipoTran__191BA3C696D8AFAC");

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.Property(e => e.DescricaoAbreviada).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);
            });

            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.HasKey(e => e.IdTransacao)
                    .HasName("PK__Transaca__38B2A1BADDBF2C51");

                entity.Property(e => e.Historico).IsUnicode(false);

                entity.HasOne(d => d.IdContaNavigation)
                    .WithMany(p => p.Transacao)
                    .HasForeignKey(d => d.IdConta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacao__Id_Co__4A4E069C");

                entity.HasOne(d => d.IdTipoTransacaoNavigation)
                    .WithMany(p => p.Transacao)
                    .HasForeignKey(d => d.IdTipoTransacao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacao__Id_Ti__4B422AD5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
