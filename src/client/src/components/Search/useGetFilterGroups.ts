import { Aspect, State } from "@mimirorg/typelibrary-types";
import { useGetPurposes } from "api/purpose.queries";
import { FilterGroup } from "types/filterGroup";
import { getOptionsFromEnum } from "utils";

export const useGetFilterGroups = (): FilterGroup[] => [
  getEntityFilters(),
  getStateFilters(),
  getAspectFilters(),
  useGetPurposeFilters(),
];

const useGetPurposeFilters = (): FilterGroup => {
  const purposeQuery = useGetPurposes();

  return {
    name: "Purpose",
    filters: purposeQuery.data?.map((p) => ({ key: "purposeName", label: p.name, value: p.name })),
  };
};

const getAspectFilters = (): FilterGroup => {
  const aspectOptions = getOptionsFromEnum<Aspect>(Aspect);

  return {
    name: "Aspect",
    filters: aspectOptions.map((a) => ({
      key: "aspect",
      label: a.label,
      value: a.value.toString(),
    })),
  };
};

const getStateFilters = (): FilterGroup => {
  const stateOptions = getOptionsFromEnum<State>(State).filter((s) => s.label !== "Deleted");

  return {
    name: "State",
    filters: stateOptions.map((s) => ({
      key: "state",
      label: s.label,
      value: s.value.toString(),
    })),
  };
};

const getEntityFilters = (): FilterGroup => ({
  name: "Entity",
  filters: [
    {
      key: "kind",
      label: "Block",
      value: "BlockLibCm",
    },
    {
      key: "kind",
      label: "Terminal",
      value: "TerminalLibCm",
    },
    {
      key: "kind",
      label: "Attribute",
      value: "AttributeLibCm",
    },
    {
      key: "kind",
      label: "Attribute group",
      value: "AttributeGroupLibCm",
    },
    {
      key: "kind",
      label: "Unit",
      value: "UnitLibCm",
    },
    {
      key: "kind",
      label: "Quantity datum",
      value: "QuantityDatumLibCm",
    },
    {
      key: "kind",
      label: "RDS",
      value: "RdsLibCm",
    },
  ],
});
