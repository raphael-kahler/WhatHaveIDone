namespace Whid.Functional
{
    /// <summary>
    /// Option class that can either have a content of the specified type (Some) or not have a content (None).
    /// </summary>
    /// <typeparam name="T">The type of the option content.</typeparam>
    public class Option<T>
    {
        /// <summary>
        /// Create a new option value that has no content.
        /// </summary>
        public static Option<T> None { get; } = new None<T>();

        public static implicit operator Option<T>(T content) => new Some<T>(content);
        public static implicit operator Option<T>(Option _) => new None<T>();
    }

    /// <summary>
    /// Option class that has a content of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the option content.</typeparam>
    public class Some<T> : Option<T>
    {
        private T Content { get; }

        /// <summary>
        /// Create a new option value with the specified content.
        /// </summary>
        /// <param name="content">The option content.</param>
        public Some(T content)
        {
            Content = content;
        }

        public static implicit operator T(Some<T> value) => value.Content;
    }

    /// <summary>
    /// Option class that has no content.
    /// </summary>
    /// <typeparam name="T">The type of the option content.</typeparam>
    public class None<T> : Option<T>
    {
    }

    /// <summary>
    /// A non-generic option class that can only be None (have no content).
    /// The non-generic <see cref="Option.None"/> has an implicit cast to the generic <see cref="Option{T}.None"/> and can therefore be used as a shorthand.
    /// </summary>
    public class Option
    {
        private Option() { }

        /// <summary>
        /// An option value that has no content.
        /// </summary>
        public static Option None { get; } = new Option();
    }

}
