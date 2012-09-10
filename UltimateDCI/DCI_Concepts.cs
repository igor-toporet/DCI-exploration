namespace DCI.Exploration.UltimateDCI
{
    using System;
    using System.Collections.Generic;

    public abstract class Context
    {
        //public UseCase UseCase<TUseCase>() where TUseCase : UseCase, new()
        //{
        //    return new TUseCase();
        //}
        private readonly IDictionary<Type, object> _roles = new Dictionary<Type, object>();

        protected void RegisterRole<TRole>(TRole role)
        {
            _roles.Add(typeof (TRole), role);
        }

        protected void RegisterRole<TRole,TDomain>(TDomain roleContext) where TRole:Role<TDomain>, new()
        {
            var role = UltimateDCI.Role<TDomain>.Create<TRole>(roleContext);
            _roles.Add(typeof (TRole), role);
        }

        protected TRole Role<TRole>()
        {
            return (TRole) _roles[typeof (TRole)];
        }
    }

    //public abstract class UseCase
    //{
    //    protected UseCase(Context context)
    //    {
    //        Context = context;
    //    }

    //    private Context Context { get; set; }

    //    public abstract void Execute();
    //}

    public abstract class Role<TDomain>
    {
        public static TRole Create<TRole>(TDomain roleContext) where TRole:Role<TDomain>
        {
            return (TRole) Activator.CreateInstance(typeof (TRole), roleContext);
        }

        //protected void SetRoleContext(TDomain roleContext)
        //{
        //    Self = roleContext;
        //}
        protected Role(TDomain roleContext)
        {
            Self = roleContext;
        }

        public TDomain Self { get; private set; }
    }
}
