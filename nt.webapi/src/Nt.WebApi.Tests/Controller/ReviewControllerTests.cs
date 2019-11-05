using AutoMapper;
using Nt.WebApi.Profiles;
using Nt.WebApi.Shared.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.WebApi.Tests.Controller
{
    public class ReviewControllerTests : BaseControllerTests<ReviewEntity>
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MovieEntityProfile());
                mc.AddProfile(new UserEntityProfile());
            });

            _mapper = mappingConfig.CreateMapper();
            InitliazeCollection();
        }
        private void InitliazeCollection()
        {
            EntityCollection = Enumerable.Range(1, 10).Select(x => new ReviewEntity
            {
                MovieId = 1,
                ReviewTitle = $"Title {x}",
                ReviewDescription = $"Description {x}",
            }).ToList();
        }



    }
}
