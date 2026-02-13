namespace LB.Utility.Collections
{
    public class PathNode
    {
        public PathNode()
        {
            Children = new PathTree();
        }

        public String Key { get; set; } = String.Empty;
        public String Path { get; set; } = String.Empty;
        public Boolean IsLeaf { get => Children?.Any() == false; }
        public PathTree Children { get; set; }
    }
}
