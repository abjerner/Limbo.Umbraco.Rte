using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Skybrud.Essentials.Strings.Extensions;

namespace Limbo.Umbraco.Rte {

    internal class RteUtils {

        public static string? GetTypeName(Type type) {
            return type.AssemblyQualifiedName is { } name ? GetTypeName(name) : null;
        }

        [return: NotNullIfNotNull("typeName")]
        public static string? GetTypeName(string? typeName) {
            return typeName?.Split(',').Take(2).Join(",");
        }

    }

}