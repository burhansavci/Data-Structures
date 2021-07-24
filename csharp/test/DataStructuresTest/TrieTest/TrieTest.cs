using System;
using DataStructures.Trie;
using Xunit;

namespace DataStructuresTest.TrieTest
{
    public class TrieTest
    {
        [Fact]
        public void TestBadTrieDelete1() =>
                Assert.Throws<ArgumentException>(() =>
                {
                    var t = new Trie();
                    t.Insert("some string");
                    t.Delete("some string", 0);
                });

        [Fact]
        public void TestBadTrieDelete2() =>
                Assert.Throws<ArgumentException>(() =>
                {
                    var t = new Trie();
                    t.Insert("some string");
                    t.Delete("some string", -1);
                });

        [Fact]
        public void TestBadTrieDelete3() =>
                Assert.Throws<ArgumentException>(() =>
                {
                    var t = new Trie();
                    t.Insert("some string");
                    t.Delete("some string", -345);
                });

        [Fact]
        public void TestBadTrieInsert() => Assert.Throws<ArgumentException>(() => new Trie().Insert(null));

        [Fact]
        public void TestBadTrieCount() => Assert.Throws<ArgumentException>(() => new Trie().Count(null));

        [Fact]
        public void TestBadTrieContains() => Assert.Throws<ArgumentException>(() => new Trie().Contains(null));

        [Fact]
        public void TestContains()
        {
            var t1 = new Trie();

            // This implementation doesn't Count the empty string as
            // a valid string to be Inserted into the trie (although it
            // would be easy to acCount for)
            t1.Insert("");
            Assert.False(t1.Contains(""));
            t1.Insert("");
            Assert.False(t1.Contains(""));
            t1.Insert("");
            Assert.False(t1.Contains(""));

            var t2 = new Trie();
            t2.Insert("aaaaa");
            t2.Insert("aaaaa");
            t2.Insert("aaaaa");
            t2.Insert("aaaaa");
            t2.Insert("aaaaa");

            Assert.True(t2.Contains("aaaaa"));
            Assert.True(t2.Contains("aaaa"));
            Assert.True(t2.Contains("aaa"));
            Assert.True(t2.Contains("aa"));
            Assert.True(t2.Contains("a"));

            var t3 = new Trie();

            t3.Insert("AE");
            t3.Insert("AE");
            t3.Insert("AH");
            t3.Insert("AH");
            t3.Insert("AH7");
            t3.Insert("A7");
            t3.Insert("7");
            t3.Insert("7");
            t3.Insert("B");
            t3.Insert("B");
            t3.Insert("B");
            t3.Insert("B");

            Assert.True(t3.Contains("A"));
            Assert.True(t3.Contains("AH"));
            Assert.True(t3.Contains("A7"));
            Assert.True(t3.Contains("AE"));
            Assert.True(t3.Contains("AH7"));
            Assert.True(t3.Contains("7"));
            Assert.True(t3.Contains("B"));

            Assert.False(t3.Contains("Ar"));
            Assert.False(t3.Contains("A8"));
            Assert.False(t3.Contains("AH6"));
            Assert.False(t3.Contains("C"));
        }

        [Fact]
        public void TestCount()
        {
            var t1 = new Trie();

            // This implementation doesn't Count the empty string as
            // a valid string to be Inserted into the trie (although it
            // would be easy to acCount for)
            t1.Insert("");
            Assert.Equal(0, t1.Count(""));
            t1.Insert("");
            Assert.Equal(0, t1.Count(""));
            t1.Insert("");
            Assert.Equal(0, t1.Count(""));

            var t2 = new Trie();
            t2.Insert("aaaaa");
            t2.Insert("aaaaa");
            t2.Insert("aaaaa");
            t2.Insert("aaaaa");
            t2.Insert("aaaaa");
            Assert.Equal(5, t2.Count("aaaaa"));
            Assert.Equal(5, t2.Count("aaaa"));
            Assert.Equal(5, t2.Count("aaa"));
            Assert.Equal(5, t2.Count("aa"));
            Assert.Equal(5, t2.Count("a"));

            var t3 = new Trie();
            t3.Insert("AE");
            t3.Insert("AE");
            t3.Insert("AH");
            t3.Insert("AH");
            t3.Insert("AH7");
            t3.Insert("A7");

            t3.Insert("7");
            t3.Insert("7");

            t3.Insert("B");
            t3.Insert("B");
            t3.Insert("B");
            t3.Insert("B");

            Assert.Equal(6, t3.Count("A"));
            Assert.Equal(3, t3.Count("AH"));
            Assert.Equal(1, t3.Count("A7"));
            Assert.Equal(2, t3.Count("AE"));
            Assert.Equal(1, t3.Count("AH7"));
            Assert.Equal(2, t3.Count("7"));
            Assert.Equal(0, t3.Count("Ar"));
            Assert.Equal(0, t3.Count("AB"));
            Assert.Equal(0, t3.Count("AH6"));
            Assert.Equal(0, t3.Count("C"));
        }

        [Fact]
        public void TestInsert()
        {
            var t = new Trie();
            Assert.False(t.Insert("a"));
            Assert.False(t.Insert("b"));
            Assert.False(t.Insert("c"));
            Assert.False(t.Insert("d"));
            Assert.False(t.Insert("x"));

            Assert.True(t.Insert("ab"));
            Assert.True(t.Insert("xkcd"));
            Assert.True(t.Insert("dogs"));
            Assert.True(t.Insert("bears"));

            Assert.False(t.Insert("mo"));
            Assert.True(t.Insert("mooooose"));

            t.Clear();

            Assert.False(t.Insert("aaaa", 4));
            Assert.Equal(4, t.Count("aaaa"));

            Assert.True(t.Insert("aaa", 3));
            Assert.Equal(7, t.Count("a"));
            Assert.Equal(7, t.Count("aa"));
            Assert.Equal(7, t.Count("aaa"));
            Assert.Equal(4, t.Count("aaaa"));
            Assert.Equal(0, t.Count("aaaaa"));

            Assert.True(t.Insert("a", 5));
            Assert.Equal(12, t.Count("a"));
            Assert.Equal(7, t.Count("aa"));
            Assert.Equal(7, t.Count("aaa"));
            Assert.Equal(4, t.Count("aaaa"));
            Assert.Equal(0, t.Count("aaaaa"));
        }

        [Fact]
        public void TestClear()
        {
            var t = new Trie();
            Assert.False(t.Insert("a"));
            Assert.False(t.Insert("b"));
            Assert.False(t.Insert("c"));

            Assert.True(t.Contains("a"));
            Assert.True(t.Contains("b"));
            Assert.True(t.Contains("c"));

            t.Clear();

            Assert.False(t.Contains("a"));
            Assert.False(t.Contains("b"));
            Assert.False(t.Contains("c"));

            t.Insert("aaaa");
            t.Insert("aaab");
            t.Insert("aaab5");
            t.Insert("aaac");
            t.Insert("aaacb");

            Assert.True(t.Contains("aaa"));
            Assert.True(t.Contains("aaacb"));
            Assert.True(t.Contains("aaab5"));

            t.Clear();

            Assert.False(t.Contains("aaaa"));
            Assert.False(t.Contains("aaab"));
            Assert.False(t.Contains("aaab5"));
            Assert.False(t.Contains("aaac"));
            Assert.False(t.Contains("aaacb"));
        }

        [Fact]
        public void TestDelete()
        {
            var t = new Trie();
            t.Insert("AAC");
            t.Insert("AA");
            t.Insert("A");
            Assert.True(t.Delete("AAC"));
            Assert.False(t.Contains("AAC"));
            Assert.True(t.Contains("AA"));
            Assert.True(t.Contains("A"));

            Assert.True(t.Delete("AA"));
            Assert.False(t.Contains("AAC"));
            Assert.False(t.Contains("AA"));
            Assert.True(t.Contains("A"));

            Assert.True(t.Delete("A"));
            Assert.False(t.Contains("AAC"));
            Assert.False(t.Contains("AA"));
            Assert.False(t.Contains("A"));

            t.Clear();

            t.Insert("AAC");
            t.Insert("AA");
            t.Insert("A");

            Assert.True(t.Delete("AA"));
            Assert.True(t.Delete("AA"));

            Assert.False(t.Contains("AAC"));
            Assert.False(t.Contains("AA"));
            Assert.True(t.Contains("A"));

            t.Clear();

            t.Insert("$A");
            t.Insert("$B");
            t.Insert("$C");

            Assert.True(t.Delete("$", 3));
            Assert.False(t.Delete("$"));
            Assert.False(t.Contains("$"));
            Assert.False(t.Contains("$A"));
            Assert.False(t.Contains("$B"));
            Assert.False(t.Contains("$C"));
            Assert.False(t.Delete("$A"));
            Assert.False(t.Delete("$B"));
            Assert.False(t.Delete("$C"));

            t.Clear();

            t.Insert("$A");
            t.Insert("$B");
            t.Insert("$C");

            Assert.True(t.Delete("$", 2));
            Assert.True(t.Delete("$"));

            Assert.False(t.Contains("$"));
            Assert.False(t.Contains("$A"));
            Assert.False(t.Contains("$B"));
            Assert.False(t.Contains("$C"));
            Assert.False(t.Delete("$A"));
            Assert.False(t.Delete("$B"));
            Assert.False(t.Delete("$C"));

            t.Clear();

            t.Insert("$A");
            t.Insert("$B");
            t.Insert("$C");

            Assert.True(t.Delete("$", 2));

            Assert.True(t.Contains("$"));
            Assert.True(t.Contains("$A"));
            Assert.True(t.Contains("$B"));
            Assert.True(t.Contains("$C"));
            Assert.True(t.Delete("$A"));
            Assert.False(t.Delete("$B"));
            Assert.False(t.Delete("$C"));

            t.Clear();

            t.Insert("CAT", 3);
            t.Insert("DOG", 3);

            Assert.False(t.Delete("parrot", 50));

            t.Clear();

            t.Insert("1234");
            t.Insert("122", 2);
            t.Insert("123", 3);
            Assert.True(t.Delete("12", 6));
            Assert.False(t.Delete("12"));
            Assert.False(t.Delete("1"));
            Assert.False(t.Contains("1234"));
            Assert.False(t.Contains("123"));
            Assert.False(t.Contains("12"));
            Assert.False(t.Contains("1"));

            t.Clear();

            t.Insert("1234");
            t.Insert("122", 2);
            t.Insert("123", 3);

            t.Delete("12", 999999);

            Assert.False(t.Contains("1234"));
            Assert.False(t.Contains("123"));
            Assert.False(t.Contains("12"));
            Assert.False(t.Contains("1"));

            t.Clear();

            t.Insert("1234");
            t.Insert("122", 2);
            t.Insert("123", 3);

            t.Delete("12", 999999);

            Assert.False(t.Contains("1234"));
            Assert.False(t.Contains("123"));
            Assert.False(t.Contains("12"));
            Assert.False(t.Contains("1"));

            t.Clear();

            t.Insert("1234");
            t.Insert("122", 2);
            t.Insert("123", 3);
            Assert.True(t.Delete("1234"));
            Assert.True(t.Delete("123", 4));
            Assert.True(t.Delete("122", 2));

            Assert.False(t.Contains("1"));
            Assert.False(t.Contains("12"));
            Assert.False(t.Contains("122"));
            Assert.False(t.Contains("123"));
            Assert.False(t.Contains("1234"));
        }

        [Fact]
        public void TestEdgeCases()
        {
            var t = new Trie();
            Assert.Equal(0, t.Count(""));
            Assert.Equal(0, t.Count("\0"));
            Assert.Equal(0, t.Count("\0\0"));
            Assert.Equal(0, t.Count("\0\0\0"));

            for (var c = 0; c < 128; c++)
                Assert.Equal(0, t.Count("" + c));
            
            Assert.False(t.Contains(""));
            Assert.False(t.Contains("\0"));
            Assert.False(t.Contains("\0\0"));
            Assert.False(t.Contains("\0\0\0"));

            for (var c = 0; c < 128; c++)
                Assert.False(t.Contains("" + c));
        }
    }
}