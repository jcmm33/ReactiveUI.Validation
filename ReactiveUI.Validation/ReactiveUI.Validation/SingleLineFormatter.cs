﻿using System;

namespace ReactiveUI.Validation
{
    /// <summary>
    /// Helper class to generate a single formatted line for a <see cref="ValidationText"/>
    /// </summary>
    public class SingleLineFormatter : IValidationTextFormatter<string>
    {
        private readonly string _separator;

        /// <summary>
        /// Create an instance with an optional, custom separator.
        /// </summary>
        /// <param name="separator"></param>
        public SingleLineFormatter(string separator = null)
        {
            _separator = separator;
        }

        public string Format(ValidationText validationText)
        {
            return validationText != null ? validationText.ToSingleLine(_separator) : string.Empty;
        }

        public static SingleLineFormatter Default { get; } = new SingleLineFormatter();
    }
}