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
            foreach (var query in queriesChain)
                query.BuildQuery(parameters);
            parameters.AddRange(additionalProperties);
        }

        public FlexibleQuery AddToChain(IQuery query)
        {
            queriesChain.AddLast(query);
            return this;
        }

        public FlexibleQuery AddProperty(Property property)
        {
            additionalProperties.AddLast(property);
            return this;
        }
    }
}
