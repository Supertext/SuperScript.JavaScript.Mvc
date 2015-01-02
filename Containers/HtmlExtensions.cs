using System;
using System.Web.Mvc;
using SuperScript.Configuration;
using SuperScript.Container.Mvc;
using SuperScript.ExtensionMethods;
using SuperScript.JavaScript.Declarables;
using SuperScript.JavaScript.ExtensionMethods;

namespace SuperScript.JavaScript.Mvc.Containers
{
    public static class HtmlExtensions
    {
        private class InternalJavaScriptContainer : BaseContainer
        {
            // only calls the constructor on the base class
            public InternalJavaScriptContainer(HtmlHelper helper, string emitterKey, int? insertAt)
                : base(helper, emitterKey, insertAt)
            {
            }


            /// <summary>
            /// Append the internal content to the context's cached list of output delegates
            /// </summary>
            public override void Dispose()
            {
                // render the internal content of the injection block helper
                // make sure to pop from the stack rather than just render from the Writer
                // so it will remove it from regular rendering
                var content = WebPage.OutputStack;
                var renderedContent = content.Count == 0
                                          ? string.Empty
                                          : content.Pop().ToString();

                var cleanedContents = InternalLogic.EnsureStripppedContents(renderedContent);

                var declaration = new CollectedScript
                                      {
                                          EmitterKey = EmitterKey,
                                          Value = cleanedContents
                                      };

                if (Configuration.Settings.Instance.AddLocationComments.IsCurrentlyEmittable())
                {
                    declaration.WrapInLocationComment(WebPage.VirtualPath);
                }

                SuperScript.Declarations.AddDeclaration<CollectedScript>(declaration, InsertAt);
            }
        }


        /// <summary>
        /// Start a block of movable JavaScript content.
        /// </summary>
        /// <param name="helper">
        /// The helper from which we use the context.
        /// </param>
        /// <returns>
        /// This extension method will return an empty <see cref="string"/> at runtime.
        /// </returns>
        public static IDisposable JavaScriptContainer(this HtmlHelper helper)
        {
            return new InternalJavaScriptContainer(helper, Settings.Instance.DefaultEmitter.Key, null);
        }


        /// <summary>
        /// Start a block of movable JavaScript content.
        /// </summary>
        /// <param name="helper">
        /// The helper from which we use the context.
        /// </param>
        /// <param name="emitterKey">
        /// <para>Indicates which instance of <see cref="SuperScript.Emitters.IEmitter"/> the content should be added to.</para>
        /// <para>If not specified then the contents will be added to the default implementation of <see cref="SuperScript.Emitters.IEmitter"/>.</para>
        /// </param>
        /// <returns>
        /// This extension method will return an empty <see cref="string"/> at runtime.
        /// </returns>
        public static IDisposable JavaScriptContainer(this HtmlHelper helper, string emitterKey)
        {
            return new InternalJavaScriptContainer(helper, emitterKey, null);
        }


        /// <summary>
        /// Start a block of movable JavaScript content.
        /// </summary>
        /// <param name="helper">
        /// The helper from which we use the context.
        /// </param>
        /// <param name="insertAt">
        /// Indicates the index in the collection at which the contents are to be inserted.
        /// </param>
        /// <returns>
        /// This extension method will return an empty <see cref="string"/> at runtime.
        /// </returns>
        public static IDisposable JavaScriptContainer(this HtmlHelper helper, int insertAt)
        {
            return new InternalJavaScriptContainer(helper, Settings.Instance.DefaultEmitter.Key, insertAt);
        }


        /// <summary>
        /// Start a block of movable JavaScript content.
        /// </summary>
        /// <param name="helper">
        /// The helper from which we use the context.
        /// </param>
        /// <param name="emitterKey">
        /// <para>Indicates which instance of <see cref="SuperScript.Emitters.IEmitter"/> the content should be added to.</para>
        /// <para>If not specified then the contents will be added to the default implementation of <see cref="SuperScript.Emitters.IEmitter"/>.</para>
        /// </param>
        /// <param name="insertAt">
        /// Indicates the index in the collection at which the contents are to be inserted.
        /// </param>
        /// <returns>
        /// This extension method will return an empty <see cref="string"/> at runtime.
        /// </returns>
        public static IDisposable JavaScriptContainer(this HtmlHelper helper, string emitterKey, int insertAt)
        {
            return new InternalJavaScriptContainer(helper, emitterKey, insertAt);
        }
    }
}