﻿using System;
using System.Collections.Generic;

namespace Bogus.Extensions
{
   /// <summary>
   /// Extensions for <see cref="Faker{T}"/>.
   /// </summary>
   public static class ExtensionsForFakerT
   {
      /// <summary>
      /// Generate multiple fake objects of T. The number of generated items is randomly chosen between <see cref="min"/> and <see cref="max"/> values.
      /// The random number between <see cref="min"/> and <see cref="max"/> should be considered non-deterministic but technically depends on the parameters each time this method was called.
      /// </summary>
      /// <param name="faker">The <see cref="Faker{T}"/> to extend with this extension method.</param>
      /// <param name="min">Minimum number of T objects to create. Inclusive.</param>
      /// <param name="max">Maximum number of T objects to create. Inclusive.</param>
      public static List<T> GenerateBetween<T>(this Faker<T> faker, int min, int max, string ruleSets = null) where T : class
      {
         var internals = faker as IFakerTInternal;
         var r = internals.FakerHub.Random;
         var n = r.Number(min, max);
         return faker.Generate(n, ruleSets);
      }

      /// <summary>
      /// Helpful extension for creating randomly null values for <seealso cref="Faker{T}"/>.RuleFor() rules.
      /// Example: .RuleFor(x=>x.Prop, f=>f.Random.Word().OrNull(f))
      /// </summary>
      /// <param name="f">The Faker facade. This is usually the f from f => lambda.</param>
      /// <param name="nullWeight">The probability of null occuring. Range [1.0f - 0.0f] (100% and 0%) respectively. For example, if 15% null is desired pass nullWeight = 0.15f.</param>
      public static object OrNull(this object value, Faker f, float nullWeight = 0.5f)
      {
         if (nullWeight > 1 || nullWeight < 0) throw new ArgumentOutOfRangeException(nameof(nullWeight), $".{nameof(OrNull)}() {nameof(nullWeight)} of '{nullWeight}' must be between 1.0f and 0.0f. ");
         return f.Random.Float() > nullWeight ? value : null;
      }

      /// <summary>
      /// Helpful extension for creating randomly default(T) values for <seealso cref="Faker{T}"/>.RuleFor() rules.
      /// Example: .RuleFor(x=>x.Prop, f=>f.Random.Word().OrDefault(f))
      /// </summary>
      /// <param name="f">The Faker facade. This is usually the f from f => lambda.</param>
      /// <param name="defaultWeight">The probability of default(T) occuring. Range [1.0f - 0.0f] (100% and 0%) respectively. For example, if 15% default(T) is desired pass defaultWeight = 0.15f.</param>
      public static T OrDefault<T>(this T value, Faker f, float defaultWeight = 0.5f)
      {
         if (defaultWeight > 1 || defaultWeight < 0) throw new ArgumentOutOfRangeException(nameof(defaultWeight), $".{nameof(OrDefault)}() {nameof(defaultWeight)} of '{defaultWeight}' must be between 1.0f and 0.0f. ");
         return f.Random.Float() > defaultWeight ? value : default;
      }
   }
}