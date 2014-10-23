# Challenge - Design & Implement stable sort method

Given the data structures defined in the DataStructures.cs (and considering there may be more data structures implementing BoxPositioned),
design and implement the StableSort method so that it meets the following criteria.

Note: usually elements implementing BoxPositioned are already almost sorted, with a few elements that are not. 

```csharp
static void Main(string[] args)
{
    Token[] tokens = Generator.GenerateTokens();
    Word[] words = Generator.GenerateWords();

    tokens.OrderByDescending(t => t.Box.Top).ThenBy(t => t.Box.Left);
    StableSort(tokens);
    StableSort(words);	
	
	// use tokens and words to do other stuff
}
```

1. after calling the StableSort method the tokens and words must be sorted on Box.Top descending then on Box.Left ascending
(equivalent of tokens.OrderByDescending(t => t.Box.Top).ThenBy(t => t.Box.Left) )
2. a stable sorting algorithm must be used (Stable sorting algorithms maintain the relative order of records with equal keys)
2. arrays must be sorted in-place by swapping elements and no copies should be made
3. method is called many times so it must be as efficient as possible <- this is very important
