using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace TypeLibrary.Services.Tests
{
    public class LibraryServiceTests
    {
        private readonly ILibraryService _libraryService;

        public LibraryServiceTests(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [Theory]
        [InlineData("Fake_Node_A")]
        public async Task GetNode_Returns_Correct_Object(string id)
        {
            var test = await _libraryService.GetNode(id);
            Assert.NotNull(test);
            Assert.Equal(id, test.Id);
        }

        [Theory]
        [InlineData("Stupid_Fake")]
        public async Task GetNode_No_Matching_Id_Throws_MimirorgNotFoundException(string id)
        {
            _ = await Assert.ThrowsAsync<MimirorgNotFoundException>(() => _libraryService.GetNode(id));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task GetNode_Missing_Id_Throws_MimirorgBadRequestException(string id)
        {
            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _libraryService.GetNode(id));
        }

        [Theory]
        [InlineData("", "Fake_Rds", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00 +03:00")]
        [InlineData(" ", "Fake_Rds", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00 +03:00")]
        [InlineData(null, "Fake_Rds", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00 +03:00")]
        [InlineData("Invalid_Node_Object", "", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00 +03:00")]
        [InlineData("Invalid_Node_Object", " ", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00 +03:00")]
        [InlineData("Invalid_Node_Object", null, "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00 +03:00")]
        [InlineData("Invalid_Node_Object", "Fake_Rds", "", "Test Tester", "2022-01-01T00:00:00 +03:00")]
        [InlineData("Invalid_Node_Object", "Fake_Rds", " ", "", "2022-01-01T00:00:00 +03:00")]
        [InlineData("Invalid_Node_Object", "Fake_Rds", null, "Test Tester", "2022-01-01T00:00:00 +03:00")]
        [InlineData("Invalid_Node_Object", "Fake_Rds", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00")]
        [InlineData("Invalid_Node_Object", "Fake_Rds", "Fake_Purpose", "Test Tester", "")]
        [InlineData("Invalid_Node_Object", "Fake_Rds", "Fake_Purpose", "Test Tester", null)]
        [InlineData("Invalid_Node_Object", "Fake_Rds", "Fake_Purpose", "Test Tester", " ")]
        public async Task Create_Node_Returns_MimirorgBadRequestException_When_Missing_Parameters(string name, string rds, string purpose, string createdBy, string created)
        {
            var s = created.ParseUtcDateTime();

            var nodeToCreate = new NodeLibAm
            {
                Name = name,
                RdsId = rds,
                PurposeId = purpose,
                CreatedBy = createdBy,
                Created = s ?? DateTime.MinValue
            };

            _ = await Assert.ThrowsAsync<MimirorgBadRequestException>(() => _libraryService.CreateNode(nodeToCreate));
        }

        [Theory]
        [InlineData("Invalid_Node_Object", "Fake_Rds", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00 +03:00")]
        public async Task Create_Node_Returns_MimirorgDuplicateException_When_Already_Exist(string name, string rds, string purpose, string createdBy, string created)
        {
            var s = created.ParseUtcDateTime();

            var nodeToCreate = new NodeLibAm
            {
                Name = name,
                RdsId = rds,
                PurposeId = purpose,
                CreatedBy = createdBy,
                Created = s ?? DateTime.MinValue
            };

            await _libraryService.CreateNode(nodeToCreate);
            _ = await Assert.ThrowsAsync<MimirorgDuplicateException>(() => _libraryService.CreateNode(nodeToCreate));
        }

        [Fact]
        public async Task Create_Node_Returns_MimirorgNullReferenceException_When_Null_Parameters()
        {
            _ = await Assert.ThrowsAsync<MimirorgNullReferenceException>(() => _libraryService.CreateNode(null));
        }

        [Theory]
        [InlineData("Ok_Object_1", "Fake_Rds", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00 +03:00")]
        [InlineData("Ok_Object_1_2", "Fake_Rds", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00z")]
        [InlineData("Ok_Object_1_3", "Fake_Rds", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00Z")]
        [InlineData("Ok_Object_1_4", "Fake_Rds", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00 z")]
        [InlineData("Ok_Object_1_5", "Fake_Rds", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00 Z")]
        public async Task Create_Node_Create_Node_When_Ok_Parameters(string name, string rds, string purpose, string createdBy, string created)
        {
            var s = created.ParseUtcDateTime();

            var nodeToCreate = new NodeLibAm
            {
                Name = name,
                RdsId = rds,
                PurposeId = purpose,
                CreatedBy = createdBy,
                Created = s ?? DateTime.MinValue,
                Aspect = Aspect.Function
            };

            var id = $"{name}-{rds}-{nodeToCreate.Aspect}-{nodeToCreate.Version}".CreateMd5();

            var node = await _libraryService.CreateNode(nodeToCreate);
            Assert.NotNull(node);
            Assert.Equal(name, node.Name);
            Assert.Equal(rds, node.RdsId);
            Assert.Equal(purpose, node.PurposeId);
            Assert.Equal(createdBy, node.CreatedBy);
            Assert.Equal(nodeToCreate.Created, node.Created);
            Assert.Equal(nodeToCreate.Aspect, node.Aspect);
            Assert.Equal(id, node.Id);
        }

        [Theory]
        [InlineData("Ok_Object", "Fake_Rds", "Fake_Purpose", "Test Tester", "2022-01-01T00:00:00 +03:00")]
        public async Task Create_Node_Node_With_Attributes_Result_Ok(string name, string rds, string purpose, string createdBy, string created)
        {
            var s = created.ParseUtcDateTime();

            var nodeToCreate = new NodeLibAm
            {
                Name = name,
                RdsId = rds,
                PurposeId = purpose,
                CreatedBy = createdBy,
                Created = s ?? DateTime.MinValue,
                Aspect = Aspect.Function,
                AttributeIdList = new List<string>
                {
                    "Fake_Attribute_A"
                }
            };

            var id = $"{name}-{rds}-{nodeToCreate.Aspect}-{nodeToCreate.Version}".CreateMd5();

            var node = await _libraryService.CreateNode(nodeToCreate);
            Assert.NotNull(node);
            Assert.Equal(name, node.Name);
            Assert.Equal(rds, node.RdsId);
            Assert.Equal(purpose, node.PurposeId);
            Assert.Equal(createdBy, node.CreatedBy);
            Assert.Equal(nodeToCreate.Created, node.Created);
            Assert.Equal(nodeToCreate.Aspect, node.Aspect);
            Assert.Equal(id, node.Id);
        }
    }
}