using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseRepository.Models
{
    /// <summary>
    /// Статья.
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Дата создания по UTC.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Заголовок статьи.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст статьи.
        /// </summary>
        public string Text { get; set; }

    }
}
