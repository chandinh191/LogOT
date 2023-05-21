﻿using LogOT.Application.Common.Exceptions;
using LogOT.Application.TodoLists.Commands.CreateTodoList;
using LogOT.Application.TodoLists.Commands.DeleteTodoList;
using LogOT.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace LogOT.Application.IntegrationTests.TodoLists.Commands;

using static Testing;

public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(new Guid());
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
