using Application.Dtos.Bill.Requests;
using Domain.Models.Entities;
using System.Collections;
using System.Collections.Generic;

namespace UnitTests.Data.BillService;

class CreateAsyncTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<CreateBillDtoRequest>
            {
                new CreateBillDtoRequest
                {
                    Description = "Test description 1",
                    Value = 1
                },
            },
            new ApplicationUser
            {
                Id = "ownerId1",
                FirstName = "OwnerFirstName",
                LastName = "OwnerLastName",
                Email = "owner@email.com"
            }
        };

        yield return new object[]
        {
            new List<CreateBillDtoRequest>
            {
                new CreateBillDtoRequest
                {
                    Description = "Test description 1",
                    Value = 1
                },
                new CreateBillDtoRequest
                {
                    Description = "Test description 2",
                    Value = 2
                },
            },
            new ApplicationUser
            {
                Id = "ownerId2",
                FirstName = "OwnerFirstName",
                LastName = "OwnerLastName",
                Email = "owner@email.com"
            }
    };

        yield return new object[]
        {
            new List<CreateBillDtoRequest>
            {
                new CreateBillDtoRequest
                {
                    Description = "Test description 1",
                    Value = 1
                },
                new CreateBillDtoRequest
                {
                    Description = "Test description 2",
                    Value = 2
                },
                new CreateBillDtoRequest
                {
                    Description = "Test description 3",
                    Value = 3
                },
            },
            new ApplicationUser
            {
                Id = "ownerId3",
                FirstName = "OwnerFirstName",
                LastName = "OwnerLastName",
                Email = "owner@email.com"
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}