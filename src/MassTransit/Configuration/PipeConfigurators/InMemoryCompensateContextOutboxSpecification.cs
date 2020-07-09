namespace MassTransit.PipeConfigurators
{
    using System.Collections.Generic;
    using Courier;
    using Courier.Contexts;
    using GreenPipes;
    using Pipeline.Filters;


    public class InMemoryCompensateContextOutboxSpecification<TArguments> :
        IPipeSpecification<CompensateContext<TArguments>>,
        IOutboxConfigurator
        where TArguments : class
    {
        public bool ConcurrentMessageDelivery { get; set; }

        public void Apply(IPipeBuilder<CompensateContext<TArguments>> builder)
        {
            builder.AddFilter(
                new InMemoryOutboxFilter<CompensateContext<TArguments>, InMemoryOutboxCompensateContext<TArguments>>(Factory, ConcurrentMessageDelivery));
        }

        public IEnumerable<ValidationResult> Validate()
        {
            yield break;
        }

        static InMemoryOutboxCompensateContext<TArguments> Factory(CompensateContext<TArguments> context)
        {
            return new InMemoryOutboxCompensateContext<TArguments>(context);
        }
    }
}
