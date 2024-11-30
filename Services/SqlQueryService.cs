namespace uvodni_strana.Services
{
    using global::uvodni_strana.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
 

    public class SqlQueryService
    {
        private readonly AppDbContext _context;

        public SqlQueryService(AppDbContext context)
        {
            _context = context;
        }



        // Metoda pro aktualizaci sloupce namevote tabulky voteparam
        /// <summary>
        /// uloží řetězec namevote do sloupce namevote do prvního řádku tabulky voteparam
        /// </summary>
        /// <param name="namevote"></param>
        /// <returns></returns>
        public async Task UpdateNameVoteAsync(string namevote)
        {
            string sqlFilePath = Path.Combine("Sql", "update_namevote.sql"); // připraví cestu k sql dotazu
            string query = await File.ReadAllTextAsync(sqlFilePath); //natáhne dotaz do stringu query

            var parameters = new Dictionary<string, object> //naplní seznam parametrů
            {
                { "@namevote", namevote }, // hodnota do sloupce namevote
                { "@id", 1 } // Aktualizuje řádek s id = 1
            };

            await ExecuteRawSqlAsync(query, parameters); //provede dotaz - aktualizuje tabulku, nevrací žádný výsledek
        }


        /// <summary>
        /// UpdateAllVoteParamsAsync
        /// Uloží více hodnot do sloupců v prvním řádku tabulky voteparam.
        /// </summary>
        /// <param name="namevote"></param>
        /// <param name="firma"></param>
        /// <param name="misto"></param>
        /// <param name="vedouci"></param>
        /// <param name="email"></param>
        /// <param name="auditor"></param>
        /// <param name="emailAuditor"></param>
        /// <returns></returns>
        public async Task UpdateAllVoteParamsAsync(
            string namevote, string firma, string misto, string vedouci,
            string email, string auditor, string emailAuditor)
        {
            string sqlFilePath = Path.Combine("SQL", "update_whole_input_form.sql"); // připraví cestu k sql dotazu
            string query = await File.ReadAllTextAsync(sqlFilePath); // načte obsah SQL dotazu ze souboru

            var parameters = new Dictionary<string, object> //naplní seznam parametrů
    {
        { "@namevote", namevote },
        { "@firma", firma },
        { "@misto", misto },
        { "@vedouci", vedouci },
        { "@email", email },
        { "@auditor", auditor },
        { "@emailAuditor", emailAuditor },
        { "@id", 1 } // Aktualizuje první řádek (id = 1)
    };

            await ExecuteRawSqlAsync(query, parameters); // Provede SQL dotaz
        }


        // Metoda pro provedení SQL dotazu
        /// <summary>
        /// to je volámí dotazu s parametry
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<List<dynamic>> ExecuteRawSqlAsync(string query, Dictionary<string, object> parameters)
        {
            List<dynamic> result = new();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;

                // Přidání parametrů
                foreach (var param in parameters)
                {
                    var dbParameter = command.CreateParameter();
                    dbParameter.ParameterName = param.Key;
                    dbParameter.Value = param.Value;
                    command.Parameters.Add(dbParameter);
                }

                _context.Database.OpenConnection();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader.GetValue(i));
                        }
                        result.Add(row);
                    }
                }
            }
            return result;
        }

    }
}
