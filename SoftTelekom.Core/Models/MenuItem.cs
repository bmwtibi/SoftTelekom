using System;
using SoftTelekom.Core.Models.Bases;

namespace SoftTelekom.Core.Models
{

    public class MenuItem : BaseModel
    {
        /// <summary>
        /// Menü neve
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A  menü valóban-e egy menüpont vagy csak egy cím az alatta lévő menükhöz
        /// </summary>
        public bool MenuTitle { get; set; }

        /// <summary>
        /// ViewModel tipusa
        /// </summary>
        public Type ViewModelType { get; set; }

        /// <summary>
        /// Szülő ViewModel típusa
        /// </summary>
        public Type ParentViewModelType { get; set; }

        /// <summary>
        /// Menü indexe
        /// </summary>
        public int MenuIndex { get; set; }
    }
}