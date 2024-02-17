using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Domain.Database
{
    public partial class RpaControlDBContext : DbContext
    {
     

        public RpaControlDBContext(DbContextOptions<RpaControlDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<LogEvent> LogEvents { get; set; } = null!;
        public virtual DbSet<LogSlip> LogSlips { get; set; } = null!;
        public virtual DbSet<TblAccount> TblAccounts { get; set; } = null!;
        public virtual DbSet<TblScheduler> TblSchedulers { get; set; } = null!;
        public virtual DbSet<TransBank> TransBanks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=RpaControlDB;Trusted_Connection=True;User ID=sa;Password=Admin1234!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Thai_CI_AI");

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Application");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LogEvent>(entity =>
            {
                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Detail)
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .IsFixedLength();
            });

            modelBuilder.Entity<LogSlip>(entity =>
            {
                entity.Property(e => e.Amt)
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.Property(e => e.Bank)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Datetime)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Message)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TblAccount>(entity =>
            {
                entity.HasKey(e => new { e.AccName, e.AccEmail });

                entity.ToTable("tblAccount");

                entity.Property(e => e.AccName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccPwd)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AccTel)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifyTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TblScheduler>(entity =>
            {
                entity.ToTable("tblScheduler");

                entity.Property(e => e.ActionTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Hashtag)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RefNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State).HasDefaultValueSql("((1))");

                entity.Property(e => e.TitelName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransBank>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Trans_Bank");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.PId)
                    .HasMaxLength(20)
                    .HasColumnName("pID")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
