using System.Formats.Asn1;
using System.Transactions;
using BackendTask.Data.Models;
using BackendTask.Domain.Contract.TransactionsContract;
using BackendTask.Domain.Models.Transactions;
using BackendTask.Manager;
using CsvHelper;

namespace BackendTask.WinService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;
        private DateTime _lastRunTime;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _lastRunTime = DateTime.MinValue;
            _scopeFactory = scopeFactory;

        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Transaction Report Service started.");

            // Set up the timer to trigger immediately and then every 24 hours
            _timer = new Timer(GenerateReports, null, TimeSpan.Zero, TimeSpan.FromHours(24));

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }

            _logger.LogInformation("Transaction Report Service is stopping.");
        }

        private async void GenerateReports(object state)
        {
            try
            {
                var now = DateTime.Now;

                // Only generate reports if it's a new day since last run
                if (_lastRunTime.Date < now.Date)
                {
                    _logger.LogInformation($"Generating transaction reports for {now.Date:yyyy-MM-dd}");
                    var transactions = await GetDailyTransactions(now.Date);

                    var userGroups = transactions.GroupBy(t => t.BankCardId).ToList();

                    foreach (var group in userGroups)
                    {
                        GenerateUserReport(group.Key, group.ToList(), now.Date);
                    }

                    _lastRunTime = now;
                    _logger.LogInformation($"Successfully generated reports for {now.Date:yyyy-MM-dd}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating transaction reports");
            }
        }

        private void GenerateUserReport(long bankCardId, List<TransactionDomain> transactions, DateTime reportDate)
        {
            var reportDirectory = Path.Combine(AppContext.BaseDirectory, "Reports");

            // Ensure directory exists
            Directory.CreateDirectory(reportDirectory);

            var fileName = $"TransactionReport_{bankCardId}_{reportDate:yyyyMMdd}.csv";
            var filePath = Path.Combine(reportDirectory, fileName);

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(transactions);
            }

            _logger.LogInformation($"Generated report for user {bankCardId} at {filePath}");
        }

        private async Task<IReadOnlyList<TransactionDomain>> GetDailyTransactions(DateTime date)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var transactionManager = scope.ServiceProvider
                    .GetRequiredService<ITransactionsManager>();

                var transactions = await transactionManager.GetAllAsync(new Shared.PagedAndSortedRequestDto
                {
                    Min = DateTime.Now.Date,
                    PageSize = 1000 //to get all data
                });

                return transactions.Items;
            }                      
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
