using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Web.Extensions
{
    /// <summary>
    /// MVC action result that generates the file content using a delegate that writes the content directly to the output stream.
    /// </summary>
    public class FileGeneratingResult : FileResult
    {
        /// <summary>
        /// The delegate that will generate the file content.
        /// </summary>
        private readonly Action<Stream> _content;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileGeneratingResult" /> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="content">the contents of the file.</param>
        public FileGeneratingResult(string fileName, string contentType, Action<Stream> content)
            : base(contentType)
        {
            if (content == null)
                throw new ArgumentNullException("content");

            _content = content;
            FileDownloadName = fileName;
        }

        /// <summary>
        /// Writes the file to the response.
        /// </summary>
        /// <param name="response">The response object.</param>
        protected override void WriteFile(HttpResponseBase response)
        {
            _content(response.OutputStream);
        }
    }
}