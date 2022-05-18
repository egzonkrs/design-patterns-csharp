namespace Structural.Composite
{
    abstract class Component
    {
        public Component()
        {
        }

        public abstract string Operation();

        public virtual void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(Component component)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsComposite()
        {
            return true;
        }
    }

    class Leaf : Component
    {
        public override string Operation()
        {
            return "Leaf";
        }

        public override bool IsComposite()
        {
            return false;
        }
    }

    class Composite : Component
    {
        protected List<Component> _children = new List<Component>();

        public override void Add(Component component)
        {
            _children.Add(component);
        }

        public override void Remove(Component component)
        {
            _children.Remove(component);
        }

        public override string Operation()
        {
            int index = 0;
            string result = "Branch(";

            foreach (Component component in _children)
            {
                result += component.Operation();
                if (index != _children.Count - 1)
                {
                    result += "+";
                }
                index++;
            }

            return result + ")";
        }
    }

    class Client
    {
        public void ClientCode(Component leaf)
        {
            Console.WriteLine($"Result: {leaf.Operation()}\n");
        }

        public void ClientCode2(Component component1, Component component2)
        {
            if (component1.IsComposite())
            {
                component1.Add(component2);
            }

            Console.WriteLine($"Result: {component1.Operation()}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            Leaf leaf = new Leaf();

            Console.WriteLine("Client: I get a simple component:");
            client.ClientCode(leaf);

            Composite tree = new Composite();
            Composite branch1 = new Composite();

            branch1.Add(new Leaf());
            branch1.Add(new Leaf());

            Composite branch2 = new Composite();
            branch2.Add(new Leaf());

            tree.Add(branch1);
            tree.Add(branch2);

            Console.WriteLine("Client: Now I've got a composite tree:");
            client.ClientCode(tree);

            Console.Write("Client: I do not need to check the components classes even when managing the tree:\n");
            client.ClientCode2(tree, leaf);
        }
    }
}