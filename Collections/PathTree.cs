namespace LB.Utility.Collections
{
    public class PathTree : Dictionary<String, PathNode>
    {
        public PathTree() { }
        public PathTree(IEnumerable<String> paths)
        {
            foreach(var path in paths)
            {
                Add(path);
            }
        }

        public void Add(String path) => AddEntry(path, 0);

        public void AddEntry(String path, Int32 start)
        {
            if (start < path.Length)
            {
                string key;
                Int32 end;

                end = path.IndexOf("/", start);
                if (end == 0)
                    end = path[1..].IndexOf("/", start);

                if (end == -1)
                    end = path.Length;
                
                key = path[start..end];
                if (!string.IsNullOrEmpty(key))
                {
                    PathNode node;

                    if (ContainsKey(key))
                    {
                        node = this[key];
                    }
                    else
                    {
                        node = new()
                        {
                            Path = path,
                            Key = key
                        };
                        Add(key, node);
                    }
                    // Now add the rest to the new item's children
                    node.Children.AddEntry(path, end + 1);
                }
            }
        }
    }
}
