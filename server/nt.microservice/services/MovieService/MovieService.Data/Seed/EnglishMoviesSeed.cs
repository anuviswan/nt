using MovieService.Data.Interfaces.Entities;

namespace MovieService.Data.Seed;

internal static class EnglishMoviesSeed
{
    public static IEnumerable<MovieEntity> Movies => [
            new() {
                ID = "5ecda28b-7c24-4ea8-94a0-7f85a36a53f8",
                Title = "The Godfather",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2007, 10, 9).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Francis Ford Coppola")],
                    ["Music Director"] = [new("Nino Rota")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Marlon Brando"), new("Al Pacino"), new("James Caan")
                }
            },

            new() {
                ID = "40eefd3b-d5ad-4332-a9b3-ad819be11949",
                Title = "No Country for Old Men",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1996, 4, 26).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Joel Coen")],
                    ["Music Director"] = [new("Carter Burwell")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Tommy Lee Jones"), new("Javier Bardem"), new("Josh Brolin")
                }
            },

            new() {
                ID = "c2ea2023-0216-4748-bcd0-f73c8adfb64d",
                Title = "The Social Network",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1996, 4, 1).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("David Fincher")],
                    ["Music Director"] = [new("Trent Reznor")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Jesse Eisenberg"), new("Andrew Garfield"), new("Justin Timberlake")
                }
            },

            new() {
                ID = "abe1a2a7-ae02-4860-86bf-99c56d4b7e82",
                Title = "Schindler's List",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1995, 1, 9).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Steven Spielberg")],
                    ["Music Director"] = [new("John Williams")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Liam Neeson"), new("Ben Kingsley"), new("Ralph Fiennes")
                }
            },

            new() {
                ID = "57ec1723-3cb2-41f1-9172-eae148558842",
                Title = "Arrival",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2022, 1, 9).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Denis Villeneuve")],
                    ["Music Director"] = [new("Jóhann Jóhannsson")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Amy Adams"), new("Jeremy Renner"), new("Forest Whitaker")
                }
            },

            new() {
                ID = "aec591b9-1906-4076-ae70-6277d4ba2d9d",
                Title = "The Shawshank Redemption",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1998, 8, 4).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Frank Darabont")],
                    ["Music Director"] = [new("Thomas Newman")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Tim Robbins"), new("Morgan Freeman"), new("Bob Gunton")
                }
            },

            new() {
                ID = "3904e67e-a437-4c3d-9b18-c8c4dd239ad2",
                Title = "1917",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1991, 5, 17).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Sam Mendes")],
                    ["Music Director"] = [new("Thomas Newman")]
                },
                Cast = new List<PersonEntity>
                {
                    new("George MacKay"), new("Dean-Charles Chapman"), new("Mark Strong")
                }
            },

            new() {
                ID = "2e34adef-2834-47f6-8787-70ce5796b5fc",
                Title = "Mad Max: Fury Road",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2000, 5, 26).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("George Miller")],
                    ["Music Director"] = [new("Junkie XL")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Tom Hardy"), new("Charlize Theron"), new("Nicholas Hoult")
                }
            },

            new() {
                ID = "63621d2e-b576-4d82-8c66-f73822633fbd",
                Title = "Titanic",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2002, 6, 27).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("James Cameron")],
                    ["Music Director"] = [new("James Horner")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Leonardo DiCaprio"), new("Kate Winslet"), new("Billy Zane")
                }
            },

            new() {
                ID = "2cc97e56-8fb2-47a2-b931-9cb5b8eae34a",
                Title = "The Silence of the Lambs",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2018, 8, 12).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Jonathan Demme")],
                    ["Music Director"] = [new("Howard Shore")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Jodie Foster"), new("Anthony Hopkins"), new("Scott Glenn")
                }
            },

            new() {
                ID = "ee2c06e4-0f3e-4570-8fc3-47fac0fc25e9",
                Title = "The Prestige",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1997, 9, 5).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Christopher Nolan")],
                    ["Music Director"] = [new("David Julyan")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Hugh Jackman"), new("Christian Bale"), new("Michael Caine")
                }
            },

            new() {
                ID = "729c469e-1a64-41e3-aa7c-d253144da6e2",
                Title = "The Departed",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2016, 2, 2).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Martin Scorsese")],
                    ["Music Director"] = [new("Howard Shore")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Leonardo DiCaprio"), new("Matt Damon"), new("Jack Nicholson")
                }
            },

            new() {
                ID = "a94d2669-79b2-4690-bf9f-c5bd17286a23",
                Title = "Lincoln",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2021, 8, 18).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Steven Spielberg")],
                    ["Music Director"] = [new("John Williams")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Daniel Day-Lewis"), new("Sally Field"), new("Tommy Lee Jones")
                }
            },

            new() {
                ID = "cd281384-c8dc-4b6c-b145-2e4f93f9fbc0",
                Title = "The Grand Budapest Hotel",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2002, 1, 14).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Wes Anderson")],
                    ["Music Director"] = [new("Alexandre Desplat")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Ralph Fiennes"), new("Tony Revolori"), new("Saoirse Ronan")
                }
            },

            new() {
                ID = "396aa8bf-e80b-43be-813b-18088fab1d0f",
                Title = "The Irishman",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2001, 10, 27).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Martin Scorsese")],
                    ["Music Director"] = [new("Robbie Robertson")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Robert De Niro"), new("Al Pacino"), new("Joe Pesci")
                }
            },

            new() {
                ID = "cfec18bd-bb89-4d16-b3c2-b913ff2c9c91",
                Title = "Spotlight",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1992, 12, 26).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Tom McCarthy")],
                    ["Music Director"] = [new("Howard Shore")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Mark Ruffalo"), new("Michael Keaton"), new("Rachel McAdams")
                }
            },

            new() {
                ID = "fd25403f-b070-4f7e-97ff-8d9f75c9268d",
                Title = "Joker",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2012, 5, 10).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Todd Phillips")],
                    ["Music Director"] = [new("Hildur Guðnadóttir")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Joaquin Phoenix"), new("Robert De Niro"), new("Zazie Beetz")
                }
            },

            new() {
                ID = "ae3b1d8c-646a-465f-8eec-ae8c948ea3b3",
                Title = "Birdman",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1995, 3, 11).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Alejandro G. Iñárritu")],
                    ["Music Director"] = [new("Antonio Sánchez")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Michael Keaton"), new("Emma Stone"), new("Edward Norton")
                }
            },

            new() {
                ID = "ea2988ca-8b2d-49af-9ac6-256e6369ffb7",
                Title = "Fight Club",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1999, 7, 25).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("David Fincher")],
                    ["Music Director"] = [new("The Dust Brothers")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Brad Pitt"), new("Edward Norton"), new("Helena Bonham Carter")
                }
            },

            new() {
                ID = "651d6cdc-72b7-43f0-a576-ca183c34458d",
                Title = "Pulp Fiction",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2002, 7, 14).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Quentin Tarantino")],
                    ["Music Director"] = [new("Various Artists")]
                },
                Cast = new List<PersonEntity>
                {
                    new("John Travolta"), new("Samuel L. Jackson"), new("Uma Thurman")
                }
            },

            new() {
                ID = "d35f550a-fec8-4b44-8d0b-730f7ba7014b",
                Title = "Parasite",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1996, 3, 10).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Bong Joon-ho")],
                    ["Music Director"] = [new("Jung Jae-il")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Song Kang-ho"), new("Lee Sun-kyun"), new("Cho Yeo-jeong")
                }
            },

            new() {
                ID = "a6086a10-d4fd-426f-bf52-4d11eaf6aaa4",
                Title = "The Dark Knight",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2014, 6, 2).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Christopher Nolan")],
                    ["Music Director"] = [new("Hans Zimmer")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Christian Bale"), new("Heath Ledger"), new("Aaron Eckhart")
                }
            },

            new() {
                ID = "b37614e0-abed-4f5a-bd2e-78e4ff7f8deb",
                Title = "The Theory of Everything",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2000, 10, 3).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("James Marsh")],
                    ["Music Director"] = [new("Jóhann Jóhannsson")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Eddie Redmayne"), new("Felicity Jones"), new("Charlie Cox")
                }
            },

            new() {
                ID = "de1aae81-fa95-45da-8ae1-9d153f215a0e",
                Title = "Cast Away",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2023, 1, 9).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Robert Zemeckis")],
                    ["Music Director"] = [new("Alan Silvestri")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Tom Hanks"), new("Helen Hunt"), new("Nick Searcy")
                }
            },

            new() {
                ID = "b65bb93a-626f-4b4c-a63b-c5d1421e42c1",
                Title = "The Revenant",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1992, 5, 25).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Alejandro G. Iñárritu")],
                    ["Music Director"] = [new("Ryuichi Sakamoto")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Leonardo DiCaprio"), new("Tom Hardy"), new("Domhnall Gleeson")
                }
            },

            new() {
                ID = "0159dc52-50f1-453c-9382-dff9392b8133",
                Title = "The Wolf of Wall Street",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2009, 8, 10).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Martin Scorsese")],
                    ["Music Director"] = [new("Various Artists")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Leonardo DiCaprio"), new("Jonah Hill"), new("Margot Robbie")
                }
            },

            new() {
                ID = "48035188-a1d7-4e38-87ef-ff6649c795b0",
                Title = "Inglourious Basterds",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1999, 7, 2).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Quentin Tarantino")],
                    ["Music Director"] = [new("Ennio Morricone")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Brad Pitt"), new("Christoph Waltz"), new("Diane Kruger")
                }
            },

            new() {
                ID = "9134bbb3-8283-4534-b81e-a99850712adb",
                Title = "Forrest Gump",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2021, 4, 12).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Robert Zemeckis")],
                    ["Music Director"] = [new("Alan Silvestri")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Tom Hanks"), new("Robin Wright"), new("Gary Sinise")
                }
            },

            new() {
                ID = "e53e8986-a3ed-4ac2-a842-ae48741c1f90",
                Title = "Black Swan",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1996, 2, 10).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Darren Aronofsky")],
                    ["Music Director"] = [new("Clint Mansell")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Natalie Portman"), new("Mila Kunis"), new("Vincent Cassel")
                }
            },

            new() {
                ID = "139f0e18-8bd3-41a2-a074-2918191c00e2",
                Title = "Knives Out",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2004, 8, 14).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Rian Johnson")],
                    ["Music Director"] = [new("Nathan Johnson")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Daniel Craig"), new("Chris Evans"), new("Ana de Armas")
                }
            },

            new() {
                ID = "e84e19fc-3b6b-41a8-a23a-ce379c7c52e9",
                Title = "Slumdog Millionaire",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2007, 6, 15).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Danny Boyle")],
                    ["Music Director"] = [new("A. R. Rahman")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Dev Patel"), new("Freida Pinto"), new("Anil Kapoor")
                }
            },

            new() {
                ID = "873afb46-de02-41d2-b539-8ff7050c6780",
                Title = "Django Unchained",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1992, 4, 7).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Quentin Tarantino")],
                    ["Music Director"] = [new("Ennio Morricone")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Jamie Foxx"), new("Christoph Waltz"), new("Leonardo DiCaprio")
                }
            },

            new() {
                ID = "b5450a11-d7eb-4f1d-9f18-997205947e8b",
                Title = "Tenet",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2013, 6, 24).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Christopher Nolan")],
                    ["Music Director"] = [new("Ludwig Göransson")]
                },
                Cast = new List<PersonEntity>
                {
                    new("John David Washington"), new("Robert Pattinson"), new("Elizabeth Debicki")
                }
            },

            new() {
                ID = "8a60860c-f1ed-4e77-ae4e-2dbe49082c9d",
                Title = "Oppenheimer",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2003, 2, 21).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Christopher Nolan")],
                    ["Music Director"] = [new("Ludwig Göransson")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Cillian Murphy"), new("Emily Blunt"), new("Robert Downey Jr.")
                }
            },

            new() {
                ID = "42e264e2-25f0-4ba8-826d-c59e8e476adf",
                Title = "The Big Short",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1997, 1, 21).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Adam McKay")],
                    ["Music Director"] = [new("Nicholas Britell")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Christian Bale"), new("Steve Carell"), new("Ryan Gosling")
                }
            },

            new() {
                ID = "dbbe19aa-9d5e-4f11-8237-d9d6ad2939ba",
                Title = "Argo",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2023, 8, 7).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Ben Affleck")],
                    ["Music Director"] = [new("Alexandre Desplat")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Ben Affleck"), new("Bryan Cranston"), new("Alan Arkin")
                }
            },

            new() {
                ID = "0bfcf0cb-3edb-4204-9600-b46fce279bd5",
                Title = "La La Land",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2003, 4, 10).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Damien Chazelle")],
                    ["Music Director"] = [new("Justin Hurwitz")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Ryan Gosling"), new("Emma Stone"), new("John Legend")
                }
            },

            new() {
                ID = "da4ebd41-fc90-4878-937b-5dcbcc3cde21",
                Title = "The Curious Case of Benjamin Button",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2001, 7, 2).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("David Fincher")],
                    ["Music Director"] = [new("Alexandre Desplat")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Brad Pitt"), new("Cate Blanchett"), new("Taraji P. Henson")
                }
            },

            new() {
                ID = "cb14366e-be91-4048-9f70-286b9aff1a29",
                Title = "Dune",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2017, 7, 23).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Denis Villeneuve")],
                    ["Music Director"] = [new("Hans Zimmer")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Timothée Chalamet"), new("Rebecca Ferguson"), new("Oscar Isaac")
                }
            },

            new() {
                ID = "e960f268-daa8-40a9-a06c-67cc58f629fd",
                Title = "Ford v Ferrari",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1992, 1, 16).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("James Mangold")],
                    ["Music Director"] = [new("Marco Beltrami")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Matt Damon"), new("Christian Bale"), new("Caitriona Balfe")
                }
            },

            new() {
                ID = "95b52ef0-fe2e-46fb-a3a3-ca8fbfe5f2ed",
                Title = "Interstellar",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2020, 8, 12).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Christopher Nolan")],
                    ["Music Director"] = [new("Hans Zimmer")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Matthew McConaughey"), new("Anne Hathaway"), new("Jessica Chastain")
                }
            },

            new() {
                ID = "e1b36f00-431e-44a0-a97a-30d0d990280c",
                Title = "The Matrix",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1991, 11, 16).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Lana Wachowski")],
                    ["Music Director"] = [new("Don Davis")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Keanu Reeves"), new("Laurence Fishburne"), new("Carrie-Anne Moss")
                }
            },

            new() {
                ID = "4763c3b4-943e-4395-a711-caf28f8fac15",
                Title = "Gladiator",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2001, 8, 9).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Ridley Scott")],
                    ["Music Director"] = [new("Hans Zimmer")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Russell Crowe"), new("Joaquin Phoenix"), new("Connie Nielsen")
                }
            },

            new() {
                ID = "4c468af3-c65e-43f6-9139-4d73b5d44b03",
                Title = "Whiplash",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1998, 8, 21).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Damien Chazelle")],
                    ["Music Director"] = [new("Justin Hurwitz")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Miles Teller"), new("J.K. Simmons"), new("Paul Reiser")
                }
            },

            new() {
                ID = "d1da19d4-cfdb-4f64-bdb6-735dd99d33d9",
                Title = "A Beautiful Mind",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2008, 2, 21).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Ron Howard")],
                    ["Music Director"] = [new("James Horner")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Russell Crowe"), new("Jennifer Connelly"), new("Paul Bettany")
                }
            },

            new() {
                ID = "60efe29d-1590-47cd-adf6-9c1533fad0d2",
                Title = "Her",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2008, 1, 20).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Spike Jonze")],
                    ["Music Director"] = [new("Arcade Fire")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Joaquin Phoenix"), new("Scarlett Johansson"), new("Amy Adams")
                }
            },

            new() {
                ID = "4579451f-9bc0-4fcf-8db7-96a6ea10c358",
                Title = "Inception",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2011, 6, 9).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Christopher Nolan")],
                    ["Music Director"] = [new("Hans Zimmer")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Leonardo DiCaprio"), new("Joseph Gordon-Levitt"), new("Elliot Page")
                }
            },

            new() {
                ID = "bcc8fcfc-4e3d-4de2-b1b7-ca6d5f7a5dbc",
                Title = "The Hateful Eight",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(2018, 6, 24).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Quentin Tarantino")],
                    ["Music Director"] = [new("Ennio Morricone")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Samuel L. Jackson"), new("Kurt Russell"), new("Jennifer Jason Leigh")
                }
            },

            new() {
                ID = "c339f596-2af8-4bcb-86f7-01d488cfe84e",
                Title = "The Imitation Game",
                MovieLanguage = "English",
                ReleaseDate = new DateOnly(1994, 6, 15).ToDateTime(TimeOnly.MinValue),
                Crew = new Dictionary<string, List<PersonEntity>>
                {
                    ["Director"] = [new("Morten Tyldum")],
                    ["Music Director"] = [new("Alexandre Desplat")]
                },
                Cast = new List<PersonEntity>
                {
                    new("Benedict Cumberbatch"), new("Keira Knightley"), new("Matthew Goode")
                }
            }
            ];
}
