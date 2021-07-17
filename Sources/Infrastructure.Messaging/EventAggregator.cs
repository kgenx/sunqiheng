namespace Infrastructure.Messaging {
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
            this.Public