﻿using System;

namespace Moon.OData
{
    /// <summary>
    /// OData query options to allow for querying.
    /// </summary>
    [Flags]
    public enum AllowedOptions
    {
        /// <summary>
        /// A value that corresponds to allowing no query options.
        /// </summary>
        None = 0x0,

        /// <summary>
        /// A value that corresponds to allowing the $count query option.
        /// </summary>
        Count = 0x1,

        /// <summary>
        /// A value that corresponds to allowing the $deltatoken query option.
        /// </summary>
        DeltaToken = 0x2,

        /// <summary>
        /// A value that corresponds to allowing the $format query option.
        /// </summary>
        Format = 0x4,

        /// <summary>
        /// A value that corresponds to allowing the $filter query option.
        /// </summary>
        Filter = 0x8,

        /// <summary>
        /// A value that corresponds to allowing the $orderby query option.
        /// </summary>
        OrderBy = 0x10,

        /// <summary>
        /// A value that corresponds to allowing the $search query option.
        /// </summary>
        Search = 0x20,

        /// <summary>
        /// A value that corresponds to allowing the $select query option.
        /// </summary>
        Select = 0x40,

        /// <summary>
        /// A value that corresponds to allowing the $expand query option.
        /// </summary>
        Expand = 0x80,

        /// <summary>
        /// A value that corresponds to allowing the $skip query option.
        /// </summary>
        Skip = 0x100,

        /// <summary>
        /// A value that corresponds to allowing the $skiptoken query option.
        /// </summary>
        SkipToken = 0x200,

        /// <summary>
        /// A value that corresponds to allowing the $top query option.
        /// </summary>
        Top = 0x400,

        /// <summary>
        /// A value that corresponds to allowing all query options.
        /// </summary>
        All = Count | DeltaToken | Format | Filter | OrderBy | Search | Select | Expand | Skip | SkipToken | Top
    }
}