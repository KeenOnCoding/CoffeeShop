﻿namespace CoffeeShop.Api.Hateoas.Resources
{
    public interface IResourceMapper
    {
        Task<TResource> MapAsync<T, TResource>(T source)
            where TResource : Resource;

        Task<TContainer> MapContainerAsync<T, TResource, TContainer>(IEnumerable<T> models)
            where TContainer : ResourceContainer<TResource>, new()
            where TResource : Resource;

        Task<TResource> CreateEmptyResourceAsync<TResource>(Action<TResource> beforeMap = null)
            where TResource : Resource, new();
    }
}