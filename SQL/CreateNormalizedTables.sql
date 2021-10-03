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
    estimated_time_arrival date null
);

create table shipment_orgs(
    shipment_reference_id varchar(50),
    org_id uuid not null,
    primary key (shipment_reference_id, org_id),
    constraint fk_shipment
        foreign key(shipment_reference_id)
            references shipment(reference_id),
    constraint fk_organization
        foreign key(org_id)
            references organization(id)
);

create table transport_pack(
    id bigint primary key generated always as identity,
    shipment_reference_id varchar(50) not null,
    weight numeric not null,
    unit varchar(20) not null,
    constraint fk_shipment
        foreign key(shipment_reference_id)
            references shipment(reference_id)
);





