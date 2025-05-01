using MovieService.Data.Interfaces.Entities;

namespace MovieService.Data.Seed;

internal class MalayalamMoviesSeed
{
    public static IEnumerable<MovieEntity> Movies => [
        new() {
            ID = "ab86612b-47f0-497c-b5a5-a2d06a5af163",
            Title = "Movie 1",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1998, 1, 24).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Madhu C. Narayanan")],
                ["Music Director"] = [new("A. R. Rahman")]
            },
            Cast = new List<PersonEntity>
            {
                new("Nithya Menen"), new("Prithviraj Sukumaran"), new("Asif Ali")
            }
        },
        new() {
            ID = "882c2168-347c-4e23-89fc-4f4f88aa940d",
            Title = "Movie 2",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1989, 8, 15).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Fazil")],
                ["Music Director"] = [new("Sushin Shyam")]
            },
            Cast = new List<PersonEntity>
            {
                new("Mamta Mohandas"), new("Mammootty"), new("Revathi")
            }
        },
        new() {
            ID = "bfabe9b0-53be-43e1-af00-a08fddff6ebf",
            Title = "Movie 3",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1982, 12, 26).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Jeethu Joseph")],
                ["Music Director"] = [new("Gopi Sundar")]
            },
            Cast = new List<PersonEntity>
            {
                new("Prithviraj Sukumaran"), new("Fahadh Faasil"), new("Mammootty")
            }
        },
        new() {
            ID = "0f23a4e0-9d42-467a-8f9c-6a2c7743f9c3",
            Title = "Movie 4",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2004, 12, 15).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Madhu C. Narayanan")],
                ["Music Director"] = [new("Rajesh Murugesan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Nivin Pauly"), new("Parvathy Thiruvothu"), new("Meena")
            }
        },
        new() {
            ID = "66556ebb-8a03-4182-a3c3-ffedf7d7d482",
            Title = "Movie 5",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2004, 2, 15).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Lal Jose")],
                ["Music Director"] = [new("Ilaiyaraaja")]
            },
            Cast = new List<PersonEntity>
            {
                new("Nithya Menen"), new("Jayasurya"), new("Aparna Balamurali")
            }
        },
        new() {
            ID = "3e76233b-a382-44d1-ae0e-5900d89fd551",
            Title = "Movie 6",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2009, 11, 25).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Jeethu Joseph")],
                ["Music Director"] = [new("Ilaiyaraaja")]
            },
            Cast = new List<PersonEntity>
            {
                new("Manju Warrier"), new("Aparna Balamurali"), new("Sai Pallavi")
            }
        },
        new() {
            ID = "3c9085fe-744a-4eeb-902b-5664aec51cd4",
            Title = "Movie 7",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2000, 11, 24).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Jeethu Joseph")],
                ["Music Director"] = [new("Sushin Shyam")]
            },
            Cast = new List<PersonEntity>
            {
                new("Jayasurya"), new("Meena"), new("Mamta Mohandas")
            }
        },
        new() {
            ID = "51cd6175-bf70-4369-822b-8bee6fb3cbfd",
            Title = "Movie 8",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1990, 3, 14).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Anwar Rasheed")],
                ["Music Director"] = [new("A. R. Rahman")]
            },
            Cast = new List<PersonEntity>
            {
                new("Nithya Menen"), new("Mohanlal"), new("Sai Pallavi")
            }
        },
        new() {
            ID = "c72b2778-3a17-47e9-b72b-ef06f7aa1f0d",
            Title = "Movie 9",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2013, 8, 16).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Lijo Jose Pellissery")],
                ["Music Director"] = [new("Ouseppachan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Kunchacko Boban"), new("Dulquer Salmaan"), new("Sai Pallavi")
            }
        },
        new() {
            ID = "cb6290a9-4841-4ddf-ac59-caa77aefd4d5",
            Title = "Movie 10",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2002, 11, 17).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Madhu C. Narayanan")],
                ["Music Director"] = [new("A. R. Rahman")]
            },
            Cast = new List<PersonEntity>
            {
                new("Nithya Menen"), new("Indrajith Sukumaran"), new("Mamta Mohandas")
            }
        },
        new() {
            ID = "588f316f-c83f-40e8-a52a-513231ffac6f",
            Title = "Movie 11",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1993, 7, 1).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Anwar Rasheed")],
                ["Music Director"] = [new("M. Jayachandran")]
            },
            Cast = new List<PersonEntity>
            {
                new("Shobana"), new("Revathi"), new("Asif Ali")
            }
        },
        new() {
            ID = "6897737b-eac5-46ef-a9dd-3dcb31b31ddf",
            Title = "Movie 12",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2017, 12, 8).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Anwar Rasheed")],
                ["Music Director"] = [new("Anil Johnson")]
            },
            Cast = new List<PersonEntity>
            {
                new("Dulquer Salmaan"), new("Mammootty"), new("Mamta Mohandas")
            }
        },
        new() {
            ID = "c30bf16b-0632-4bec-a328-db3884ae99d5",
            Title = "Movie 13",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1985, 10, 3).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Alphonse Puthren")],
                ["Music Director"] = [new("Rajesh Murugesan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Nithya Menen"), new("Dulquer Salmaan"), new("Indrajith Sukumaran")
            }
        },
        new() {
            ID = "405097f4-3c2a-4cb2-9134-1a8385b465f4",
            Title = "Movie 14",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1989, 12, 26).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Priyadarshan")],
                ["Music Director"] = [new("Ouseppachan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Kunchacko Boban"), new("Prithviraj Sukumaran"), new("Asif Ali")
            }
        },
        new() {
            ID = "8216c07f-cfa2-4eb0-8c39-495091032766",
            Title = "Movie 15",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2019, 5, 25).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Lal Jose")],
                ["Music Director"] = [new("Sushin Shyam")]
            },
            Cast = new List<PersonEntity>
            {
                new("Mammootty"), new("Revathi"), new("Meena")
            }
        },
        new() {
            ID = "31533ec1-1d7b-4e79-be25-242ee511b9d4",
            Title = "Movie 16",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2023, 6, 10).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Alphonse Puthren")],
                ["Music Director"] = [new("Anil Johnson")]
            },
            Cast = new List<PersonEntity>
            {
                new("Kunchacko Boban"), new("Nithya Menen"), new("Parvathy Thiruvothu")
            }
        },
        new() {
            ID = "5bf70320-c38a-47b9-bbd0-231a658efc4d",
            Title = "Movie 17",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2017, 11, 4).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Madhu C. Narayanan")],
                ["Music Director"] = [new("Ouseppachan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Mammootty"), new("Parvathy Thiruvothu"), new("Nithya Menen")
            }
        },
        new() {
            ID = "11a5e43b-24e0-4a60-8d8d-20aab582d513",
            Title = "Movie 18",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1990, 3, 15).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Lijo Jose Pellissery")],
                ["Music Director"] = [new("M. Jayachandran")]
            },
            Cast = new List<PersonEntity>
            {
                new("Jayasurya"), new("Mammootty"), new("Nazriya Nazim")
            }
        },
        new() {
            ID = "36638118-aabf-4f47-8851-901564ed2155",
            Title = "Movie 19",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1985, 1, 16).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Fazil")],
                ["Music Director"] = [new("Ouseppachan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Nivin Pauly"), new("Meena"), new("Indrajith Sukumaran")
            }
        },
        new() {
            ID = "e0ae9299-3d10-4221-b6a7-4d42aceda407",
            Title = "Movie 20",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1997, 1, 5).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Lijo Jose Pellissery")],
                ["Music Director"] = [new("A. R. Rahman")]
            },
            Cast = new List<PersonEntity>
            {
                new("Aparna Balamurali"), new("Meena"), new("Sai Pallavi")
            }
        },
        new() {
            ID = "27411384-3835-4c32-a169-002b8f900313",
            Title = "Movie 21",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1995, 5, 28).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Lijo Jose Pellissery")],
                ["Music Director"] = [new("Ilaiyaraaja")]
            },
            Cast = new List<PersonEntity>
            {
                new("Dulquer Salmaan"), new("Prithviraj Sukumaran"), new("Sai Pallavi")
            }
        },
        new() {
            ID = "9770ca5c-0ec8-418d-a53e-70d5b14b480a",
            Title = "Movie 22",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1980, 11, 22).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Madhu C. Narayanan")],
                ["Music Director"] = [new("Anil Johnson")]
            },
            Cast = new List<PersonEntity>
            {
                new("Meena"), new("Nazriya Nazim"), new("Shobana")
            }
        },
        new() {
            ID = "8db87df1-bb6a-405b-8660-52d427bc7482",
            Title = "Movie 23",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2016, 11, 17).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Jeethu Joseph")],
                ["Music Director"] = [new("Ilaiyaraaja")]
            },
            Cast = new List<PersonEntity>
            {
                new("Revathi"), new("Sai Pallavi"), new("Jayasurya")
            }
        },
        new() {
            ID = "4e9cf1a3-c4d9-44f9-8b3b-1bb00d808191",
            Title = "Movie 24",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2009, 11, 8).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Fazil")],
                ["Music Director"] = [new("Ilaiyaraaja")]
            },
            Cast = new List<PersonEntity>
            {
                new("Prithviraj Sukumaran"), new("Kunchacko Boban"), new("Parvathy Thiruvothu")
            }
        },
        new() {
            ID = "345c69f6-f15a-41af-b92c-6e1317a3d59a",
            Title = "Movie 25",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2004, 1, 16).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Lijo Jose Pellissery")],
                ["Music Director"] = [new("Ouseppachan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Prithviraj Sukumaran"), new("Aparna Balamurali"), new("Fahadh Faasil")
            }
        },
        new() {
            ID = "a2c9d082-be56-44fe-a056-ed9e44876a26",
            Title = "Movie 26",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1983, 8, 4).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Alphonse Puthren")],
                ["Music Director"] = [new("Rajesh Murugesan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Prithviraj Sukumaran"), new("Kunchacko Boban"), new("Nazriya Nazim")
            }
        },
        new() {
            ID = "7314162a-7e0e-484f-a685-880a33e05240",
            Title = "Movie 27",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1990, 8, 20).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Sathyan Anthikad")],
                ["Music Director"] = [new("Gopi Sundar")]
            },
            Cast = new List<PersonEntity>
            {
                new("Prithviraj Sukumaran"), new("Kunchacko Boban"), new("Nazriya Nazim")
            }
        },
        new() {
            ID = "b2f0ddf2-ccc4-4611-8090-83f4ed6f571a",
            Title = "Movie 28",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1985, 8, 18).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Anwar Rasheed")],
                ["Music Director"] = [new("Gopi Sundar")]
            },
            Cast = new List<PersonEntity>
            {
                new("Manju Warrier"), new("Revathi"), new("Mamta Mohandas")
            }
        },
        new() {
            ID = "9b9706bc-9617-45a1-9d89-2ba663d7612b",
            Title = "Movie 29",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2001, 11, 22).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Anwar Rasheed")],
                ["Music Director"] = [new("Ouseppachan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Sai Pallavi"), new("Mamta Mohandas"), new("Indrajith Sukumaran")
            }
        },
        new() {
            ID = "ff68f07d-643c-4d5b-a246-dabf2929ec22",
            Title = "Movie 30",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2016, 5, 26).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Madhu C. Narayanan")],
                ["Music Director"] = [new("Sushin Shyam")]
            },
            Cast = new List<PersonEntity>
            {
                new("Mammootty"), new("Revathi"), new("Nazriya Nazim")
            }
        },
        new() {
            ID = "27605915-a6fe-47e1-8de4-f9f45f65e1cc",
            Title = "Movie 31",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1997, 6, 10).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Anwar Rasheed")],
                ["Music Director"] = [new("Ouseppachan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Kunchacko Boban"), new("Nivin Pauly"), new("Parvathy Thiruvothu")
            }
        },
        new() {
            ID = "9b9b4f80-e875-4990-a510-94085d74d148",
            Title = "Movie 32",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2008, 8, 22).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Lal Jose")],
                ["Music Director"] = [new("Ilaiyaraaja")]
            },
            Cast = new List<PersonEntity>
            {
                new("Nazriya Nazim"), new("Jayasurya"), new("Nivin Pauly")
            }
        },
        new() {
            ID = "eb48f102-8523-4f3d-9266-625ce558e8fa",
            Title = "Movie 33",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1988, 1, 15).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Anwar Rasheed")],
                ["Music Director"] = [new("Sushin Shyam")]
            },
            Cast = new List<PersonEntity>
            {
                new("Sai Pallavi"), new("Indrajith Sukumaran"), new("Revathi")
            }
        },
        new() {
            ID = "961326f8-d798-4357-8e02-b1f8d7253564",
            Title = "Movie 34",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1987, 2, 28).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Blessy")],
                ["Music Director"] = [new("A. R. Rahman")]
            },
            Cast = new List<PersonEntity>
            {
                new("Mammootty"), new("Revathi"), new("Fahadh Faasil")
            }
        },
        new() {
            ID = "af2b905e-10e8-481f-b05f-d7d1d43db407",
            Title = "Movie 35",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1987, 8, 2).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Madhu C. Narayanan")],
                ["Music Director"] = [new("M. Jayachandran")]
            },
            Cast = new List<PersonEntity>
            {
                new("Manju Warrier"), new("Asif Ali"), new("Dulquer Salmaan")
            }
        },
        new() {
            ID = "2a62335c-b448-4c93-b088-f73cda91ccc1",
            Title = "Movie 36",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1997, 1, 4).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Lal Jose")],
                ["Music Director"] = [new("Ouseppachan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Nazriya Nazim"), new("Jayasurya"), new("Manju Warrier")
            }
        },
        new() {
            ID = "804393d3-b4b7-4326-8819-f07d5269951c",
            Title = "Movie 37",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1986, 3, 25).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Lal Jose")],
                ["Music Director"] = [new("Sushin Shyam")]
            },
            Cast = new List<PersonEntity>
            {
                new("Asif Ali"), new("Prithviraj Sukumaran"), new("Kunchacko Boban")
            }
        },
        new() {
            ID = "d93232b0-6258-4bed-9dc7-5ea7cd627be3",
            Title = "Movie 38",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2012, 8, 17).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Madhu C. Narayanan")],
                ["Music Director"] = [new("Sushin Shyam")]
            },
            Cast = new List<PersonEntity>
            {
                new("Fahadh Faasil"), new("Jayasurya"), new("Dulquer Salmaan")
            }
        },
        new() {
            ID = "66ce0d24-de65-4189-9ffb-0410b5e0ef0d",
            Title = "Movie 39",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2015, 10, 13).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Sathyan Anthikad")],
                ["Music Director"] = [new("M. Jayachandran")]
            },
            Cast = new List<PersonEntity>
            {
                new("Jayasurya"), new("Mohanlal"), new("Asif Ali")
            }
        },
        new() {
            ID = "3c5dbe87-4644-4e4d-b034-d3157dbc981d",
            Title = "Movie 40",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1980, 12, 2).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Sathyan Anthikad")],
                ["Music Director"] = [new("Anil Johnson")]
            },
            Cast = new List<PersonEntity>
            {
                new("Jayasurya"), new("Nazriya Nazim"), new("Prithviraj Sukumaran")
            }
        },
        new() {
            ID = "50369d20-45d2-4d32-86a3-2fa44d5ca3a2",
            Title = "Movie 41",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2011, 11, 26).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Madhu C. Narayanan")],
                ["Music Director"] = [new("Ilaiyaraaja")]
            },
            Cast = new List<PersonEntity>
            {
                new("Mohanlal"), new("Prithviraj Sukumaran"), new("Kunchacko Boban")
            }
        },
        new() {
            ID = "cf904688-c327-4e0b-9b87-586e5048b935",
            Title = "Movie 42",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1987, 12, 21).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Lal Jose")],
                ["Music Director"] = [new("Gopi Sundar")]
            },
            Cast = new List<PersonEntity>
            {
                new("Revathi"), new("Asif Ali"), new("Prithviraj Sukumaran")
            }
        },
        new() {
            ID = "5b4fb9bd-164a-4603-b89f-3d0bf3e2be5e",
            Title = "Movie 43",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1981, 12, 28).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Blessy")],
                ["Music Director"] = [new("A. R. Rahman")]
            },
            Cast = new List<PersonEntity>
            {
                new("Parvathy Thiruvothu"), new("Aparna Balamurali"), new("Sai Pallavi")
            }
        },
        new() {
            ID = "0c7d09a7-eb87-45a6-81f3-af1ccfca9961",
            Title = "Movie 44",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1990, 2, 13).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Sathyan Anthikad")],
                ["Music Director"] = [new("Sushin Shyam")]
            },
            Cast = new List<PersonEntity>
            {
                new("Fahadh Faasil"), new("Nazriya Nazim"), new("Nivin Pauly")
            }
        },
        new() {
            ID = "b7d4ef46-6ba6-4645-b01b-1d07293668c6",
            Title = "Movie 45",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1993, 2, 27).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Fazil")],
                ["Music Director"] = [new("Ouseppachan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Sai Pallavi"), new("Nivin Pauly"), new("Aparna Balamurali")
            }
        },
        new() {
            ID = "cffd2c4c-ff87-4302-86fa-6fc8676fbced",
            Title = "Movie 46",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2013, 6, 6).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Madhu C. Narayanan")],
                ["Music Director"] = [new("M. Jayachandran")]
            },
            Cast = new List<PersonEntity>
            {
                new("Nithya Menen"), new("Shobana"), new("Nazriya Nazim")
            }
        },
        new() {
            ID = "e6f2dd2b-6ed8-4717-b5b5-3051dd2917b4",
            Title = "Movie 47",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2018, 12, 8).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Priyadarshan")],
                ["Music Director"] = [new("Rajesh Murugesan")]
            },
            Cast = new List<PersonEntity>
            {
                new("Nazriya Nazim"), new("Parvathy Thiruvothu"), new("Asif Ali")
            }
        },
        new() {
            ID = "9a672d98-a423-4f44-acd7-e8c3cef44564",
            Title = "Movie 48",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1986, 11, 17).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Alphonse Puthren")],
                ["Music Director"] = [new("Gopi Sundar")]
            },
            Cast = new List<PersonEntity>
            {
                new("Parvathy Thiruvothu"), new("Aparna Balamurali"), new("Manju Warrier")
            }
        },
        new() {
            ID = "81dfef2b-847f-4541-af62-20cdba68cb65",
            Title = "Movie 49",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(1998, 11, 9).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Sathyan Anthikad")],
                ["Music Director"] = [new("Sushin Shyam")]
            },
            Cast = new List<PersonEntity>
            {
                new("Indrajith Sukumaran"), new("Dulquer Salmaan"), new("Meena")
            }
        },
        new() {
            ID = "75e65625-2d36-461a-9bcf-61098f651fd4",
            Title = "Movie 50",
            MovieLanguage = "Malayalam",
            ReleaseDate = new DateOnly(2013, 11, 21).ToDateTime(TimeOnly.MinValue),
            Crew = new Dictionary<string, List<PersonEntity>>
            {
                ["Director"] = [new("Anwar Rasheed")],
                ["Music Director"] = [new("A. R. Rahman")]
            },
            Cast = new List<PersonEntity>
            {
                new("Manju Warrier"), new("Nivin Pauly"), new("Jayasurya")
            }
        }
];


}
