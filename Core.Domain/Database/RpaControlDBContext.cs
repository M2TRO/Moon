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
        public virtual DbSet<LogsMsgsm> LogsMsgsms { get; set; } = null!;
        public virtual DbSet<MtBank> MtBanks { get; set; } = null!;
        public virtual DbSet<MtTransType> MtTransTypes { get; set; } = null!;
        public virtual DbSet<TblAccount> TblAccounts { get; set; } = null!;
        public virtual DbSet<TblScheduler> TblSchedulers { get; set; } = null!;
        public virtual DbSet<TransBank> TransBanks { get; set; } = null!;
        public virtual DbSet<TransVer> TransVers { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.1.78;Database=RpaControlDB;User ID=sa;Password=Admin1234!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Thai_100_CI_AI");

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("Application");

                entity.Property(e => e.AccRef)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<LogEvent>(entity =>
            {
                entity.ToTable("LogEvent");

                entity.Property(e => e.Addr)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Detail)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Remark)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<LogSlip>(entity =>
            {
                entity.ToTable("LogSlip");

                entity.Property(e => e.AccInput)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amt)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bank)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Datetime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Message)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ref)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LogsMsgsm>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.Amout).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Detail)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Msg)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("msg")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Sender)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sender")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<MtBank>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MT_Bank");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DescEn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DescEN")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.DescTh)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DescTH")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MtTransType>(entity =>
            {
                entity.ToTable("MT_TransType");

                entity.Property(e => e.DescEn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DescEN")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<TblAccount>(entity =>
            {
                entity.HasKey(e => e.AccTel);

                entity.ToTable("TblAccount");

                entity.Property(e => e.AccTel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.AccEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.AccName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.AccPwd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.AccRef)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifyTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblScheduler>(entity =>
            {
                entity.ToTable("TblScheduler");

                entity.Property(e => e.ActionTime).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Hashtag)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ModifyBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.ModifyTime).HasColumnType("datetime");

                entity.Property(e => e.RefNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.TitelName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<TransBank>(entity =>
            {
                entity.HasKey(e => e.PromNo)
                    .HasName("PK_TransBank_1");

                entity.ToTable("TransBank");

                entity.Property(e => e.PromNo)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.AccRef)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Token)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("token")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            });

            modelBuilder.Entity<TransVer>(entity =>
            {
                entity.ToTable("TransVer");

                entity.Property(e => e.AccountRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amout)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State).HasColumnName("state");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.AccRef)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amout).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.CeatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateTimeSlip).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Sender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
