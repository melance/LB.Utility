using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB.Utility
{
    /// <summary>
    /// A class to calculate pagination for lists
    /// </summary>
    public class Pagination
    {
        #region Constants
        private const String DefaultEndCharacter = "…";
        #endregion

        #region Factory
        /// <summary>
        /// The default items per page used by the <see cref="DefaultPagination"/> factory property
        /// </summary>
        public static Int32 ItemsPerPageDefault { get; set; } = 10;
        /// <summary>
        /// The default max pages to display used by the <see cref="DefaultPagination"/> factory property
        /// </summary>
        public static Int32 MaxPagesToDisplayDefault { get; set; } = 10;

        /// <summary>
        /// A default instance of the <see cref="Pagination"/> class
        /// </summary>
        public static Pagination DefaultPagination => new()
        {
            CurrentPage = 1,
            ItemsPerPage = ItemsPerPageDefault,
            MaxPagesToDisplay = MaxPagesToDisplayDefault
        };
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a default pagination object
        /// </summary>
        public Pagination() { }

        /// <summary>
        /// Constructs a pagination object
        /// </summary>
        /// <param name="currentPage">The current page</param>
        /// <param name="totalItems">The total number of items</param>
        /// <param name="itemsPerPage">How many items to display per page</param>
        /// <param name="maxPagesToDisplay">The number of pages to display</param>
        public Pagination(Int32 currentPage, Int32 totalItems, Int32 itemsPerPage, Int32 maxPagesToDisplay)
        {
            TotalItems = totalItems;
            ItemsPerPage = itemsPerPage;
            MaxPagesToDisplay = maxPagesToDisplay;
            CurrentPage = Math.Min(currentPage, PageCount);
        }
        #endregion

        #region Members
        private Int32 _itemsPerPage = ItemsPerPageDefault;
        private Int32 _maxPages = MaxPagesToDisplayDefault;
        private Int32 _currentPage = 1;
        #endregion

        #region Properties
        /// <summary>
        /// The character to display in the <see cref="PageList"/> for continuation in either direction
        /// </summary>
        /// <remarks>Default is an ellipsis character: "…"</remarks>
        public String EndCharacter { get; set; } = DefaultEndCharacter;
        /// <summary>
        /// The current page
        /// </summary>
        [Display(Name = "Current Page")]
        public Int32 CurrentPage { get => _currentPage; set => _currentPage = Math.Max(value, 1); }
        /// <summary>
        /// The total number of items
        /// </summary>
        [Display(Name = "Total Items")]
        public Int32 TotalItems { get; set; }
        /// <summary>
        /// The number of items to display on each page
        /// </summary>
        [Display(Name = "Items Per Page")]
        public Int32 ItemsPerPage { get => _itemsPerPage; set => _itemsPerPage = Math.Max(value, 1); }
        /// <summary>
        /// The maximum number of pages to include in the <see cref="PageList"/>
        /// </summary>
        public Int32 MaxPagesToDisplay { get => _maxPages; set => _maxPages = Math.Max(value, 1); }
        /// <summary>
        /// The total number of pages
        /// </summary>
        public Int32 PageCount => (Int32)Math.Ceiling((Double)TotalItems / ItemsPerPage);
        /// <summary>
        /// The minimum index displayed on the current page
        /// </summary>
        public Int32 MinItem => ((CurrentPage - 1) * ItemsPerPage) + 1;
        /// <summary>
        /// The maximum index displayed on the current page
        /// </summary>
        public Int32 MaxItem => Math.Min(CurrentPage * ItemsPerPage, TotalItems);
        /// <summary>
        /// The list of pages to display
        /// </summary>
        public IEnumerable<(String Display, Int32 PageNumber)>? PageList => GetPageList();
        #endregion

        #region Private Methods
        /// <summary>
        /// Calculates the list of pages
        /// </summary>
        /// <returns>A list of pages to display</returns>
        private IEnumerable<(String Display, Int32 PageNumber)> GetPageList()
        {
            if (PageCount == 0) yield break;
            if (PageCount <= MaxPagesToDisplay)
            {
                for (var i = 1; i <= PageCount; i++)
                {
                    yield return (i.ToString(), i);
                }
            }
            else if (CurrentPage < MaxPagesToDisplay)
            {
                var start = 1;
                var end = MaxPagesToDisplay;
                for (var i = start; i < end; i++)
                {
                    yield return (i.ToString(), i);
                }
                yield return (EndCharacter, end);
            }
            else if (CurrentPage < (PageCount - (MaxPagesToDisplay - 2)))
            {
                var start = CurrentPage - 1;
                var end = CurrentPage + (MaxPagesToDisplay - 3);
                yield return (EndCharacter, start - 1);
                for (var i = start; i < end; i++)
                {
                    yield return (i.ToString(), i);
                }
                yield return (EndCharacter, end);
            }
            else
            {
                var start = PageCount - (MaxPagesToDisplay - 2);
                var end = PageCount;
                yield return (EndCharacter, start - 1);
                for (var i = start; i <= end; i++)
                {
                    yield return (i.ToString(), i);
                }
            }
        } 
        #endregion
    }
}
