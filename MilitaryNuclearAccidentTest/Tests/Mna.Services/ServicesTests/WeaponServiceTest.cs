﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MilitaryNuclearAccident.Src.Mna.Common.DbSet;
using MilitaryNuclearAccident.Src.Mna.Common.Dtos;
using MilitaryNuclearAccident.Src.Mna.Common.Enum;
using MilitaryNuclearAccident.Src.Mna.Data;
using MilitaryNuclearAccident.Src.Mna.Services.Implementation;
using MilitaryNuclearAccident.Src.Mna.UI.Controllers.Profiles;
using MilitaryNuclearAccidentTest.Utils;

namespace MilitaryNuclearAccidentTest.Tests.Mna.Services.ServicesTests
{
    public class WeaponServiceTest
    {

        private readonly BrokenArrowContext _dbContext;
        private readonly Mapper _mapper;
        private readonly ILogger<WeaponServiceImpl> _logger;
        private readonly WeaponServiceImpl _weaponService;

        private readonly Guid weaponOneId = new("ae90b98d-a924-46cb-ac07-e44e5486d346");
        private readonly string weaponOneName = "mark15";
        private readonly string weaponOneBuilder = "U.S";
        private readonly string weaponOneDescription = "This is a weapon";

        private readonly Guid weaponTwoId = new("eda423b5-0245-46eb-a4be-cd0218971fb4");
        private readonly string weaponTwoName = "Mark4";
        private readonly string weaponTwoBuilder = "U.S";
        private readonly string weaponTwoDescription = "This is a weapon";

        public WeaponServiceTest()
        {
            var options = new DbContextOptionsBuilder<BrokenArrowContext>()
            .UseSqlite("Data Source =:memory:")
            .Options;

            _dbContext = new BrokenArrowContext(options);
            _dbContext.Database.OpenConnection();
            _dbContext.Database.EnsureCreated();
            DataInitializer.Initialize(_dbContext);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WeaponProfile>();
            });
            _mapper = (Mapper?)config.CreateMapper();

            _weaponService = new WeaponServiceImpl(_dbContext, _mapper, _logger);
        }

        private void InsertWeapon()
        {
            _dbContext.Weapons.AddRange(BuildWeapon());
            _dbContext.SaveChanges();
        }

        private void ClearWeapon()
        {
            _dbContext.RemoveRange(_dbContext.Weapons);
            _dbContext.SaveChanges();
        }

        [Fact]
        public async Task Test_GetSingleWeaponAsync()
        {
            InsertWeapon();
            WeaponResponse? weapon = await _weaponService.GetSingleWeaponAsync(weaponOneId);
            Assert.NotNull(weapon);
            Assert.NotNull(weapon.BrokenArrows);
            Assert.True(weapon.BrokenArrows.Any());
            Assert.Equal(weapon.WeaponId, weaponOneId);
            Assert.Equal(weapon.Builder, weaponOneBuilder);
            Assert.Equal(weapon.Name, weaponOneName);
            Assert.Equal(weapon.Description, weaponOneDescription);
            ClearWeapon();
        }

        [Fact]
        public async Task Test_GetWeaponsAsync()
        {
            InsertWeapon();
            IEnumerable<WeaponResponse?> weapons = await _weaponService.GetWeaponsAsync();
            Assert.NotNull(weapons);
            Assert.True(weapons.Any());
            Assert.Equal(2, weapons.Count());
            ClearWeapon();
        }

        [Fact]
        public async Task Test_GetBrokenArrowsByWeaponAsync()
        {
            InsertWeapon();
            IEnumerable<WeaponResponse?> weapons = await _weaponService.GetBrokenArrowsByWeaponAsync(AvailableWeapon.MARK15);
            Assert.NotNull(weapons);
            Assert.True(weapons.Any());
            WeaponResponse? expectedWeapon = weapons.First();
            Assert.NotNull(expectedWeapon.BrokenArrows);
            Assert.True(expectedWeapon.BrokenArrows.Any());
            Assert.Equal(expectedWeapon.WeaponId, weaponOneId);
            Assert.Equal(expectedWeapon.Builder, weaponOneBuilder);
            Assert.Equal(expectedWeapon.Name, weaponOneName);
            Assert.Equal(expectedWeapon.Description, weaponOneDescription);
            ClearWeapon();
        }

        [Fact]
        public async Task Test_GetSingleWeaponAsync_ShouldReturnNull_WhenNoWeaponsExist()
        {
            ClearWeapon();
            WeaponResponse? weapon = await _weaponService.GetSingleWeaponAsync(weaponOneId);
            Assert.Null(weapon);
        }

        [Fact]
        public async Task Test_GetWeaponsAsync_ShouldReturnEmptyList_WhenNoWeaponsExist()
        {
            ClearWeapon();
            IEnumerable<WeaponResponse?> weapons = await _weaponService.GetWeaponsAsync();
            Assert.Empty(weapons);
        }

        [Fact]
        public async Task Test_GetBrokenArrowsByWeaponAsync_ShouldReturnEmptyList_WhenNoWeaponsExist()
        {
            ClearWeapon();
            IEnumerable<WeaponResponse?> weapons = await _weaponService.GetBrokenArrowsByWeaponAsync(AvailableWeapon.MARK15);
            Assert.Empty(weapons);
        }

        private List<Weapon> BuildWeapon()
        {
            Weapon weapon = new()
            {
                WeaponId = weaponOneId,
                Name = weaponOneName,
                Builder = weaponOneBuilder,
                Description = weaponOneDescription,
                BrokenArrows =
                    [
                    new() { BrokenArrowId = Guid.NewGuid(), ShortDescription = "Short description 1" },
                    new() { BrokenArrowId = Guid.NewGuid(), ShortDescription = "Short description 2"  }
                    ]
            };
            Weapon weaponTwo = new()
            {
                WeaponId = weaponTwoId,
                Name = weaponTwoName,
                Builder = weaponTwoBuilder,
                Description = weaponTwoDescription,
                BrokenArrows =
                    [
                    new() { BrokenArrowId = Guid.NewGuid(), ShortDescription = "Short description 1" },
                    new() { BrokenArrowId = Guid.NewGuid(), ShortDescription = "Short description 2"  }
                ]
            };
            return [weapon, weaponTwo];
        }
    }
}
