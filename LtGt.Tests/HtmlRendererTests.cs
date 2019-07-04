﻿using System.Collections.Generic;
using LtGt.Models;
using NUnit.Framework;

namespace LtGt.Tests
{
    [TestFixture]
    public class HtmlRendererTests
    {
        private static IEnumerable<TestCaseData> GetTestCases_RenderNode()
        {
            yield return new TestCaseData(
                new HtmlDocument(HtmlDeclaration.DoctypeHtml,
                    new HtmlElement("html",
                        new HtmlElement("head",
                            new HtmlElement("title", new HtmlText("Test document")),
                            new HtmlElement("meta", new HtmlAttribute("name", "description"), new HtmlAttribute("content", "Test")),
                            new HtmlComment("Some test comment"),
                            new HtmlElement("script",
                                new HtmlText("let a = Math.random()*100 < 50 ? true : false;"))),
                        new HtmlElement("body",
                            new HtmlElement("div", new HtmlAttribute("id", "content"),
                                new HtmlElement("a", new HtmlAttribute("href", "https://example.com"),
                                    new HtmlText("Test <link>"))))))
            );
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases_RenderNode))]
        public void RenderNode_Test(HtmlNode node)
        {
            // Act
            var actual = HtmlRenderer.Default.RenderNode(node);
            var roundTrip = HtmlParser.Default.ParseNode(actual);

            // Assert
            Assert.That(roundTrip, Is.EqualTo(node).Using(HtmlEntity.EqualityComparer));
        }
    }
}