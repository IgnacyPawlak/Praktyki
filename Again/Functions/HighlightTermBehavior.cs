using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Again.Functions
{
    public static class HighlightTermBehavior
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
            "Text",
            typeof(string),
            typeof(HighlightTermBehavior),
            new FrameworkPropertyMetadata("", OnTextChanged));
        public static string GetText(FrameworkElement frameworkElement) => (string)frameworkElement.GetValue(TextProperty);
        public static void SetText(FrameworkElement frameworkElement, string value) => frameworkElement.SetValue(TextProperty, value);
        public static readonly DependencyProperty TermToBeHighlightedProperty = DependencyProperty.RegisterAttached(
            "TermToBeHighlighted",
            typeof(List<string>),
            typeof(HighlightTermBehavior),
            new FrameworkPropertyMetadata(new List<string>(), OnTextChanged));
        public static List<string> GetTermToBeHighlighted(FrameworkElement frameworkElement)
        {
            return (List<string>)frameworkElement.GetValue(TermToBeHighlightedProperty);
        }

        public static void SetTermToBeHighlighted (FrameworkElement frameworkElement, string value)
        {
            frameworkElement.SetValue(TermToBeHighlightedProperty, value);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBlock textBlock)
                SetTextBlockTextAndHighlightTerm(textBlock, GetText(textBlock), GetTermToBeHighlighted(textBlock));
        }

        private static void SetTextBlockTextAndHighlightTerm(TextBlock textBlock, string text, List<string> termToBeHighlighted)
        {
            textBlock.Text = string.Empty;
            if (TextIsEmpty(text))
                return;
            foreach (var item in termToBeHighlighted)
            {
                if (TextIsNotContainingTermToBeHighlighted(text, termToBeHighlighted))
                {
                    AddPartToTextBlock(textBlock, text);
                    return;
                }
            }
            AddPartToTextBlockAndHighlightIfNecessarry(textBlock, termToBeHighlighted, text);
        }

        private static void AddPartToTextBlockAndHighlightIfNecessarry(TextBlock textBlock, List<string> termToBeHighlighted, string textPart)
        {
            List<string> FoundRegexMatches = new List<string>();
            for (int i = 0; i < termToBeHighlighted.Count; i++)
            {
                List<string> textParts = SplitTextIntoTermAndNotTermParts(textPart, termToBeHighlighted[i]);                
                for (int j = 0; j < textParts.Count; j++)
                {
                    Regex regex = new Regex(termToBeHighlighted[i]);
                    if(regex.Match(textParts[i])==null)
                    {
                        if (j % 2 == 0)
                        {
                            FoundRegexMatches.Add(textParts[j]);
                        }
                    }
                    else
                    {
                        if (j % 2 != 0)
                        {
                            FoundRegexMatches.Add(textParts[j]);
                        }
                    }                    
                }
            }
            List<string> test = textPart.Split(FoundRegexMatches.ToArray(), StringSplitOptions.None).ToList();
            for (int i = 0; i < test.Count; i++)
            {
                AddPartToTextBlock(textBlock, test[i]);
                if(i<test.Count-1)
                {
                    try
                    {
                        AddHighlightedPartToTextBlock(textBlock, FoundRegexMatches[i]);

                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        public static List<string> SplitTextIntoTermAndNotTermParts(string text, string term)
        {
            if (String.IsNullOrEmpty(text))
                return new List<string>() { string.Empty };
            List<string> parts = new List<string>();
                List<string> temp = Regex.Split(text, $@"({term})").Where(p => p != string.Empty).ToList();
                if (temp.Count>1)
                {
                    foreach (var part in temp)
                    {
                        parts.Add(part);
                    }
                }
            return parts;
        }

        private static void AddHighlightedPartToTextBlock(TextBlock textBlock, string part)
        {
            textBlock.Inlines.Add(new Run { Text = part, FontWeight = FontWeights.Bold ,Background = Brushes.Yellow , Foreground = Brushes.Black});
        }

        private static void AddPartToTextBlock(TextBlock textBlock, string part)
        {
            textBlock.Inlines.Add(new Run { Text = part, FontWeight = FontWeights.Light });
        }

        private static bool TextIsNotContainingTermToBeHighlighted(string text, List<string> termToBeHighlighted)
        {
            bool textisnotcontainingtermtobehighlighted = true;
            foreach (var item in termToBeHighlighted)
            {
                Regex regex = new Regex(item);
                MatchCollection mc = regex.Matches(text);
                if (mc.Count > 0)
                {
                    textisnotcontainingtermtobehighlighted = false;
                }
            }
            return textisnotcontainingtermtobehighlighted;
        }

        private static bool TextIsEmpty(string text)
        {
            return text.Length == 0;
        }
    }
}
