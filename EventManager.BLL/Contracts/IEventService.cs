﻿using EventManager.DAL.Entities;
using EventManager.Models.DTO;
using EventManager.Util.Models;

namespace EventManager.BLL.Contracts
{
    /// <summary>
    /// The IEventService interface defines methods for managing events.
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Retrieves a paged list of all events.
        /// </summary>
        /// <param name="pagerSettings">PagerSettings object for pagination.</param>
        /// <returns>A paged list of EventDTO objects representing the events.</returns>
        Task<PagedList<EventDTO>> AllEvents(PagerSettings pagerSettings);

        /// <summary>
        /// Retrieves a paged list of events for a specific user by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the events are to be retrieved.</param>
        /// <param name="pagerSettings">PagerSettings object for pagination.</param>
        /// <returns>A paged list of EventDTO objects representing the user's events.</returns>
        Task<PagedList<EventDTO>> AllUserEvents(int userId, PagerSettings pagerSettings);

        /// <summary>
        /// Retrieves a paged list of past events for a specific user by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user for whom the past events are to be retrieved.</param>
        /// <param name="pagerSettings">PagerSettings object for pagination.</param>
        /// <returns>A paged list of EventDTO objects representing the user's past events.</returns>
        Task<PagedList<EventDTO>> AllUserPastEvents(int userId, PagerSettings pagerSettings);

        /// <summary>
        /// Retrieves an event by event ID.
        /// </summary>
        /// <param name="eventId">The ID of the event to retrieve.</param>
        /// <returns>The EventDTO object representing the event, or null if not found.</returns>
        Task<EventDTO?> GetEventById(int eventId);

        /// <summary>
        /// Deletes an event by event ID.
        /// </summary>
        /// <param name="eventId">The ID of the event to be deleted.</param>
        /// <returns>True if the event is deleted successfully, otherwise false.</returns>
        Task<bool> DeleteEvent(int eventId);

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="eventDto">The EventDTO object containing the event information.</param>
        /// <returns>The ID of the created event.</returns>
        Task<int> CreateEvent(EventDTO eventDto);

        /// <summary>
        /// Updates an existing event.
        /// </summary>
        /// <param name="eventDto">The EventDTO object containing the updated event information.</param>
        /// <returns>True if the event is updated successfully, otherwise false.</returns>
        Task<bool> UpdateEvent(EventDTO eventDto);

        /// <summary>
        /// Retrieves a list of top N events.
        /// </summary>
        /// <param name="count">The number of top events to retrieve.</param>
        /// <returns>A list of EventDTO objects representing the top events.</returns>
        Task<List<EventDTO>> GetTopNEvents(int count);

        /// <summary>
        /// Publishes an event.
        /// </summary>
        /// <param name="data">The PublishDTO object containing the event data to be published.</param>
        /// <returns>True if the event is published successfully, otherwise false.</returns>
        Task<bool> PublishEvent(PublishDTO data);

        /// <summary>
        /// Retrieves a paged list of registered users for a specific event by event ID.
        /// </summary>
        /// <param name="eventId">The ID of the event for which the registered users are to be retrieved.</param>
        /// <param name="pagerSettings">PagerSettings object for pagination.</param>
        /// <returns>A paged list of RegisteredUsersDto objects representing the registered users.</returns>
        Task<PagedList<RegisteredUsersDto>> GetRegisteredUsers(int eventId, PagerSettings pagerSettings);

        /// <summary>
        /// Retrieves a paged list of events filtered by filterDTO.
        /// </summary>
        /// <param name="filterDTO">The FilterDTO object containing the filter settings.</param>
        /// <returns>A paged list of EventDTO objects representing the filtered events.</returns>
        Task<PagedList<EventDTO>> FilterEvents(FilterDTO filterDTO);
    }
}
