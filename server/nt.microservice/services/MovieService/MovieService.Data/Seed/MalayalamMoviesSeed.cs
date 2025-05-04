using MovieService.Data.Interfaces.Entities;

namespace MovieService.Data.Seed;

internal class MalayalamMoviesSeed
{

    public static IEnumerable<MovieEntity> Movies => [
        new() {
        ID = "6191e634-14c8-45d1-898f-191060cdbec1",
        Title = "Ustad Hotel",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2015, 12, 17).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Anwar Rasheed")],
            ["Music Director"] = [new("Gopi Sundar")]
        },
        Cast = new List<PersonEntity>
        {
            new("Dulquer Salmaan"), new("Thilakan"), new("Nithya Menen")
        }
    },

    new() {
        ID = "bea50cb7-41b6-4a35-b77f-358e8c43f850",
        Title = "Joji",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2018, 9, 5).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Dileesh Pothan")],
            ["Music Director"] = [new("Justin Varghese")]
        },
        Cast = new List<PersonEntity>
        {
            new("Fahadh Faasil"), new("Baburaj"), new("Unnimaya Prasad")
        }
    },

    new() {
        ID = "bd4b80a0-1516-4b71-887b-714c52459f23",
        Title = "Oru Vadakkan Selfie",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2007, 11, 26).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("G. Prajith")],
            ["Music Director"] = [new("Shaan Rahman")]
        },
        Cast = new List<PersonEntity>
        {
            new("Nivin Pauly"), new("Manjima Mohan"), new("Aju Varghese")
        }
    },

    new() {
        ID = "0003be11-f19a-4b9c-a1b2-b7e195b53d3e",
        Title = "Malik",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2021, 2, 25).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Mahesh Narayanan")],
            ["Music Director"] = [new("Sushin Shyam")]
        },
        Cast = new List<PersonEntity>
        {
            new("Fahadh Faasil"), new("Nimisha Sajayan"), new("Joju George")
        }
    },

    new() {
        ID = "af3e9bed-3e04-4f06-856d-3c572605bf4d",
        Title = "Premam",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2022, 3, 15).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Alphonse Puthren")],
            ["Music Director"] = [new("Rajesh Murugesan")]
        },
        Cast = new List<PersonEntity>
        {
            new("Nivin Pauly"), new("Sai Pallavi"), new("Madonna Sebastian")
        }
    },

    new() {
        ID = "47435a1d-6b24-4cfc-b3a6-0be543322187",
        Title = "Jana Gana Mana",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2011, 1, 4).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Dijo Jose Antony")],
            ["Music Director"] = [new("Jakes Bejoy")]
        },
        Cast = new List<PersonEntity>
        {
            new("Prithviraj"), new("Suraj Venjaramoodu"), new("Mamta Mohandas")
        }
    },

    new() {
        ID = "b290c4b1-4559-4a7a-bac5-b936622758d0",
        Title = "Ayyappanum Koshiyum",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(1992, 9, 14).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Sachy")],
            ["Music Director"] = [new("Jakes Bejoy")]
        },
        Cast = new List<PersonEntity>
        {
            new("Biju Menon"), new("Prithviraj"), new("Gowri Nandha")
        }
    },

    new() {
        ID = "332ed142-5a5f-48ef-a98a-bfc4bbfee8d6",
        Title = "Thanmathra",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(1996, 1, 13).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Blessy")],
            ["Music Director"] = [new("Ilaiyaraaja")]
        },
        Cast = new List<PersonEntity>
        {
            new("Mohanlal"), new("Meera Vasudevan"), new("Arjun Lal")
        }
    },

    new() {
        ID = "218a6510-9a01-4bbd-b351-b5a268e3d46c",
        Title = "Maheshinte Prathikaaram",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2011, 10, 11).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Dileesh Pothan")],
            ["Music Director"] = [new("Bijibal")]
        },
        Cast = new List<PersonEntity>
        {
            new("Fahadh Faasil"), new("Anusree"), new("Soubin Shahir")
        }
    },

    new() {
        ID = "b39fc1ce-a243-4392-8abd-019b047d786b",
        Title = "Nayattu",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2017, 5, 25).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Martin Prakkat")],
            ["Music Director"] = [new("Jakes Bejoy")]
        },
        Cast = new List<PersonEntity>
        {
            new("Kunchacko Boban"), new("Joju George"), new("Nimisha Sajayan")
        }
    },

    new() {
        ID = "36d4129a-6da5-47e1-920a-512ef1e324b4",
        Title = "Kumbalangi Nights",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2006, 9, 19).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Madhu C. Narayanan")],
            ["Music Director"] = [new("Sushin Shyam")]
        },
        Cast = new List<PersonEntity>
        {
            new("Fahadh Faasil"), new("Shane Nigam"), new("Soubin Shahir")
        }
    },

    new() {
        ID = "d91956c4-325c-480e-9c45-22e869b2471d",
        Title = "Jacobinte Swargarajyam",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2002, 10, 26).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Vineeth Sreenivasan")],
            ["Music Director"] = [new("Shaan Rahman")]
        },
        Cast = new List<PersonEntity>
        {
            new("Nivin Pauly"), new("Renji Panicker"), new("Lakshmi Ramakrishnan")
        }
    },

    new() {
        ID = "2df29b68-27d2-4e51-b823-e3fc3509c70e",
        Title = "Helen",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(1995, 2, 1).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Mathukutty Xavier")],
            ["Music Director"] = [new("Shaanu Rahman")]
        },
        Cast = new List<PersonEntity>
        {
            new("Anna Ben"), new("Lal"), new("Aju Varghese")
        }
    },

    new() {
        ID = "02c50af7-076a-4f4f-848c-9f4ea75d3ff4",
        Title = "Spadikam",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(1996, 5, 7).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Bhadran")],
            ["Music Director"] = [new("S. P. Venkatesh")]
        },
        Cast = new List<PersonEntity>
        {
            new("Mohanlal"), new("Thilakan"), new("Urvashi")
        }
    },

    new() {
        ID = "51c86f9a-a7b7-45ad-b435-68395545b9a1",
        Title = "Thankam",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2005, 5, 22).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Saheed Arafath")],
            ["Music Director"] = [new("Bijibal")]
        },
        Cast = new List<PersonEntity>
        {
            new("Biju Menon"), new("Vineeth Sreenivasan"), new("Aparna Balamurali")
        }
    },

    new() {
        ID = "c3f11393-6022-43f9-bd55-990dc3dd9688",
        Title = "The Great Indian Kitchen",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2023, 2, 16).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Jeo Baby")],
            ["Music Director"] = [new("Sooraj S. Kurup")]
        },
        Cast = new List<PersonEntity>
        {
            new("Nimisha Sajayan"), new("Suraj Venjaramoodu"), new("Ajitha")
        }
    },

    new() {
        ID = "51aa905d-add5-48f6-b561-06e54ccfb929",
        Title = "Jaya Jaya Jaya Jaya Hey",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2016, 12, 22).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Vipin Das")],
            ["Music Director"] = [new("Ankit Menon")]
        },
        Cast = new List<PersonEntity>
        {
            new("Basil Joseph"), new("Darshana Rajendran"), new("Aju Varghese")
        }
    },

    new() {
        ID = "a427252e-79c8-477d-8e5b-a14b6baeba05",
        Title = "Ishq",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2017, 6, 27).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Anuraj Manohar")],
            ["Music Director"] = [new("Jakes Bejoy")]
        },
        Cast = new List<PersonEntity>
        {
            new("Shane Nigam"), new("Ann Sheetal"), new("Shine Tom Chacko")
        }
    },

    new() {
        ID = "5fdd1144-7a66-4b5a-8f50-3239b4dee5ef",
        Title = "Mumbai Police",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2009, 7, 10).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Rosshan Andrrews")],
            ["Music Director"] = [new("Gopi Sundar")]
        },
        Cast = new List<PersonEntity>
        {
            new("Prithviraj"), new("Jayasurya"), new("Rahman")
        }
    },

    new() {
        ID = "5d68ee73-3626-4fcd-9af6-090e1e78d1ca",
        Title = "Manichitrathazhu",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2013, 3, 28).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Fazil")],
            ["Music Director"] = [new("M. G. Radhakrishnan")]
        },
        Cast = new List<PersonEntity>
        {
            new("Shobana"), new("Suresh Gopi"), new("Mohanlal")
        }
    },

    new() {
        ID = "b6c75c7c-8181-4e64-bd63-69cab4770b14",
        Title = "Traffic",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2004, 5, 25).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Rajesh Pillai")],
            ["Music Director"] = [new("Mejo Joseph")]
        },
        Cast = new List<PersonEntity>
        {
            new("Sreenivasan"), new("Kunchacko Boban"), new("Rahman")
        }
    },

    new() {
        ID = "5172192e-2ff1-4e28-97a0-dd43697e5d77",
        Title = "Ee.Ma.Yau",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2008, 2, 8).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Lijo Jose Pellissery")],
            ["Music Director"] = [new("Prashant Pillai")]
        },
        Cast = new List<PersonEntity>
        {
            new("Chemban Vinod Jose"), new("Vinayakan"), new("Dileesh Pothan")
        }
    },

    new() {
        ID = "680861c8-499a-42af-acba-f4e9af480572",
        Title = "Hridayam",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2012, 9, 4).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Vineeth Sreenivasan")],
            ["Music Director"] = [new("Hesham Abdul Wahab")]
        },
        Cast = new List<PersonEntity>
        {
            new("Pranav Mohanlal"), new("Kalyani Priyadarshan"), new("Darshana Rajendran")
        }
    },

    new() {
        ID = "d3d03c59-25ed-4ec7-87c8-ad0c625ed2ed",
        Title = "Thondimuthalum Driksakshiyum",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2018, 2, 14).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Dileesh Pothan")],
            ["Music Director"] = [new("Bijibal")]
        },
        Cast = new List<PersonEntity>
        {
            new("Fahadh Faasil"), new("Suraj Venjaramoodu"), new("Nimisha Sajayan")
        }
    },

    new() {
        ID = "4fd06781-6891-4427-b5ba-d1bd3b5ea2fa",
        Title = "C U Soon",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2014, 8, 25).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Mahesh Narayanan")],
            ["Music Director"] = [new("Gopi Sundar")]
        },
        Cast = new List<PersonEntity>
        {
            new("Fahadh Faasil"), new("Roshan Mathew"), new("Darshana Rajendran")
        }
    },

    new() {
        ID = "ff3f02e0-b0db-47c2-a32b-0f63636fb408",
        Title = "Puzhu",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2021, 12, 15).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Ratheena")],
            ["Music Director"] = [new("Jakes Bejoy")]
        },
        Cast = new List<PersonEntity>
        {
            new("Mammootty"), new("Parvathy"), new("Appunni Sasi")
        }
    },

    new() {
        ID = "a49b402f-25da-42fc-a455-c80e70ea6b18",
        Title = "Thallumaala",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(1993, 8, 12).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Khalid Rahman")],
            ["Music Director"] = [new("Vishnu Vijay")]
        },
        Cast = new List<PersonEntity>
        {
            new("Tovino Thomas"), new("Kalyani Priyadarshan"), new("Shine Tom Chacko")
        }
    },

    new() {
        ID = "be4ef187-ede2-4ac8-bf95-3e921aef164b",
        Title = "Njan Prakashan",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(1992, 7, 11).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Sathyan Anthikad")],
            ["Music Director"] = [new("Shaan Rahman")]
        },
        Cast = new List<PersonEntity>
        {
            new("Fahadh Faasil"), new("Sreenivasan"), new("Nikhila Vimal")
        }
    },

    new() {
        ID = "769cb7a3-a5f5-475c-867c-480f19d7922f",
        Title = "Kappela",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2021, 5, 4).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Muhammed Musthafa")],
            ["Music Director"] = [new("Sushin Shyam")]
        },
        Cast = new List<PersonEntity>
        {
            new("Anna Ben"), new("Roshan Mathew"), new("Sreenath Bhasi")
        }
    },

    new() {
        ID = "108bb076-7c8e-4502-a97c-7d400f9bf496",
        Title = "Drishyam",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(1991, 1, 6).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Jeethu Joseph")],
            ["Music Director"] = [new("Anil Johnson")]
        },
        Cast = new List<PersonEntity>
        {
            new("Mohanlal"), new("Meena"), new("Ansiba Hassan")
        }
    },

    new() {
        ID = "df951dfb-03f7-453d-9ae3-a4ccccd7c549",
        Title = "Minnal Murali",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2011, 1, 22).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Basil Joseph")],
            ["Music Director"] = [new("Shaan Rahman")]
        },
        Cast = new List<PersonEntity>
        {
            new("Tovino Thomas"), new("Guru Somasundaram"), new("Femina George")
        }
    },

    new() {
        ID = "78076fe7-1038-4874-8544-22991af899ca",
        Title = "Forensic",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2022, 3, 4).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Akhil Paul")],
            ["Music Director"] = [new("Jakes Bejoy")]
        },
        Cast = new List<PersonEntity>
        {
            new("Tovino Thomas"), new("Mamta Mohandas"), new("Reba Monica John")
        }
    },

    new() {
        ID = "4390703b-bf9d-48a3-bf49-acbe29b30f4b",
        Title = "Kaathal - The Core",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2017, 4, 16).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Jeo Baby")],
            ["Music Director"] = [new("Mathews Pulickan")]
        },
        Cast = new List<PersonEntity>
        {
            new("Mammootty"), new("Jyothika"), new("Joji John")
        }
    },

    new() {
        ID = "60e3d234-b1af-4387-aad4-a5ca9aedf171",
        Title = "Virus",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2002, 11, 3).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Aashiq Abu")],
            ["Music Director"] = [new("Sushin Shyam")]
        },
        Cast = new List<PersonEntity>
        {
            new("Kunchacko Boban"), new("Parvathy"), new("Tovino Thomas")
        }
    },

    new() {
        ID = "7f19f1f8-af21-4ef6-8fb7-3cf81085237a",
        Title = "Neelavelicham",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2012, 10, 28).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Aashiq Abu")],
            ["Music Director"] = [new("Bijibal")]
        },
        Cast = new List<PersonEntity>
        {
            new("Tovino Thomas"), new("Rima Kallingal"), new("Roshan Mathew")
        }
    },

    new() {
        ID = "903e9ce5-0c7d-45b9-9899-aedf09aea74a",
        Title = "Kurup",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2002, 7, 16).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Srinath Rajendran")],
            ["Music Director"] = [new("Sushin Shyam")]
        },
        Cast = new List<PersonEntity>
        {
            new("Dulquer Salmaan"), new("Indrajith"), new("Shine Tom Chacko")
        }
    },

    new() {
        ID = "82f690e5-bf03-4468-ae07-58293a69cace",
        Title = "Chithram",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2010, 1, 15).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Priyadarshan")],
            ["Music Director"] = [new("Kannur Rajan")]
        },
        Cast = new List<PersonEntity>
        {
            new("Mohanlal"), new("Ranjini"), new("Nedumudi Venu")
        }
    },

    new() {
        ID = "928f0db4-95aa-4ee5-bd40-677d9640230a",
        Title = "Ennu Ninte Moideen",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2023, 8, 8).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("R. S. Vimal")],
            ["Music Director"] = [new("M. Jayachandran")]
        },
        Cast = new List<PersonEntity>
        {
            new("Prithviraj"), new("Parvathy"), new("Saikumar")
        }
    },

    new() {
        ID = "dbe91110-1928-489c-b005-f4c3b793de78",
        Title = "Pathemari",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2005, 8, 19).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Salim Ahamed")],
            ["Music Director"] = [new("Bijibal")]
        },
        Cast = new List<PersonEntity>
        {
            new("Mammootty"), new("Jewel Mary"), new("Siddique")
        }
    },

    new() {
        ID = "99f1a6a6-0b33-4ae1-a1bd-245d8bb95506",
        Title = "Bangalore Days",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2022, 3, 6).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Anjali Menon")],
            ["Music Director"] = [new("Gopi Sundar")]
        },
        Cast = new List<PersonEntity>
        {
            new("Dulquer Salmaan"), new("Nivin Pauly"), new("Nazriya Nazim")
        }
    },

    new() {
        ID = "9d91f395-cda6-4822-98ab-24191a323ea9",
        Title = "Sandesham",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2003, 3, 4).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Sathyan Anthikad")],
            ["Music Director"] = [new("Johnson")]
        },
        Cast = new List<PersonEntity>
        {
            new("Jayaram"), new("Sreenivasan"), new("Thilakan")
        }
    },

    new() {
        ID = "5b963c8f-89d5-4c44-b8f8-00988218776a",
        Title = "Aavesham",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(1993, 5, 27).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Jithu Madhavan")],
            ["Music Director"] = [new("Sushin Shyam")]
        },
        Cast = new List<PersonEntity>
        {
            new("Fahadh Faasil"), new("Hipzster"), new("Sajin Gopu")
        }
    },

    new() {
        ID = "1a9cdaab-0d51-494a-8082-d2e6852096cf",
        Title = "Take Off",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2017, 2, 2).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Mahesh Narayanan")],
            ["Music Director"] = [new("Gopi Sundar")]
        },
        Cast = new List<PersonEntity>
        {
            new("Parvathy"), new("Kunchacko Boban"), new("Fahadh Faasil")
        }
    },

    new() {
        ID = "8262463d-dd6f-45ca-a4c1-21138c4a8724",
        Title = "Charlie",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2003, 5, 23).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Martin Prakkat")],
            ["Music Director"] = [new("Gopi Sundar")]
        },
        Cast = new List<PersonEntity>
        {
            new("Dulquer Salmaan"), new("Parvathy"), new("Aparna Gopinath")
        }
    },

    new() {
        ID = "dfd19000-8e67-4373-8e0a-54ca15cb4ac7",
        Title = "Sudani from Nigeria",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(1997, 3, 14).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Zakariya")],
            ["Music Director"] = [new("Rex Vijayan")]
        },
        Cast = new List<PersonEntity>
        {
            new("Soubin Shahir"), new("Samuel Abiola Robinson"), new("Savithri Sreedharan")
        }
    },

    new() {
        ID = "7fb60181-f79e-4ef0-ae01-4b67b52dcab5",
        Title = "Rorschach",
        MovieLanguage = "Malayalam",
        ReleaseDate = new DateOnly(2004, 10, 21).ToDateTime(TimeOnly.MinValue),
        Crew = new Dictionary<string, List<PersonEntity>>
        {
            ["Director"] = [new("Nissam Basheer")],
            ["Music Director"] = [new("Midhun Mukundan")]
        },
        Cast = new List<PersonEntity>
        {
            new("Mammootty"), new("Sharaf U Dheen"), new("Jagadish")
        }
    }
    ];


}
