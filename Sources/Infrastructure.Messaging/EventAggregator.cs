﻿namespace Infrastructure.Messaging {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    
    /// <summary>
    ///   Enables loosely-coupled publication of and subscription to events.
    /// </summary>
    public class EventAggregator : IEventAggregator {
        readonly List<Handler> handlers = new List<Handler>();

        /// <summary>
        ///   The default thread marshaller used for publication;
        /// </summary>
        public static Action<System.Action> DefaultPublicationThreadMarshaller = action => action();

        /// <summary>
        /// Processing of handler results on publication thread.
        /// </summary>
        public static Action<object, object> HandlerResultProcessing = (target, result) => { };

        /// <summary>
        ///   Initializes a new instance of the <see cref = "EventAggregator" /> class.
        /// </summary>
        public EventAggregator() {
            this.PublicationThreadMarshaller = DefaultPublicationThreadMarshaller;
        }

        /// <summary>
        ///   Gets or sets the default publication thread marshaller.
        /// </summary>
        /// <value>
        ///   The default publication thread marshaller.
        /// </value>
        public Action<System.Action> PublicationThreadMarshaller { get; set; }

        /// <summary>
        /// Searches the subscribed handlers to check if we have a handler for
        /// the message type supplied.
        /// </summary>
        /// <param name="messageType">The message type to check with</param>
        /// <returns>True if any handler is found, false if not.</returns>
        public bool HandlerExistsFor(Type messageType) {
                return this.handlers.Any(handler => handler.Handles(messageType) & !handler.IsDead);
        }

        /// <summary>
        ///   Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />
        /// </summary>
        /// <param name = "subscriber">The instance to subscribe for event publication.</param>
        public virtual void Subscribe(object subscriber) {
            if (subscriber == null) {
                throw new ArgumentNullException("subscriber");
            }
            lock(this.handlers) {
                if (this.handlers.Any(x => x.Matches(subscriber))) {
                    return;
                }

                this.handlers.Add(new Handler(subscriber));
            }
        }

        /// <summary>
        ///   Unsubscribes the instance from all events.
        /// </summary>
        /// <param name = "subscriber">The instance to unsubscribe.</param>
        public virtual void Unsubscribe(object subscriber) {
            if (subscriber == null) {
                throw new ArgumentNullException("subscriber");
            }
            lock(this.handlers) {
                var found = this.