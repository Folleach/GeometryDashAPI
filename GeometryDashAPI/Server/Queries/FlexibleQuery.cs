using System.Collections.Generic;

namespace GeometryDashAPI.Server.Queries
{
    public class FlexibleQuery : IQuery
    {
        private LinkedList<IQuery> queriesChain = new LinkedList<IQuery>();
        private LinkedList<Property> additionalProperties = new LinkedList<Property>();

        public Parameters BuildQuery()
        {
            Parameters result = new Parameters();
            BuildQuery(result);
            return result;
        }

        public void BuildQuery(Parameters parameters)
        {
            LinkedList<IQuery>.Enumerator chainEnumerator = queriesChain.GetEnumerator();
            while (chainEnumerator.MoveNext())
                chainEnumerator.Current.BuildQuery(parameters);
            foreach (var property in additionalProperties)
                parameters.Add(property);
        }

        public LinkedListNode<IQuery> AddToChain(IQuery query)
        {
            return queriesChain.AddLast(query);
        }

        public LinkedListNode<Property> AddProperty(Property property)
        {
            return additionalProperties.AddLast(property);
        }
    }
}
