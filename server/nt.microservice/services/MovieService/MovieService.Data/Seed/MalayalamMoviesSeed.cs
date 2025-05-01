using MovieService.Data.Interfaces.Entities;

namespace MovieService.Data.Seed;

internal class MalayalamMoviesSeed
{
    public static IEnumerable<MovieEntity> Movies =>
    [
        new()
        {
            ID = "e2a834de-a569-4c59-88b8-b9e1445bb75d",
            Title = "Yodha",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1980, 1, 1).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 1")],
                ["Story"] = [new("Writer 1")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "e334a509-2134-4fb6-bf11-ccb3c977ed0a",
            Title = "Manichitrathazhu",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1981, 2, 2).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 2")],
                ["Story"] = [new("Writer 2")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "41a1ccb5-dd81-4996-b1dc-df067411a45c",
            Title = "Amaram",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1982, 3, 3).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 3")],
                ["Story"] = [new("Writer 3")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "d3c6cdc1-032a-40a4-9b1b-0b83d7c9dc56",
            Title = "Kireedam",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1983, 4, 4).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 4")],
                ["Story"] = [new("Writer 4")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "e02844bb-9639-4546-b142-8693ed2cbaea",
            Title = "Drishyam",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1984, 5, 5).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 5")],
                ["Story"] = [new("Writer 5")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "6061d09f-62f5-4a02-840c-3f826be6441b",
            Title = "Premam",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1985, 6, 6).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 6")],
                ["Story"] = [new("Writer 6")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "b603f88a-fa3a-4bf5-892c-acc1fec7ff1b",
            Title = "Bangalore Days",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1986, 7, 7).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 7")],
                ["Story"] = [new("Writer 7")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "80e59f21-aad7-4494-8de5-0912a834eb9c",
            Title = "Chithram",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1987, 8, 8).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 8")],
                ["Story"] = [new("Writer 8")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "91c77e2c-b73d-45b9-83fe-74d8dd65dd40",
            Title = "Nadodikkattu",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1988, 9, 9).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 9")],
                ["Story"] = [new("Writer 9")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "1fe48b70-bad7-4e4e-8c59-ae73d2663347",
            Title = "Spadikam",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1989, 10, 10).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 10")],
                ["Story"] = [new("Writer 10")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "941f91e6-3730-42b8-b79b-06df6e700edb",
            Title = "Thanmathra",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1990, 11, 11).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 11")],
                ["Story"] = [new("Writer 11")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "b7bc1c74-155d-4e3a-92f5-eb167bcb1cab",
            Title = "Ustad Hotel",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1991, 12, 12).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 12")],
                ["Story"] = [new("Writer 12")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "73e24416-6b05-4d68-ba1a-c5eac9102fbe",
            Title = "Classmates",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1992, 1, 13).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 13")],
                ["Story"] = [new("Writer 13")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "8d7ee962-2a9c-4e86-b195-b4e720a1e390",
            Title = "Traffic",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1993, 2, 14).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 14")],
                ["Story"] = [new("Writer 14")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "07ba2c80-8803-463e-9d16-428cf04e9699",
            Title = "Mumbai Police",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1994, 3, 15).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 15")],
                ["Story"] = [new("Writer 15")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "9acf289e-ecce-4c97-a6a4-d3ff3a117d60",
            Title = "Kali",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1995, 4, 16).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 16")],
                ["Story"] = [new("Writer 16")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "34a293e4-f98f-4c29-a2e6-f4e0a52d85dc",
            Title = "Ennu Ninte Moideen",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1996, 5, 17).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 17")],
                ["Story"] = [new("Writer 17")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "f9e3bb51-0698-4c8c-9716-53eb4e2f614c",
            Title = "Charlie",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1997, 6, 18).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 18")],
                ["Story"] = [new("Writer 18")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "cf0e3561-a163-4f0a-b899-fcddba52e20f",
            Title = "Maheshinte Prathikaaram",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1998, 7, 19).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 19")],
                ["Story"] = [new("Writer 19")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "b517606f-fb29-4cf2-a9cf-d036ebe4f139",
            Title = "Ee.Ma.Yau",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1999, 8, 20).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 20")],
                ["Story"] = [new("Writer 20")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "61e6f2a3-ae74-4d9b-b5df-7f7516ed2cd8",
            Title = "Jallikattu",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2000, 9, 21).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 21")],
                ["Story"] = [new("Writer 21")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "ab8d4b76-bcdb-4dcf-93ef-bb303517999e",
            Title = "Angamaly Diaries",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2001, 10, 22).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 22")],
                ["Story"] = [new("Writer 22")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "f0bc7cac-0288-4d87-889a-d9bf46f7b25e",
            Title = "Kumbalangi Nights",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2002, 11, 23).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 23")],
                ["Story"] = [new("Writer 23")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "42941c88-8961-47b2-a389-32a175626605",
            Title = "Thondimuthalum Driksakshiyum",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2003, 12, 24).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 24")],
                ["Story"] = [new("Writer 24")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "8ef1709a-28fb-411d-85ff-af1ff706610c",
            Title = "Take Off",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2004, 1, 25).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 25")],
                ["Story"] = [new("Writer 25")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "ac956138-b7e0-4713-a4dd-33a7ad26dce6",
            Title = "Oru Indian Pranayakatha",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2005, 2, 26).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 26")],
                ["Story"] = [new("Writer 26")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "af0b141d-154d-45d7-b568-9d60b5753aa1",
            Title = "Anjaam Pathiraa",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2006, 3, 27).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 27")],
                ["Story"] = [new("Writer 27")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "e05a3db7-1090-480b-b118-cdec1e33969e",
            Title = "Joseph",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2007, 4, 28).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 28")],
                ["Story"] = [new("Writer 28")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "6d1d3074-4ded-4b62-b127-d57616bf5b5a",
            Title = "1983",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2008, 5, 1).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 29")],
                ["Story"] = [new("Writer 29")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "f1b4ce5e-e794-4063-a7d1-88ddfa0c1fd0",
            Title = "Salt N' Pepper",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2009, 6, 2).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 30")],
                ["Story"] = [new("Writer 30")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "cbc14099-92ca-4070-bf40-410db71cb442",
            Title = "Rani Padmini",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2010, 7, 3).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 31")],
                ["Story"] = [new("Writer 31")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "bd2c60e8-6a95-4543-8ae0-286adefab358",
            Title = "Njan Prakashan",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2011, 8, 4).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 32")],
                ["Story"] = [new("Writer 32")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "a72b2af5-175d-43dc-aa44-269ba7c064ca",
            Title = "Moothon",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2012, 9, 5).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 33")],
                ["Story"] = [new("Writer 33")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "254ae319-4174-4c35-b86a-fe560c758e5c",
            Title = "Vikramadithyan",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2013, 10, 6).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 34")],
                ["Story"] = [new("Writer 34")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "9c4675c8-4f37-4469-9173-d25cd36321ec",
            Title = "Kayamkulam Kochunni",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2014, 11, 7).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 35")],
                ["Story"] = [new("Writer 35")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "fa17d4e4-4467-4231-979c-93a9d44ef817",
            Title = "Pavada",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2015, 12, 8).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 36")],
                ["Story"] = [new("Writer 36")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "7d877cdf-f69f-4aa8-b42c-5cd2506f0130",
            Title = "Action Hero Biju",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2016, 1, 9).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 37")],
                ["Story"] = [new("Writer 37")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "3864f53d-ba39-43de-a8ee-06b5b7bf4bef",
            Title = "Kammattippadam",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2017, 2, 10).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 38")],
                ["Story"] = [new("Writer 38")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "3e0c538e-bc39-4f39-bdb2-a6246dfdcba7",
            Title = "Virus",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2018, 3, 11).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 39")],
                ["Story"] = [new("Writer 39")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "0c519eef-3c40-48b5-a052-a01e1a3619b7",
            Title = "Uyare",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2019, 4, 12).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 40")],
                ["Story"] = [new("Writer 40")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "c34071f5-64b3-4842-8f92-4b15f000c399",
            Title = "North 24 Kaatham",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1980, 5, 13).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 41")],
                ["Story"] = [new("Writer 41")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "304e9f55-6800-4a52-adf2-605cebdb0bad",
            Title = "Ayyappanum Koshiyum",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1981, 6, 14).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 42")],
                ["Story"] = [new("Writer 42")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "adafa66c-46a2-4321-8ad0-62d29f837fe0",
            Title = "Pathemari",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1982, 7, 15).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 43")],
                ["Story"] = [new("Writer 43")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "d013b36f-b93e-4847-bbb1-08b56d3c4f1e",
            Title = "Sudani from Nigeria",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1983, 8, 16).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 44")],
                ["Story"] = [new("Writer 44")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "4c88ec15-c7f6-482c-86d2-06671a91380d",
            Title = "Neram",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1984, 9, 17).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 45")],
                ["Story"] = [new("Writer 45")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "ac9979d3-04e1-4b94-9eb4-835f10d0e419",
            Title = "Zachariayude Garbhinikal",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1985, 10, 18).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 46")],
                ["Story"] = [new("Writer 46")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "774920ce-7eef-4c6b-a904-cadddc74e251",
            Title = "Jacobinte Swargarajyam",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1986, 11, 19).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 47")],
                ["Story"] = [new("Writer 47")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "2bcd614a-d02b-408c-acf1-1786bb49c521",
            Title = "Thira",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1987, 12, 20).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 48")],
                ["Story"] = [new("Writer 48")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "23366fe0-9785-40c3-972a-b3b918320fe4",
            Title = "Koode",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1988, 1, 21).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 49")],
                ["Story"] = [new("Writer 49")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },

        new()
        {
            ID = "33020661-a8a8-4834-8f0c-a66320ba9e38",
            Title = "Yodha",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1989, 2, 22).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Director 50")],
                ["Story"] = [new("Writer 50")]
            },
            Cast = new List<PersonEntity>
            {
                new("Actor 1"),
                new("Actor 2"),
                new("Actor 3")
            }
        },
    ];


}
