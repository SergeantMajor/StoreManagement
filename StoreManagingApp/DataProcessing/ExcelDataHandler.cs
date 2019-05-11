using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using GemBox.Spreadsheet;
using StoreManagement.Model;

namespace StoreManagingApp.DataProcessing
{
    public class ExcelDataHandler
    {
        private readonly string _path;

        public ExcelDataHandler(int sheet)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appSettings.json", true, true)
                .AddJsonFile($"appSettings.{"Development"}.json", true, true);
            var configuration = builder.Build();

            _path = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + configuration["FilePath"];
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public IEnumerable<StoreCharacters> GetStores()
        {
            var result = new List<StoreCharacters>();
            try
            {
                using (FileStream stream = new FileStream(_path, FileMode.Open))
                {
                    var file = ExcelFile.Load(stream, LoadOptions.XlsxDefault);
                    var worksheet = file.Worksheets[0];

                    // Iterate through all rows in an Excel worksheet.
                    foreach (var row in worksheet.Rows)
                    {
                        if (row.Index == 0)
                        {
                            continue;
                        }

                        var line = new StoreCharacters()
                        {
                            Id = row.Index,
                            Name = row.AllocatedCells[0].Value.ToString(),
                            CountryCode = row.AllocatedCells[1].Value.ToString(),
                            Email = row.AllocatedCells[2].Value.ToString(),
                            StoreManagerName = row.AllocatedCells[3].Value.ToString(),
                            StoreManagerLastName = row.AllocatedCells[4].Value.ToString(),
                            StoreManagerEmail = row.AllocatedCells[5].Value.ToString(),
                            Category = (int)row.AllocatedCells[6].Value,
                            StockBackStore = (int)row.AllocatedCells[7].Value,
                            StockFrontStore = (int)row.AllocatedCells[8].Value,
                            StockShoppingWindow = (int)row.AllocatedCells[9].Value,
                            StockAccuracy = Convert.ToDouble(row.AllocatedCells[10].Value),
                            OnFloorAvailability = Convert.ToDouble(row.AllocatedCells[11].Value),
                            StockMeanAgeDays = (int)row.AllocatedCells[12].Value
                        };
                        line.TotalStock = line.StockBackStore + line.StockFrontStore + line.StockShoppingWindow;

                        result.Add(line);
                    }
                }            

                return result.OrderBy(x => x.Name);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int? AddStore(StoreCharacters store)
        {
            ExcelFile file;
            int id;

            try
            {
                using (FileStream stream = new FileStream(_path, FileMode.Open))
                {
                    file = ExcelFile.Load(stream, LoadOptions.XlsxDefault);
                    var worksheet = file.Worksheets[0];

                    id = worksheet.Rows.Count;
                    var row = worksheet.Rows[id];

                    row.Cells[0].Value = store.Name;
                    row.Cells[1].Value = store.CountryCode;
                    row.Cells[2].Value = store.Email;
                    row.Cells[3].Value = store.StoreManagerName;
                    row.Cells[4].Value = store.StoreManagerLastName;
                    row.Cells[5].Value = store.StoreManagerEmail;
                    row.Cells[6].Value = store.Category;
                    row.Cells[7].Value = store.StockBackStore;
                    row.Cells[8].Value = store.StockFrontStore;
                    row.Cells[9].Value = store.StockShoppingWindow;
                    row.Cells[10].Value = Convert.ToDouble(store.StockAccuracy);
                    row.Cells[11].Value = Convert.ToDouble(store.OnFloorAvailability);
                    row.Cells[12].Value = store.StockMeanAgeDays;
                }

                file.Save(_path);
                return id;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int? UpdateStore(StoreCharacters store)
        {
            try
            {
                ExcelFile file;
                using (FileStream stream = new FileStream(_path, FileMode.Open))
                {
                    file = ExcelFile.Load(stream, LoadOptions.XlsxDefault);
                    var worksheet = file.Worksheets[0];

                    var row = worksheet.Rows[store.Id];

                    row.Cells[0].Value = store.Name;
                    row.Cells[1].Value = store.CountryCode;
                    row.Cells[2].Value = store.Email;
                    row.Cells[3].Value = store.StoreManagerName;
                    row.Cells[4].Value = store.StoreManagerLastName;
                    row.Cells[5].Value = store.StoreManagerEmail;
                    row.Cells[6].Value = store.Category;
                    row.Cells[7].Value = store.StockBackStore;
                    row.Cells[8].Value = store.StockFrontStore;
                    row.Cells[9].Value = store.StockShoppingWindow;
                    row.Cells[10].Value = store.StockAccuracy;
                    row.Cells[11].Value = store.OnFloorAvailability;
                    row.Cells[12].Value = store.StockMeanAgeDays;
                }

                file.Save(_path);
                return store.Id;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int? DeleteStore(int id)
        {
            try
            {
                ExcelFile file;
                using (FileStream stream = new FileStream(_path, FileMode.Open))
                {
                    file = ExcelFile.Load(stream, LoadOptions.XlsxDefault);
                    var worksheet = file.Worksheets[0];

                    worksheet.Rows.Remove(id, 1);
                }

                file.Save(_path);
                return id;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
