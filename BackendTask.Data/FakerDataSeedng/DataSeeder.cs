using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTask.Data.Models.Identity;
using BackendTask.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Bogus;
using BackendTask.Shared.Enums;

namespace BackendTask.Data.FakerDataSeedng
{
    public class DataSeeder : IDataSeeder
    {
        private readonly BackendTaskDbContext _context;
        private readonly ILogger<DataSeeder> _logger;

        public DataSeeder(BackendTaskDbContext context, ILogger<DataSeeder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            try
            {
                await SeedCompaniesAndDepartments();
                await SeedUsersWithBankCards();
                await SeedTransactions();
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding data");
                throw;
            }
        }

        private async Task SeedCompaniesAndDepartments()
        {
            if (await _context.Companies.AnyAsync())
                return;

            var companies = new Faker<Company>()
                .RuleFor(c => c.Name, f => f.Company.CompanyName())
                .Generate(5);

            foreach (var company in companies)
            {
                company.Departments = new Faker<Department>()
                    .RuleFor(d => d.Name, f => f.Commerce.Department())
                    .RuleFor(d => d.CompanyId, company.Id)
                    .Generate(3);
            }

            await _context.Companies.AddRangeAsync(companies);
            await _context.SaveChangesAsync();
        }

        private async Task SeedUsersWithBankCards()
        {
            if (await _context.Users.CountAsync() > 25)
                return;

            var departments = await _context.Departments.ToListAsync();

            var users = new Faker<UserAccount>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.Suffix())
                .RuleFor(u => u.PhoneNumber, "999999999")
                .RuleFor(u => u.UserName, f => f.Internet.UserName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.DepartmentId, f => f.PickRandom(departments).Id)
                .Generate(20);

            foreach (var user in users)
            {
                user.BankCard = new Faker<BankCard>()
                    .RuleFor(b => b.CardNumber, f => string.Concat(f.Finance.CreditCardNumber().Take(10)))
                    .RuleFor(b => b.ExpirationDate, f => f.Date.Future(3))
                    .RuleFor(b => b.CardType, f => f.PickRandom<CardType>())
                    .RuleFor(b => b.IssuingBank, f => f.Company.CompanyName())
                    .RuleFor(b => b.AccountNumber, f => f.Finance.Account())
                    .RuleFor(b => b.UserId, user.Id)
                    .Generate();
            }

            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }

        private async Task SeedTransactions()
        {
            if (await _context.Transactions.AnyAsync())
                return;

            var bankCards = await _context.BankCards.ToListAsync();

            var transactions = new Faker<Transaction>()
                .RuleFor(t => t.Amount, f => f.Finance.Amount(1, 1000))
                .RuleFor(t => t.Fee, f => f.Finance.Amount(0, 50))
                .RuleFor(t => t.Timestamp, f => f.Date.RecentOffset(30))
                .RuleFor(t => t.BankCardId, f => f.PickRandom(bankCards).Id)
                .Generate(100);

            await _context.Transactions.AddRangeAsync(transactions);
            await _context.SaveChangesAsync();
        }
    }
}
