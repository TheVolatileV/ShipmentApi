drop table if exists transport_pack;
drop table if exists shipment_orgs;
drop table if exists organization;
drop table if exists shipment;


create table organization(
    id uuid primary key,
    code varchar(50) not null
);

create index org_code_index on organization (code);

create table shipment(
    reference_id varchar(50) primary key,
    estimated_time_arrival date null,
    organizations jsonb,
    transport_packs jsonb
);





