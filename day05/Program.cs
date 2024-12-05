var input = File.ReadLines("input.txt").ToArray();

var pageOrderingRules = input[..1176].Select(x => (x[..2], x[3..])).GroupBy(x => x.Item1)
    .ToDictionary(x => x.Key, x => x.Select(y => y.Item2).ToArray());

var updates = input[1177..].Select(x => x.Split(',')).ToArray();

Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => updates.Where(PagesAreOrdered).Sum(MiddlePageNumber),
    "part2" => updates.Where(PagesAreNotOrdered).Select(OrderPages).Sum(MiddlePageNumber),
    _ => "Please run with environment variable part=part1 or part=part2"
});

bool PageIsOrdered(string page, string[] nextPages) => nextPages.All(nextPage => pageOrderingRules[page].Contains(nextPage));
bool PagesAreOrdered(string[] pages) => pages.Select((page, i) => PageIsOrdered(page, pages[(i + 1)..])).All(x => x);
bool PagesAreNotOrdered(string[] pages) => !PagesAreOrdered(pages);
int MiddlePageNumber(string[] pages) => int.Parse(pages[pages.Length / 2]);
string[] OrderPages(string[] pages) => pages.OrderBy(page => pageOrderingRules[page].Intersect(pages).Count()).ToArray();