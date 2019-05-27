using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using Whid.Domain;
using Whid.Domain.Dates;
using Whid.Functional;

namespace Whid.Framework.DB
{
    public class DbSummaryService : ISummaryService
    {
        private readonly LiteDatabase _db;
        private readonly LiteCollection<SummaryModel> _summaries;

        public DbSummaryService(string dbFilePath)
        {
            var connectionString = new ConnectionString($"Filename={dbFilePath}");
            var logger = new Logger(Logger.FULL, (msg) => System.Diagnostics.Debug.WriteLine(msg));
            _db = new LiteDatabase(connectionString, log: logger);
            _summaries = _db.GetCollection<SummaryModel>("summaries");
        }

        public void Seed()
        {
            var startDate = new Date(2019, 1, 1);

            Enumerable
                .Range(1, 365)
                .Select(day => Summary.DailySummary(startDate.AddDays(day - 1), $"What I did on day {day}"))
                .Select(summary => summary.ToDbModel())
                .ForEach(summary => _summaries.Insert(summary));

            Enumerable
                .Range(1, 52)
                .Select(week => Summary.WeeklySummary(startDate.AddDays(7 * (week - 1)), $"What I did in week {week}"))
                .Select(summary => summary.ToDbModel())
                .ForEach(summary => _summaries.Insert(summary));

            Enumerable
                .Range(1, 12)
                .Select(month => Summary.MonthlySummary(((Month)startDate).AddMonths(month - 1), $"What I did in month {month}"))
                .Select(summary => summary.ToDbModel())
                .ForEach(summary => _summaries.Insert(summary));
        }

        public bool DeleteSummary(Guid id)
        {
            return _summaries.Delete(id);
        }

        public IEnumerable<Summary> GetSummaries()
        {
            return _summaries.FindAll().Select(dbModel => dbModel.ToDomainModel());
        }

        public Option<Summary> GetSummary(Guid id)
        {
            var dbModel =_summaries.FindById(id);
            if (null == dbModel)
            {
                return Option.None;
            }
            return dbModel.ToDomainModel();
        }

        public Summary SaveSummary(Summary summary)
        {
            var dbModel = summary.ToDbModel();
            _summaries.Upsert(dbModel);
            return dbModel.ToDomainModel();
        }
    }
}
