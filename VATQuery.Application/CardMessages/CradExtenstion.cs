using Kook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VATQuery.Application.CardMessages {
    public static class CradExtenstion {
        public static CardBuilder AddSmallTitle(this CardBuilder builder, string title) {
            return builder.AddModule(new ContextModuleBuilder()
                .AddElement(new KMarkdownElementBuilder().WithContent($"> {title}")));
        }

        public static CardBuilder AddSmallDescrption(this CardBuilder builder, string descrption) {
            return builder.AddModule(new ContextModuleBuilder()
                .AddElement(new KMarkdownElementBuilder().WithContent(descrption)));
        }

        public static CardBuilder AddHeader(this CardBuilder builder, string title) {
            return builder.AddModule(new HeaderModuleBuilder()
                .WithText(new PlainTextElementBuilder().WithContent(title)));
        }

        public static CardBuilder AddDivider(this CardBuilder builder) {
            return builder.AddModule(new DividerModuleBuilder());
        }

        public static ParagraphStructBuilder AddParagraphStructField(this ParagraphStructBuilder builder, string kmarkdown) {
            return builder.AddField(new KMarkdownElementBuilder().WithContent(kmarkdown));
        }

        public static ParagraphStructBuilder AddParagraphStructContentField(this ParagraphStructBuilder builder, string title, string content) {
            return builder.AddParagraphStructField($"> {title}\n{content}");
        }
    }
}
