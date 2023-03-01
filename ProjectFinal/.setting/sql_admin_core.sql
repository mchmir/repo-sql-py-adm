CREATE ROLE admin
 LOGIN        -- указывает, что пользователь может входить в систему.
 SUPERUSER    -- назначает пользователя суперпользователем, что дает ему полный доступ к системе.
 INHERIT      -- указывает, что пользователь будет наследовать права доступа от своих родительских ролей.
 CREATEDB     -- разрешает пользователю создание баз данных.
 CREATEROLE   -- разрешает пользователю создание новых ролей (пользователей).
 REPLICATION  -- разрешает пользователю работу с репликацией.
 PASSWORD 'AdminPG$';
 
 
ALTER ROLE admin SET search_path = admin_core, admin_app, public;


/**
 * Drops all variants of the function text
 * either in all schemas or in specific schema.
 */
create or replace function admin_core.x_drop_func(
  in a_funcname            name,
  in a_schema              name default null
) returns void
language plpgsql security definer set search_path=''
as $$
declare
  l_dropcmd text;
begin
  select string_agg(format('drop function %s(%s);',
                           p.oid::regproc,
                           pg_catalog.pg_get_function_identity_arguments(p.oid)
                          ),
                    E'\n'
                   ) into l_dropcmd
    from pg_proc p
      join pg_namespace n on p.pronamespace = n.oid
    where proname = lower(a_funcname) and p.prokind = 'f'
      and (a_schema is null or n.nspname = a_schema);

  if l_dropcmd is not null then
    execute(l_dropcmd);
  end if;
end; $$;


select admin_core.x_drop_func('x_drop_proc');
/**
 * Drops all variants of the procedure text
 * either in all schemas or in specific schema.
 */
create or replace function admin_core.x_drop_proc(
  in a_procname            name,
  in a_schema              name default null
) returns void
language plpgsql security definer set search_path=''
as $$
declare
  l_dropcmd text;
begin
  select string_agg(format('drop procedure %s(%s);',
                           p.oid::regproc,
                           pg_catalog.pg_get_function_identity_arguments(p.oid)
                          ),
                    E'\n'
                   ) into l_dropcmd
    from pg_proc p
      join pg_namespace n on p.pronamespace = n.oid
    where proname = lower(a_procname) and p.prokind = 'p'
      and (a_schema is null or n.nspname = a_schema);

  if l_dropcmd is not null then
    execute(l_dropcmd);
  end if;
end; $$;


select admin_core.x_drop_func('x_drop_view');
/**
 * Drops view
 * either in all schemas or in specific schema.
 */
create or replace function admin_core.x_drop_view(
  in a_viewname            name,
  in a_schema              name default null
) returns void
language plpgsql security definer set search_path=''
as $$
declare
  l_dropcmd text;
begin
  select string_agg(format('drop view %s;',
                           c.oid::regclass
                          ),
                    E'\n'
                   ) into l_dropcmd
    from pg_class c
      join pg_namespace n on c.relnamespace = n.oid
    where relname = lower(a_viewname) and c.relkind = 'v'
      and (a_schema is null or n.nspname = a_schema);

  if l_dropcmd is not null then
    execute(l_dropcmd);
  end if;
end; $$;


select admin_core.x_drop_func('x_grant_func');
/**
 * Grants execute to specific role on all variants function text
 * either in all schemas or in specific schema.
 * Checks whether only one or multiple variants exists according the a_multiple argument
 */
create or replace function admin_core.x_grant_func(
  in a_funcname            name,
  in a_role                name,
  in a_multiple            boolean default false,
  in a_schema              name default null
) returns void
language plpgsql security definer set search_path=''
as $$
declare
  l_CNT integer;
  l_SQL text;
begin
  select count(*), string_agg(format('grant execute on function %s(%s) to %s;',
                                     p.oid::regproc,
                                     pg_catalog.pg_get_function_identity_arguments(p.oid),
                                     a_role
                                    ),
                              E'\n'
                             )
      into l_CNT, l_SQL
    from pg_proc p
      join pg_namespace n on p.pronamespace = n.oid
    where proname = lower(a_funcname) and p.prokind = 'f'
      and (a_schema is null or n.nspname = a_schema);

  if l_CNT > 0 then

    if l_CNT > 1 and not a_multiple then
      raise exception 'Function % has multiple variants (%), but grant_func expects only one!', a_funcname, l_CNT;
    end if;

    if l_CNT = 1 and a_multiple then
      raise exception 'Function % has only one variant, but grant_func expects  multiple variants!', a_funcname;
    end if;

    execute l_SQL;
  else
    raise notice 'Function % not found, nothing granted!', a_funcname;
  end if;
end; $$;


select admin_core.x_drop_func('x_grant_proc');
/**
 * Grants execute to specific role on all variants procedure text
 * either in all schemas or in specific schema.
 * Checks whether only one or multiple variants exists according the a_multiple argument
 */
create or replace function admin_core.x_grant_proc(
  in a_procname            name,
  in a_role                name,
  in a_multiple            boolean default false,
  in a_schema              name default null
) returns void
language plpgsql security definer set search_path=''
as $$
declare
  l_cnt                    int;
  l_grantcmd               text;
begin
  select count(*), string_agg(format('grant execute on procedure %s(%s) to %s;',
                                     p.oid::regproc,
                                     pg_catalog.pg_get_function_identity_arguments(p.oid),
                                     a_role
                                    ),
                              E'\n'
                             )
      into l_cnt, l_grantcmd
    from pg_proc p
      join pg_namespace n on p.pronamespace = n.oid
    where proname = lower(a_procname) and p.prokind = 'p'
      and (a_schema is null or n.nspname = a_schema);

  if l_cnt > 0 then

    if l_cnt > 1 and not a_multiple then
      raise exception 'Procedure % has multiple variants (%), but grant_func expects only one!', a_procname, l_cnt;
    end if;

    if l_cnt = 1 and a_multiple then
      raise exception 'Procedure % has only one variant, but grant_func expects  multiple variants!', a_procname;
    end if;

    execute l_grantcmd;
  else
    raise exception 'Procedure % not found, nothing granted!', a_procname;
  end if;
end; $$;


select admin_core.x_drop_func('x_grant_view');
/**
 * Grants select to specific role on view
 * either in all schemas or in specific schema.
 */

create or replace function admin_core.x_grant_view(
  in a_viewname            name,
  in a_role                name,
  in a_schema              name default null
) returns void
language plpgsql security definer set search_path=''
as $$
declare
  l_grantcmd               text;
  l_cnt                    int;
begin
  select count(*), string_agg(format('grant view %s;',
                           c.oid::regclass
                          ),
                    E'\n'
                   ) into l_grantcmd
    from pg_class c
      join pg_namespace n on c.relnamespace = n.oid
    where relname = lower(a_viewname) and c.relkind = 'v'
      and (a_schema is null or n.nspname = a_schema);

  if l_cnt > 0 then
    execute l_grantcmd;
  else
    raise exception 'View % not found, nothing granted!', a_viewname;
  end if;

exception
  when others then
    raise exception 'Error granting view % to %', a_viewname, a_role;
end; $$;
