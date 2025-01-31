using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Lhs.Entity.DbEntity.DbModel;

// Ensure Dapper is referenced in your project
// You can add the Dapper package via NuGet Package Manager or by running the following command in the Package Manager Console:
// Install-Package Dapper

public class HeroRepository
{
    private readonly string _connectionString;

    public HeroRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }

    public async Task<IEnumerable<T_Hero>> GetAllHeroesAsync()
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<T_Hero>("SELECT * FROM T_Hero");
    }

    public async Task<T_Hero> GetHeroByIdAsync(int id)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<T_Hero>("SELECT * FROM T_Hero WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> InsertHeroAsync(T_Hero hero)
    {
        using var connection = CreateConnection();
        var sql = @"INSERT INTO T_Hero (Name, Desc, Cost, Star, Gender, Country, Corps, AttackRange, InitialAttack, InitialDefense, InitialSiege, InitialSpeed, InitialStrategy, DefaultMagic) 
                    VALUES (@Name, @Desc, @Cost, @Star, @Gender, @Country, @Corps, @AttackRange, @InitialAttack, @InitialDefense, @InitialSiege, @InitialSpeed, @InitialStrategy, @DefaultMagic);
                    SELECT last_insert_rowid();";
        return await connection.ExecuteScalarAsync<int>(sql, hero);
    }

    public async Task<int> UpdateHeroAsync(T_Hero hero)
    {
        using var connection = CreateConnection();
        var sql = @"UPDATE T_Hero SET Name = @Name, Desc = @Desc, Cost = @Cost, Star = @Star, Gender = @Gender, Country = @Country, Corps = @Corps, AttackRange = @AttackRange, InitialAttack = @InitialAttack, InitialDefense = @InitialDefense, InitialSiege = @InitialSiege, InitialSpeed = @InitialSpeed, InitialStrategy = @InitialStrategy, DefaultMagic = @DefaultMagic WHERE Id = @Id";
        return await connection.ExecuteAsync(sql, hero);
    }

    public async Task<int> DeleteHeroAsync(int id)
    {
        using var connection = CreateConnection();
        return await connection.ExecuteAsync("DELETE FROM T_Hero WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> GetHeroCountAsync()
    {
        using var connection = CreateConnection();
        return await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM T_Hero");
    }
}
