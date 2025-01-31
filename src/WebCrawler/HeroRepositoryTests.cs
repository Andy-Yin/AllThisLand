using Dapper;
using Lhs.Entity.DbEntity.DbModel;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class HeroRepositoryTests
{
    private readonly HeroRepository _repository;

    public HeroRepositoryTests()
    {
        // 使用内存数据库进行测试
        var connectionString = "Data Source=:memory:";
        _repository = new HeroRepository(connectionString);

        // 初始化数据库
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        connection.Execute(@"
            CREATE TABLE T_Hero (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT,
                Desc TEXT,
                Cost REAL,
                Star INTEGER,
                Gender BOOLEAN,
                Country INTEGER,
                Corps INTEGER,
                AttackRange INTEGER,
                InitialAttack REAL,
                InitialDefense REAL,
                InitialSiege REAL,
                InitialSpeed REAL,
                InitialStrategy REAL,
                DefaultMagic TEXT
            )");
    }

    [Fact]
    public async Task TestInsertHeroAsync()
    {
        var hero = new T_Hero { Name = "Test Hero", Desc = "Test Desc", Cost = 1.0, Star = 5, Gender = true, Country = EnumCountry.CountryWei, Corps = EnumCorps.CorpsArcher, AttackRange = 3, InitialAttack = 100, InitialDefense = 100, InitialSiege = 100, InitialSpeed = 100, InitialStrategy = 100, DefaultMagic = "Test Magic" };
        var id = await _repository.InsertHeroAsync(hero);
        Assert.IsTrue(id > 0);
    }

    [Fact]
    public async Task TestGetAllHeroesAsync()
    {
        var heroes = await _repository.GetAllHeroesAsync();
        Assert.IsNotNull(heroes);
    }

    [Fact]
    public async Task TestGetHeroByIdAsync()
    {
        var hero = new T_Hero { Name = "Test Hero", Desc = "Test Desc", Cost = 1.0, Star = 5, Gender = true, Country = EnumCountry.CountryWei, Corps = EnumCorps.CorpsArcher, AttackRange = 3, InitialAttack = 100, InitialDefense = 100, InitialSiege = 100, InitialSpeed = 100, InitialStrategy = 100, DefaultMagic = "Test Magic" };
        var id = await _repository.InsertHeroAsync(hero);
        var retrievedHero = await _repository.GetHeroByIdAsync(id);
        Assert.IsNotNull(retrievedHero);
        Assert.AreEqual(hero.Name, retrievedHero.Name);
    }

    [Fact]
    public async Task TestUpdateHeroAsync()
    {
        var hero = new T_Hero { Name = "Test Hero", Desc = "Test Desc", Cost = 1.0, Star = 5, Gender = true, Country = EnumCountry.CountryWei, Corps = EnumCorps.CorpsArcher, AttackRange = 3, InitialAttack = 100, InitialDefense = 100, InitialSiege = 100, InitialSpeed = 100, InitialStrategy = 100, DefaultMagic = "Test Magic" };
        var id = await _repository.InsertHeroAsync(hero);
        hero.Id = id;
        hero.Name = "Updated Hero";
        var rowsAffected = await _repository.UpdateHeroAsync(hero);
        Assert.AreEqual(1, rowsAffected);
    }

    [Fact]
    public async Task TestDeleteHeroAsync()
    {
        var hero = new T_Hero { Name = "Test Hero", Desc = "Test Desc", Cost = 1.0, Star = 5, Gender = true, Country = EnumCountry.CountryWei, Corps = EnumCorps.CorpsArcher, AttackRange = 3, InitialAttack = 100, InitialDefense = 100, InitialSiege = 100, InitialSpeed = 100, InitialStrategy = 100, DefaultMagic = "Test Magic" };
        var id = await _repository.InsertHeroAsync(hero);
        var rowsAffected = await _repository.DeleteHeroAsync(id);
        Assert.AreEqual(1, rowsAffected);
    }

    [Fact]
    public async Task TestGetHeroCountAsync()
    {
        var count = await _repository.GetHeroCountAsync();
        Assert.IsTrue(count >= 0);
    }
}
