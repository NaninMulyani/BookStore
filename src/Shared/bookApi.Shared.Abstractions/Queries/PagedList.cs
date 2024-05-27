﻿namespace bookApi.Shared.Abstractions.Queries;

/// <summary>
/// Represents the paged list.
/// </summary>
/// <typeparam name="T">The type of the response.</typeparam>
public sealed class PagedList<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedList{T}"/> class.
    /// </summary>
    /// <param name="items">The readonly collection of items.</param>
    /// <param name="totalCount">The total count of items.</param>
    /// <param name="page">The page.</param>
    /// <param name="pageSize">The page size.</param>
    public PagedList(IReadOnlyCollection<T> items, int page, int pageSize)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
    }

    /// <summary>
    /// Gets the empty paged result for the specified type.
    /// </summary>
    public static PagedList<T> Empty => new(Array.Empty<T>(), 0, 0);

    /// <summary>
    /// Gets the readonly collection of items.
    /// </summary>
    public IReadOnlyCollection<T> Items { get; }

    /// <summary>
    /// Gets the page.
    /// </summary>
    public int Page { get; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Gets a value indicating whether or not there is a next page.
    /// </summary>
    public bool HasNextPage => PageSize < Items.Count;
}