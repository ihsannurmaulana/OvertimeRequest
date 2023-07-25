using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class OvertimeDbContext : DbContext
{

    public OvertimeDbContext(DbContextOptions<OvertimeDbContext> options) : base(options)
    {

    }

    // Tables
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<History> Histories { get; set; }
    public DbSet<Overtime> Overtimes { get; set; }
    public DbSet<Payslip> Payslips { get; set; }
    public DbSet<Role> Roles { get; set; }

    // Other Configuration or Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Contraints Unique
        modelBuilder.Entity<Employee>()
                    .HasIndex(e => new
                    {
                        e.Nik,
                        e.PhoneNumber
                    }).IsUnique();

        modelBuilder.Entity<Employee>()
            .HasKey(e => e.Guid);

        modelBuilder.Entity<Account>()
                    .HasIndex(a => new
                    {
                        a.Email,
                    }).IsUnique();

        modelBuilder.Entity<Overtime>()
                    .HasIndex(o => new
                    {
                        o.OvertimeNumber,
                    }).IsUnique();


        // Create Relation
        // Employee - Employee
        modelBuilder.Entity<Employee>()
                    .HasOne(employee => employee.Manager)
                    .WithMany(employee => employee.Employees)
                    .HasForeignKey(employee => employee.ManagerGuid);

        // Employee - Overtime
        modelBuilder.Entity<Employee>()
                    .HasMany(employee => employee.Overtimes)
                    .WithOne(overtime => overtime.Employee)
                    .HasForeignKey(overtime => overtime.EmployeeGuid);

        // History - Overtime
        modelBuilder.Entity<History>()
                    .HasOne(history => history.Overtime)
                    .WithMany(overtime => overtime.Histories)
                    .HasForeignKey(history => history.OvertimeGuid);

        // Payslip - Employee
        modelBuilder.Entity<Payslip>()
                    .HasOne(payslip => payslip.Employee)
                    .WithOne(employee => employee.Payslip)
                    .HasForeignKey<Payslip>(payslip => payslip.EmployeeGuid)
                    .OnDelete(DeleteBehavior.SetNull);

        // Employee - Account 
        modelBuilder.Entity<Employee>()
                    .HasOne(employee => employee.Account)
                    .WithOne(account => account.Employee)
                    .HasForeignKey<Account>(account => account.Guid);

        // Account - AccountRole 
        modelBuilder.Entity<Account>()
                    .HasMany(account => account.AccountRoles)
                    .WithOne(accountRole => accountRole.Account)
                    .HasForeignKey(accountRole => accountRole.AccountGuid);

        // AccountRole - Role
        modelBuilder.Entity<AccountRole>()
                    .HasOne(accountRole => accountRole.Role)
                    .WithMany(role => role.AccountRoles)
                    .HasForeignKey(accountRole => accountRole.RoleGuid);
    }
}
