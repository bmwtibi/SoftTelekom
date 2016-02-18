namespace SoftTelekom.Core.Services
{
    public interface IDialogService
    {
        /// <summary>
        /// Felugró ablak megjelenítése
        /// </summary>
        /// <param name="caption">Felugró ablak címe</param>
        /// <param name="message">Felugó ablak szövege</param>
        void ShowDialogBox(string caption, string message);

        /// <summary>
        /// Ha van felugó ablak akkor azt eltünteti
        /// </summary>
        void HideDialogBox();
        
        /// <summary>
        /// Töltőképernyő megjelneítése
        /// </summary>
        /// <param name="text">Töltőképernyő információs szövege</param>
        void ShowProgressBar(string text);

        /// <summary>
        /// Töltőképernyő elrelytése
        /// </summary>
        void HideProgressBar();
    }
}